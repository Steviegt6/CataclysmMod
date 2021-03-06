﻿using CalamityMod;
using CalamityMod.Items.Fishing.SulphurCatches;
using CataclysmMod.Common.ModCompatibility;
using CataclysmMod.Content.Calamity.Items.Tools;
using CataclysmMod.Content.Default.GlobalModifications;
using Terraria;
using Terraria.ModLoader;

namespace CataclysmMod.Content.Calamity.GlobalModifications.GlobalItems
{
    [ModDependency("CalamityMod")]
    public class CalamityDropModifier : CataclysmGlobalItem
    {
        public override void RightClick(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<AbyssalCrate>())
                DropHelper.DropItemChance(player, ModContent.ItemType<SulphurousShell>(), 0.1f, 1, 1);
        }
    }
}