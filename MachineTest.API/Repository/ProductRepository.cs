using MachineTest.API.Data;
using MachineTest.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }
        //public async Task<List<ProductModel>> GetAllProductsAsync(int page)
        //{
        //    var pageResults = 3f;
        //    var pageCount = Math.Ceiling(_context.Product.Count() / pageResults);

        //    var products = await _context.Product.Select(x => new ProductModel()
        //    {
        //        Id = x.Id,
        //        ProductName = x.ProductName,
        //        CategoryId = x.CategoryId,
        //        CategoryName = x.CategoryName,
        //    })
        //        .Skip((page - 1) * (int)pageResults)
        //        .Take((int)pageResults)
        //        .ToListAsync();
        //    return products;
        //}

        //public async Task<List<ProductModel>> GetAllProductsAsync()
        //{
        //    var records = await _context.Product.Select(x => new ProductModel()
        //    {
        //        Id = x.Id,
        //        ProductName = x.ProductName,
        //        CategoryId = x.CategoryId,
        //        CategoryName = x.CategoryName,
        //    }).ToListAsync();
        //    return records;
        //}




        //public async Task<List<ProductModel>> GetAllProductsAsync(int PageNo = 1, bool a =true)
        //{
        //    var product = await _context.Product.Include(x => x.Category)
        //                                       .Where(c => c.Category.CategoryName
        //                                       .Equals(a)).ToListAsync();
        //    //var p = _db.Product.Where(x => x.Category.ActiveOrNot == a).Select(x => x.Category).ToList();
        //    int NoOfRecordsPerPage = 3;
        //    int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(product.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
        //    int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
        //    //ViewBag.PageNo = PageNo;
        //    //ViewBag.NoOfPages = NoOfPages;
        //    product = product.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
        //    return product;
        //}

        public object GetProductCategory(RequestParams requestParams)
        {
            var productCategory = (from a in _context.Product
                                   join b in _context.Category on a.CategoryId equals b.CategoryId
                                   select new
                                   {
                                       ProductId = a.Id,
                                       ProductName = a.ProductName,
                                       CategoryId = b.CategoryId,
                                       CategoryName = b.CategoryName
                                   })
                                   .Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
                                   .Take(requestParams.PageSize)
                                   .ToList();
            return productCategory;
        }

        public List<ProductCategoryModel> GetProductCategoryModel()
        {
            var productCategoryModel = (from a in _context.Product
                                        join b in _context.Category on a.CategoryId equals b.CategoryId
                                        select new ProductCategoryModel
                                        {
                                            ProductId = a.Id,
                                            ProductName = a.ProductName,
                                            CategoryId = b.CategoryId,
                                            CategoryName = b.CategoryName
                                        }).ToList();
            return productCategoryModel;
        }

        public async Task<ProductModel> GetProductByIdAsync(int Id)
        {
            //var records = await _context.Product.Where(x => x.Id == Id)
            var records = await (from a in _context.Product
                                 join b in _context.Category on a.CategoryId equals b.CategoryId
                                 where a.Id == Id
                                 select new ProductModel()
                                 {
                                     Id = a.Id,
                                     ProductName = a.ProductName,
                                     CategoryId = b.CategoryId,
                                     CategoryName = b.CategoryName
                                 }).FirstOrDefaultAsync();
            return records;
        }
        public async Task<Product> AddProductAsync(Product productModel)
        {
            _context.Product.Add(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product> AddUpdateProductAsync(int Id, Product productModel)
        {
            var _productModel = new Product();
            if (Id == 0)
            {
                _context.Product.Add(productModel);
                await _context.SaveChangesAsync();
                _productModel = productModel;
            }

            var product = await _context.Product.FindAsync(Id);
            if (product != null)
            {
                product.ProductName = productModel.ProductName;
                product.CategoryId = productModel.CategoryId;
                await _context.SaveChangesAsync();
                _productModel = product;
            }
            return _productModel;
        }

        public async Task DeleteProductAsync(int Id)
        {
            var product = new Product() { Id = Id };
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
