using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public DatabaseService()
        {
            string tee = Path.Combine(FileSystem.AppDataDirectory, "cityexplorer.db3");
            db = new SQLiteConnection(tee);
            db.CreateTable<Lemmik>();
        }

        public List<Lemmik> GetLemmikud() => db.Table<Lemmik>().ToList();

        public void LisaLemmik(Lemmik lemmik)
        {
            if (!OnLemmik(lemmik.Nimi))
                db.Insert(lemmik);
        }

        public void KustutaLemmik(int id) => db.Delete<Lemmik>(id);

        public bool OnLemmik(string nimi) => db.Table<Lemmik>().Any(l => l.Nimi == nimi);
    }
}
