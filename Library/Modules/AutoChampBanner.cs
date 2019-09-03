using LCUSharp;
using LCUSharp.Websocket;
using Newtonsoft.Json;
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

        int counter = 0;
        private void OnChampSelectSessionTriggered(object sender, LeagueEvent e)
        {
            dynamic dataObject = e.Data.ToString();
            dynamic dyn = JsonConvert.DeserializeObject(dataObject);

            string json = JsonConvert.SerializeObject(dyn.actions, Formatting.Indented);

            Action[,] _actions = JsonConvert.DeserializeObject<Action[,]>(json);
            Console.WriteLine(JsonConvert.SerializeObject(_actions, Formatting.Indented));

            //foreach (var item in _actions)
            //{
            //    Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            //}


            return;

            dynamic _actionSet = dataObject?.actions;

            List<List<Action>> actions = new List<List<Action>>();

            try
            {
                //Action[,] actionSet = (Action[])(dataObject?.actions);

                for (int i = 0; i < _actionSet.Length; i++)
                {
                    actions.Add(_actionSet[i]);
                }

                foreach (dynamic action in actions)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(action, Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine("champion select triggered");

            //Console.WriteLine(dataObject.actions[0][0]);

            // Get local player cell id
            int localCellId = dataObject.localPlayerCellId;
            //Console.WriteLine(localCellId);

            //Console.WriteLine($"{dataObject.timer.phase} ({counter++})");
        }

        int[] GetBannableChampions()
        {
            return new int[] { 266 };
        }

        public struct Action
        {
            public int actorCellId;
            public int championId;
            public bool completed;
            public uint id;
            public string type; // TODO: enum
            public int? pickTurn;
        }
    }

}
