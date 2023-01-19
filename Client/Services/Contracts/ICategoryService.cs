namespace Client.Services.Contracts;

public interface ICategoryService
{
    Task<CategoryDTO> Create(CategoryDTO category);
    Task<ICollection<CategoryDTO>> GetAll();
    Task<CategoryDTO> GetById(int id);
    Task<CategoryDTO> Update(CategoryDTO category);       
    Task<bool> DeleteById(int id);
}
