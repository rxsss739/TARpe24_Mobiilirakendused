using System.Collections.ObjectModel;
using System.Globalization;

namespace Naidis_TARpe24;

public class Galerii : ContentPage
{
    ObservableCollection<ProgrammeerimisKeel> keeled;
    CarouselView carouselView;
    IndicatorView indicatorView;
    Label pealkiriLabel;
    Button keeleNupp;
    IDispatcherTimer timer;
    bool onEesti = true;
    VerticalStackLayout vsl;

    public Galerii()
    {
        Background = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(0, 1),
            GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Color.FromArgb("#0f0c29"), Offset = 0.0f },
                new GradientStop { Color = Color.FromArgb("#302b63"), Offset = 0.5f },
                new GradientStop { Color = Color.FromArgb("#24243e"), Offset = 1.0f }
            }
        };

        keeled = new ObservableCollection<ProgrammeerimisKeel>
        {
            new ProgrammeerimisKeel
            {
                Nimi = "C#",
                KirjeldusEt = "Võimas objektorienteeritud keel",
                KirjeldusEn = "Powerful object-oriented language",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Logo_C_sharp.svg/200px-Logo_C_sharp.svg.png",
                HelloWorld = "Console.WriteLine(\"Hello, World!\");",
                LoomisAasta = "2000"
            },
            new ProgrammeerimisKeel
            {
                Nimi = "Python",
                KirjeldusEt = "Suurepärane andmetöötluseks ja automatiseerimiseks",
                KirjeldusEn = "Great for data and automation",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Python-logo-notext.svg/200px-Python-logo-notext.svg.png",
                HelloWorld = "print(\"Hello, World!\")",
                LoomisAasta = "1991"
            },
            new ProgrammeerimisKeel
            {
                Nimi = "JavaScript",
                KirjeldusEt = "Veebiarenduse põhikeel",
                KirjeldusEn = "The language of the web",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Unofficial_JavaScript_logo_2.svg/200px-Unofficial_JavaScript_logo_2.svg.png",
                HelloWorld = "console.log(\"Hello, World!\");",
                LoomisAasta = "1995"
            },
            new ProgrammeerimisKeel
            {
                Nimi = "Java",
                KirjeldusEt = "Kirjuta kord, käivita igal pool",
                KirjeldusEn = "Write once, run anywhere",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/3/30/Java_programming_language_logo.svg/200px-Java_programming_language_logo.svg.png",
                HelloWorld = "System.out.println(\"Hello, World!\");",
                LoomisAasta = "1995"
            },
            new ProgrammeerimisKeel
            {
                Nimi = "C++",
                KirjeldusEt = "Suure jõudlusega süsteemikeel",
                KirjeldusEn = "High-performance system language",
                PiltUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/ISO_C%2B%2B_Logo.svg/200px-ISO_C%2B%2B_Logo.svg.png",
                HelloWorld = "std::cout << \"Hello, World!\";",
                LoomisAasta = "1985"
            }
        };

        UuendaKirjeldused();

        pealkiriLabel = new Label
        {
            Text = AppResources.Pealkiri,
            FontSize = 26,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        indicatorView = new IndicatorView
        {
            HorizontalOptions = LayoutOptions.Center,
            IndicatorColor = Color.FromArgb("#555577"),
            SelectedIndicatorColor = Colors.White,
            IndicatorSize = 10,
            Margin = new Thickness(0, 4, 0, 4)
        };

        carouselView = new CarouselView
        {
            ItemsSource = keeled,
            Loop = true,
            IndicatorView = indicatorView,
            HeightRequest = 390,
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 20,
                    Padding = new Thickness(24),
                    Margin = new Thickness(20, 8),
                    BackgroundColor = Color.FromArgb("#1e1e3f"),
                    BorderColor = Color.FromArgb("#4444aa"),
                    HasShadow = true
                };

                var logo = new Image
                {
                    HeightRequest = 110,
                    WidthRequest = 110,
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, 14)
                };
                logo.SetBinding(Image.SourceProperty, "PiltUrl");

                var nimiLabel = new Label
                {
                    FontSize = 30,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.White
                };
                nimiLabel.SetBinding(Label.TextProperty, "Nimi");

                var kirjeldusLabel = new Label
                {
                    FontSize = 15,
                    FontFamily = "Lufio",
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.FromArgb("#aaaacc"),
                    Margin = new Thickness(0, 8, 0, 0)
                };
                kirjeldusLabel.SetBinding(Label.TextProperty, "Kirjeldus");

                var aastaLabel = new Label
                {
                    FontSize = 13,
                    FontFamily = "Lufio",
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#7777aa"),
                    Margin = new Thickness(0, 6, 0, 0)
                };
                aastaLabel.SetBinding(Label.TextProperty, "AastaKuvamine");

                var cardStack = new VerticalStackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Children = { logo, nimiLabel, kirjeldusLabel, aastaLabel }
                };

                frame.Content = cardStack;

                var tap = new TapGestureRecognizer();
                tap.Tapped += KaartTapped;
                frame.GestureRecognizers.Add(tap);

                return frame;
            })
        };

        keeleNupp = new Button
        {
            Text = AppResources.KeeleNupp,
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#302b63"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 280
        };
        keeleNupp.Clicked += KeeleNupp_Clicked;

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { pealkiriLabel, carouselView, indicatorView, keeleNupp }
        };

        Content = new ScrollView { Content = vsl };

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(4);
        timer.Tick += (s, e) =>
        {
            int next = (carouselView.Position + 1) % keeled.Count;
            carouselView.ScrollTo(next, animate: true);
        };
        timer.Start();
    }

    private void UuendaKirjeldused()
    {
        foreach (var keel in keeled)
        {
            keel.Kirjeldus = onEesti ? keel.KirjeldusEt : keel.KirjeldusEn;
            keel.AastaKuvamine = $"{AppResources.LoodudPrefix}: {keel.LoomisAasta}";
        }
    }

    private void KeeleNupp_Clicked(object sender, EventArgs e)
    {
        onEesti = !onEesti;
        AppResources.Culture = new CultureInfo(onEesti ? "et" : "en-US");

        UuendaKirjeldused();
        pealkiriLabel.Text = AppResources.Pealkiri;
        keeleNupp.Text = AppResources.KeeleNupp;

        // Nimekirja värskendamine (sama "häkk" nagu EuroopaRiigid lehel)
        carouselView.ItemsSource = null;
        carouselView.ItemsSource = keeled;
    }

    private async void KaartTapped(object sender, EventArgs e)
    {
        var view = sender as View;
        var keel = view?.BindingContext as ProgrammeerimisKeel;
        if (keel == null) return;

        string sisu = $"Hello World:\n{keel.HelloWorld}\n\n{AppResources.LoodudPrefix}: {keel.LoomisAasta}";
        await DisplayAlertAsync(keel.Nimi, sisu, AppResources.Sulge);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer?.Stop();
    }
}