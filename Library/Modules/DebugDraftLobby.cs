using LCUSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Modules
{
    class DebugDraftLobby : IBaseModule
    {
        LeagueClientApi api;

        protected override async void OnEnable()
        {
            base.OnEnable();

            api = await LeagueClientApi.ConnectAsync();

            //await CreateLobbyAsync();
        }

        private async Task CreateLobbyAsync()
        {
            var obj = new
            {
                customGameLobby = new
                {
                    configuration = new
                    {
                        gameMode = "PRACTICETOOL",
                        gameMutator = "",
                        gameServerRegion = "",
                        mapId = 11,
                        mutators = new
                        {
                            id = 1
                        },
                        spectatorPolicy = "AllAllowed",
                        teamSize = 5
                    },
                    lobbyName = "Name",
                    lobbyPassword = "",
                },
                isCustom = true
            };

            var queryParameters = new string[] {
                "Accept: application/json"
            };

            string body = JsonConvert.SerializeObject(obj);
            //client.Init();
            //var res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, "/lol-lobby/v2/lobby", null, body);
            var res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Get, "/lol-lobby/v2/lobby");

            //client.Init(InitializeMethod.CommandLine);
            //client.MakeRequestAsync(, , body);

            //string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-lobby/v2/lobby", null, body);

            //string result = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-lobby/v2/lobby");
        }
    }
}
