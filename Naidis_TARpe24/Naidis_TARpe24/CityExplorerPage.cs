namespace Naidis_TARpe24;

public class CityExplorerPage : TabbedPage
{
    public CityExplorerPage()
    {
        Title = "CityExplorer";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        var dbService = new DatabaseService();
        var exploreVM = new ExploreViewModel(dbService);
        var lemmikudVM = new LemmikudViewModel(dbService);

        Children.Add(new AvastaLeht(exploreVM));
        Children.Add(new LemmikudLeht(lemmikudVM));
        Children.Add(new SeadedLeht());
    }
}