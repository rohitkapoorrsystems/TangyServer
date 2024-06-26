using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProdcutRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductDto> Create(ProductDto objDto)
        {
            var product = _mapper.Map<ProductDto, Product>(objDto);

            var addedObj = _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (obj is not null)
            {
                _dbContext.Remove(obj);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_dbContext.Products.Include(x => x.Category));
        }

        public async Task<ProductDto> GetById(int id)
        {
            var obj = await _dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (obj is not null)
            {
                return _mapper.Map<Product, ProductDto>(obj);
            }

            return new ProductDto();
        }

        public async Task<ProductDto> Update(ProductDto objDto)
        {
            var objFromDb = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == objDto.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = objDto.Name;
                objFromDb.Description = objDto.Description;
                objFromDb.CustomFavorites = objDto.CustomFavorites;
                objFromDb.ShopFavorites = objDto.ShopFavorites;
                objFromDb.Description = objDto.Description;
                objFromDb.ImageUrl = objDto.ImageUrl;
                _dbContext.Update(objFromDb);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<Product, ProductDto>(objFromDb);
            }

            return objDto;
        }
    }
}
