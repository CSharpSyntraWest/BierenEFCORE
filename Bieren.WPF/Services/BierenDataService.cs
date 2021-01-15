using Bieren.DataLayer.Models;
using Bieren.WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                var dbBieren = bierenDb.DbBiers.Include(b => b.BrouwerNrNavigation).Include(b => b.SoortNrNavigation);
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
                Brouwer = DbBrouwerToBrouwer(dbBier.BrouwerNrNavigation),
                BierSoort = DbSoortToBierSoort(dbBier.SoortNrNavigation)
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
                List<DbBrouwer> dbBrouwers = db.DbBrouwers.ToList();//.Include(b => b.DbBiers).ToList();
                foreach (DbBrouwer dbBrouwer in dbBrouwers)
                    brouwers.Add(DbBrouwerToBrouwer(dbBrouwer));
            }
            return brouwers;
        }

        private Brouwer DbBrouwerToBrouwer(DbBrouwer dbBrouwer)
        {
            if (dbBrouwer == null) return null;
            Brouwer brouwer = new Brouwer()
            {
                 BrouwerNr = dbBrouwer.BrouwerNr,
                 BrNaam = dbBrouwer.BrNaam,
                 Straat = dbBrouwer.Adres,
                 PostCode = dbBrouwer.PostCode,
                 Gemeente = dbBrouwer.Gemeente,
                 Omzet = dbBrouwer.Omzet,
                
            };

            return brouwer;
        }

        public IList<Bier> GeefBierenVoorBrouwer(Brouwer brouwer)
        {
            IList<Bier> bieren = new List<Bier>();
            using (BierenDbContext db = new BierenDbContext())
            {
                var dbBieren = db.DbBiers.Where(b => b.BrouwerNr == brouwer.BrouwerNr).Include(b => b.BrouwerNrNavigation);
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
            using (BierenDbContext db = new BierenDbContext())
            {
                DbSoort dbBierSoort = new DbSoort() { Soort = biersoort.SoortNaam };
                db.DbSoorts.Add(dbBierSoort);
                db.SaveChanges();
               
            }
            return DbSoortenToBierSoorten();
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

        public IList<BierSoort> WijzigBierSoort(BierSoort selectedSoort)
        {
            using (BierenDbContext db = new BierenDbContext())
            {
                DbSoort dbSoort = db.DbSoorts.Where(s => s.SoortNr == selectedSoort.SoortNr).FirstOrDefault();
                dbSoort.Soort = selectedSoort.SoortNaam;
                db.DbSoorts.Update(dbSoort);
                db.SaveChanges();
            }

            return DbSoortenToBierSoorten();
        }

        public IList<BierSoort> VerwijderBierSoort(BierSoort selectedSoort)
        {
            using (BierenDbContext db = new BierenDbContext())
            {
                var dbSoort = db.DbSoorts.Where(s => s.SoortNr == selectedSoort.SoortNr).FirstOrDefault();
                db.DbSoorts.Remove(dbSoort);
                db.SaveChanges();
            }

            return DbSoortenToBierSoorten();
        }

        public IList<User> GeefAlleUsers()
        {
            throw new NotImplementedException();
            //using (BierenDbContext db = new BierenDbContext())
            //{ 

            //}

        }
    }
}
