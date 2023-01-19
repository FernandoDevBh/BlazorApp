using Models;
using DataAccess;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Business.Repository.IRepository;

namespace Business.Repository;

public class ProductPriceRepository : IProductPriceRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductPriceRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
    {
        var obj = _mapper.Map<ProductPrice>(objDTO);

        _dbContext.ProductPrices.Add(obj);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductPriceDTO>(obj);
    }

    public async Task<int> Delete(int id)
    {
        var obj = await _dbContext.ProductPrices.SingleOrDefaultAsync(u => u.Id == id);

        if (obj == null) return 0;

        _dbContext.ProductPrices.Remove(obj);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
    {
        var items = from price in _dbContext.ProductPrices
                    select price;

        if (id > 0)
            items = items.Where(e => e.ProductId == id.Value);

        return _mapper.Map<IEnumerable<ProductPriceDTO>>(await items.ToListAsync());
    }

    public async Task<ProductPriceDTO> GetById(int id)
    {
        var obj = await _dbContext.ProductPrices.SingleOrDefaultAsync(u => u.Id == id);

        if (obj == null) return new();

        return _mapper.Map<ProductPriceDTO>(obj);
    }

    public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
    {
        var obj = await _dbContext.ProductPrices.SingleOrDefaultAsync(u => u.Id == objDTO.Id);
        if (obj == null) return objDTO;
        obj.ProductId = objDTO.ProductId;
        obj.Size = objDTO.Size;
        obj.Price = objDTO.Price;

        _dbContext.ProductPrices.Update(obj);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductPriceDTO>(obj);
    }
}
