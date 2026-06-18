using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public static class AppState
    {
        public static string AktiivneKeel { get; private set; } = "et";
        public static event Action<string> KeelMuutus;

        public static void ValiKeel(string keel)
        {
            AktiivneKeel = keel;
            KeelMuutus?.Invoke(keel);
        }
    }
}
