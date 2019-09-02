using LCUSharp;
using LCUSharp.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace Library.Modules
{
    public class AutoReadyCheck : BaseModule
    {
        public const bool autoAccept = true; // TODO: Settings

        protected async override void OnEnable()
        {
            base.OnEnable();

            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            api.EventHandler.Subscribe("/lol-matchmaking/v1/ready-check", OnReadyCheck);
        }

        private async void OnReadyCheck(object sender, LeagueEvent e)
        {
            ReadyCheckResponse res = e.Data.ToObject<ReadyCheckResponse>();

            if (autoAccept && res.playerResponse == "None")
            {
                Console.WriteLine("Game found!");

                var api = await LeagueClientApi.ConnectAsync();
                await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-matchmaking/v1/ready-check/accept");

                Console.WriteLine("Accepted Readycheck!");
            }
        }
    }

    struct ReadyCheckResponse           // TODO: Enums
    {
        public int[] declinerIds;
        public string dodgeWarning;     // None, Warning, Penalty
        public string playerResponse;   // None, Accepted, Declined
        public string state;            // Invalid, InProgress, EveryoneReady, StrangerNotReady, PartyNotReady, Error
        public bool suppressUx;
        public double timer;
    }
}
