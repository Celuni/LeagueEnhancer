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

        NotifyIcon notifyIcon = new NotifyIcon();

        private List<BaseModule> modules = new List<BaseModule>();

        public LeagueEnhancer()
        {
            Application.ApplicationExit += OnApplicationExit;


            // Initializing
            Console.WriteLine("Initializing...");

            InitializeModules();
            InitializeNotifyIcon();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // Cleanly dispose notifyIcon
            notifyIcon.Icon.Dispose();
            notifyIcon.Dispose();

            Environment.Exit(0);
        }

        private void InitializeModules()
        {
            Console.WriteLine("\nAdding modules...");

            AddModule(new AutoReadyCheck());
            AddModule(new AutoChampBanner());
            AddModule(new Misc());
            AddModule(new DebugDraftLobby());
            AddModule(new AutoTFTOrbCollector());

            Console.WriteLine($"\nAdded {modules.Count} modules\n"); // TODO:
        }

        public void AddModule(BaseModule module)
        {
            modules.Add(module);
        }

        private void InitializeNotifyIcon()
        {
            Console.WriteLine("\nInitializing NotifyIcon (Windows System Tray)...");

            // Create Modules Menu Items
            List<MenuItem> modulesMenuItems = new List<MenuItem>();
            foreach (var module in modules)
            {
                modulesMenuItems.Add(new MenuItem(module.GetType().Name));
            }

            // Create Notify Icon
            notifyIcon = new NotifyIcon()
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
                    new MenuItem("Modules", modulesMenuItems.ToArray()),
                    new MenuItem("About", (sender, ev) =>
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
