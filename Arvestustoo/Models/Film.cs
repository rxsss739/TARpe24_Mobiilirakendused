using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.Models
{
    public class Film
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Zanr { get; set; }
        public int Hinne { get; set; }
        public string PildiUrl { get; set; }
        public string Markused { get; set; }
        public DateTime LisamisKuupaev { get; set; }

        public string HinneKuvamine => new string('⭐', Hinne) + new string('☆', 5 - Hinne);
        public string KuupaevKuvamine => LisamisKuupaev.ToString("dd.MM.yyyy");
    }
}
