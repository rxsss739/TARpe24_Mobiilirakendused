namespace Naidis_TARpe24;

public class PopUpPage : ContentPage
{
    Label lbl;
    VerticalStackLayout vsl;
    FlexLayout buttonLayout;

    Dictionary<string, string> sonastik = new Dictionary<string, string>
    {
        { "Koer", "peamiselt hundist põlvnev koduloom, keda peetakse majavalvurina ja lemmikloomana" },
        { "Kass", "metskassist põlvnev pehme karvaga koduloom" },
        { "Maja", "hoone inimestele elamiseks, töötamiseks, nende teenindamiseks vms" },
        { "Kool", "asutus, kus õpilased õpetaja juhtimisel õpivad" },
        { "Laud", "palgist lõigatud pikk ja suhteliselt õhuke puitmaterjal hrl millegi ehitamiseks (nt põrandalaud, voodrilaud)" },
        { "Tool", "hrl nelja jalaga ja seljatoega iste ühe inimese jaoks" },
        { "Monitor", "ekraaniga varustatud seade, lauaarvuti osa" },
        { "Graafikakaart", "Protsess, mille käigus leitakse ja parandatakse programmis vigu." }
    };

    public PopUpPage()
    {
        lbl = new Label
        {
            Text = "Vali sõna et näha selgitust",
            FontSize = 18,
            HorizontalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        buttonLayout = new FlexLayout
        {
            Wrap = Microsoft.Maui.Layouts.FlexWrap.Wrap,
            JustifyContent = Microsoft.Maui.Layouts.FlexJustify.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        foreach (var sonajaselgitus in sonastik)
        {
            string sona = sonajaselgitus.Key;
            string selgitus = sonajaselgitus.Value;

            Button nupp = new Button
            {
                Text = sona,
                FontSize = 16,
                BackgroundColor = Colors.SteelBlue,
                TextColor = Colors.White,
                CornerRadius = 10,
                Margin = new Thickness(5),
                Padding = new Thickness(14, 8)
            };
            nupp.Clicked += async (sender, e) =>
            {
                await DisplayAlertAsync(sona, selgitus, "Sulge");
            };
            buttonLayout.Add(nupp);
        }

        Button nimeNupp = new Button
        {
            Text = "Sisesta enda nimi",
            FontSize = 18,
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            Margin = new Thickness(0, 10, 0, 0)
        };
        nimeNupp.Clicked += async (sender, e) =>
        {
            string nimi = await DisplayPromptAsync("Tere!", "Mis on sinu nimi?", "Kinnita", "Loobu");
            if (!string.IsNullOrWhiteSpace(nimi))
            {
                lbl.Text = "Tere, " + nimi + "! Vali sõna.";
            }
        };

        Button valikNupp = new Button
        {
            Text = "Vali sõna nimekirjast",
            FontSize = 18,
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            Margin = new Thickness(0, 5, 0, 0)
        };
        valikNupp.Clicked += async (sender, e) =>
        {
            string[] voimalused = new string[sonastik.Keys.Count];
            sonastik.Keys.CopyTo(voimalused, 0);
            string valik = await DisplayActionSheetAsync("Vali sõna", "Loobu", null, voimalused);
            if (valik != null && valik != "Loobu")
            {
                await DisplayAlertAsync(valik, sonastik[valik], "Sulge");
            }
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { lbl, buttonLayout, nimeNupp, valikNupp }
        };

        Content = new ScrollView { Content = vsl };
    }
}