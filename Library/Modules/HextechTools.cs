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
        }

        async void DisenchantDuplicates()
        {
            var api = await LeagueClientApi.ConnectAsync();

            Console.WriteLine("Owned:");
            foreach (var item in OwnedItems)
            {
                Console.WriteLine(item.itemDesc);
                string res = await api.RequestHandler.GetJsonResponseAsync(HttpMethod.Get, "/lol-loot/v1/player-loot/{lootName}/redeem");
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
