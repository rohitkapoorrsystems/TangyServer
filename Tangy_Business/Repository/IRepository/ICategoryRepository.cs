using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDto> Create(CategoryDto objDto);
        public Task<CategoryDto> Update(CategoryDto objDto);
        public Task<int> Delete(int id);
        public Task<CategoryDto> GetById(int id);
        public Task<IEnumerable<CategoryDto>> GetAll();
    }
}
