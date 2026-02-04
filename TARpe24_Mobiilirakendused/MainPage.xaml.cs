namespace TARpe24_Mobiilirakendused
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count >= 10)
            {
                BotImage.IsVisible = false; // Peidab pildi
                CounterLabel.Text = "Pilt kadus ära! Vajuta Reset.";
            }

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            BotImage.Rotation += 20;

            if (count >= 5)
            {
                CounterBtn.BackgroundColor = Colors.Red;
                CounterBtn.TextColor = Colors.White;
            }
            else
            {
                var rnd = new Random();
                var rndColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                CounterBtn.BackgroundColor = rndColor;
            }

            if (count % 2 == 0)
            {
                CounterBtn.CornerRadius += 5;
            }

            BotImage.Scale += 0.1;
            BotImage.Opacity -= 0.1;

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {

            count = 0;
            CounterBtn.Text = "Click me";
            BotImage.IsVisible = true;
            BotImage.Rotation = 0;
            BotImage.Scale = 1;
            BotImage.Opacity = 1;

            if (BotImage.HorizontalOptions == LayoutOptions.Start)
            {
                BotImage.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                BotImage.HorizontalOptions = LayoutOptions.Start;
            }
        }
    }
}
