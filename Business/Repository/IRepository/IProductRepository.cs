using Models;

namespace Business.Repository.IRepository;

public interface IProductRepository
{
    public Task<ProductDTO> Create(ProductDTO objDTO);
    public Task<ProductDTO> Update(ProductDTO objDTO);
    public Task<int> Delete(int id);
    public Task<ProductDTO> GetById(int categoryId, int productId);
    public Task<IEnumerable<ProductDTO>> GetAll(int categoryId);
}
