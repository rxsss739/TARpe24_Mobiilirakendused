using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Naidis_TARpe24
{
    public class ExploreViewModel : BaseViewModel
    {
        DatabaseService db;
        List<Vaatamisvaarsus> koikKohad;

        ObservableCollection<Vaatamisvaarsus> _kohad;
        public ObservableCollection<Vaatamisvaarsus> Kohad
        {
            get => _kohad;
            set { _kohad = value; OnPropertyChanged(); }
        }

        public ExploreViewModel(DatabaseService databaseService)
        {
            db = databaseService;
            KäivitaKohad();
            UuendaKeel("et");
        }

        void KäivitaKohad()
        {
            koikKohad = new List<Vaatamisvaarsus>
        {
            new Vaatamisvaarsus
            {
                Nimi = "Raekoda", NimiEn = "Town Hall", NimiRu = "Ратуша",
                Kirjeldus = "Keskaegne raekoda Tallinna südames",
                KirjeldusEn = "Medieval town hall in the heart of Tallinn",
                KirjeldusRu = "Средневековая ратуша в сердце Таллинна",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Tallinn_Town_Hall_2013_03.jpg/400px-Tallinn_Town_Hall_2013_03.jpg",
                Kategooria = "🏰", Aadress = "Raekoja plats 1"
            },
            new Vaatamisvaarsus
            {
                Nimi = "Toompea loss", NimiEn = "Toompea Castle", NimiRu = "Замок Тоомпеа",
                Kirjeldus = "Eesti parlamendi asukoht, keskaegne kindlus",
                KirjeldusEn = "Home of the Estonian parliament, medieval fortress",
                KirjeldusRu = "Место заседания парламента Эстонии",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Toompea_in_winter.jpg/400px-Toompea_in_winter.jpg",
                Kategooria = "🏰", Aadress = "Lossi plats 1a"
            },
            new Vaatamisvaarsus
            {
                Nimi = "Kadrioru park", NimiEn = "Kadriorg Park", NimiRu = "Парк Кадриорг",
                Kirjeldus = "Ilus barokne park ja loss mere lähedal",
                KirjeldusEn = "Beautiful baroque park and palace near the sea",
                KirjeldusRu = "Красивый барочный парк и дворец у моря",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Kadrioru_park_2.jpg/400px-Kadrioru_park_2.jpg",
                Kategooria = "🌳", Aadress = "Weizenbergi 37"
            },
            new Vaatamisvaarsus
            {
                Nimi = "Toompark", NimiEn = "Toompark", NimiRu = "Тоомпарк",
                Kirjeldus = "Rahulik park vanalinna kõrval, jalutamiseks ideaalne",
                KirjeldusEn = "Peaceful park next to old town, ideal for walks",
                KirjeldusRu = "Тихий парк рядом со старым городом",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Tallinn_Toompark.jpg/400px-Tallinn_Toompark.jpg",
                Kategooria = "🌳", Aadress = "Toompark"
            },
            new Vaatamisvaarsus
            {
                Nimi = "Olde Hansa", NimiEn = "Olde Hansa", NimiRu = "Олде Ханса",
                Kirjeldus = "Keskaegne restoran autentse atmosfääriga",
                KirjeldusEn = "Medieval restaurant with an authentic atmosphere",
                KirjeldusRu = "Средневековый ресторан с аутентичной атмосферой",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/Olde_Hansa.jpg/400px-Olde_Hansa.jpg",
                Kategooria = "🍽️", Aadress = "Vana turg 1"
            },
            new Vaatamisvaarsus
            {
                Nimi = "Leib Resto & Aed", NimiEn = "Leib Resto & Aed", NimiRu = "Лейб Ресто",
                Kirjeldus = "Kaasaegne eesti köök ajaloolises hoones",
                KirjeldusEn = "Modern Estonian cuisine in a historic building",
                KirjeldusRu = "Современная эстонская кухня в историческом здании",
                PildiUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Tallinn_Street.jpg/400px-Tallinn_Street.jpg",
                Kategooria = "🍽️", Aadress = "Uus 31"
            }
        };

            Kohad = new ObservableCollection<Vaatamisvaarsus>(koikKohad);
        }

        public void UuendaKeel(string keel)
        {
            foreach (var koht in koikKohad)
            {
                koht.KuvaNimi = keel switch { "en" => koht.NimiEn, "ru" => koht.NimiRu, _ => koht.Nimi };
                koht.KuvaKirjeldus = keel switch { "en" => koht.KirjeldusEn, "ru" => koht.KirjeldusRu, _ => koht.Kirjeldus };
            }
        }

        public void FiltereeriKategooria(string kategooria)
        {
            var filtered = kategooria == "kõik"
                ? koikKohad
                : koikKohad.Where(k => k.Kategooria == kategooria).ToList();
            Kohad = new ObservableCollection<Vaatamisvaarsus>(filtered);
        }

        public void LisaLemmik(Vaatamisvaarsus koht)
        {
            db.LisaLemmik(new Lemmik
            {
                Nimi = koht.Nimi,
                Kirjeldus = koht.Kirjeldus,
                PildiUrl = koht.PildiUrl,
                Kategooria = koht.Kategooria,
                Aadress = koht.Aadress
            });
        }

        public bool OnLemmik(string nimi) => db.OnLemmik(nimi);
    }
}
