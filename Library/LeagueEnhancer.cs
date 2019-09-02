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
            modules.Add(new AutoChampBanner());

            await Task.Run(ChangeSummonerIcon);
        }

        public async void ChangeSummonerIcon()
        {
            uint profileIconId = 58;

            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            // Update the current summoner's profile icon
            var body = new { profileIconId = profileIconId };
            var queryParameters = Enumerable.Empty<string>();
            var json = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Put, "lol-summoner/v1/current-summoner/icon",
                                                                     queryParameters, body);
        }
    }


}
