using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Loot
{
    public class LootItem
    {
        public string asset { get; set; }
        public int count { get; set; }
        public string disenchantLootName { get; set; }
        public int disenchantValue { get; set; }
        public string displayCategories { get; set; }
        public int expiryTime { get; set; }
        public bool isNew { get; set; }
        public bool isRental { get; set; }
        public string itemDesc { get; set; }
        public ItemStatus itemStatus { get; set; }
        public string localizedDescription { get; set; }
        public string localizedName { get; set; }
        public string localizedRecipeSubtitle { get; set; }
        public string localizedRecipeTitle { get; set; }
        public string lootId { get; set; }
        public string lootName { get; set; }
        public string parentItemStatus { get; set; }
        public int parentStoreItemId { get; set; }
        public string rarity { get; set; }
        public RedeemableStatus redeemableStatus { get; set; }
        public string refId { get; set; }
        public int rentalGames { get; set; }
        public int rentalSeconds { get; set; }
        public string shadowPath { get; set; }
        public string splashPath { get; set; }
        public int storeItemId { get; set; }
        public string tags { get; set; }
        public string tilePath { get; set; }
        public string type { get; set; }
        public string upgradeEssenceName { get; set; }
        public int upgradeEssenceValue { get; set; }
        public string upgradeLootName { get; set; }
        public int value { get; set; }
    }

    public enum ItemStatus
    {
        NONE,
        FREE,
        RENTAL,
        OWNED
    }

    public enum RedeemableStatus
    {
        UNKNOWN,
        REDEEMABLE,
        REDEEMABLE_RENTAL,
        NOT_REDEEMABLE,
        NOT_REDEEMABLE_RENTAL,
        ALREADY_OWNED,
        ALREADY_RENTED,
        CHAMPION_NOT_OWNED,
        SKIN_NOT_OWNED
    }
}
