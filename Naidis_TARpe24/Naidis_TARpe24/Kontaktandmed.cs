using System.Text.Json;

namespace Naidis_TARpe24;

public class Kontaktandmed : ContentPage
{
    Entry nimeEntry;
    Entry telefonEntry;
    Entry emailEntry;
    Entry kirjeldusEntry;
    Entry sonumEntry;
    Image fotoImage;
    Label statusLabel;

    readonly List<string> tervitused = new()
    {
        "Palju onne sünnipaevaks",
        "Haid puhi!",
        "Palju onne!",
        "Head uut aastat!"
    };

    public Kontaktandmed()
    {
        Title = "Sobra kontaktandmed";

        fotoImage = new Image
        {
            Source = "dotnet_bot.png",
            HeightRequest = 100,
            WidthRequest = 100,
            Aspect = Aspect.AspectFill,
            HorizontalOptions = LayoutOptions.Center
        };

        var fotoTap = new TapGestureRecognizer();
        fotoTap.Tapped += OnFotoTapped;
        fotoImage.GestureRecognizers.Add(fotoTap);

        var fotoRaam = new Frame
        {
            Content = fotoImage,
            CornerRadius = 50,
            HeightRequest = 100,
            WidthRequest = 100,
            IsClippedToBounds = true,
            Padding = 0,
            HorizontalOptions = LayoutOptions.Center,
            BorderColor = Colors.LightGray,
            HasShadow = false
        };

        var fotoHint = new Label
        {
            Text = "Puuduta foto muutmiseks",
            FontSize = 13,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Gray
        };

        nimeEntry = new Entry
        {
            Placeholder = "Nimi",
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };

        telefonEntry = new Entry
        {
            Placeholder = "Telefon",
            FontSize = 16,
            Keyboard = Keyboard.Telephone,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };

        emailEntry = new Entry
        {
            Placeholder = "Email",
            FontSize = 16,
            Keyboard = Keyboard.Email,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };

        kirjeldusEntry = new Entry
        {
            Placeholder = "Kirjeldus",
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };

        sonumEntry = new Entry
        {
            Placeholder = "Viimane sonum",
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };

        statusLabel = new Label
        {
            Text = "",
            FontSize = 14,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Gray
        };

        Button helistaBtn = new Button
        {
            Text = "Helista",
            FontSize = 16,
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        helistaBtn.Clicked += OnHelistaTapped;

        Button smsBtn = new Button
        {
            Text = "Saada SMS",
            FontSize = 16,
            BackgroundColor = Colors.DodgerBlue,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        smsBtn.Clicked += OnSmsTapped;

        Button emailBtn = new Button
        {
            Text = "Saada Email",
            FontSize = 16,
            BackgroundColor = Colors.Orange,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        emailBtn.Clicked += OnEmailTapped;

        Grid kontaktGrid = new Grid
        {
            ColumnSpacing = 8,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            }
        };
        kontaktGrid.Add(helistaBtn, 0, 0);
        kontaktGrid.Add(smsBtn, 1, 0);
        kontaktGrid.Add(emailBtn, 2, 0);

        Button tervitusBtn = new Button
        {
            Text = "Saada juhuslik tervitus",
            FontSize = 16,
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 300
        };
        tervitusBtn.Clicked += OnTervitusTapped;

        Button salvestaBtn = new Button
        {
            Text = "Salvesta",
            FontSize = 16,
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        salvestaBtn.Clicked += OnSalvestaTapped;

        Button laadiBtn = new Button
        {
            Text = "Laadi",
            FontSize = 16,
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 50
        };
        laadiBtn.Clicked += OnLaadiTapped;

        HorizontalStackLayout allNupud = new HorizontalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.Center,
            Children = { salvestaBtn, laadiBtn }
        };

        Label kontaktandmed = new Label
        {
            Text = "Sobra kontaktandmed",
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 12,
                Children =
                {
                    kontaktandmed,
                    fotoRaam,
                    fotoHint,
                    nimeEntry,
                    telefonEntry,
                    emailEntry,
                    kirjeldusEntry,
                    sonumEntry,
                    statusLabel,
                    kontaktGrid,
                    tervitusBtn,
                    allNupud
                }
            }
        };
    }

    private async void OnFotoTapped(object? sender, EventArgs e)
    {
        string valik = await DisplayActionSheetAsync("Vali foto allikas", "Tuhista", null, "Kaamera", "Galerii");

        if (valik == "Kaamera")
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var foto = await MediaPicker.Default.CapturePhotoAsync();
                if (foto != null)
                    fotoImage.Source = ImageSource.FromFile(foto.FullPath);
            }
            else
            {
                await DisplayAlertAsync("Viga", "Kaamera ei ole selles seadmes toetatud.", "OK");
            }
        }
        else if (valik == "Galerii")
        {
            var foto = await MediaPicker.Default.PickPhotoAsync();
            if (foto != null)
                fotoImage.Source = ImageSource.FromFile(foto.FullPath);
        }
    }

    private async void OnHelistaTapped(object? sender, EventArgs e)
    {
        string telefon = telefonEntry.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(telefon))
        {
            await DisplayAlertAsync("Viga", "Sisesta telefoninumber.", "OK");
            return;
        }

        try
        {
            PhoneDialer.Default.Open(telefon);
        }
        catch
        {
            await DisplayAlertAsync("Viga", "Helistamine ei ole selles seadmes toetatud.", "OK");
        }
    }

    private async void OnSmsTapped(object? sender, EventArgs e)
    {
        string telefon = telefonEntry.Text?.Trim() ?? "";
        string sonum = sonumEntry.Text?.Trim() ?? "Tere!";

        if (string.IsNullOrEmpty(telefon))
        {
            await DisplayAlertAsync("Viga", "Sisesta telefoninumber.", "OK");
            return;
        }

        if (Sms.Default.IsComposeSupported)
        {
            var sms = new SmsMessage(sonum, new[] { telefon });
            await Sms.Default.ComposeAsync(sms);
        }
        else
        {
            await DisplayAlertAsync("Viga", "SMS saatmine ei ole selles seadmes toetatud.", "OK");
        }
    }

    private async void OnEmailTapped(object? sender, EventArgs e)
    {
        string email = emailEntry.Text?.Trim() ?? "";
        string sonum = sonumEntry.Text?.Trim() ?? "";
        string nimi = nimeEntry.Text?.Trim() ?? "Sober";

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlertAsync("Viga", "Sisesta emaili aadress.", "OK");
            return;
        }

        if (Email.Default.IsComposeSupported)
        {
            var kiri = new EmailMessage
            {
                Subject = $"Tere, {nimi}!",
                Body = sonum,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string> { email }
            };
            await Email.Default.ComposeAsync(kiri);
        }
        else
        {
            await DisplayAlertAsync("Viga", "E-maili saatmine ei ole selles seadmes toetatud.", "OK");
        }
    }

    private async void OnTervitusTapped(object? sender, EventArgs e)
    {
        string email = emailEntry.Text?.Trim() ?? "";
        string telefon = telefonEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(telefon))
        {
            await DisplayAlertAsync("Viga", "Sisesta email voi telefoninumber.", "OK");
            return;
        }

        var rng = new Random();
        string tervitus = tervitused[rng.Next(tervitused.Count)];

        string viis = await DisplayActionSheetAsync(
            $"Saada: \"{tervitus}\"",
            "Tuhista", null,
            string.IsNullOrEmpty(telefon) ? null : "SMS",
            string.IsNullOrEmpty(email) ? null : "Email"
        );

        if (viis == "SMS" && Sms.Default.IsComposeSupported)
        {
            var sms = new SmsMessage(tervitus, new[] { telefon });
            await Sms.Default.ComposeAsync(sms);
        }
        else if (viis == "Email" && Email.Default.IsComposeSupported)
        {
            var kiri = new EmailMessage
            {
                Subject = "Tervitus sulle!",
                Body = tervitus,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string> { email }
            };
            await Email.Default.ComposeAsync(kiri);
        }
    }

    private async void OnSalvestaTapped(object? sender, EventArgs e)
    {
        var andmed = new KontaktMudel
        {
            Nimi = nimeEntry.Text ?? "",
            Telefon = telefonEntry.Text ?? "",
            Email = emailEntry.Text ?? "",
            Kirjeldus = kirjeldusEntry.Text ?? "",
            Sonum = sonumEntry.Text ?? ""
        };

        string tee = Path.Combine(FileSystem.AppDataDirectory, "kontakt.json");
        string json = JsonSerializer.Serialize(andmed);
        await File.WriteAllTextAsync(tee, json);

        statusLabel.Text = $"Salvestatud: {andmed.Nimi}";
        statusLabel.TextColor = Colors.Green;
    }

    private async void OnLaadiTapped(object? sender, EventArgs e)
    {
        string tee = Path.Combine(FileSystem.AppDataDirectory, "kontakt.json");

        if (!File.Exists(tee))
        {
            await DisplayAlertAsync("Viga", "Salvestatud kontakti ei leitud.", "OK");
            return;
        }

        string json = await File.ReadAllTextAsync(tee);
        var andmed = JsonSerializer.Deserialize<KontaktMudel>(json);

        if (andmed != null)
        {
            nimeEntry.Text = andmed.Nimi;
            telefonEntry.Text = andmed.Telefon;
            emailEntry.Text = andmed.Email;
            kirjeldusEntry.Text = andmed.Kirjeldus;
            sonumEntry.Text = andmed.Sonum;

            statusLabel.Text = $"Laaditud: {andmed.Nimi}";
            statusLabel.TextColor = Colors.Gray;
        }
    }
}

public class KontaktMudel
{
    public string Nimi { get; set; } = "";
    public string Telefon { get; set; } = "";
    public string Email { get; set; } = "";
    public string Kirjeldus { get; set; } = "";
    public string Sonum { get; set; } = "";
}
