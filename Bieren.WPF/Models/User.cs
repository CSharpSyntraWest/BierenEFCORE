using System;
using System.Collections.Generic;
using System.Text;

namespace Bieren.WPF.Models
{
    public class User
    {
        public int UserNr { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime GeboorteDatum { get; set; }

        public string Email { get; set; }
        public IList<Bier> FavorieteBieren { get; set; }

    }
}
