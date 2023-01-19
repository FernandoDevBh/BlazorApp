using Models;
using DataAccess;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Business.Repository.IRepository;

namespace Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            var obj = _mapper.Map<Category>(objDTO);
            obj.CreatedDate = DateTime.UtcNow;

            _dbContext.Categories.Add(obj);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(obj);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _dbContext.Categories.SingleOrDefaultAsync(u =>  u.Id == id);

            if (obj == null) return 0;
                _dbContext.Categories.Remove(obj);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var items = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(items);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var obj = await _dbContext.Categories.SingleOrDefaultAsync(u =>  u.Id == id);

            if (obj == null) return new() { Name = string.Empty, Id=0 };

            return _mapper.Map<CategoryDTO>(obj);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var obj = await _dbContext.Categories.SingleOrDefaultAsync(u => u.Id == objDTO.Id);
            if (obj == null) return objDTO;

            obj.Name = objDTO.Name;
            obj.Symbol = objDTO.Symbol;
            _dbContext.Categories.Update(obj);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(obj);
        }
    }
}
