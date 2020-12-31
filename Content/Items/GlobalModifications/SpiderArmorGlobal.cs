﻿using CataclysmMod.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CataclysmMod.Content.Items.GlobalModifications
{
    public class SpiderArmorGlobal : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            switch (item.type)
            {
                case ItemID.SpiderMask:
                    if (CalamityChangesConfig.Instance.spiderArmorBuff)
                        item.defense += 3;
                    break;

                case ItemID.SpiderBreastplate:
                    if (CalamityChangesConfig.Instance.spiderArmorBuff)
                        item.defense += 2;
                    break;

                case ItemID.SpiderGreaves:
                    if (CalamityChangesConfig.Instance.spiderArmorBuff)
                        item.defense += 1;
                    break;
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.SpiderMask && body.type == ItemID.SpiderBreastplate && legs.type == ItemID.SpiderGreaves)
                return "Cataclysm:SpiderArmor";

            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            switch (set)
            {
                case "Cataclysm:SpiderArmor":
                    if (CalamityChangesConfig.Instance.spiderArmorBuff)
                    {
                        player.setBonus += "\nYou can stick to walls like a spider";
                        player.spikedBoots = 3;
                    }
                    break;
            }
        }
    }
}