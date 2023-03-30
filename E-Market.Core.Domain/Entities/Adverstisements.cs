
using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entities
{
    public class Adverstisements : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }

        //Foreign Key
        public int CategoryId { get; set; }
        public int UserId { get; set; }


        //Navigation Property
        public Category? Category { get; set; }
        public User? User { get; set; }


    }
}
