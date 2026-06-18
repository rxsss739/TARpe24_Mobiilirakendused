using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class Lemmik
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Kirjeldus { get; set; }
        public string PildiUrl { get; set; }
        public string Kategooria { get; set; }
        public string Aadress { get; set; }
    }
}
