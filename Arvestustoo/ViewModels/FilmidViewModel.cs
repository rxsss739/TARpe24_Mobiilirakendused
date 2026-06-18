using Arvestustoo.Models;
using Arvestustoo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Arvestustoo.ViewModels
{
    public class FilmidViewModel : BaseViewModel
    {
        DatabaseService db;

        ObservableCollection<Film> _filmid;
        public ObservableCollection<Film> Filmid
        {
            get => _filmid;
            set { _filmid = value; OnPropertyChanged(); }
        }

        bool _onTyhi;
        public bool OnTyhi
        {
            get => _onTyhi;
            set { _onTyhi = value; OnPropertyChanged(); }
        }

        public ICommand KustutaKäsk { get; }

        public FilmidViewModel(DatabaseService databaseService)
        {
            db = databaseService;
            Filmid = new ObservableCollection<Film>();

            KustutaKäsk = new Command<Film>(film =>
            {
                db.KustutaFilm(film.Id);
                LaeFilmid();
            });
        }

        public void LaeFilmid()
        {
            var andmed = db.GetFilmid();
            Filmid = new ObservableCollection<Film>(andmed);
            OnTyhi = Filmid.Count == 0;
        }

        public void KustutaFilm(Film film)
        {
            db.KustutaFilm(film.Id);
            LaeFilmid();
        }
    }
}
