using System.Globalization;

namespace Naidis_TARpe24;

public class LemmikudLeht : ContentPage
{
    LemmikudViewModel viewModel;
    ListView listView;
    Label pealkiriLabel;
    Label tyhiLabel;
    VerticalStackLayout vsl;

    public LemmikudLeht(LemmikudViewModel vm)
    {
        viewModel = vm;
        Title = "⭐ Lemmikud";

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
            Text = AppResources.LemmikudPealkiri,
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        tyhiLabel = new Label
        {
            Text = AppResources.LemmikudTyhi,
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
            SeparatorColor = Color.FromArgb("#302b63"),
            HasUnevenRows = true,
            ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();

                var grid = new Grid
                {
                    Padding = new Thickness(10),
                    ColumnSpacing = 12,
                    BackgroundColor = Color.FromArgb("#16213e"),
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = 80 },
                        new ColumnDefinition { Width = GridLength.Star }
                    }
                };

                var pilt = new Image
                {
                    WidthRequest = 70,
                    HeightRequest = 70,
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

                var katLabel = new Label
                {
                    FontSize = 13,
                    FontFamily = "Lufio",
                    TextColor = Color.FromArgb("#aaaacc")
                };
                katLabel.SetBinding(Label.TextProperty, "Kategooria");

                var aadressLabel = new Label
                {
                    FontSize = 12,
                    FontFamily = "Lufio",
                    TextColor = Color.FromArgb("#7777aa")
                };
                aadressLabel.SetBinding(Label.TextProperty, "Aadress");

                var textStack = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 3,
                    Children = { nimiLabel, katLabel, aadressLabel }
                };

                grid.Add(pilt, 0, 0);
                grid.Add(textStack, 1, 0);

                var kustutaAction = new MenuItem
                {
                    Text = "🗑️ Kustuta",
                    IsDestructive = true
                };
                kustutaAction.Clicked += KustutaLemmik_Clicked;
                cell.ContextActions.Add(kustutaAction);

                cell.View = grid;
                return cell;
            })
        };

        listView.ItemTapped += LemmikTapped;

        vsl = new VerticalStackLayout
        {
            Padding = new Thickness(0, 20, 0, 0),
            Spacing = 10,
            Children = { pealkiriLabel, tyhiLabel, listView }
        };

        Content = new ScrollView { Content = vsl };

        AppState.KeelMuutus += (keel) =>
        {
            AppResources.Culture = new CultureInfo(keel switch { "en" => "en-US", "ru" => "ru", _ => "et" });
            pealkiriLabel.Text = AppResources.LemmikudPealkiri;
            tyhiLabel.Text = AppResources.LemmikudTyhi;
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.LaeLemmikud();
        listView.ItemsSource = viewModel.Lemmikud;
        tyhiLabel.IsVisible = viewModel.OnTyhi;
        listView.IsVisible = !viewModel.OnTyhi;
    }

    private async void LemmikTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Lemmik lemmik) return;
        listView.SelectedItem = null;

        await DisplayAlertAsync(lemmik.Nimi,
            $"{lemmik.Kirjeldus}\n\n📍 {lemmik.Aadress}",
            AppResources.Sulge);
    }

    private async void KustutaLemmik_Clicked(object sender, EventArgs e)
    {
        var item = sender as MenuItem;
        if (item?.BindingContext is not Lemmik lemmik) return;

        bool kindel = await DisplayAlertAsync("🗑️ Kustuta",
            $"\"{lemmik.Nimi}\"",
            AppResources.Eemalda, AppResources.Tyhista);
        if (!kindel) return;

        viewModel.KustutaLemmik(lemmik);
        listView.ItemsSource = null;
        listView.ItemsSource = viewModel.Lemmikud;
        tyhiLabel.IsVisible = viewModel.OnTyhi;
        listView.IsVisible = !viewModel.OnTyhi;
    }
}