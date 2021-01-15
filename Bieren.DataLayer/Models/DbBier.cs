using System;
using System.Collections.Generic;

#nullable disable

namespace Bieren.DataLayer.Models
{
    public partial class DbBier
    {
        public DbBier()
        {
            Users = new HashSet<DbUser>();
        }
        public int BierNr { get; set; }
        public string Naam { get; set; }
        public int? BrouwerNr { get; set; }
        public int? SoortNr { get; set; }
        public double? Alcohol { get; set; }

        public virtual DbBrouwer BrouwerNrNavigation { get; set; }
        public virtual DbSoort SoortNrNavigation { get; set; }

        public virtual ICollection<DbUser> Users { get; set; }

    }
}
