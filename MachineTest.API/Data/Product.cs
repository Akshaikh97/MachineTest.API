using MachineTest.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public virtual int CategoryId { get; set; }
        //public virtual string CategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }

    public class ProductCategoryModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
}
