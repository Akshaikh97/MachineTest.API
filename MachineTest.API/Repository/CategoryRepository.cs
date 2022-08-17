using MachineTest.API.Data;
using MachineTest.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            var records = await _context.Category.Select(x => new CategoryModel()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToListAsync();
            return records;
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            //"FindAsync" will work only with the primaey key.
            //var records = await _context.Category.FindAsync(categoryId).Select(x => new CategoryModel()
            //{
            //    CategoryId=x.CategoryId,
            //    CategoryName=x.CategoryName
            //}).ToListAsync();//If I used ToListAsync(), some Category does not exist then I'll get an error. 
            //return records;

            //If I want to fetch data based on some other column use "Where"
            var records = await _context.Category.Where(x => x.CategoryId == categoryId).Select(x => new CategoryModel()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).FirstOrDefaultAsync();
            return records;
        }

        public async Task<int> AddCategoryAsync(CategoryModel categoryModel)
        {
            var category = new Category()
            {
                CategoryName = categoryModel.CategoryName
            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return category.CategoryId;
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryModel categoryModel)
        {
            var category = await _context.Category.FindAsync(categoryId);
            if (category != null)
            {
                category.CategoryName = categoryModel.CategoryName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = new Category() { CategoryId= categoryId};
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
