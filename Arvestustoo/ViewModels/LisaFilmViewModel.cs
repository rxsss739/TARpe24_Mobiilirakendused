using Arvestustoo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.ViewModels
{
    public class LisaFilmViewModel : BaseViewModel
    {
        string _nimi;
        public string Nimi { get => _nimi; set { _nimi = value; OnPropertyChanged(); } }

        string _zanr;
        public string Zanr { get => _zanr; set { _zanr = value; OnPropertyChanged(); } }

        int _hinne = 3;
        public int Hinne
        {
            get => _hinne;
            set
            {
                _hinne = Math.Clamp(value, 1, 5);
                OnPropertyChanged();
                OnPropertyChanged(nameof(HinneTekst));
            }
        }

        string _pildiUrl;
        public string PildiUrl { get => _pildiUrl; set { _pildiUrl = value; OnPropertyChanged(); } }

        string _markused;
        public string Markused { get => _markused; set { _markused = value; OnPropertyChanged(); } }

        public string HinneTekst => new string('⭐', Hinne) + new string('☆', 5 - Hinne);

        Film _valitudFilm;
        public Film ValitudFilm
        {
            get => _valitudFilm;
            set
            {
                _valitudFilm = value;
                if (value != null)
                {
                    Nimi = value.Nimi;
                    Zanr = value.Zanr;
                    Hinne = value.Hinne;
                    PildiUrl = value.PildiUrl;
                    Markused = value.Markused;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(OnMuutmine));
            }
        }

        public bool OnMuutmine => ValitudFilm != null;

        public void Tyhjenda()
        {
            Nimi = "";
            Zanr = "";
            Hinne = 3;
            PildiUrl = "";
            Markused = "";
            ValitudFilm = null;
        }
    }
}
