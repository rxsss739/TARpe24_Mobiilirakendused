using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class Game
    {
        public Player Mangija { get; private set; }
        public bool OnAktiivne { get; private set; }
        public int KokkoYhendatud { get; private set; }
        public int KokkoParesid { get; private set; }
        public DateTime AlgusAeg { get; private set; }

        public Game(Player mangija, int paaridArv)
        {
            Mangija = mangija;
            KokkoParesid = paaridArv;
            KokkoYhendatud = 0;
            OnAktiivne = false;
        }

        public void Alusta()
        {
            OnAktiivne = true;
            AlgusAeg = DateTime.Now;
            KokkoYhendatud = 0;
            Mangija.Reset();
        }

        public void OigeVaste()
        {
            Mangija.LisaPunktid(10);
            Mangija.LisaKatse();
            KokkoYhendatud++;

            if (KokkoYhendatud >= KokkoParesid)
                OnAktiivne = false;
        }

        public void ValeVaste()
        {
            Mangija.LisaKatse();
        }

        public bool OnLabi()
        {
            return KokkoYhendatud >= KokkoParesid;
        }

        public string GetAeg()
        {
            var kestus = DateTime.Now - AlgusAeg;
            return $"{(int)kestus.TotalSeconds}s";
        }

        public string GetTulemus()
        {
            var kestus = DateTime.Now - AlgusAeg;
            return $"Aeg: {(int)kestus.TotalSeconds}s\nPunktid: {Mangija.Punktid}\nKatsed: {Mangija.Katsed}";
        }
    }
}
