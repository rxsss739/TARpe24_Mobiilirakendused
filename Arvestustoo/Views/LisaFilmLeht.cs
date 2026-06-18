using Arvestustoo.Models;
using Arvestustoo.Resources;
using Arvestustoo.Services;
using Arvestustoo.ViewModels;

namespace Arvestustoo.Views;

public class LisaFilmLeht : ContentPage
{
    LisaFilmViewModel viewModel;
    FilmidViewModel filmidVM;
    DatabaseService db;
    HeliService heli;
    Entry nimiEntry;
    Entry zanrEntry;
    Entry pildiUrlEntry;
    Entry markusedEntry;
    Label pealkiriLabel;
    Label hinneLabel;
    Label[] tahedLabels;
    Label statusLabel;
    Button lisaNupp;
    bool onTumeTeema = true;
    VerticalStackLayout vsl;

    public LisaFilmLeht(LisaFilmViewModel vm, FilmidViewModel filmidVm, DatabaseService databaseService, HeliService heliService)
    {
        viewModel = vm;
        filmidVM = filmidVm;
        db = databaseService;
        heli = heliService;
        Title = "➕";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        pealkiriLabel = new Label
        {
            Text = AppResources.LisaFilmPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        nimiEntry = new Entry
        {
            Placeholder = AppResources.FilmiNimi,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc")
        };

        zanrEntry = new Entry
        {
            Placeholder = AppResources.FilmiZanr,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc")
        };

        pildiUrlEntry = new Entry
        {
            Placeholder = AppResources.FilmiPildiUrl,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc"),
            Keyboard = Keyboard.Url
        };

        markusedEntry = new Entry
        {
            Placeholder = AppResources.FilmiMarkused,
            FontSize = 16,
            FontFamily = "Lufio",
            BackgroundColor = Color.FromArgb("#16213e"),
            TextColor = Colors.White,
            PlaceholderColor = Color.FromArgb("#aaaacc")
        };

        hinneLabel = new Label
        {
            Text = $"{AppResources.HinneLabel}: {viewModel.HinneTekst}",
            FontSize = 16,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        // Tähe reiting — Tap to Place
        tahedLabels = new Label[5];
        var tahedeRida = new HorizontalStackLayout
        {
            Spacing = 6,
            HorizontalOptions = LayoutOptions.Center
        };

        for (int i = 0; i < 5; i++)
        {
            int hinneVäärtus = i + 1;
            var taht = new Label
            {
                Text = hinneVäärtus <= viewModel.Hinne ? "⭐" : "☆",
                FontSize = 36,
                HorizontalOptions = LayoutOptions.Center
            };

            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => SeaHinne(hinneVäärtus);
            taht.GestureRecognizers.Add(tap);

            tahedLabels[i] = taht;
            tahedeRida.Add(taht);
        }

        // Drag (lohistamine üle tähtede)
        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += (s, e) =>
        {
            if (e.StatusType != GestureStatus.Running) return;
            double lahus = tahedeRida.Width / 5;
            int hinne = (int)Math.Ceiling((e.TotalX + tahedeRida.Width / 2) / lahus);
            hinne = Math.Clamp(hinne, 1, 5);
            SeaHinne(hinne);
        };
        tahedeRida.GestureRecognizers.Add(panGesture);

        statusLabel = new Label
        {
            Text = "",
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.SeaGreen
        };

        lisaNupp = new Button
        {
            Text = AppResources.LisaNupp,
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        lisaNupp.Clicked += LisaNupp_Clicked;

        Button tyhjendaNupp = new Button
        {
            Text = AppResources.TyhjendaNupp,
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.SlateGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        tyhjendaNupp.Clicked += (s, e) =>
        {
            viewModel.Tyhjenda();
            SyncValjadest();
            statusLabel.Text = "";
            lisaNupp.Text = AppResources.LisaNupp;
            pealkiriLabel.Text = AppResources.LisaFilmPealkiri;
        };

        HorizontalStackLayout nuppudeRida = new HorizontalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lisaNupp, tyhjendaNupp }
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 14,
            Children =
            {
                pealkiriLabel,
                new Label { Text = AppResources.FilmiNimi, FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                nimiEntry,
                new Label { Text = AppResources.FilmiZanr, FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                zanrEntry,
                new Label { Text = AppResources.FilmiPildiUrl, FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                pildiUrlEntry,
                new Label { Text = AppResources.FilmiMarkused, FontSize = 13, FontFamily = "Lufio", TextColor = Color.FromArgb("#aaaacc") },
                markusedEntry,
                hinneLabel,
                tahedeRida,
                statusLabel,
                nuppudeRida
            }
        };

        Content = new ScrollView { Content = vsl };

        AppState.KeelMuutus += (keel) =>
        {
            pealkiriLabel.Text = AppResources.LisaFilmPealkiri;
            lisaNupp.Text = viewModel.OnMuutmine ? AppResources.SalvestaNupp : AppResources.LisaNupp;
        };

        AppState.TeemaMuutus += RakendaTeema;
    }

    private void SeaHinne(int hinne)
    {
        viewModel.Hinne = hinne;
        hinneLabel.Text = $"{AppResources.HinneLabel}: {viewModel.HinneTekst}";
        for (int i = 0; i < 5; i++)
            tahedLabels[i].Text = (i + 1) <= hinne ? "⭐" : "☆";
    }

    private void SyncValjadest()
    {
        nimiEntry.Text = viewModel.Nimi;
        zanrEntry.Text = viewModel.Zanr;
        pildiUrlEntry.Text = viewModel.PildiUrl;
        markusedEntry.Text = viewModel.Markused;
        SeaHinne(viewModel.Hinne);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SyncValjadest();

        if (viewModel.OnMuutmine)
        {
            lisaNupp.Text = AppResources.SalvestaNupp;
            pealkiriLabel.Text = $"{AppResources.MuutmineRezim}: {viewModel.Nimi}";
        }
        else
        {
            lisaNupp.Text = AppResources.LisaNupp;
            pealkiriLabel.Text = AppResources.LisaFilmPealkiri;
        }
    }

    private async void LisaNupp_Clicked(object sender, EventArgs e)
    {
        string nimi = nimiEntry.Text?.Trim();
        string zanr = zanrEntry.Text?.Trim();

        if (string.IsNullOrEmpty(nimi) || string.IsNullOrEmpty(zanr))
        {
            await DisplayAlertAsync("Viga", "Error", "OK");
            return;
        }

        viewModel.Nimi = nimi;
        viewModel.Zanr = zanr;
        viewModel.PildiUrl = pildiUrlEntry.Text?.Trim() ?? "";
        viewModel.Markused = markusedEntry.Text?.Trim() ?? "";

        if (viewModel.OnMuutmine)
        {
            viewModel.ValitudFilm.Nimi = viewModel.Nimi;
            viewModel.ValitudFilm.Zanr = viewModel.Zanr;
            viewModel.ValitudFilm.Hinne = viewModel.Hinne;
            viewModel.ValitudFilm.PildiUrl = viewModel.PildiUrl;
            viewModel.ValitudFilm.Markused = viewModel.Markused;
            db.UuendaFilm(viewModel.ValitudFilm);

            statusLabel.Text = $"✓ \"{viewModel.Nimi}\" Edukalt muudetud!";
        }
        else
        {
            db.LisaFilm(new Film
            {
                Nimi = viewModel.Nimi,
                Zanr = viewModel.Zanr,
                Hinne = viewModel.Hinne,
                PildiUrl = viewModel.PildiUrl,
                Markused = viewModel.Markused
            });

            statusLabel.Text = $"✓ \"{viewModel.Nimi}\" Edukalt lisatud!";
        }

        statusLabel.TextColor = Colors.SeaGreen;
        await heli.MängiEdukusHeli();

        viewModel.Tyhjenda();
        SyncValjadest();
        lisaNupp.Text = AppResources.LisaNupp;
        pealkiriLabel.Text = AppResources.LisaFilmPealkiri;
    }

    private void RakendaTeema(bool tumeTeema)
    {
        onTumeTeema = tumeTeema;
        BackgroundColor = tumeTeema ? Color.FromArgb("#1a1a2e") : Colors.WhiteSmoke;
        pealkiriLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;
        hinneLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;

        Color sisendTaust = tumeTeema ? Color.FromArgb("#16213e") : Colors.White;
        Color sisendTekst = tumeTeema ? Colors.White : Colors.Black;

        nimiEntry.BackgroundColor = sisendTaust;
        nimiEntry.TextColor = sisendTekst;
        zanrEntry.BackgroundColor = sisendTaust;
        zanrEntry.TextColor = sisendTekst;
        pildiUrlEntry.BackgroundColor = sisendTaust;
        pildiUrlEntry.TextColor = sisendTekst;
        markusedEntry.BackgroundColor = sisendTaust;
        markusedEntry.TextColor = sisendTekst;
    }
}