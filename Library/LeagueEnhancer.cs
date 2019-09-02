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

        public async void Initialize()
        {
            modules.Add(new AutoReadyCheck());

            // Request
            //ChangeSummonerIcon(58);
            await EventExampleAsync();
        }

        public async void ChangeSummonerIcon(uint profileIconId)
        {
            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            // Update the current summoner's profile icon
            var body = new { profileIconId = profileIconId };
            var queryParameters = Enumerable.Empty<string>();
            var json = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Put, "lol-summoner/v1/current-summoner/icon",
                                                                     queryParameters, body);
        }






        public event EventHandler<LeagueEvent> GameFlowChanged;

        public async Task EventExampleAsync()
        {
            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();
            Console.WriteLine("Connected!");

            // Register game flow event.
            GameFlowChanged += OnGameFlowChanged;
            api.EventHandler.Subscribe("/lol-gameflow/v1/gameflow-phase", GameFlowChanged);
            api.EventHandler.Subscribe("/lol-champ-select/v1/session", ChampionSelectSessionChanged);
            api.EventHandler.Subscribe("/lol-matchmaking/v1/ready-check", GameFound);

            // Wait until work is complete.
            Console.WriteLine("Done.");
        }

        private async void GameFound(object sender, LeagueEvent e)
        {
            var api = await LeagueClientApi.ConnectAsync();
            var queryParameters = Enumerable.Empty<string>();
            var json = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-matchmaking/v1/ready-check/accept",
                                                                     queryParameters);
            Console.WriteLine("Match found and accepted");
        }

        private void ChampionSelectSessionChanged(object sender, LeagueEvent e)
        {
            //Console.WriteLine("-----------------\n-----------------\n-----------------");
            //Console.WriteLine("-----------------\n-------LCU-------\n-----------------");
            //Console.WriteLine("-----------------\n-----------------\n-----------------");
            //Console.WriteLine(e.Data.ToString());

            //BanChampion(266);
        }

        //private async void GameFound(int championId)
        //{
        //    var api = await LeagueClientApi.ConnectAsync();

        //    var queryParameters = Enumerable.Empty<string>();

        //    var json = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-matchmaking/v1/ready-check/accept",
        //                                                             queryParameters);
        //}

        private void OnGameFlowChanged(object sender, LeagueEvent e)
        {
            var result = e.Data.ToString();
            var state = string.Empty;

            if (result == "None")
                state = "main menu";
            else if (result == "ChampSelect")
                state = "champ select";
            else if (result == "Lobby")
                state = "lobby";
            else if (result == "InProgress")
                state = "game";

            // Print new state and set work to complete.
            Console.WriteLine($"Status update: Entered {state}. ({result})");
        }
    }


}
