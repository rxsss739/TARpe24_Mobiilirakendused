namespace Naidis_TARpe24;

public class Lumememm : ContentPage
{
    Picker picker;
    Frame pea, keha, jalad, amber;
    AbsoluteLayout lumememmLayout;
    Label tegevusLabel, kiirusLabel;
    Slider labipaistmatusSlider;
    Stepper kiirusStepper;
    Button button;

    public Lumememm()
    {
        amber = new Frame
        {
            WidthRequest = 40,
            HeightRequest = 25,
            BackgroundColor = Colors.DarkGray,
            CornerRadius = 5,
            HasShadow = false,
            Padding = 0
        };
        pea = new Frame
        {
            WidthRequest = 70,
            HeightRequest = 70,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 35,
            HasShadow = false,
            Padding = 0
        };
        keha = new Frame
        {
            WidthRequest = 100,
            HeightRequest = 100,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 50,
            HasShadow = false,
            Padding = 0
        };
        jalad = new Frame
        {
            WidthRequest = 130,
            HeightRequest = 130,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 65,
            HasShadow = false,
            Padding = 0
        };

        lumememmLayout = new AbsoluteLayout
        {
            HeightRequest = 340,
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center
        };

        AbsoluteLayout.SetLayoutBounds(jalad, new Rect(35, 205, 130, 130));
        AbsoluteLayout.SetLayoutBounds(keha, new Rect(50, 120, 100, 100));
        AbsoluteLayout.SetLayoutBounds(pea, new Rect(65, 55, 70, 70));
        AbsoluteLayout.SetLayoutBounds(amber, new Rect(80, 30, 40, 25));

        lumememmLayout.Children.Add(jalad);
        lumememmLayout.Children.Add(keha);
        lumememmLayout.Children.Add(pea);
        lumememmLayout.Children.Add(amber);

        picker = new Picker
        {
            Title = "Vali tegevus",
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300,
            ItemsSource = new List<string> { "Peida lumememm", "Näita lumememm", "Muuda värvi", "Sulata", "Tantsi" }
        };
        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedItem != null)
                tegevusLabel.Text = $"Valitud: {picker.SelectedItem}";
        };

        button = new Button
        {
            Text = "Käivita tegevus",
            FontSize = 18,
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 60,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };
        button.Clicked += OnButtonClicked;

        tegevusLabel = new Label
        {
            Text = "Tegevus: pole valitud",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center
        };

        labipaistmatusSlider = new Slider
        {
            Minimum = 0.0,
            Maximum = 1.0,
            Value = 1.0,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };
        labipaistmatusSlider.ValueChanged += (s, e) =>
        {
            pea.Opacity = keha.Opacity = jalad.Opacity = amber.Opacity = e.NewValue;
        };

        kiirusStepper = new Stepper
        {
            Minimum = 1,
            Maximum = 10,
            Value = 5,
            Increment = 1,
            HorizontalOptions = LayoutOptions.Center
        };
        kiirusLabel = new Label
        {
            Text = "Kiirus: 5",
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center
        };
        kiirusStepper.ValueChanged += (s, e) =>
        {
            kiirusLabel.Text = $"Kiirus: {(int)kiirusStepper.Value}";
        };

        Content = new VerticalStackLayout
        {
            Spacing = 10,
            Padding = new Thickness(10),
            Children =
            {
                new Label
                {
                    Text = "Lumememm",
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center
                },
                lumememmLayout,
                picker,
                button,
                tegevusLabel,
                new Label { Text = "Läbipaistvus:", FontSize = 18, HorizontalOptions = LayoutOptions.Center },
                labipaistmatusSlider,
                new Label { Text = "Kiirus:", FontSize = 18, HorizontalOptions = LayoutOptions.Center },
                kiirusStepper,
                kiirusLabel
            }
        };
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        if (picker.SelectedItem == null)
        {
            await DisplayAlertAsync("Viga", "Vali tegevus!", "OK");
            return;
        }

        int kiirus = (int)(1100 - kiirusStepper.Value * 100);

        switch (picker.SelectedItem.ToString())
        {
            case "Peida lumememm":
                pea.IsVisible = keha.IsVisible = jalad.IsVisible = amber.IsVisible = false;
                tegevusLabel.Text = "Lumememm on peidetud";
                break;
            case "Näita lumememm":
                pea.IsVisible = keha.IsVisible = jalad.IsVisible = amber.IsVisible = true;
                pea.Opacity = keha.Opacity = jalad.Opacity = amber.Opacity = labipaistmatusSlider.Value;
                await Task.WhenAll(pea.ScaleToAsync(1.0, 300), keha.ScaleToAsync(1.0, 300), jalad.ScaleToAsync(1.0, 300));
                tegevusLabel.Text = "Lumememm on nähtav";
                break;
            case "Muuda värvi":
                if (!await DisplayAlertAsync("Muuda värvi", "Kas oled kindel?", "Jah", "Ei")) return;
                Random rnd = new Random();
                pea.BackgroundColor = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                keha.BackgroundColor = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                jalad.BackgroundColor = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                tegevusLabel.Text = "Värvid muudetud";
                break;
            case "Sulata":
                tegevusLabel.Text = "Lumememm sulab...";
                await Task.WhenAll(
                    pea.ScaleToAsync(0.1, (uint)(kiirus * 3)),
                    keha.ScaleToAsync(0.1, (uint)(kiirus * 3)),
                    jalad.ScaleToAsync(0.1, (uint)(kiirus * 3)),
                    pea.FadeToAsync(0, (uint)(kiirus * 3)),
                    keha.FadeToAsync(0, (uint)(kiirus * 3)),
                    jalad.FadeToAsync(0, (uint)(kiirus * 3))
                );
                pea.IsVisible = keha.IsVisible = jalad.IsVisible = amber.IsVisible = false;
                tegevusLabel.Text = "Lumememm on sulanud";
                break;
            case "Tantsi":
                tegevusLabel.Text = "Lumememm tantsib!";
                for (int i = 0; i < 4; i++)
                {
                    await lumememmLayout.TranslateToAsync(40, 0, (uint)kiirus, Easing.SinInOut);
                    await lumememmLayout.TranslateToAsync(-40, 0, (uint)kiirus, Easing.SinInOut);
                }
                await lumememmLayout.TranslateToAsync(0, 0, (uint)(kiirus / 2));
                tegevusLabel.Text = "Tantsimine lõpetatud";
                break;
        }
    }
}
