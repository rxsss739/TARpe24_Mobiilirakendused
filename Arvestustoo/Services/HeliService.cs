using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arvestustoo.Services
{
    public class HeliService
    {
        public async Task MängiEdukusHeli()
        {
            if (!Preferences.Get("heli", true)) return;
            try
            {
                var player = AudioManager.Current.CreatePlayer(
                    await FileSystem.OpenAppPackageFileAsync("success.mp3"));
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Heli viga: {ex.Message}");
            }
        }
    }
}
