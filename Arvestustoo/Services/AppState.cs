using Arvestustoo.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Arvestustoo.Services
{
    public static class AppState
    {
        public static string AktiivneKeel { get; private set; } = "et";
        public static event Action<string> KeelMuutus;
        public static event Action<bool> TeemaMuutus;

        public static void ValiKeel(string keel)
        {
            AktiivneKeel = keel;
            Preferences.Set("keel", keel);
            AppResources.Culture = new CultureInfo(keel == "en" ? "en-US" : "et");
            KeelMuutus?.Invoke(keel);
        }

        public static void ValiTeema(bool tumeTeema)
        {
            TeemaMuutus?.Invoke(tumeTeema);
        }

        public static void TaastaSeaded()
        {
            AktiivneKeel = Preferences.Get("keel", "et");
            AppResources.Culture = new CultureInfo(AktiivneKeel == "en" ? "en-US" : "et");
        }
    }
}
