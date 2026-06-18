using Arvestustoo.Resources;
using Arvestustoo.Services;
using Arvestustoo.ViewModels;

namespace Arvestustoo.Views;

public class SeadedLeht : ContentPage
{
    SeadedViewModel viewModel;
    ThemeService themeService;
    Label pealkiriLabel;
    Label keelLabel;
    Label teemaLabel;
    Label heliLabel;
    Label aktKeelLabel;
    Switch heliSwitch;
    VerticalStackLayout vsl;

    public SeadedLeht(SeadedViewModel vm, ThemeService themeServ)
    {
        viewModel = vm;
        themeService = themeServ;
        Title = "⚙️";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        pealkiriLabel = new Label
        {
            Text = AppResources.SeadedPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        keelLabel = new Label
        {
            Text = AppResources.AktiivneKeel,
            FontSize = 16,
            FontFamily = "Lufio",
            TextColor = Color.FromArgb("#aaaacc"),
            HorizontalOptions = LayoutOptions.Center
        };

        aktKeelLabel = new Label
        {
            Text = viewModel.AktiivneKeel == "et" ? "🇪🇪 Eesti" : "🇬🇧 English",
            FontSize = 18,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        };

        Button eestiNupp = new Button
        {
            Text = "🇪🇪 Eesti keel",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 48,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 240
        };
        eestiNupp.Clicked += (s, e) =>
        {
            AppState.ValiKeel("et");
            viewModel.AktiivneKeel = "et";
            aktKeelLabel.Text = "🇪🇪 Eesti";
            UuendaLabels();
        };

        Button ingliseNupp = new Button
        {
            Text = "🇬🇧 English",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 48,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 240
        };
        ingliseNupp.Clicked += (s, e) =>
        {
            AppState.ValiKeel("en");
            viewModel.AktiivneKeel = "en";
            aktKeelLabel.Text = "🇬🇧 English";
            UuendaLabels();
        };

        teemaLabel = new Label
        {
            Text = "🎨 Teema",
            FontSize = 16,
            FontFamily = "Lufio",
            TextColor = Color.FromArgb("#aaaacc"),
            HorizontalOptions = LayoutOptions.Center
        };

        Button tumeNupp = new Button
        {
            Text = "🌙 " + AppResources.TumeTeema,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 48,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 240
        };
        tumeNupp.Clicked += (s, e) =>
        {
            themeService.RakendaTumeTeema();
            viewModel.OnTumeTeema = true;
            AppState.ValiTeema(true);
            UuendaTeemaVisuaal(true);
        };

        Button heleNupp = new Button
        {
            Text = "☀️ " + AppResources.HeleTeema,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 48,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 240
        };
        heleNupp.Clicked += (s, e) =>
        {
            themeService.RakendaHeleTeema();
            viewModel.OnTumeTeema = false;
            AppState.ValiTeema(false);
            UuendaTeemaVisuaal(false);
        };

        heliLabel = new Label
        {
            Text = "🔊 " + AppResources.HeliSeaded,
            FontSize = 16,
            FontFamily = "Lufio",
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center
        };

        heliSwitch = new Switch
        {
            IsToggled = viewModel.HeliSees,
            OnColor = Color.FromArgb("#4444aa"),
            ThumbColor = Colors.White
        };
        heliSwitch.Toggled += (s, e) => viewModel.HeliSees = e.Value;

        HorizontalStackLayout heliRida = new HorizontalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.Center,
            Children = { heliLabel, heliSwitch }
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 16,
            Children =
            {
                pealkiriLabel,
                new BoxView { HeightRequest = 1, BackgroundColor = Color.FromArgb("#302b63"), HorizontalOptions = LayoutOptions.Fill },
                keelLabel,
                aktKeelLabel,
                eestiNupp,
                ingliseNupp,
                new BoxView { HeightRequest = 1, BackgroundColor = Color.FromArgb("#302b63"), HorizontalOptions = LayoutOptions.Fill },
                teemaLabel,
                tumeNupp,
                heleNupp,
                new BoxView { HeightRequest = 1, BackgroundColor = Color.FromArgb("#302b63"), HorizontalOptions = LayoutOptions.Fill },
                heliRida
            }
        };

        Content = new ScrollView { Content = vsl };

        AppState.KeelMuutus += (keel) => UuendaLabels();
    }

    private void UuendaLabels()
    {
        pealkiriLabel.Text = AppResources.SeadedPealkiri;
        keelLabel.Text = AppResources.AktiivneKeel;
        heliLabel.Text = "🔊 " + AppResources.HeliSeaded;
    }

    private void UuendaTeemaVisuaal(bool tumeTeema)
    {
        BackgroundColor = tumeTeema ? Color.FromArgb("#1a1a2e") : Colors.WhiteSmoke;
        pealkiriLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;
        aktKeelLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;
        heliLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;
    }
}