using Bieren.DataLayer.Models;
using Bieren.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.WPF.Services
{
    public class BierenDataService : IDataService
    {
        public IList<Bier> GeefAlleBieren()
        {
            return DbBierenToBieren();
        }

        private IList<Bier> DbBierenToBieren()
        {
            IList<Bier> bieren = new List<Bier>();
            using (BierenDbContext bierenDb = new BierenDbContext())
            {
                var dbBieren = bierenDb.DbBiers;
                foreach (DbBier dbBier in dbBieren)
                {
                    Bier bier = DbBierToBier(dbBier);
                    bieren.Add(bier);
                }
            }
            return bieren;
        }

        private Bier DbBierToBier(DbBier dbBier)
        {
            if (dbBier == null) return null;
            Bier bier = new Bier()
            {
                BierNr = dbBier.BierNr,
                Naam = dbBier.Naam,
                Alcohol = dbBier.Alcohol,
                Brouwer = null,
                BierSoort = null
            };
            return bier;
        }

        public IList<BierSoort> GeefAlleBierSoorten()
        {
            return DbSoortenToBierSoorten();
        }

        private IList<BierSoort> DbSoortenToBierSoorten()
        {
            IList<BierSoort> bierSoorten = new List<BierSoort>();
            using(BierenDbContext db = new BierenDbContext())
            {
                var dbBierSoorten = db.DbSoorts;
                foreach (DbSoort dbSoort in dbBierSoorten)
                {
                    bierSoorten.Add(DbSoortToBierSoort(dbSoort));     
                }
            }
            return bierSoorten;
        }
        private BierSoort DbSoortToBierSoort(DbSoort dbSoort)
        {
            if (dbSoort == null) return null;
            BierSoort bierSoort = new BierSoort()
            { 
                 SoortNr = dbSoort.SoortNr,
                 SoortNaam = dbSoort.Soort
            };
            return bierSoort;
        }

        public IList<Brouwer> GeefAlleBrouwers()
        {
            return DbBrouwersToBrouwers();
        }

        private IList<Brouwer> DbBrouwersToBrouwers()
        {
            IList<Brouwer> brouwers = new List<Brouwer>();
            using (BierenDbContext db = new BierenDbContext())
            {
                foreach (DbBrouwer dbBrouwer in db.DbBrouwers)
                    brouwers.Add(DbBrouwerToBrouwer(dbBrouwer));
            }
            return brouwers;
        }

        private Brouwer DbBrouwerToBrouwer(DbBrouwer dbBrouwer)
        {
            Brouwer brouwer = new Brouwer()
            {
                 BrouwerNr = dbBrouwer.BrouwerNr,
                 BrNaam = dbBrouwer.BrNaam,
                 Straat = dbBrouwer.Adres,
                 PostCode = dbBrouwer.PostCode,
                 Gemeente = dbBrouwer.Gemeente,
                 Omzet = dbBrouwer.Omzet
                 //Bieren = DbBierenToBieren(dbBrouwer.DbBiers)
            };
            return brouwer;
        }

        public IList<Bier> GeefBierenVoorBrouwer(Brouwer brouwer)
        {
            IList<Bier> bieren = new List<Bier>();
            using (BierenDbContext db = new BierenDbContext())
            {
                var dbBieren = db.DbBiers.Where(b => b.BrouwerNr == brouwer.BrouwerNr);
                foreach(DbBier dbBier in dbBieren)
                {
                    bieren.Add(DbBierToBier(dbBier));
                }
            }
            return bieren;
        }

        public IList<Bier> VerwijderBier(Bier bier)
        {
            throw new NotImplementedException();
        }

        public IList<Brouwer> VerwijderBrouwer(Brouwer selectedBrouwer)
        {
            throw new NotImplementedException();
        }

        public IList<BierSoort> VoegBierSoortToe(BierSoort biersoort)
        {
            throw new NotImplementedException();
        }

        public IList<Bier> VoegBierToe(Bier bier)
        {
            throw new NotImplementedException();
        }

        public IList<Brouwer> VoegBrouwerToe(Brouwer brouwer)
        {
            throw new NotImplementedException();
        }

        public void WijzigBier(Bier bier)
        {
            throw new NotImplementedException();
        }

        public void WijzigBrouwer(Brouwer selectedBrouwer)
        {
            throw new NotImplementedException();
        }
    }
}
