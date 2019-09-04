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
using Action = Library.Models.ChampSelect.Action;

namespace Library.Modules
{
    public class AutoChampBanner : BaseModule
    {
        public const bool moduleEnabled = true; // TODO: Settings
        public const bool ignoreTeamPicks = true;

        public int[] championBanList = { 266 };

        protected async override void OnEnable()
        {
            base.OnEnable();

            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();

            api.EventHandler.Subscribe("/lol-champ-select/v1/session", OnChampSelectSessionTriggered);
        }

        uint lastActionId = 0;
        private async void OnChampSelectSessionTriggered(object sender, LeagueEvent e)
        {
            // Get 
            Session? session = null;
            try
            {
                session = e?.Data?.ToObject<Session>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (session.actions == null || session.actions.Length <= 0)
                return;

            Action[] currentActions = session.actions[session.actions.Length - 1];

            if (session == null)
                return;

            if (session.timer.phase != "BAN_PICK")
                return;

            //if (localPlayerAction.type != "ban" || localPlayerAction.completed)
            //    return;

            foreach (var action in currentActions)
            {
                bool isLocalPlayerAction = session.localPlayerCellId == action.actorCellId;

                if (isLocalPlayerAction && action.type == "ban" && lastActionId <= action.id)
                {
                    lastActionId = action.id;
                    BanChampion(action);
                    return;
                }
            }

        }

        private async void BanChampion(Action action)
        {
            if (action.type != "ban" || action.completed)
                return;

            var body = new
            {
                action.actorCellId,
                action.championId,
                completed = true,
                action.id,
                type = "ban"
            };

            // Initialize a connection to the league client.
            var api = await LeagueClientApi.ConnectAsync();
            var queryParameters = Enumerable.Empty<string>();

            try
            {
                Console.WriteLine($"Banning ChampionId ({action.championId})...");
                string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Patch, $"/lol-champ-select/v1/session/actions/{action.id}", queryParameters, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        int[] GetBannableChampions()
        {
            return new int[] { 266 };
        }
    }
}
