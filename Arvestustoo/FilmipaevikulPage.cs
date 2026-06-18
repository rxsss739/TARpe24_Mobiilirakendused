using Arvestustoo.Services;
using Arvestustoo.ViewModels;
using Arvestustoo.Views;

namespace Arvestustoo;

public class FilmipaevikulPage : TabbedPage
{
    public FilmipaevikulPage()
    {
        Title = "Filmipäevik";
        BackgroundColor = Color.FromArgb("#1a1a2e");

        AppState.TaastaSeaded();

        var db = new Arvestustoo.Services.DatabaseService();
        var themeService = new ThemeService();
        var heliService = new HeliService();

        themeService.TaastaTeema();

        var filmidVM = new FilmidViewModel(db);
        var lisaFilmVM = new LisaFilmViewModel();
        var seadedVM = new SeadedViewModel();

        Children.Add(new FilmidLeht(filmidVM, lisaFilmVM));
        Children.Add(new LisaFilmLeht(lisaFilmVM, filmidVM, db, heliService));
        Children.Add(new SeadedLeht(seadedVM, themeService));
    }
}