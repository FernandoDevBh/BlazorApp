using Models;
using AutoMapper;
using DataAccess;
using DataAccess.Data;
using Business.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Create(ProductDTO objDTO)
    {
        var obj = _mapper.Map<Product>(objDTO);

        _dbContext.Products.Add(obj);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDTO>(obj);
    }

    public async Task<int> Delete(int id)
    {
        var obj = await _dbContext.Products.SingleOrDefaultAsync(u => u.Id == id);

        if (obj == null) return 0;

        _dbContext.Products.Remove(obj);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductDTO>> GetAll(int categoryId)
    {
        var items = await _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(items);
    }

    public async Task<ProductDTO> GetById(int categoryId, int productId)
    {
        var obj = await _dbContext.Products.SingleOrDefaultAsync(u => u.CategoryId == categoryId && u.Id == productId);

        if (obj == null) return new() { Name = string.Empty, Id = 0, CategoryId = 0 };

        return _mapper.Map<ProductDTO>(obj);
    }

    public async Task<ProductDTO> Update(ProductDTO objDTO)
    {
        var obj = await _dbContext.Products.SingleOrDefaultAsync(u => u.Id == objDTO.Id);
        if (obj == null) return objDTO;

        obj.Name = objDTO.Name;
        obj.Number = objDTO.Number;
        obj.InMyCollection = objDTO.InMyCollection;
        obj.CategoryId = objDTO.CategoryId;
        obj.Image = objDTO.Image;

        _dbContext.Products.Update(obj);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDTO>(obj);
    }
}
