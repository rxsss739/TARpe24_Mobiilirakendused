namespace Naidis_TARpe24;

public class RGBVarviMuutmine : ContentPage
{
	BoxView bv;
	Slider redSlider;
	Slider greenSlider;
	Slider blueSlider;
	Label redLabel;
	Label greenLabel;
	Label blueLabel;
	AbsoluteLayout al;

	public RGBVarviMuutmine()
	{
        bv = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 9999999,
            HeightRequest = 400,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
        };
        redSlider = new Slider
		{
			Minimum = 1,
			Maximum = 255,
			Value = 128,
			HorizontalOptions = LayoutOptions.Center,
			WidthRequest = 300
		};
		redLabel = new Label
		{
			Text = $"Red = {redSlider.Value}",
            FontSize = 18,
            FontFamily = "Lufilo",
            TextColor = Colors.Red,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
		redSlider.ValueChanged += Slider_ValueChanged;
		greenSlider = new Slider
		{
			Minimum = 1,
			Maximum = 255,
			Value = 128,
			HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };
        greenLabel = new Label
        {
            Text = $"Green = {greenSlider.Value}",
            FontSize = 18,
            FontFamily = "Lufilo",
            TextColor = Colors.Green,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        greenSlider.ValueChanged += Slider_ValueChanged;
        blueSlider = new Slider
		{
			Minimum = 1,
			Maximum = 255,
			Value = 128,
			HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };
        blueLabel = new Label
        {
            Text = $"Blue = {blueSlider.Value}",
            FontSize = 18,
            FontFamily = "Lufilo",
            TextColor = Colors.Blue,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        blueSlider.ValueChanged += Slider_ValueChanged;

        Button nupp = new Button
        {
            Text = "Genereeri värv",
            FontSize = 18,
            FontFamily = "Luffio",
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 60,
        };
        nupp.Clicked += (sender, e) =>
        {
            GenerateRandomColor();
        };

        al = new AbsoluteLayout { Children = { bv, redLabel, redSlider, greenLabel, greenSlider, blueLabel, blueSlider, nupp} };

		AbsoluteLayout.SetLayoutBounds(bv, new Rect(0, 0, 1, 0.4));
		AbsoluteLayout.SetLayoutFlags(bv, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(redLabel, new Rect(0.5, 0.52, 0.8, 0.1));
        AbsoluteLayout.SetLayoutFlags(redLabel, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(redSlider, new Rect(0.5, 0.57, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(redSlider, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(greenLabel, new Rect(0.5, 0.69, 0.8, 0.1));
        AbsoluteLayout.SetLayoutFlags(greenLabel, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(greenSlider, new Rect(0.5, 0.74, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(greenSlider, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(blueLabel, new Rect(0.5, 0.86, 0.8, 0.1));
        AbsoluteLayout.SetLayoutFlags(blueLabel, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(blueSlider, new Rect(0.5, 0.91, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(blueSlider, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(nupp, new Rect(0.5, 0.96, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(nupp, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        Content = al;
    }

	private void Slider_ValueChanged(object? sender, ValueChangedEventArgs args)
	{
        if (sender == redSlider)
        {
            redLabel.Text = String.Format("Red = {0:X2}", (int)args.NewValue);
        }
        if (sender == greenSlider)
        {
            greenLabel.Text = String.Format("Green = {0:X2}", (int)args.NewValue);
        }
        if (sender == blueSlider)
        {
            blueLabel.Text = String.Format("Blue = {0:X2}", (int)args.NewValue);
        }

        bv.Color = Color.FromRgb(
			(int)redSlider.Value,
			(int)greenSlider.Value,
			(int)blueSlider.Value
		);
	}

    private void GenerateRandomColor()
    {
        Random rnd = new Random();
        int red = rnd.Next(255);
        int green = rnd.Next(255);
        int blue = rnd.Next(255);

        bv.Color = Color.FromRgb(red, green, blue);
        redSlider.Value = red;
        greenSlider.Value = green;
        blueSlider.Value = blue;
    }
}