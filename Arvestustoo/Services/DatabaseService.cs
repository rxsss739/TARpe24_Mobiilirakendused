using Arvestustoo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.Services
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public DatabaseService()
        {
            string tee = Path.Combine(FileSystem.AppDataDirectory, "filmipaevik.db3");
            db = new SQLiteConnection(tee);
            db.CreateTable<Film>();
        }

        public List<Film> GetFilmid() =>
            db.Table<Film>().OrderByDescending(f => f.LisamisKuupaev).ToList();

        public void LisaFilm(Film film)
        {
            film.LisamisKuupaev = DateTime.Now;
            db.Insert(film);
        }

        public void UuendaFilm(Film film) => db.Update(film);

        public void KustutaFilm(int id) => db.Delete<Film>(id);
    }
}