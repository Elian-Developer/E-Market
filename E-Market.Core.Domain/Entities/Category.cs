
using E_Market.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entities
{
    public class Category : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        //Foreign Key
        //public int UserId { get; set; }

        //Navigation Property
        //public User? User { get; set; }
        public ICollection<Adverstisements> Adverstisements { get; set; }

    }
}
