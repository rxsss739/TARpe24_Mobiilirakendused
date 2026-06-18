using System.Globalization;

namespace Naidis_TARpe24;

public class SeadedLeht : ContentPage
{
    Label pealkiriLabel;
    Label keelValikLabel;
    Label aktKeelLabel;
    VerticalStackLayout vsl;

    public SeadedLeht()
    {
        Title = "⚙️ Seaded";

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
            Text = AppResources.SeadedPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        keelValikLabel = new Label
        {
            Text = AppResources.KeelValik,
            FontSize = 18,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Color.FromArgb("#aaaacc")
        };

        aktKeelLabel = new Label
        {
            Text = $"{AppResources.AktiivneKeel}: 🇪🇪",
            FontSize = 16,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        Button eestiNupp = new Button
        {
            Text = "🇪🇪 Eesti keel",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 260
        };
        eestiNupp.Clicked += (s, e) =>
        {
            AppResources.Culture = new CultureInfo("et");
            AppState.ValiKeel("et");
            pealkiriLabel.Text = AppResources.SeadedPealkiri;
            keelValikLabel.Text = AppResources.KeelValik;
            aktKeelLabel.Text = $"{AppResources.AktiivneKeel}: 🇪🇪";
        };

        Button ingliseNupp = new Button
        {
            Text = "🇬🇧 English",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 260
        };
        ingliseNupp.Clicked += (s, e) =>
        {
            AppResources.Culture = new CultureInfo("en-US");
            AppState.ValiKeel("en");
            pealkiriLabel.Text = AppResources.SeadedPealkiri;
            keelValikLabel.Text = AppResources.KeelValik;
            aktKeelLabel.Text = $"{AppResources.AktiivneKeel}: 🇬🇧";
        };

        Button venekeNupp = new Button
        {
            Text = "🇷🇺 Русский",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            BorderColor = Color.FromArgb("#4444aa"),
            BorderWidth = 1.5,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 260
        };
        venekeNupp.Clicked += (s, e) =>
        {
            AppResources.Culture = new CultureInfo("ru");
            AppState.ValiKeel("ru");
            pealkiriLabel.Text = AppResources.SeadedPealkiri;
            keelValikLabel.Text = AppResources.KeelValik;
            aktKeelLabel.Text = $"{AppResources.AktiivneKeel}: 🇷🇺";
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { pealkiriLabel, keelValikLabel, aktKeelLabel, eestiNupp, ingliseNupp, venekeNupp }
        };

        Content = new ScrollView { Content = vsl };
    }
}