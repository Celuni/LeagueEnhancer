using LCUSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Library.Modules
{
    class DebugDraftLobby : IBaseModule
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            CreateLobby();
        }

        private async void CreateLobby()
        {
            // TODO:
            return;

            var api = await LeagueClientApi.ConnectAsync();

            var obj = new
            {
                customGameLobby = new {
                    configuration = new
                    {
                        gameMode = "CLASSIC",
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
                    lobbyName = "Name PBE Dev lobby",
                    lobbyPassword = "",
                },
                isCustom = true
            };

            var queryParameters = Enumerable.Empty<string>();
            string body = JsonConvert.SerializeObject(obj);
            string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-lobby/v2/lobby", queryParameters, body);
        }
    }
}
