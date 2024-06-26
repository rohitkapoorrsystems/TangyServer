using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface IProdcutRepository
    {
        public Task<ProductDto> Create(ProductDto objDto);
        public Task<ProductDto> Update(ProductDto objDto);
        public Task<int> Delete(int id);
        public Task<ProductDto> GetById(int id);
        public Task<IEnumerable<ProductDto>> GetAll();
    }
}
