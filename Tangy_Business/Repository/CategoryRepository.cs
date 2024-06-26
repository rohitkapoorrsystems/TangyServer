using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Create(CategoryDto objDto)
        {
            var category = _mapper.Map<CategoryDto, Category>(objDto);
            category.CreateDate = DateTime.Now;
            var addedObj = _dbContext.Add(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj is not null)
            {
                _dbContext.Remove(obj);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(_dbContext.Categories);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var obj = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj is not null)
            {
                return _mapper.Map<Category, CategoryDto>(obj);
            }

            return new CategoryDto();
        }

        public async Task<CategoryDto> Update(CategoryDto objDto)
        {
            var objFromDb = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == objDto.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = objDto.Name;
                _dbContext.Update(objFromDb);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDto>(objFromDb); 
            }

            return objDto;            
        }
    }
}
