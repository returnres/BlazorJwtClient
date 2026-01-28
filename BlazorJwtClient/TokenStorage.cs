using Microsoft.JSInterop;

public class TokenStorage
{
    private readonly IJSRuntime _js;

    public TokenStorage(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SetTokensAsync(string access, string refresh)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", "access", access);
        await _js.InvokeVoidAsync("localStorage.setItem", "refresh", refresh);
    }

    public async Task<string?> GetAccessAsync()
    {
        return await _js.InvokeAsync<string?>("localStorage.getItem", "access");
    }

    public async Task<string?> GetRefreshAsync()
    {
        return await _js.InvokeAsync<string?>("localStorage.getItem", "refresh");
    }

    public async Task ClearAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "access");
        await _js.InvokeVoidAsync("localStorage.removeItem", "refresh");
    }
}
