using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.DataLayer.Models
{
    //EF Core - Code first: eerst Model classes in datalayer en daaruit Database genereren
    public class DbUser
    {
        public DbUser()
        {
            FavorieteBieren = new HashSet<DbBier>();
        }
        public int UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Email { get; set; }

        public virtual ICollection<DbBier> FavorieteBieren { get; set; }

    }
}
