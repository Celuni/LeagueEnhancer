using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LCUSharp;
using LCUSharp.Websocket;
using Library.Modules;

namespace Library
{
    public class LeagueEnhancer
    {
        public List<BaseModule> modules = new List<BaseModule>();

        public LeagueEnhancer()
        {
            Initialize();
        }

        public void Initialize()
        {
            Console.WriteLine("Initializing...");
            Console.WriteLine("\nAdding modules...");

            modules.Add(new AutoReadyCheck());
            modules.Add(new AutoChampBanner());
            modules.Add(new Misc());

            Console.WriteLine($"\nAdded {modules.Count} modules\n"); // TODO:
        }
    }
}
