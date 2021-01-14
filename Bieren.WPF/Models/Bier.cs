using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.WPF.Models
{
    public class Bier : ObservableObject
    {
        //private const string defaultImagePath = "pack://siteoforigin:,,,/Images/";
        private int _bierNr;
        private string _naam;
        private double? _alcohol;
        private Brouwer _brouwer;
        private BierSoort _bierSoort;
        private DateTime _marktDatum;
        //private bool? _isLekker;
        //private string _website;


        public int BierNr { get { return _bierNr; } set { OnPropertyChanged(ref _bierNr, value); } }
        public string Naam { get { return _naam; } set { OnPropertyChanged(ref _naam, value); } }
        public double? Alcohol { get { return _alcohol; } set { OnPropertyChanged(ref _alcohol, value); } }
        public DateTime MarktDatum { get { return _marktDatum; } set { OnPropertyChanged(ref _marktDatum, value); } }
        //public string WebSite {
        //    get { return _website; }
        //    set { OnPropertyChanged(ref _website, value); }
        //}
        public Brouwer Brouwer
        {
            get { return _brouwer; }
            set
            {
                OnPropertyChanged(ref _brouwer, value);
            }
        }
        public BierSoort BierSoort
        {
            get { return _bierSoort; }
            set
            {
                OnPropertyChanged(ref _bierSoort, value);
            }
        }
        //private string _imagePad;
        //public string ImagePad {
        //    get { return defaultImagePath + _imagePad; }
        //    set
        //    {
                
        //        OnPropertyChanged(ref _imagePad, value);
        //    }
        //}

        //public bool? IsLekker
        //{
        //    get { return _isLekker; }
        //    set { OnPropertyChanged(ref _isLekker, value); }
        //}
    }
}

