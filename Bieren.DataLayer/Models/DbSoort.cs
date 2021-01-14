using System;
using System.Collections.Generic;

#nullable disable

namespace Bieren.DataLayer.Models
{
    public partial class DbSoort
    {
        public DbSoort()
        {
            DbBiers = new HashSet<DbBier>();
        }

        public int SoortNr { get; set; }
        public string Soort { get; set; }

        public virtual ICollection<DbBier> DbBiers { get; set; }
    }
}
