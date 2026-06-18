using System.Collections.ObjectModel;

namespace Naidis_TARpe24;

public class LemmikudViewModel : BaseViewModel
{
    DatabaseService db;

    ObservableCollection<Lemmik> _lemmikud;
    public ObservableCollection<Lemmik> Lemmikud
    {
        get => _lemmikud;
        set { _lemmikud = value; OnPropertyChanged(); }
    }

    bool _onTyhi;
    public bool OnTyhi
    {
        get => _onTyhi;
        set { _onTyhi = value; OnPropertyChanged(); }
    }

    public LemmikudViewModel(DatabaseService databaseService)
    {
        db = databaseService;
        Lemmikud = new ObservableCollection<Lemmik>();
    }

    public void LaeLemmikud()
    {
        var andmed = db.GetLemmikud();
        Lemmikud = new ObservableCollection<Lemmik>(andmed);
        OnTyhi = Lemmikud.Count == 0;
    }

    public void KustutaLemmik(Lemmik lemmik)
    {
        db.KustutaLemmik(lemmik.Id);
        LaeLemmikud();
    }
}