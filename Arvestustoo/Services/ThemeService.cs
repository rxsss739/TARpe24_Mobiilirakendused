using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.Services
{
    public class ThemeService
    {
        public void RakendaTumeTeema()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            Preferences.Set("teema", "tume");
        }

        public void RakendaHeleTeema()
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            Preferences.Set("teema", "hele");
        }

        public void TaastaTeema()
        {
            if (Preferences.Get("teema", "tume") == "tume")
                RakendaTumeTeema();
            else
                RakendaHeleTeema();
        }

        public bool OnTumeTeema => Preferences.Get("teema", "tume") == "tume";
    }
}
