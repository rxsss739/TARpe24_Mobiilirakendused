using Arvestustoo.Models;
using Arvestustoo.Resources;
using Arvestustoo.Services;
using Arvestustoo.ViewModels;

namespace Arvestustoo.Views;

public class FilmidLeht : ContentPage
{
    FilmidViewModel viewModel;
    LisaFilmViewModel lisaVM;
    ListView listView;
    Label pealkiriLabel;
    Label tyhiLabel;
    bool onTumeTeema = true;
    VerticalStackLayout vsl;

    public FilmidLeht(FilmidViewModel vm, LisaFilmViewModel lisaFilmVM)
    {
        viewModel = vm;
        lisaVM = lisaFilmVM;
        Title = "🎬";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        pealkiriLabel = new Label
        {
            Text = AppResources.FilmidPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        tyhiLabel = new Label
        {
            Text = AppResources.OnTyhi,
            FontSize = 15,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.FromArgb("#aaaacc"),
            IsVisible = false
        };

        listView = new ListView
        {
            BackgroundColor = Colors.Transparent,
            SeparatorColor = Colors.Transparent,
            HasUnevenRows = true,
            ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();

                var frame = new Frame
                {
                    Padding = new Thickness(12),
                    Margin = new Thickness(8, 4),
                    BackgroundColor = Color.FromArgb("#16213e"),
                    CornerRadius = 12,
                    HasShadow = true,
                    BorderColor = Color.FromArgb("#4444aa")
                };

                var grid = new Grid
                {
                    ColumnSpacing = 10,
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = 75 },
                        new ColumnDefinition { Width = GridLength.Star }
                    }
                };

                var pilt = new Image
                {
                    WidthRequest = 65,
                    HeightRequest = 95,
                    Aspect = Aspect.AspectFill
                };
                pilt.SetBinding(Image.SourceProperty, "PildiUrl");

                var nimiLabel = new Label
                {
                    FontSize = 16,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White
                };
                nimiLabel.SetBinding(Label.TextProperty, "Nimi");

                var zanrLabel = new Label
                {
                    FontSize = 13,
                    FontFamily = "Lufio",
                    TextColor = Color.FromArgb("#aaaacc")
                };
                zanrLabel.SetBinding(Label.TextProperty, "Zanr");

                var hinneLabel = new Label
                {
                    FontSize = 15,
                    FontFamily = "Lufio",
                    TextColor = Color.FromArgb("#ffcc00")
                };
                hinneLabel.SetBinding(Label.TextProperty, "HinneKuvamine");

                var kuupaevLabel = new Label
                {
                    FontSize = 11,
                    FontFamily = "Lufio",
                    TextColor = Color.FromArgb("#7777aa")
                };
                kuupaevLabel.SetBinding(Label.TextProperty, "KuupaevKuvamine");

                var textStack = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 4,
                    Children = { nimiLabel, zanrLabel, hinneLabel, kuupaevLabel }
                };

                grid.Add(pilt, 0, 0);
                grid.Add(textStack, 1, 0);
                frame.Content = grid;

                var kustutaAction = new MenuItem
                {
                    Text = AppResources.KustutaNupp,
                    IsDestructive = true
                };
                kustutaAction.Clicked += KustutaFilm_Clicked;
                cell.ContextActions.Add(kustutaAction);

                var muudaAction = new MenuItem { Text = AppResources.MuudaNupp };
                muudaAction.Clicked += MuudaFilm_Clicked;
                cell.ContextActions.Add(muudaAction);

                cell.View = frame;
                return cell;
            })
        };

        listView.ItemTapped += FilmTapped;

        vsl = new VerticalStackLayout
        {
            Padding = new Thickness(0, 20, 0, 0),
            Spacing = 6,
            Children = { pealkiriLabel, tyhiLabel, listView }
        };

        Content = new ScrollView { Content = vsl };

        AppState.KeelMuutus += (keel) =>
        {
            pealkiriLabel.Text = AppResources.FilmidPealkiri;
            tyhiLabel.Text = AppResources.OnTyhi;
        };

        AppState.TeemaMuutus += RakendaTeema;
    }

    private void RakendaTeema(bool tumeTeema)
    {
        onTumeTeema = tumeTeema;
        BackgroundColor = tumeTeema ? Color.FromArgb("#1a1a2e") : Colors.WhiteSmoke;
        pealkiriLabel.TextColor = tumeTeema ? Colors.White : Colors.Black;
        tyhiLabel.TextColor = tumeTeema ? Color.FromArgb("#aaaacc") : Colors.Gray;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LaeFilmid();
        listView.ItemsSource = null;
        listView.ItemsSource = viewModel.Filmid;
        tyhiLabel.IsVisible = viewModel.OnTyhi;
        listView.IsVisible = !viewModel.OnTyhi;
    }

    private async void FilmTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Film film) return;
        listView.SelectedItem = null;

        await DisplayAlertAsync(film.Nimi,
            $"{film.HinneKuvamine}\n\n{AppResources.ZanrLabel}: {film.Zanr}\n{AppResources.MarkusedLabel}: {film.Markused ?? "-"}\n\n📅 {film.KuupaevKuvamine}", "Tühista");
    }

    private async void KustutaFilm_Clicked(object sender, EventArgs e)
    {
        var item = sender as MenuItem;
        if (item?.BindingContext is not Film film) return;

        bool kindel = await DisplayAlertAsync(AppResources.KustutaNupp,
            $"\"{film.Nimi}\"?", AppResources.KustutaNupp, "Tuhista");
        if (!kindel) return;

        viewModel.KustutaFilm(film);
        listView.ItemsSource = null;
        listView.ItemsSource = viewModel.Filmid;
        tyhiLabel.IsVisible = viewModel.OnTyhi;
        listView.IsVisible = !viewModel.OnTyhi;
    }

    private void MuudaFilm_Clicked(object sender, EventArgs e)
    {
        var item = sender as MenuItem;
        if (item?.BindingContext is not Film film) return;

        lisaVM.ValitudFilm = film;
        if (Parent is TabbedPage tabbedPage)
            tabbedPage.CurrentPage = tabbedPage.Children[1];
    }
}