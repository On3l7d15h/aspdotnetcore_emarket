using SolutionEMarket.Core.Domain.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionEMarket.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }
        //temp
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        //Navigation Property
        public Category? Category { get; set; }
        public User? User { get; set; }
    }
}
