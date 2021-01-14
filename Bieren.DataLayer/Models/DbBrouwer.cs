using System;
using System.Collections.Generic;

#nullable disable

namespace Bieren.DataLayer.Models
{
    public partial class DbBrouwer
    {
        public DbBrouwer()
        {
            DbBiers = new HashSet<DbBier>();
        }

        public int BrouwerNr { get; set; }
        public string BrNaam { get; set; }
        public string Adres { get; set; }
        public short? PostCode { get; set; }
        public string Gemeente { get; set; }
        public int? Omzet { get; set; }

        public virtual ICollection<DbBier> DbBiers { get; set; }
    }
}
