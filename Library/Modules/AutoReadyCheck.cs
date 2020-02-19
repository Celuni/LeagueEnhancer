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
    public class AutoReadyCheck : IBaseModule
    {
        public const bool autoAccept = true; // TODO: Settings

        private LeagueClientApi api;

        protected async override void OnEnable()
        {
            base.OnEnable();

            // Initialize a connection to the league client.
            api = await LeagueClientApi.ConnectAsync();

            api.EventHandler.Subscribe("/lol-matchmaking/v1/ready-check", OnReadyCheck);
        }

        private async void OnReadyCheck(object sender, LeagueEvent e)
        {
            ReadyCheckResponse? res;
            try
            {
                res = e?.Data?.ToObject<ReadyCheckResponse>();
            }
            catch
            {
                return;
            }

            if (autoAccept && res?.playerResponse == PlayerResponse.None)
            {
                Console.WriteLine("Game found!");

                var api = await LeagueClientApi.ConnectAsync();
                await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-matchmaking/v1/ready-check/accept");

                Console.WriteLine("Accepted Readycheck!");
            }
        }
    }

    public struct ReadyCheckResponse           // TODO: Enums
    {
        public int[] declinerIds;
        public DodgeWarning dodgeWarning;     // None, Warning, Penalty
        public PlayerResponse playerResponse;   // None, Accepted, Declined
        public State state;            // Invalid, InProgress, EveryoneReady, StrangerNotReady, PartyNotReady, Error
        public bool suppressUx;
        public double timer;
    }


    public enum DodgeWarning { None, Warning, Penalty }
    public enum PlayerResponse { None, Accepted, Declined }
    public enum State { Invalid, InProgress, EveryoneReady, StrangerNotReady, PartyNotReady, Error }

}
