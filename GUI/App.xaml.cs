using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Library;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LeagueEnhancer LeagueEnhancer;
        public App()
        {
            LeagueEnhancer = new LeagueEnhancer();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            LeagueEnhancer.Shutdown();
        }

    }
}
