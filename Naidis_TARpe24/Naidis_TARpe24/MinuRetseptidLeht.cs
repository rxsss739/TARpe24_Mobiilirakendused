using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24;

public class MinuRetseptidLeht : ContentPage
{
    ListView listView;
    Label tyhiLabel;
    VerticalStackLayout vsl;

    public MinuRetseptidLeht()
    {
        Title = "Minu retseptid";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        Label pealkiriLabel = new Label
        {
            Text = "Minu retseptid",
            FontSize = 24,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        tyhiLabel = new Label
        {
            Text = "Retsepte pole veel lisatud.",
            FontSize = 15,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Color.FromArgb("#aaaacc"),
            IsVisible = false
        };

        listView = new ListView
        {
            IsGroupingEnabled = true,
            BackgroundColor = Colors.Transparent,
            SeparatorColor = Color.FromArgb("#302b63"),
            HasUnevenRows = true,

            GroupHeaderTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();

                var header = new Frame
                {
                    BackgroundColor = Color.FromArgb("#0f3460"),
                    Padding = new Thickness(16, 8),
                    CornerRadius = 0,
                    HasShadow = false
                };

                var headerLabel = new Label
                {
                    FontSize = 16,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White,
                    VerticalOptions = LayoutOptions.Center
                };
                headerLabel.SetBinding(Label.TextProperty, "Nimetus");

                header.Content = headerLabel;
                cell.View = header;
                return cell;
            }),

            ItemTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();

                var grid = new Grid
                {
                    Padding = new Thickness(10),
                    ColumnSpacing = 10,
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
                pilt.SetBinding(Image.SourceProperty, "PildiLink");

                var nimiLabel = new Label
                {
                    FontSize = 16,
                    FontFamily = "Lufio",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White,
                    VerticalOptions = LayoutOptions.Center
                };
                nimiLabel.SetBinding(Label.TextProperty, "Nimi");

                var textStack = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = { nimiLabel }
                };

                grid.Add(pilt, 0, 0);
                grid.Add(textStack, 1, 0);

                var kustutaAction = new MenuItem
                {
                    Text = "Kustuta",
                    IsDestructive = true
                };
                kustutaAction.Clicked += KustutaRetsept_Clicked;
                cell.ContextActions.Add(kustutaAction);

                cell.View = grid;
                return cell;
            })
        };

        listView.ItemTapped += ListViewItemTapped;

        vsl = new VerticalStackLayout
        {
            Padding = new Thickness(0, 20, 0, 0),
            Spacing = 10,
            Children = { pealkiriLabel, tyhiLabel, listView }
        };

        Content = new ScrollView { Content = vsl };
    }

    public void LaeRetseptid()
    {
        var koikRetseptid = FailiHaldur.LoeRetseptid();

        if (koikRetseptid.Count == 0)
        {
            tyhiLabel.IsVisible = true;
            listView.IsVisible = false;
            return;
        }

        tyhiLabel.IsVisible = false;
        listView.IsVisible = true;

        var grupid = koikRetseptid
            .GroupBy(r => r.Kategooria)
            .Select(g => new RetseptiKategooria(g.Key, g))
            .ToList();

        listView.ItemsSource = grupid;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LaeRetseptid();
    }

    private async void ListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not Retsept retsept) return;
        listView.SelectedItem = null;

        await DisplayAlertAsync(retsept.Nimi,
            $"Kategooria: {retsept.Kategooria}\n\nPildi link:\n{retsept.PildiLink}",
            "Sulge");
    }

    private async void KustutaRetsept_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        if (menuItem?.BindingContext is not Retsept retsept) return;

        bool kindel = await DisplayAlertAsync("Kustuta", $"Kas oled kindel, et soovid kustutada \"{retsept.Nimi}\"?", "Kustuta", "Tühista");
        if (!kindel) return;

        var koikRetseptid = FailiHaldur.LoeRetseptid();
        koikRetseptid.RemoveAll(r => r.Nimi == retsept.Nimi && r.Kategooria == retsept.Kategooria);
        FailiHaldur.SalvestaRetseptid(koikRetseptid);

        LaeRetseptid();
    }
}
