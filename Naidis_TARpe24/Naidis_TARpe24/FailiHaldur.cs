using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24
{
    public static class FailiHaldur
    {
        static string failiTee = Path.Combine(FileSystem.AppDataDirectory, "retseptid.txt");

        public static List<Retsept> LoeRetseptid()
        {
            var nimekiri = new List<Retsept>();

            if (!File.Exists(failiTee)) return nimekiri;

            string[] read = File.ReadAllLines(failiTee);
            foreach (string rida in read)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(rida)) continue;

                    string[] osad = rida.Split(';');
                    if (osad.Length >= 3)
                    {
                        nimekiri.Add(new Retsept
                        {
                            Nimi = osad[0],
                            Kategooria = osad[1],
                            PildiLink = osad[2]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Viga real: {ex.Message}");
                }
            }

            return nimekiri;
        }

        public static void SalvestaRetseptid(List<Retsept> retseptid)
        {
            var read = retseptid.Select(r => $"{r.Nimi};{r.Kategooria};{r.PildiLink}");
            File.WriteAllLines(failiTee, read);
        }

        public static void LisaRetsept(Retsept retsept)
        {
            File.AppendAllText(failiTee, $"{retsept.Nimi};{retsept.Kategooria};{retsept.PildiLink}\n");
        }
    }
}
