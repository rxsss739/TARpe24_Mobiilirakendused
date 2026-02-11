using Microsoft.Maui.Controls.Shapes;

namespace Naidis_TARpe24;

public class ValgusfoorPage : ContentPage
{
    Label lbl;
    BoxView boxView1;
    BoxView boxView2;
    BoxView boxView3;
    VerticalStackLayout vsl;
    HorizontalStackLayout hsl;
    bool foorOnSees = false;

	public ValgusfoorPage()
	{

        lbl = new Label
        {
            Text = "Lülita esmalt foor sisse",
            BackgroundColor = Colors.White,
            HorizontalTextAlignment = TextAlignment.Center
        };

        boxView1 = new BoxView
        { 
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 90,
        };

        boxView2 = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 90,
        };

        boxView3 = new BoxView
        {
            Color = Color.FromRgb(128, 128, 128),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 90,
        };

        hsl = new HorizontalStackLayout
        {
            Padding = 20,
            Spacing = 15
        };


        Button sisseNupp = new Button
        {
            Text = "Sisse",
            FontSize = 28,
            FontFamily = "Lufio",
            TextColor = Colors.Gray,
            BackgroundColor = Colors.DarkGray,
            CornerRadius = 10,
            HeightRequest = 50,
        };

        sisseNupp.Clicked += (sender, e) =>
        {
            boxView1.Color = Colors.Red;
            boxView2.Color = Colors.Yellow;
            boxView3.Color = Colors.Green;
            lbl.Text = "Vali valgus";
            foorOnSees = true;
        };

        hsl.Add(sisseNupp);

        Button valjaNupp = new Button
        {
            Text = "Välja",
            FontSize = 28,
            FontFamily = "Lufio",
            TextColor = Colors.Gray,
            BackgroundColor = Colors.DarkGray,
            CornerRadius = 10,
            HeightRequest = 50,
        };

        valjaNupp.Clicked += (sender, e) =>
        {
            boxView1.Color = Colors.Gray;
            boxView2.Color = Colors.Gray;
            boxView3.Color = Colors.Gray;
            lbl.Text = "Lülita esmalt foor sisse";
            foorOnSees = false;
        };

        hsl.Add(valjaNupp);

        TapGestureRecognizer tap = new TapGestureRecognizer();
        boxView1.GestureRecognizers.Add(tap);
        boxView2.GestureRecognizers.Add(tap);
        boxView3.GestureRecognizers.Add(tap);

        tap.Tapped += BoxViewTap;

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { lbl, boxView1, boxView2, boxView3, hsl },
            HorizontalOptions = LayoutOptions.Center
        };

        Content = vsl;
    }


    private void BoxViewTap(object sender, EventArgs e)
    {
        BoxView bv = sender as BoxView;

        if (foorOnSees == false)
        {
            lbl.Text = "Lülita foor sisse";
            return;
        }

        if (bv == boxView1)
        {
            lbl.Text = "Seisa";
        }
        if (bv == boxView2)
        {
            lbl.Text = "Valmista";
        }
        if (bv == boxView3)
        {
            lbl.Text = "Sõida";
        }
    }
}