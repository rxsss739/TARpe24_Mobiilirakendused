using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public class FigurePage : ContentPage
{
	BoxView boxView;
	Ellipse pall;
	Polygon kolmnurk;
	Random rnd = new Random();
	HorizontalStackLayout hsl;
	List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
	VerticalStackLayout vsl;


	public FigurePage()
	{
		int r = rnd.Next(256);
		int g = rnd.Next(256);
		int b = rnd.Next(256);

		boxView = new BoxView
		{
			Color = Color.FromRgb(r, g, b),
			WidthRequest = 200,
			HeightRequest = 200,
			HorizontalOptions = LayoutOptions.Center,
			BackgroundColor = Color.FromRgba(0, 0, 0, 0),
			CornerRadius = 30,
		};
		TapGestureRecognizer tap = new TapGestureRecognizer();
		boxView.GestureRecognizers.Add(tap);
		tap.Tapped += (sender, e) =>
		{
			int r = rnd.Next(256);
			int g = rnd.Next(256);
			int b = rnd.Next(256);
			boxView.Color = Color.FromRgb(r, g, b);
			boxView.WidthRequest = boxView.Width + 20;
			boxView.HeightRequest = boxView.Height + 30;
			if (boxView.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width / 3)
			{
				boxView.WidthRequest = 200;
				boxView.HeightRequest = 200;
			}
		};

		pall = new Ellipse
		{
			WidthRequest = 200,
			HeightRequest = 200,
			Fill = new SolidColorBrush(Color.FromRgb(b, g, r)),
			Stroke = Colors.BurlyWood,
			StrokeThickness = 5,
			HorizontalOptions = LayoutOptions.Center
		};
		pall.GestureRecognizers.Add(tap);
		kolmnurk = new Polygon
		{
			Points = new PointCollection
			{
				new Point(0, 200),
				new Point(100, 0),
				new Point(200, 200)
			},
			Fill = new SolidColorBrush(Color.FromRgb(g, b, r)),
			Stroke = Colors.Aquamarine,
			StrokeThickness = 5,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center
		};
		TapGestureRecognizer tap_kolmnurk = new TapGestureRecognizer();
		tap_kolmnurk.NumberOfTapsRequired = 2;
		kolmnurk.GestureRecognizers.Add(tap_kolmnurk);
		tap_kolmnurk.Tapped += (sender, e) =>
		{
			int r = rnd.Next(256);
			int g = rnd.Next(256);
			int b = rnd.Next(256);

			kolmnurk.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
		};

		hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };
		for (int j = 0; j < nupud.Count; j++)
		{
			Button nupp = new Button
			{
				Text = nupud[j],
				FontSize = 28,
				FontFamily = "Lufio",
				TextColor = Colors.Chocolate,
				BackgroundColor = Colors.Beige,
				CornerRadius = 10,
				HeightRequest = 50,
				ZIndex = j
			};

			hsl.Add(nupp);
			nupp.Clicked += Liikumine;
		}

		vsl = new VerticalStackLayout
		{
			Padding = 20,
			Spacing = 15,
			Children = { boxView, pall, kolmnurk, hsl },
			HorizontalOptions = LayoutOptions.Center
		};

		Content = vsl;
	}


	private void Liikumine(object? sender, EventArgs e)
	{
		Button nupp = sender as Button;
		if (nupp.ZIndex == 0)
		{
			Navigation.PushAsync(new TextPage());
		}
		else if (nupp.ZIndex == 1)
		{
			Navigation.PopToRootAsync();
		}
		else if (nupp.ZIndex == 2)
		{
			Navigation.PushAsync(new FigurePage());
		}
	}
}