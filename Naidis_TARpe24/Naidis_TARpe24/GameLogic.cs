namespace Naidis_TARpe24;

public class GameLogic
{
    public enum CellState { Empty, X, O }
    public enum GameResult { None, XWins, OWins, Draw }

    private CellState[,] _board = new CellState[3, 3];

    public CellState CurrentPlayer { get; private set; } = CellState.X;
    public GameResult Result { get; private set; } = GameResult.None;
    public bool IsGameOver => Result != GameResult.None;

    public int XWins { get; private set; }
    public int OWins { get; private set; }
    public int Draws { get; private set; }

    public void Reset(CellState startingPlayer = CellState.X)
    {
        _board = new CellState[3, 3];
        CurrentPlayer = startingPlayer;
        Result = GameResult.None;
    }

    public bool MakeMove(int row, int col)
    {
        if (IsGameOver || _board[row, col] != CellState.Empty)
            return false;

        _board[row, col] = CurrentPlayer;
        Result = CheckResult();

        if (Result == GameResult.XWins) XWins++;
        else if (Result == GameResult.OWins) OWins++;
        else if (Result == GameResult.Draw) Draws++;

        if (!IsGameOver)
            CurrentPlayer = CurrentPlayer == CellState.X ? CellState.O : CellState.X;

        return true;
    }

    public CellState GetCell(int row, int col) => _board[row, col];

    public string GetStats() => $"X voite: {XWins}   O voite: {OWins}   Viike: {Draws}";

    public void ResetStats()
    {
        XWins = 0;
        OWins = 0;
        Draws = 0;
    }

    private GameResult CheckResult()
    {
        for (int i = 0; i < 3; i++)
        {
            var row = Check3(_board[i, 0], _board[i, 1], _board[i, 2]);
            if (row != GameResult.None) return row;

            var col = Check3(_board[0, i], _board[1, i], _board[2, i]);
            if (col != GameResult.None) return col;
        }

        var diag1 = Check3(_board[0, 0], _board[1, 1], _board[2, 2]);
        if (diag1 != GameResult.None) return diag1;

        var diag2 = Check3(_board[0, 2], _board[1, 1], _board[2, 0]);
        if (diag2 != GameResult.None) return diag2;

        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (_board[r, c] == CellState.Empty) return GameResult.None;

        return GameResult.Draw;
    }

    private static GameResult Check3(CellState a, CellState b, CellState c)
    {
        if (a == CellState.Empty || a != b || b != c) return GameResult.None;
        return a == CellState.X ? GameResult.XWins : GameResult.OWins;
    }

    public List<(int row, int col)> GetWinningLine()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Check3(_board[i, 0], _board[i, 1], _board[i, 2]) != GameResult.None)
                return [(i, 0), (i, 1), (i, 2)];

            if (Check3(_board[0, i], _board[1, i], _board[2, i]) != GameResult.None)
                return [(0, i), (1, i), (2, i)];
        }

        if (Check3(_board[0, 0], _board[1, 1], _board[2, 2]) != GameResult.None)
            return [(0, 0), (1, 1), (2, 2)];

        if (Check3(_board[0, 2], _board[1, 1], _board[2, 0]) != GameResult.None)
            return [(0, 2), (1, 1), (2, 0)];

        return [];
    }
}
