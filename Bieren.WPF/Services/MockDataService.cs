using Bieren.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bieren.WPF.Services
{
    public class MockDataService : IDataService
    {
        #region fields
        private IList<Bier> _bieren;
        private IList<BierSoort> _soortenBieren;
        private IList<Brouwer> _brouwers;
        #endregion
        public MockDataService()
        {
            InitLists();
        }
        private void InitLists()
        {
            InitBrouwers();
            InitSoortenBieren();
            InitBieren();
        }

        private void InitBrouwers()
        {
            _brouwers = new List<Brouwer>() {
                 new Brouwer(){ BrouwerNr=1,BrNaam="Artois",Straat="Langestraat 20",PostCode=1741,Gemeente="Ternat-Wambeek",Omzet=3500 },
                 new Brouwer(){ BrouwerNr=2,BrNaam="Belle Vue", Straat="Delaunoy-straat 58-60", PostCode=1080 ,Gemeente="Sint-Jans-Molenbeek",Omzet= 300000.00},
                 new Brouwer(){ BrouwerNr=3,BrNaam="Liefmans",Straat="Aalststraat 200",PostCode=9700,Gemeente="Oudenaarde",Omzet=10000 }
            };
        }

        private void InitSoortenBieren()
        {
            _soortenBieren = new List<BierSoort>() {
                new BierSoort(){ SoortNr=1, SoortNaam="Lambik"},
                new BierSoort(){ SoortNr=2, SoortNaam="Pils"},
                new BierSoort(){ SoortNr=3, SoortNaam = "Geuze"}
            };
        }
        private void InitBieren()
        {
            _bieren = new List<Bier>() {
                new Bier(){ BierNr=1,Naam="Belle Vue Kriek", Alcohol=5.2, BierSoort = _soortenBieren[2],Brouwer=_brouwers[1] },
                new Bier(){ BierNr=2,Naam="Belle Vue framboise", Alcohol=5.2, BierSoort = _soortenBieren[2],Brouwer=_brouwers[1]},
                new Bier(){ BierNr=3,Naam="Stella Artois", Alcohol=5.2, BierSoort = _soortenBieren[1],Brouwer=_brouwers[0] },
                new Bier(){ BierNr=3,Naam="Liefmans Kriek", Alcohol=6.5, BierSoort = _soortenBieren[0],Brouwer=_brouwers[2]  },
                new Bier(){ BierNr=4,Naam="Heineken", Alcohol=5.2,BierSoort= _soortenBieren[1], Brouwer=_brouwers[0]  }
            };
        }
        public IList<Bier> GeefAlleBieren()
        {
            return _bieren;
        }

        public IList<Brouwer> GeefAlleBrouwers()
        {
            return _brouwers;
        }
        public IList<BierSoort> GeefAlleBierSoorten()
        {
            return _soortenBieren;
        }
        public IList<Bier> GeefBierenVoorBrouwer(Brouwer brouwer)
        {
            if (brouwer != null)
                return _bieren.Where(b => b.Brouwer.BrouwerNr == brouwer.BrouwerNr).ToList();
            else return new List<Bier>();
        }
        public IList<Bier> VoegBierToe(Bier bier)
        {     
            int bierNr =(_bieren.Count>0)? _bieren.Max(b => b.BierNr) + 1: 1;
            bier.BierNr = bierNr;
            _bieren.Add(bier);
            return _bieren;
        }
        public void WijzigBier(Bier nieuwBier)
        {
            //Bier currentBier = _bieren.Single(b => b.BierNr == bier.BierNr); //indien echte dbSet van EF Core aan database gelinkt
            int index = _bieren.IndexOf(nieuwBier);
            if (index >= 0)
            {
                _bieren[index] = nieuwBier;
            }
        }
        public IList<Bier> VerwijderBier(Bier bier)
        {
            _bieren.Remove(bier);
            return _bieren;
        }

        public IList<BierSoort> VoegBierSoortToe(BierSoort biersoort)
        {
             _soortenBieren.Add(biersoort);
            return _soortenBieren;
        }

        public IList<Brouwer> VerwijderBrouwer(Brouwer brouwer)
        {
            _brouwers.Remove(brouwer);
            return _brouwers;
        }

        public IList<Brouwer> VoegBrouwerToe(Brouwer brouwer)
        {
            int brouwerNr = (_brouwers.Count > 0) ? _brouwers.Max(b => b.BrouwerNr) + 1 : 1;
            brouwer.BrouwerNr = brouwerNr;
            _brouwers.Add(brouwer);
            return _brouwers;
        }

        public void WijzigBrouwer(Brouwer nieuwebrouwer)
        {

            int index = _brouwers.IndexOf(nieuwebrouwer);
            if (index >= 0)
            {
                _brouwers[index] = nieuwebrouwer;
            }
        }

        public IList<BierSoort> WijzigBierSoort(BierSoort selectedSoort)
        {
            throw new NotImplementedException();
        }

        public IList<BierSoort> VerwijderBierSoort(BierSoort selectedSoort)
        {
            throw new NotImplementedException();
        }
    }
}
