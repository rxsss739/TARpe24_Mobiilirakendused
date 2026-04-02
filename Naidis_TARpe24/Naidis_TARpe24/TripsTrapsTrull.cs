namespace Naidis_TARpe24;

public class TripsTrapsTrull : ContentPage
{
    GameLogic game;
    Button[,] cells;
    Label statusLabel;
    Label statsLabel;
    VerticalStackLayout vsl;

    public TripsTrapsTrull()
    {
        game = new GameLogic();
        cells = new Button[3, 3];

        statusLabel = new Label
        {
            Text = "Mangija X kaik",
            FontSize = 18,
            FontFamily = "Lufio",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Red
        };

        statsLabel = new Label
        {
            Text = game.GetStats(),
            FontSize = 14,
            FontFamily = "Lufio",
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Gray
        };

        Grid grid = new Grid
        {
            RowSpacing = 8,
            ColumnSpacing = 8,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 285,
            HeightRequest = 285
        };

        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Button btn = new Button
                {
                    Text = "",
                    FontSize = 36,
                    FontFamily = "Lufio",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    CornerRadius = 12,
                    BorderColor = Colors.LightGray,
                    BorderWidth = 1.5
                };

                int r = row, c = col;
                btn.Clicked += (sender, e) => CellClicked(r, c);

                cells[row, col] = btn;
                grid.Add(btn, col, row);
            }
        }

        Button newGameBtn = new Button
        {
            Text = "Alusta mang",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        newGameBtn.Clicked += (sender, e) => ResetBoard(GameLogic.CellState.X);

        Button whoFirstBtn = new Button
        {
            Text = "Vali esimene mängija",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50
        };
        whoFirstBtn.Clicked += WhoFirstClicked;

        Button resetStatsBtn = new Button
        {
            Text = "Nullita statistika",
            FontSize = 18,
            FontFamily = "Lufio",
            BackgroundColor = Colors.LightGray,
            TextColor = Colors.Black,
            CornerRadius = 10,
            HeightRequest = 50
        };
        resetStatsBtn.Clicked += (sender, e) =>
        {
            game.ResetStats();
            statsLabel.Text = game.GetStats();
        };

        HorizontalStackLayout hsl = new HorizontalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.Center,
            Children = { newGameBtn, whoFirstBtn }
        };

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { statusLabel, grid, statsLabel, hsl, resetStatsBtn }
        };

        Content = new ScrollView { Content = vsl };
    }

    private void CellClicked(int row, int col)
    {
        if (game.IsGameOver) return;
        if (!game.MakeMove(row, col)) return;

        bool isX = game.GetCell(row, col) == GameLogic.CellState.X;
        cells[row, col].Text = isX ? "X" : "O";
        cells[row, col].TextColor = isX ? Colors.Red : Colors.Blue;

        statsLabel.Text = game.GetStats();

        if (game.IsGameOver)
        {
            foreach (var (r, c) in game.GetWinningLine())
                cells[r, c].BackgroundColor = Colors.Yellow;

            ShowResult();
        }
        else
        {
            bool nextIsX = game.CurrentPlayer == GameLogic.CellState.X;
            statusLabel.Text = $"Mangija {(nextIsX ? "X" : "O")} kaik";
            statusLabel.TextColor = nextIsX ? Colors.Red : Colors.Blue;
        }
    }

    private async void ShowResult()
    {
        string message = game.Result switch
        {
            GameLogic.GameResult.XWins => "Mangija X voitis!",
            GameLogic.GameResult.OWins => "Mangija O voitis!",
            _ => "Viik!"
        };

        statusLabel.Text = message;
        statusLabel.TextColor = Colors.Green;

        bool again = await DisplayAlertAsync("Mang labi", message, "Mangi uuesti", "Sulge");
        if (again) ResetBoard(GameLogic.CellState.X);
    }

    private void ResetBoard(GameLogic.CellState startingPlayer)
    {
        game.Reset(startingPlayer);

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                cells[r, c].Text = "";
                cells[r, c].TextColor = Colors.Black;
                cells[r, c].BackgroundColor = Colors.White;
            }
        }

        bool isX = game.CurrentPlayer == GameLogic.CellState.X;
        statusLabel.Text = $"Mangija {(isX ? "X" : "O")} kaik";
        statusLabel.TextColor = isX ? Colors.Red : Colors.Blue;
        statsLabel.Text = game.GetStats();
    }

    private async void WhoFirstClicked(object sender, EventArgs e)
    {
        string choice = await DisplayActionSheetAsync("Kes alustab?", "Tuhista", null, "Mangija X", "Mangija O", "Juhuslik");

        if (choice == "Mangija X")
            ResetBoard(GameLogic.CellState.X);
        else if (choice == "Mangija O")
            ResetBoard(GameLogic.CellState.O);
        else if (choice == "Juhuslik")
        {
            var random = new Random().Next(2) == 0 ? GameLogic.CellState.X : GameLogic.CellState.O;
            ResetBoard(random);
        }
    }
}
