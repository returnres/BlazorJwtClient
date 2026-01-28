using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly TokenStorage _tokenStorage;

    public JwtAuthStateProvider(TokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetAccessAsync();

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void Notify()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        if (string.IsNullOrEmpty(jwt))
            return Enumerable.Empty<Claim>();

        var segments = jwt.Split('.');
        if (segments.Length != 3)
            return Enumerable.Empty<Claim>();

        var payload = segments[1];

        // Corregge il padding del Base64 senza %
        var mod = payload.Length % 4;
        if (mod == 2) payload += "==";
        else if (mod == 3) payload += "=";
        else if (mod == 1) payload += "===";

        var jsonBytes = Convert.FromBase64String(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? "")) 
               ?? Enumerable.Empty<Claim>();
    }
}
