using System.Net.Http.Json;
using HttpModels;

namespace HttpApiClient;

public class ApiClient
{
    private readonly string _host;
    private readonly HttpClient _httpClient;
    public ApiClient(string? host, HttpClient? httpClient = null)
    {
        _host = host ?? "https://localhost:7214";
        _httpClient = httpClient ?? new HttpClient();
    }
    
    public Task<Product[]?> GetProducts()
    {
        return _httpClient.GetFromJsonAsync<Product[]>($"{_host}/Catalog/Products");
    }
    
    public Task<Category[]?> GetCategories()
    {
        return  _httpClient.GetFromJsonAsync<Category[]>($"{_host}/Catalog/Categories");
    }
    
    public Task AddProduct(Product product)
    {
        return _httpClient.PostAsJsonAsync($"{_host}/Catalog/AddProduct", product);
    }
    
    public Task Registration(AccountRequestModel account)
    {
        return _httpClient.PostAsJsonAsync($"{_host}/Registration/Registration", account);
    }
    
    public async Task<Account> Autorization(AccountRequestModel account)
    {
        try
        {
            using var response = await _httpClient.PostAsJsonAsync($"{_host}/Registration/Autorization", account);
            response.EnsureSuccessStatusCode(); //кидаем исключение при плохом статусе
            return await response.Content.ReadFromJsonAsync<Account>();
        }
        catch (Exception e)
        {
            return null;
        }

    }
    
    public Task<Account[]?> GetAccounts()
    {
        return  _httpClient.GetFromJsonAsync<Account[]>($"{_host}/Registration/Accounts");
    }
    
    public Task<Account?> GetAccountByEmail(string email)
    {
        return _httpClient.GetFromJsonAsync<Account>($"{_host}/Registration/GetAccountByEmail?email={email}");
    }
}