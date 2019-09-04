using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using LCUSharp;
using LCUSharp.Websocket;
using Library.Modules;

namespace Library
{
    public class LeagueEnhancer
    {
        // Static Application Data
        public const string AppId = "at.jhinspector.league-enhancer";
        public const string AppTitle = "League Enhancer"; // TODO: Use Assembly Info
        public const string AppVersion = "0.0.1"; // TODO: Use Assembly Info


        public List<BaseModule> modules = new List<BaseModule>();

        public LeagueEnhancer()
        {
            Console.WriteLine("Initializing...");

            InitializeModules();
            InitializeNotifyIcon();
        }

        private void InitializeModules()
        {
            Console.WriteLine("\nAdding modules...");

            modules.Clear(); // TODO: Clear cleaner (clearing list after unloading modules)
            modules.Add(new AutoReadyCheck());
            modules.Add(new AutoChampBanner());
            modules.Add(new Misc());
            modules.Add(new DebugDraftLobby());
            modules.Add(new AutoTFTOrbCollector());

            Console.WriteLine($"\nAdded {modules.Count} modules\n"); // TODO:
        }

        private void InitializeNotifyIcon()
        {
            Console.WriteLine("\nInitializing NotifyIcon (Windows System Tray)...");

            NotifyIcon icon = new NotifyIcon()
            {
                Text = $"{AppTitle} ({AppVersion})",
                Icon = Properties.Resources.Icon,
                Visible = true,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem(AppTitle)
                    {
                        Enabled = false
                    },
                    new MenuItem("Settings", (sender, ev) =>
                    {
                        //new AboutWindow(sentinel.settings).Show();
                    }),
                    new MenuItem("Quit", (a, b) => Shutdown())
                })
            };

        }

        public void Shutdown()
        {
            Application.Exit();
        }
    }
}
