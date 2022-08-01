using NET106.Shared.Models;
using NET106.Shared.ViewModels;

namespace NET106.Server.Service;

public class ClassService:ISerrvice<ClassViewModel>
{
    private readonly HttpClient _httpClient;

    public ClassService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<List<ClassViewModel>> Get()
    {
        var result = await _httpClient.GetFromJsonAsync<List<ClassViewModel>>("api/");
        return result;
    }

    public Task<List<ClassViewModel>> GetId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(ClassViewModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(int id, ClassViewModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
}