namespace Naidis_TARpe24;

public class UusRetseptLeht : ContentPage
{
    Entry nimiEntry;
    Entry kategooriaEntry;
    Entry pildiLinkEntry;
    Label statusLabel;
    VerticalStackLayout vsl;

    public event Action RetseptSalvestatud;

    public UusRetseptLeht()
    {
        Title = "Uus retsept";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        Label pealkiriLabel = new Label
        {
            Text = "Lisa uus retsept",
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        nimiEntry = new Entry
        {
            Placeholder = "Retsepti nimi (nt. Pasta Carbonara)",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc")
        };

        kategooriaEntry = new Entry
        {
            Placeholder = "Kategooria (nt. Pearoad)",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc")
        };

        pildiLinkEntry = new Entry
        {
            Placeholder = "Pildi URL (nt. https://...)",
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc"),
            Keyboard = Keyboard.Url
        };

        statusLabel = new Label
        {
            Text = "",
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.SeaGreen
        };

        Button salvestaNupp = new Button
        {
            Text = "Salvesta retsept",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        salvestaNupp.Clicked += SalvestaNupp_Clicked;

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                pealkiriLabel,
                new Label { Text = "Nimi", FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                nimiEntry,
                new Label { Text = "Kategooria", FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                kategooriaEntry,
                new Label { Text = "Pildi link", FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                pildiLinkEntry,
                statusLabel,
                salvestaNupp
            }
        };

        Content = new ScrollView { Content = vsl };
    }

    private async void SalvestaNupp_Clicked(object sender, EventArgs e)
    {
        string nimi = nimiEntry.Text?.Trim();
        string kategooria = kategooriaEntry.Text?.Trim();
        string pilt = pildiLinkEntry.Text?.Trim();

        if (string.IsNullOrEmpty(nimi) || string.IsNullOrEmpty(kategooria) || string.IsNullOrEmpty(pilt))
        {
            await DisplayAlertAsync("Viga", "Kõik väljad peavad olema täidetud!", "OK");
            return;
        }

        FailiHaldur.LisaRetsept(new Retsept
        {
            Nimi = nimi,
            Kategooria = kategooria,
            PildiLink = pilt
        });

        nimiEntry.Text = "";
        kategooriaEntry.Text = "";
        pildiLinkEntry.Text = "";

        statusLabel.Text = $"✓ \"{nimi}\" salvestatud!";
        statusLabel.TextColor = Colors.SeaGreen;

        RetseptSalvestatud?.Invoke();
    }
}