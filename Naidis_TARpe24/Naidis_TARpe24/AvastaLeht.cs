using System.Globalization;

namespace Naidis_TARpe24;

public class AvastaLeht : ContentPage
{
    ExploreViewModel viewModel;
    CarouselView carouselView;
    IndicatorView indicatorView;
    Label pealkiriLabel;
    IDispatcherTimer aegTimer;
    VerticalStackLayout vsl;

    public AvastaLeht(ExploreViewModel vm)
    {
        viewModel = vm;
        Title = "🗺️ Avasta";

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

        pealkiriLabel = new Label
        {
            Text = AppResources.AvastaPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        var katNupud = new HorizontalStackLayout
        {
            Spacing = 8,
            HorizontalOptions = LayoutOptions.Center
        };

        var kategoriad = new (string tekst, string kood)[]
        {
            ("🏘️ Kõik", "kõik"), ("🏰", "🏰"), ("🌳", "🌳"), ("🍽️", "🍽️")
        };

        foreach (var (tekst, kood) in kategoriad)
        {
            Button katNupp = new Button
            {
                Text = tekst,
                FontSize = 18,
                FontFamily = "Lufio",
                BackgroundColor = Color.FromArgb("#16213e"),
                TextColor = Colors.White,
                CornerRadius = 10,
                HeightRequest = 45,
                BorderColor = Color.FromArgb("#4444aa"),
                BorderWidth = 1.5
            };
            string k = kood;
            katNupp.Clicked += (s, e) =>
            {
                viewModel.FiltereeriKategooria(k);
                carouselView.ItemsSource = null;
                carouselView.ItemsSource = viewModel.Kohad;
            };
            katNupud.Add(katNupp);
        }

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
            ItemsSource = viewModel.Kohad,
            Loop = true,
            IndicatorView = indicatorView,
            HeightRequest = 400,
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 20,
                    Padding = new Thickness(16),
                    Margin = new Thickness(16, 8),
                    BackgroundColor = Color.FromArgb("#1e1e3f"),
                    BorderColor = Color.FromArgb("#4444aa"),
                    HasShadow = true
                };

                var pilt = new Image
                {
                    HeightRequest = 180,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Fill
                };
                pilt.SetBinding(Image.SourceProperty, "PildiUrl");

                var nimiLabel = new Label
                {
                    FontSize = 22,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Colors.White,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                nimiLabel.SetBinding(Label.TextProperty, "KuvaNimi");

                var kirjeldusLabel = new Label
                {
                    FontSize = 14,
                    FontFamily = "Lufio",
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = Color.FromArgb("#aaaacc"),
                    Margin = new Thickness(0, 6, 0, 0)
                };
                kirjeldusLabel.SetBinding(Label.TextProperty, "KuvaKirjeldus");

                var aadressLabel = new Label
                {
                    FontSize = 12,
                    FontFamily = "Lufio",
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#7777aa"),
                    Margin = new Thickness(0, 4, 0, 0)
                };
                aadressLabel.SetBinding(Label.TextProperty, "Aadress");

                var cardStack = new VerticalStackLayout
                {
                    Children = { pilt, nimiLabel, kirjeldusLabel, aadressLabel }
                };

                frame.Content = cardStack;

                var tap = new TapGestureRecognizer();
                tap.Tapped += KaartTapped;
                frame.GestureRecognizers.Add(tap);

                return frame;
            })
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { pealkiriLabel, katNupud, carouselView, indicatorView }
        };

        Content = new ScrollView { Content = vsl };

        aegTimer = Dispatcher.CreateTimer();
        aegTimer.Interval = TimeSpan.FromSeconds(4);
        aegTimer.Tick += (s, e) =>
        {
            if (viewModel.Kohad.Count == 0) return;
            int next = (carouselView.Position + 1) % viewModel.Kohad.Count;
            carouselView.ScrollTo(next, animate: true);
        };

        AppState.KeelMuutus += KeelMuutus_Handler;
    }

    private void KeelMuutus_Handler(string keel)
    {
        AppResources.Culture = new CultureInfo(keel switch { "en" => "en-US", "ru" => "ru", _ => "et" });
        viewModel.UuendaKeel(keel);
        carouselView.ItemsSource = null;
        carouselView.ItemsSource = viewModel.Kohad;
        pealkiriLabel.Text = AppResources.AvastaPealkiri;
    }

    private async void KaartTapped(object sender, EventArgs e)
    {
        var view = sender as View;
        if (view?.BindingContext is not Vaatamisvaarsus koht) return;

        if (viewModel.OnLemmik(koht.Nimi))
        {
            await DisplayAlertAsync(koht.KuvaNimi,
                $"{koht.KuvaKirjeldus}\n\n📍 {koht.Aadress}\n\n⭐ {AppResources.JubaLemmik}",
                AppResources.Sulge);
        }
        else
        {
            bool lisaLemmik = await DisplayAlertAsync(koht.KuvaNimi,
                $"{koht.KuvaKirjeldus}\n\n📍 {koht.Aadress}",
                AppResources.LisaLemmik, AppResources.Sulge);

            if (lisaLemmik)
            {
                viewModel.LisaLemmik(koht);
                await DisplayAlertAsync("✅", AppResources.LemmikLisatud, AppResources.Sulge);
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        aegTimer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        aegTimer.Stop();
    }
}