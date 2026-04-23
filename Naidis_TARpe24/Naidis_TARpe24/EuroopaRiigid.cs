using System.Collections.ObjectModel;

namespace Naidis_TARpe24;

public class EuroopaRiigid : ContentPage
{
    ObservableCollection<Riik> riigid;
    ListView listView;
    Entry nimiEntry;
    Entry pealinnEntry;
    Entry rahvaarvEntry;
    Entry lippEntry;
    Label statusLabel;
    Riik valitudRiik;
    VerticalStackLayout vsl;

    public EuroopaRiigid()
    {
        riigid = new ObservableCollection<Riik>
        {
            new Riik { Nimi = "Eesti", Pealinn = "Tallinn", Rahvaarv = 1331824, Lipp = "estonia.png" },
            new Riik { Nimi = "Läti", Pealinn = "Riia", Rahvaarv = 1830211, Lipp = "latvia.png" },
            new Riik { Nimi = "Soome", Pealinn = "Helsinki", Rahvaarv = 5541274, Lipp = "finland.png" },
            new Riik { Nimi = "Rootsi", Pealinn = "Stockholm", Rahvaarv = 10379295, Lipp = "sweden.png" }
        };

        statusLabel = new Label
        {
            Text = "Vali riik nimekirjast",
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Gray
        };

        listView = new ListView
        {
            ItemsSource = riigid,
            HeightRequest = 280,
            BackgroundColor = Colors.Transparent,
            ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();

                var grid = new Grid
                {
                    Padding = new Thickness(8, 4),
                    ColumnSpacing = 10,
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = 55 },
                        new ColumnDefinition { Width = GridLength.Star }
                    }
                };

                var lippImage = new Image
                {
                    WidthRequest = 42,
                    HeightRequest = 28,
                    Aspect = Aspect.AspectFill,
                    VerticalOptions = LayoutOptions.Center
                };
                lippImage.SetBinding(Image.SourceProperty, "Lipp");

                var nimiLabel = new Label
                {
                    FontSize = 16,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.Black
                };
                nimiLabel.SetBinding(Label.TextProperty, "Nimi");

                var pealinnLabel = new Label
                {
                    FontSize = 13,
                    FontFamily = "Lufio",
                    TextColor = Colors.Gray
                };
                pealinnLabel.SetBinding(Label.TextProperty, "Pealinn");

                var textStack = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = { nimiLabel, pealinnLabel }
                };

                grid.Add(lippImage, 0, 0);
                grid.Add(textStack, 1, 0);

                cell.View = grid;
                return cell;
            })
        };
        listView.ItemTapped += ListViewItemTapped;
        listView.ItemSelected += ListViewItemSelected;

        nimiEntry = new Entry
        {
            Placeholder = "Riigi nimi",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Colors.White
        };

        pealinnEntry = new Entry
        {
            Placeholder = "Pealinn",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Colors.White
        };

        rahvaarvEntry = new Entry
        {
            Placeholder = "Rahvaarv",
            FontSize = 16,
            FontFamily = "Lufio",
            Keyboard = Keyboard.Numeric,
            BackgroundColor = Colors.White
        };

        lippEntry = new Entry
        {
            Placeholder = "Lipu failinimi (nt. estonia.png)",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Colors.White
        };

        Button lisaNupp = new Button
        {
            Text = "Lisa riik",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        lisaNupp.Clicked += LisaNupp_Clicked;

        Button kustutaNupp = new Button
        {
            Text = "Kustuta valitud",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.IndianRed,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        kustutaNupp.Clicked += KustutaNupp_Clicked;

        Button salvestaNupp = new Button
        {
            Text = "Salvesta muudatused",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.SteelBlue,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        salvestaNupp.Clicked += SalvestaNupp_Clicked;

        HorizontalStackLayout nuppudeRida = new HorizontalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lisaNupp, kustutaNupp }
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                new Label
                {
                    Text = "Euroopa Riigid",
                    FontSize = 24,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.DarkBlue
                },
                listView,
                statusLabel,
                nimiEntry,
                pealinnEntry,
                rahvaarvEntry,
                lippEntry,
                nuppudeRida,
                salvestaNupp
            }
        };

        Content = new ScrollView { Content = vsl };
    }

    private async void ListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Riik riik) return;

        await DisplayAlertAsync("Riigi info",
            $"Riik: {riik.Nimi}\nPealinn: {riik.Pealinn}\nRahvaarv: {riik.Rahvaarv} inimest",
            "Sulge");
    }

    private void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not Riik riik) return;

        valitudRiik = riik;
        nimiEntry.Text = riik.Nimi;
        pealinnEntry.Text = riik.Pealinn;
        rahvaarvEntry.Text = riik.Rahvaarv.ToString();
        lippEntry.Text = riik.Lipp;

        statusLabel.Text = $"Valitud: {riik.Nimi}";
        statusLabel.TextColor = Colors.SteelBlue;
    }

    private async void LisaNupp_Clicked(object sender, EventArgs e)
    {
        string uusNimi = nimiEntry.Text?.Trim();

        if (string.IsNullOrEmpty(uusNimi))
        {
            await DisplayAlertAsync("Viga", "Riigi nimi ei tohi olla tühi!", "OK");
            return;
        }

        bool riikOnOlemas = riigid.Any(r => r.Nimi.Equals(uusNimi, StringComparison.OrdinalIgnoreCase));

        if (riikOnOlemas)
        {
            await DisplayAlertAsync("Viga", "See riik on juba nimekirjas!", "OK");
            return;
        }

        if (!int.TryParse(rahvaarvEntry.Text, out int rahvaarv))
        {
            await DisplayAlertAsync("Viga", "Rahvaarv peab olema number!", "OK");
            return;
        }

        riigid.Add(new Riik
        {
            Nimi = uusNimi,
            Pealinn = pealinnEntry.Text?.Trim() ?? "",
            Rahvaarv = rahvaarv,
            Lipp = lippEntry.Text?.Trim() ?? "default.png"
        });

        TyhjendalValjad();
        statusLabel.Text = $"{uusNimi} lisatud!";
        statusLabel.TextColor = Colors.Green;
    }

    private async void KustutaNupp_Clicked(object sender, EventArgs e)
    {
        if (valitudRiik == null)
        {
            await DisplayAlertAsync("Viga", "Vali esmalt riik nimekirjast!", "OK");
            return;
        }

        string kustututNimi = valitudRiik.Nimi;
        riigid.Remove(valitudRiik);

        valitudRiik = null;
        listView.SelectedItem = null;
        TyhjendalValjad();

        statusLabel.Text = $"{kustututNimi} kustutatud!";
        statusLabel.TextColor = Colors.IndianRed;
    }

    private async void SalvestaNupp_Clicked(object sender, EventArgs e)
    {
        if (valitudRiik == null)
        {
            await DisplayAlertAsync("Viga", "Vali esmalt riik nimekirjast!", "OK");
            return;
        }

        if (string.IsNullOrEmpty(nimiEntry.Text?.Trim()))
        {
            await DisplayAlertAsync("Viga", "Riigi nimi ei tohi olla tühi!", "OK");
            return;
        }

        if (!int.TryParse(rahvaarvEntry.Text, out int rahvaarv))
        {
            await DisplayAlertAsync("Viga", "Rahvaarv peab olema number!", "OK");
            return;
        }

        valitudRiik.Nimi = nimiEntry.Text.Trim();
        valitudRiik.Pealinn = pealinnEntry.Text?.Trim() ?? "";
        valitudRiik.Rahvaarv = rahvaarv;
        valitudRiik.Lipp = lippEntry.Text?.Trim() ?? "default.png";

        listView.ItemsSource = null;
        listView.ItemsSource = riigid;

        statusLabel.Text = $"{valitudRiik.Nimi} uuendatud!";
        statusLabel.TextColor = Colors.Green;

        valitudRiik = null;
        listView.SelectedItem = null;
        TyhjendalValjad();
    }

    private void TyhjendalValjad()
    {
        nimiEntry.Text = "";
        pealinnEntry.Text = "";
        rahvaarvEntry.Text = "";
        lippEntry.Text = "";
    }
}