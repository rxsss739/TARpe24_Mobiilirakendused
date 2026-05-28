using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class KaartItem
    {
        public string Id { get; set; }
        public string VasakTekst { get; set; }
        public string ParemTekst { get; set; }
        public bool OnYhendatud { get; set; }

        public KaartItem(string id, string vasakTekst, string paremTekst)
        {
            Id = id;
            VasakTekst = vasakTekst;
            ParemTekst = paremTekst;
            OnYhendatud = false;
        }
    }
}
