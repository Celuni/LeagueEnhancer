using LCUSharp;
using LCUSharp.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Library.Modules
{
    public class AutoReadyCheck: BaseModule
    {
        protected async override void OnEnable()
        {
            base.OnEnable();

            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            api.EventHandler.Subscribe("/lol-matchmaking/v1/ready-check", OnReadyCheck);
        }

        private async void OnReadyCheck(object sender, LeagueEvent e)
        {
            Console.WriteLine("--- Response <GameFound> ---");
            Console.WriteLine(e.Data);
            Console.WriteLine("----------------------------");

            var api = await LeagueClientApi.ConnectAsync();
            var queryParameters = Enumerable.Empty<string>();
            var json = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-matchmaking/v1/ready-check/accept", queryParameters);

            Console.WriteLine("--- Response <Accept> ---");
            Console.WriteLine(json);
            Console.WriteLine("-------------------------");
        }
    }
}
