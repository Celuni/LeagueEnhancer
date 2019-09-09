using LCUSharp;
using Library.Models.Loot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Modules
{
    public class HextechTools : IBaseModule
    {
        List<LootItem> ownedItems = new List<LootItem>();
        LootItem[] items = new LootItem[0];

        List<LootItem> OwnedItems => items.ToList().Where(p => p.itemStatus == ItemStatus.OWNED).ToList();

        protected override async void OnEnable()
        {
            base.OnEnable();

            items = await GetItems();
            DisenchantDuplicates();
            DisenchantOwnedChampions();
        }

        async void DisenchantDuplicates()
        {
            var api = await LeagueClientApi.ConnectAsync();

            foreach (var item in OwnedItems)
            {
                string body = JsonConvert.SerializeObject(item.lootId, Formatting.Indented);

                //string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-loot/v1/recipes/{item.disenchantLootName}/craft", new string[0], $"[{item.lootId}]");
                string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-loot/v1/player-loot/CHAMPION_31/context-menu");


                //string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Get, $"/lol-loot/v1/recipes/initial-item/{lootId}", new string[0], $"[{item.lootId}]");
                //Console.WriteLine(res);
            }
        }

        async void DisenchantOwnedChampions()
        {
            var api = await LeagueClientApi.ConnectAsync();

            foreach (var item in OwnedItems.Where(p => p.displayCategories == "CHAMPION"))
            {
                Console.WriteLine(item.itemDesc);

                //string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-loot/v1/player-loot/CURRENCY_champion", new string[0], $"[{item.lootId}]");
                string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Post, $"/lol-loot/v1/recipes/CURRENCY_champion", new string[0], $"[\"{item.lootId}\"]");
            }


        }

        async Task<LootItem[]> GetItems()
        {
            var api = await LeagueClientApi.ConnectAsync();

            try
            {
                string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Get, "/lol-loot/v1/player-loot");
                return JsonConvert.DeserializeObject<LootItem[]>(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new LootItem[0];
            }
        }
    }
}
