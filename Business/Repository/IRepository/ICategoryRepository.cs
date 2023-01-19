using Models;

namespace Business.Repository.IRepository;

public interface ICategoryRepository
{
    public Task<CategoryDTO> Create(CategoryDTO categoryDTO);
    public Task<CategoryDTO> Update(CategoryDTO categoryDTO);
    public Task<int> Delete(int id);
    public Task<CategoryDTO> GetById(int id);
    public Task<IEnumerable<CategoryDTO>> GetAll();
}
