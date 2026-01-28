using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly TokenStorage _storage;

    public AuthService(HttpClient http, TokenStorage storage)
    {
        _http = http;
        _storage = storage;
    }

    public async Task<bool> Login(string email, string password)
    {

var res = await _http.PostAsJsonAsync(
    "api/auth/login",
    new
    {
         email,
        password
    });

       

        if (!res.IsSuccessStatusCode)
            return false;

        var tokens = await res.Content.ReadFromJsonAsync<TokenResponse>();
        await _storage.SetTokensAsync(tokens!.AccessToken, tokens.RefreshToken);

        return true;
    }
}
