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
using Library.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library
{
    public class LeagueEnhancer
    {
        // Static Application Data
        public const string AppId = "at.jhinspector.league-enhancer";
        public const string AppTitle = "League Enhancer"; // TODO: Use Assembly Info
        public const string AppVersion = "0.0.1"; // TODO: Use Assembly Info
        public const string LeaguePatch = "9.17.1"; // TODO: Use functionality

        NotifyIcon notifyIcon = new NotifyIcon();

        private List<IBaseModule> modules = new List<IBaseModule>();

        public IServiceProvider Services { get; private set; }
        public LeagueClientApi LeagueClient { get; private set; }


        public LeagueEnhancer()
        {
            Application.ApplicationExit += OnApplicationExit;

            LeagueClient = LeagueClientApi.ConnectAsync().GetAwaiter().GetResult();

            // Register Services
            Services = new ServiceCollection()
                .AddSingleton(LeagueClient)
                .AddSingleton<ChampionSelectService>()
                .AddSingleton<NotificationService>()
                .AddSingleton<ChampionService>()
                .AddSingleton<QueueService>()
                .BuildServiceProvider();

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
            AddModule(new AutoChampBanner(Services.GetRequiredService<ChampionSelectService>()));
            AddModule(new DebugDraftLobby());

            //AddModule(new Misc());
            //AddModule(new AutoTFTOrbCollector());
            //AddModule(new ChampionData());
            //AddModule(new HextechTools());
            //AddModule(new NotificationManager());

            Console.WriteLine($"\nAdded {modules.Count} modules\n"); // TODO:
        }

        public void AddModule(IBaseModule module)
        {
            modules.Add(module);
        }

        private void InitializeNotifyIcon()
        {
            Console.WriteLine("\nInitializing NotifyIcon (Windows System Tray)...");

            // Create Modules Menu Items
            List<MenuItem> modulesMenuItems = new List<MenuItem>();
            foreach (var module in modules)
                modulesMenuItems.Add(new MenuItem(module.GetType().Name));

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
