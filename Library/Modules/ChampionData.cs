using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Modules
{
    public class ChampionData : IBaseModule
    {
        public static Champion[] champions;
        public static string ChampionDataUrl => $"http://ddragon.leagueoflegends.com/cdn/{LeagueEnhancer.LeaguePatch}/data/en_US/champion.json";

        public static string championBanListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "championBanList.json");

        protected override void OnEnable()
        {
            base.OnEnable();

            Console.WriteLine($"Loading Championdata from ({ChampionDataUrl})");


            Console.WriteLine("Banned champion ids:");
            foreach (var championId in ChampionBanList)
                Console.WriteLine(championId);

        }

        public static uint[] ChampionBanList
        {
            get
            {
                try
                {
                    if (!File.Exists(championBanListPath))
                    {
                        File.Create(championBanListPath).Dispose();
                        Console.WriteLine("Created ban list file");
                    }

                    string json = File.ReadAllText(championBanListPath);
                    uint[] championIds = JsonConvert.DeserializeObject<uint[]>(json);
                    
                    Console.WriteLine("Read champion ban list...");
                    return championIds;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new uint[0];
                }
            }
            set
            {
                try
                {
                    string json = JsonConvert.SerializeObject(value);
                    File.WriteAllText(championBanListPath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine("Saved champion ban list...");
            }
        }
    }
}
