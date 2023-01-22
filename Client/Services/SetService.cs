using Client.Services.Contracts;

namespace Client.Services;

public class SetService : ICategoryService
{    
    private readonly HttpClient httpClient;

    public SetService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<CategoryDTO> Create(CategoryDTO category)
    {
        var bodyContent = category.GetStringContent();
        var response = await httpClient.PostAsync("category", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<CategoryDTO>();

        if (response.IsSuccessStatusCode)        
            return responseDTO;        
        else        
            throw new Exception("Error on create");
    }    

    public async Task<ICollection<CategoryDTO>> GetAll()
    {        
        var response = await httpClient.GetAsync("category");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ICollection<CategoryDTO>>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetAll");
    }

    public async Task<CategoryDTO> GetById(int id)
    {        
        var response = await httpClient.GetAsync($"category/{id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<CategoryDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetById");
    }

    public async Task<CategoryDTO> Update(CategoryDTO category)
    {
        var bodyContent = category.GetStringContent();
        var response = await httpClient.PutAsync($"category/{category.Id}", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<CategoryDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on Update");
    }

    public async Task<bool> DeleteById(int id)
    {
        var response = await httpClient.DeleteAsync($"category/{id}");        
        return response.IsSuccessStatusCode;
    }
}
