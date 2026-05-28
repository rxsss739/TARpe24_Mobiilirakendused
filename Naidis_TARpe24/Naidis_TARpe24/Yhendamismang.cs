namespace Naidis_TARpe24;

public class Yhendamismang : ContentPage
{
    List<KaartItem> kaardid;
    List<KaartItem> segitudParemPool;
    Button[] vasakNupud;
    Button[] paremNupud;
    Game game;
    Player player;
    Theme aktiivseTeem;
    Label pealkiriLabel;
    Label statsLabel;
    Label aegLabel;
    Button valitudVasakNupp;
    KaartItem valitudVasakKaart;
    IDispatcherTimer aegTimer;
    Frame mangContainer;
    Grid mangGrid;
    VerticalStackLayout vsl;

    public Yhendamismang()
    {
        player = new Player("Mängija");
        aktiivseTeem = Theme.Tume;

        KäivitaAndmed();

        pealkiriLabel = new Label
        {
            Text = "Ühendamismäng",
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = aktiivseTeem.TekstiVärv
        };

        Label juhendLabel = new Label
        {
            Text = "Vali vasakult keel, seejärel õige kirjeldus paremalt",
            FontSize = 13,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.FromArgb("#aaaacc")
        };

        aegLabel = new Label
        {
            Text = "Aeg: 0s",
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = aktiivseTeem.TekstiVärv
        };

        statsLabel = new Label
        {
            Text = player.GetStats(),
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = aktiivseTeem.TekstiVärv
        };

        Button heleNupp = new Button
        {
            Text = "☀️ Hele",
            FontSize = 14,
            FontFamily = "Lufio",
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 8,
            HeightRequest = 40
        };
        heleNupp.Clicked += (s, e) => RakendaTeem(Theme.Hele);

        Button tumeNupp = new Button
        {
            Text = "🌙 Tume",
            FontSize = 14,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 8,
            HeightRequest = 40
        };
        tumeNupp.Clicked += (s, e) => RakendaTeem(Theme.Tume);

        Button värvNupp = new Button
        {
            Text = "🎨 Värviline",
            FontSize = 14,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#ffc8dd"),
            TextColor = Color.FromArgb("#22223b"),
            CornerRadius = 8,
            HeightRequest = 40
        };
        värvNupp.Clicked += (s, e) => RakendaTeem(Theme.Värviline);

        HorizontalStackLayout teemadeRida = new HorizontalStackLayout
        {
            Spacing = 8,
            HorizontalOptions = LayoutOptions.Center,
            Children = { heleNupp, tumeNupp, värvNupp }
        };

        mangGrid = EhitaMänguGrid();
        mangContainer = new Frame
        {
            Padding = 0,
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            Content = mangGrid
        };

        Button uusMangNupp = new Button
        {
            Text = "Uus mäng",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 200
        };
        uusMangNupp.Clicked += (s, e) => UusMang();

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                pealkiriLabel,
                juhendLabel,
                teemadeRida,
                aegLabel,
                statsLabel,
                mangContainer,
                uusMangNupp
            }
        };

        aktiivseTeem.Apply(this);
        Content = new ScrollView { Content = vsl };

        aegTimer = Dispatcher.CreateTimer();
        aegTimer.Interval = TimeSpan.FromSeconds(1);
        aegTimer.Tick += (s, e) =>
        {
            if (game.OnAktiivne)
                aegLabel.Text = $"Aeg: {game.GetAeg()}";
        };
    }

    private void KäivitaAndmed()
    {
        game = new Game(player, 6);

        kaardid = new List<KaartItem>
        {
            new KaartItem("1", "C#",         "Objektorienteeritud .NET keel"),
            new KaartItem("2", "Python",      "Andmetöötlus ja automatiseerimine"),
            new KaartItem("3", "JavaScript",  "Veebiarenduse põhikeel"),
            new KaartItem("4", "Java",        "Kirjuta kord, käivita igal pool"),
            new KaartItem("5", "C++",         "Süsteemiprogrammeerimise keel"),
            new KaartItem("6", "Swift",       "Apple'i platvormide keel")
        };

        segitudParemPool = kaardid.OrderBy(_ => Guid.NewGuid()).ToList();
        valitudVasakNupp = null;
        valitudVasakKaart = null;
    }

    private Grid EhitaMänguGrid()
    {
        var grid = new Grid
        {
            ColumnSpacing = 10,
            RowSpacing = 10,
            HorizontalOptions = LayoutOptions.Fill,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            }
        };

        for (int i = 0; i < kaardid.Count; i++)
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        vasakNupud = new Button[kaardid.Count];
        paremNupud = new Button[kaardid.Count];

        for (int i = 0; i < kaardid.Count; i++)
        {
            var kaart = kaardid[i];
            var paremKaart = segitudParemPool[i];

            Button vasakNupp = new Button
            {
                Text = kaart.VasakTekst,
                FontSize = 16,
                FontFamily = "Lufio",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = aktiivseTeem.NupiVärv,
                TextColor = aktiivseTeem.NupiTekstiVärv,
                CornerRadius = 10,
                HeightRequest = 60,
                BorderColor = Colors.Transparent,
                BorderWidth = 2
            };

            int idx = i;
            vasakNupp.Clicked += (s, e) => VasakNuppKlõps(idx);

            Button paremNupp = new Button
            {
                Text = paremKaart.ParemTekst,
                FontSize = 12,
                FontFamily = "Lufio",
                BackgroundColor = aktiivseTeem.NupiVärv,
                TextColor = aktiivseTeem.NupiTekstiVärv,
                CornerRadius = 10,
                HeightRequest = 60,
                LineBreakMode = LineBreakMode.WordWrap,
                BorderColor = Colors.Transparent,
                BorderWidth = 2
            };

            paremNupp.Clicked += (s, e) => ParemNuppKlõps(idx);

            vasakNupud[i] = vasakNupp;
            paremNupud[i] = paremNupp;

            grid.Add(vasakNupp, 0, i);
            grid.Add(paremNupp, 1, i);
        }

        return grid;
    }

    private void VasakNuppKlõps(int idx)
    {
        if (!game.OnAktiivne) return;
        if (kaardid[idx].OnYhendatud) return;

        if (valitudVasakNupp != null)
            valitudVasakNupp.BackgroundColor = aktiivseTeem.NupiVärv;

        valitudVasakNupp = vasakNupud[idx];
        valitudVasakKaart = kaardid[idx];

        vasakNupud[idx].BackgroundColor = aktiivseTeem.ValitudVärv;
        vasakNupud[idx].BorderColor = Colors.White;
    }

    private async void ParemNuppKlõps(int idx)
    {
        if (!game.OnAktiivne) return;
        if (valitudVasakKaart == null) return;
        if (segitudParemPool[idx].OnYhendatud) return;

        var paremKaart = segitudParemPool[idx];

        if (paremKaart.Id == valitudVasakKaart.Id)
        {
            paremKaart.OnYhendatud = true;
            valitudVasakKaart.OnYhendatud = true;

            valitudVasakNupp.BackgroundColor = Colors.SeaGreen;
            valitudVasakNupp.BorderColor = Colors.Transparent;
            paremNupud[idx].BackgroundColor = Colors.SeaGreen;
            valitudVasakNupp.IsEnabled = false;
            paremNupud[idx].IsEnabled = false;

            game.OigeVaste();
            statsLabel.Text = player.GetStats();

            await valitudVasakNupp.ScaleTo(1.08, 80);
            await valitudVasakNupp.ScaleTo(1.0, 80);
            await paremNupud[idx].ScaleTo(1.08, 80);
            await paremNupud[idx].ScaleTo(1.0, 80);

            valitudVasakNupp = null;
            valitudVasakKaart = null;

            if (game.OnLabi())
            {
                aegTimer.Stop();
                await ShowResult();
            }
        }
        else
        {
            game.ValeVaste();
            statsLabel.Text = player.GetStats();

            valitudVasakNupp.BackgroundColor = Colors.IndianRed;
            paremNupud[idx].BackgroundColor = Colors.IndianRed;

            await Task.Delay(400);

            valitudVasakNupp.BackgroundColor = aktiivseTeem.NupiVärv;
            valitudVasakNupp.BorderColor = Colors.Transparent;
            paremNupud[idx].BackgroundColor = aktiivseTeem.NupiVärv;

            valitudVasakNupp = null;
            valitudVasakKaart = null;
        }
    }

    private async Task ShowResult()
    {
        string tulemus = game.GetTulemus();
        bool uuesti = await DisplayAlertAsync("Mäng läbi! 🎉", $"Tubli, {player.Nimi}!\n\n{tulemus}", "Mängi uuesti", "Sulge");
        if (uuesti) UusMang();
    }

    private void UusMang()
    {
        aegTimer.Stop();
        KäivitaAndmed();
        game.Alusta();

        mangGrid = EhitaMänguGrid();
        mangContainer.Content = mangGrid;

        statsLabel.Text = player.GetStats();
        aegLabel.Text = "Aeg: 0s";
        aktiivseTeem.Apply(this);
        aegTimer.Start();
    }

    private void RakendaTeem(Theme teem)
    {
        aktiivseTeem = teem;
        teem.Apply(this);

        pealkiriLabel.TextColor = teem.TekstiVärv;
        statsLabel.TextColor = teem.TekstiVärv;
        aegLabel.TextColor = teem.TekstiVärv;

        for (int i = 0; i < vasakNupud.Length; i++)
        {
            if (!kaardid[i].OnYhendatud)
            {
                vasakNupud[i].BackgroundColor = teem.NupiVärv;
                vasakNupud[i].TextColor = teem.NupiTekstiVärv;
                vasakNupud[i].BorderColor = Colors.Transparent;
            }
        }

        for (int i = 0; i < paremNupud.Length; i++)
        {
            if (!segitudParemPool[i].OnYhendatud)
            {
                paremNupud[i].BackgroundColor = teem.NupiVärv;
                paremNupud[i].TextColor = teem.NupiTekstiVärv;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        game.Alusta();
        aegTimer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        aegTimer?.Stop();
    }
}