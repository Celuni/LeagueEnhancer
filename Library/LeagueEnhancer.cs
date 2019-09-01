using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LCUSharp;
using LCUSharp.Websocket;

namespace Library
{
    public class LeagueEnhancer
    {
        public LeagueEnhancer()
        {
            Initialize();

            Console.ReadKey();
        }

        public async void Initialize()
        {
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

            // Wait until work is complete.
            Console.WriteLine("Done.");
        }

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
