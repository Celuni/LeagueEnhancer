using LCUSharp;
using LCUSharp.Websocket;
using System;
using System.Collections.Generic;
using System.Text;

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

        private void OnChampSelectSessionTriggered(object sender, LeagueEvent e)
        {

        }

        async void GetBannableChampions()
        {

        }
    }
}
