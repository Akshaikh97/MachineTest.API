using MachineTest.API.Data;
using MachineTest.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Repository
{
    public interface IProductRepository
    {
        //Task<List<ProductModel>> GetAllProductsAsync(int page);

        object GetProductCategory();

        List<ProductCategoryModel> GetProductCategoryModel();
        Task<ProductModel> GetProductByIdAsync(int Id);
        Task<Product> AddProductAsync(Product productModel);
        Task<Product> AddUpdateProductAsync(int Id, Product product);
    }
}
