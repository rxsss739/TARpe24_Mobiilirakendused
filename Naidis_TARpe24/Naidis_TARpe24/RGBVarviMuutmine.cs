using Microsoft.Maui.Layouts;

namespace Naidis_TARpe24;

public class RGBVarviMuutmine : ContentPage
{
	BoxView bv;
    BoxView redBox;
    BoxView greenBox;
    BoxView blueBox;
    Slider redSlider;
	Slider greenSlider;
	Slider blueSlider;
    Stepper stepper;
	Label redLabel;
	Label greenLabel;
	Label blueLabel;
    Button nupp;
	AbsoluteLayout al;

	public RGBVarviMuutmine()
	{
        bv = new BoxView
        {
            BackgroundColor = Color.FromRgb(128, 128, 128),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
        };
        redBox = new BoxView
        {
            WidthRequest = 60,
            HeightRequest = 60,
            Color = Color.FromRgb(128, 0, 0)
        };

        greenBox = new BoxView
        {
            WidthRequest = 60,
            HeightRequest = 60,
            Color = Color.FromRgb(0, 128, 0)
        };

        blueBox = new BoxView
        {
            WidthRequest = 60,
            HeightRequest = 60,
            Color = Color.FromRgb(0, 0, 128)
        };

        HorizontalStackLayout colorBoxesLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center,
            Children = { redBox, greenBox, blueBox }
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

        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 90,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            Increment = 5
        };
        stepper.ValueChanged += Stepper_ValueChanged;
        
        nupp = new Button
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

        al = new AbsoluteLayout { Children = { bv, colorBoxesLayout, redLabel, redSlider, greenLabel, greenSlider, blueLabel, blueSlider, stepper, nupp } };

        AbsoluteLayout.SetLayoutBounds(bv, new Rect(0.5, 0.05, 200, 200));
        AbsoluteLayout.SetLayoutFlags(bv, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(colorBoxesLayout, new Rect(0.26, 0.35, 0.8, 100));
        AbsoluteLayout.SetLayoutFlags(colorBoxesLayout, AbsoluteLayoutFlags.PositionProportional);

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

        AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.5, 1.02, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(stepper, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        AbsoluteLayout.SetLayoutBounds(nupp, new Rect(0.5, 1.05, 0.8, 0.15));
        AbsoluteLayout.SetLayoutFlags(nupp, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.All);

        Content = al;
    }

    private void Stepper_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        bv.CornerRadius = (float)e.NewValue;
    }

    private void Slider_ValueChanged(object? sender, ValueChangedEventArgs args)
	{
        int red = (int)redSlider.Value;
        int green = (int)greenSlider.Value;
        int blue = (int)blueSlider.Value;

        redLabel.Text = $"Red = {red}";
        greenLabel.Text = $"Green = {green}";
        blueLabel.Text = $"Blue = {blue}";

        redBox.Color = Color.FromRgb(red, 0, 0);
        greenBox.Color = Color.FromRgb(0, green, 0);
        blueBox.Color = Color.FromRgb(0, 0, blue);

        bv.Color = Color.FromRgb(red, green, blue);
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