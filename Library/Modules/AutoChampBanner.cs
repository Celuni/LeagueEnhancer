using LCUSharp.Websocket;
using Library.Models.ChampSelect;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Modules
{
    public class AutoChampBanner : IBaseModule
    {
        public const bool moduleEnabled = true; // TODO: Settings
        public const bool ignoreTeamPicks = true;

        public int[] championBanList = { 266, 103, 84 };

        ChampionSelectService ChampionSelectService { get; set; }
        public AutoChampBanner(ChampionSelectService championSelectService)
        {
            ChampionSelectService = championSelectService;
            ChampionSelectService.OnChampSelectSessionChanged += ChampSelectSessionChanged;
        }

        private async void ChampSelectSessionChanged(object sender, LeagueEvent e)
        {
            var session = e?.Data?.ToObject<Session>();

            if (session == null)
                return;

            var actions = session.actions.SelectMany(inner => inner);
            var myActions = actions.Where(action => action.actorCellId == session.localPlayerCellId);
            var myActiveAction = myActions.FirstOrDefault(action => action.isInProgress);
            
            if (myActiveAction == null)
                return;

            if (myActiveAction.type == "ban")
            {
                // Get first available champ in banlist!
                var bans = session.bans.myTeamBans.Concat(session.bans.theirTeamBans);

                foreach (var championId in championBanList)
                {
                    if (bans.Contains(championId))
                        continue;

                    await ChampionSelectService.BanChampionAsync(myActiveAction, championId);
                }

                Console.WriteLine($"Action {myActiveAction.id} ({myActiveAction.type}): {(myActiveAction.completed ? "completed" : "in Progress")}");
            }
        }
    }
}
