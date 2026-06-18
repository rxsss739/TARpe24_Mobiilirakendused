using Arvestustoo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.ViewModels
{
    public class SeadedViewModel : BaseViewModel
    {
        string _aktiivneKeel;
        public string AktiivneKeel
        {
            get => _aktiivneKeel;
            set { _aktiivneKeel = value; OnPropertyChanged(); }
        }

        bool _onTumeTeema;
        public bool OnTumeTeema
        {
            get => _onTumeTeema;
            set { _onTumeTeema = value; OnPropertyChanged(); }
        }

        bool _heliSees;
        public bool HeliSees
        {
            get => _heliSees;
            set
            {
                _heliSees = value;
                Preferences.Set("heli", value);
                OnPropertyChanged();
            }
        }

        public SeadedViewModel()
        {
            AktiivneKeel = AppState.AktiivneKeel;
            OnTumeTeema = Preferences.Get("teema", "tume") == "tume";
            HeliSees = Preferences.Get("heli", true);
        }
    }
}
