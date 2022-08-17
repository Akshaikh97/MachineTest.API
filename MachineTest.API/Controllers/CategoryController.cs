using MachineTest.API.Data;
using MachineTest.API.Models;
using MachineTest.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel categoryModel)
        {
            var categoryId = await _categoryRepository.AddCategoryAsync(categoryModel);
            return CreatedAtAction(nameof(GetCategoryById), 
                new { categoryId = categoryId, controller = "category" }, categoryId);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, 
                                                        [FromBody] CategoryModel categoryModel)
        {
            await _categoryRepository.UpdateCategoryAsync(categoryId, categoryModel);
            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
            return Ok();
        }
    }
}
