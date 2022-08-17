using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MachineTest.API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Pages { get; set; }//"pages" means Numbers of page
        public int CurrentPage { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
