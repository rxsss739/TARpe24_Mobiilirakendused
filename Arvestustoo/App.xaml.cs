using Microsoft.Extensions.DependencyInjection;

namespace Arvestustoo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new FilmipaevikulPage();
        }

    }
}