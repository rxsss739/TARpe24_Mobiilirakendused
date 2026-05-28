using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public class RetseptiKategooria : List<Retsept>
    {
        public string Nimetus { get; set; }

        public RetseptiKategooria(string nimetus, IEnumerable<Retsept> retseptid)
        {
            Nimetus = nimetus;
            AddRange(retseptid);
        }
    }
}
