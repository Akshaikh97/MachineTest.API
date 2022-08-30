using MachineTest.API.Data;
using MachineTest.API.Models;
using MachineTest.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        //[HttpGet("{page}")]
        //public async Task<IActionResult> GetAllProducts(int page)
        //{
        //    var products = await _productRepository.GetAllProductsAsync(3);
        //    return Ok(products);
        //}

        [HttpGet]
        [Route("GetProductCategory")]
        public IActionResult GetProductCategory([FromQuery]RequestParams requestParams)
        {
            var productCategories = _productRepository.GetProductCategory(requestParams);
            return Ok(productCategories);
         }

        [HttpGet]
        [Route("GetProductCategoryModel")]
        public List<ProductCategoryModel> GetProductCategoryModel()
        {
            var productCategories = _productRepository.GetProductCategoryModel();
            return productCategories;
        }

        [HttpGet("{Id}")]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById([FromRoute] int Id)
        {
            var product = await _productRepository.GetProductByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProductAsync(Product productModel)
        {
            var product = await _productRepository.AddProductAsync(productModel);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("AddUpdateProduct/{Id}")]
        public async Task<IActionResult> UpdateProductAsync(int Id,Product product)
        {
            var response = await _productRepository.AddUpdateProductAsync(Id,product);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete]
        [Route("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProductAsync(int Id)
        {
            await _productRepository.DeleteProductAsync(Id);
            return Ok();
        }
    }
}
