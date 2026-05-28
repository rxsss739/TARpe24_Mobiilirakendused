using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class Theme
    {
        public string Nimi { get; set; }
        public Color TaustVärv { get; set; }
        public Color TekstiVärv { get; set; }
        public Color NupiVärv { get; set; }
        public Color NupiTekstiVärv { get; set; }
        public Color ValitudVärv { get; set; }
        public string Font { get; set; }

        public static Theme Hele => new Theme
        {
            Nimi = "Hele",
            TaustVärv = Colors.WhiteSmoke,
            TekstiVärv = Colors.Black,
            NupiVärv = Colors.LightGray,
            NupiTekstiVärv = Colors.Black,
            ValitudVärv = Colors.SteelBlue,
            Font = "Lufio"
        };

        public static Theme Tume => new Theme
        {
            Nimi = "Tume",
            TaustVärv = Color.FromArgb("#1a1a2e"),
            TekstiVärv = Colors.White,
            NupiVärv = Color.FromArgb("#16213e"),
            NupiTekstiVärv = Colors.White,
            ValitudVärv = Color.FromArgb("#0f3460"),
            Font = "Lufio"
        };

        public static Theme Värviline => new Theme
        {
            Nimi = "Värviline",
            TaustVärv = Color.FromArgb("#f8edeb"),
            TekstiVärv = Color.FromArgb("#22223b"),
            NupiVärv = Color.FromArgb("#ffc8dd"),
            NupiTekstiVärv = Color.FromArgb("#22223b"),
            ValitudVärv = Color.FromArgb("#c77dff"),
            Font = "Lufio"
        };

        public void Apply(ContentPage page)
        {
            page.BackgroundColor = TaustVärv;
        }
    }
}
