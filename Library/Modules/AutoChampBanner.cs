using LCUSharp.Websocket;
using Library.Models.ChampSelect;
using Library.Services;
using System;
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
            {
                Console.WriteLine("Data is null!");
                return;
            }

            var currentAction = session?.actions?.LastOrDefault()?.LastOrDefault();

            if (currentAction == null || currentAction.actorCellId != session.localPlayerCellId)
                return;

            // TODO: Fails bc bans are global (multiple active actorCells) => Use RiftExplorer to check state when everyone is able to ban
            if (currentAction.actorCellId == session.localPlayerCellId && currentAction.type == "ban")
            {
                // Get first available champ in banlist!

                var bans = session.bans.myTeamBans.Concat(session.bans.theirTeamBans);

                foreach (var championId in championBanList)
                {
                    if (bans.Contains(championId))
                        continue;

                    await ChampionSelectService.BanChampionAsync(currentAction, championId);
                }

                Console.WriteLine($"Action {currentAction.id} ({currentAction.type}): {(currentAction.completed ? "completed" : "in Progress")}");
            }
        }
    }
}
