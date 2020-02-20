using LCUSharp;
using LCUSharp.Websocket;
using Library.Models.ChampSelect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ChampionSelectService
    {
        const string Endpoint = "/lol-champ-select/v1/session";

        // Pick Champion


        // Hover Champion

        // Get Picks (for each team)
        // Get Bans

        public EventHandler<LeagueEvent> OnChampSelectSessionChanged;


        public LeagueClientApi LeagueClient { get; set; }


        public List<Models.ChampSelect.Action> SentActions { get; private set; } = new List<Models.ChampSelect.Action>();

        public ChampionSelectService(LeagueClientApi leagueClient)
        {
            OnChampSelectSessionChanged += OnChampSelectSessionTriggered;

            LeagueClient = leagueClient;
            LeagueClient.EventHandler.Subscribe(Endpoint, (object sender, LeagueEvent ev) => OnChampSelectSessionChanged?.Invoke(sender, ev));
        }

        private void OnChampSelectSessionTriggered(object sender, LeagueEvent e)
        {
            var session = e.Data.ToObject<Session>();

            if (session == null || (SentActions.Count > 0 && string.IsNullOrEmpty(session.timer.phase)))
                SentActions = new List<Models.ChampSelect.Action>();
        }


        /// <summary>
        /// Bans a Champion
        /// </summary>
        /// <param name="action">Action Data</param>
        /// <param name="championId">Champion Id</param>
        public async Task BanChampionAsync(Models.ChampSelect.Action action, int championId)
        {
            // Check if request already sent with that action
            if (SentActions.Select(action => action.id).Contains(action.id))
                return;

            var patchActionBody = new
            {
                action.actorCellId,
                championId,
                completed = true,
                action.id,
                isAllyAction = true,
                type = "ban"
            };

            var queryParameters = Enumerable.Empty<string>();

            try
            {
                await LeagueClient.RequestHandler.GetJsonResponseAsync(HttpMethod.Patch, $"/lol-champ-select/v1/session/actions/{action.id}", queryParameters, patchActionBody);
                Console.WriteLine($"Banned champion with id: " + championId);
                SentActions.Add(action);
            }
            catch (Exception) { }
        }
    }
}
