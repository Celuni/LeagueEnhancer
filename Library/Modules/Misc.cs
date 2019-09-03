using LCUSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Library.Modules
{
    public class Misc: BaseModule
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            ChangeSummonerIcon(58);
        }

        public async void ChangeSummonerIcon(uint profileIconId)
        {
            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            // Update the current summoner's profile icon
            var body = new { profileIconId };
            var queryParameters = Enumerable.Empty<string>();
            await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Put, "lol-summoner/v1/current-summoner/icon", queryParameters, body);
        }
    }
}
