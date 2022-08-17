using MachineTest.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> GetCategoryByIdAsync(int categoryId);
        Task<int> AddCategoryAsync(CategoryModel categoryModel);
        Task UpdateCategoryAsync(int categoryId, CategoryModel categoryModel);
        Task DeleteCategoryAsync(int categoryId);
    }
}
