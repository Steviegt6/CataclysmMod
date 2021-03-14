﻿using CataclysmMod.Common.Utilities;
using CataclysmMod.Content.Configs;
using IL.CalamityMod.World;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ID;

namespace CataclysmMod.Common.MonoMod.ILEdits
{
    public class CavernShrineIL : ILEdit
    {
        public override string DictKey =>
            "CalamityMod.World.SmallBiomesWorldGenerationMethods.PlaceShrinesSpecialChest";

        public override bool Autoload() => CataclysmConfig.Instance.cavernShrineChanges;

        public override void Load()
        {
            SmallBiomes.PlaceShrines += ChangeCavernShrineBlocks;
            WorldGenerationMethods.SpecialChest += ChangeCavernShrineChest;
        }

        public override void Unload()
        {
            SmallBiomes.PlaceShrines -= ChangeCavernShrineBlocks;
            WorldGenerationMethods.SpecialChest -= ChangeCavernShrineChest;
        }

        private static void ChangeCavernShrineChest(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            /* Match the specific chest style of obsidian chests, the style used for the cavern shrines normally. (Style 44)
             * // contain = ModContent.ItemType<OnyxExcavatorKey>();
             * IL_005b: call int32 [Terraria]Terraria.ModLoader.ModContent::ItemType<class CalamityMod.Items.Mounts.OnyxExcavatorKey>()
             * IL_0060: stloc.0
             * // style = 44;
             * IL_0061: ldc.i4.s 44
             * IL_0063: stloc.1
             * // break;
             * IL_0064: br.s IL_00a5
             */
            if (!c.TryGotoNext(i => i.MatchLdcI4(44)))
            {
                ILLogger.LogILError("ldc.i4.s", "44");
                return;
            }

            c.Index++;

            // Pop 44 and replace it with 1, the style for gold chests
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, 1);

            ILLogger.LogILCompletion("IL.CalamityMod.World.WorldGenerationMethods.SpecialChest");
        }

        private static void ChangeCavernShrineBlocks(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            /* Match the first number in the call for WorldGenerationMethods.SpecialHut for cavern shrines (75)
             * // WorldGenerationMethods.SpecialHut(75, 56, 20, 2, num8, num10);
             * IL_0225: ldc.i4.s 75 // Cavern shrine tile
             * IL_0227: ldc.i4.s 56 // Cavern shrine tile
             * IL_0229: ldc.i4.s 20 // Cavern shrine tile
             * IL_022b: ldc.i4.2
             * IL_022c: ldloc.s 13
             * IL_022e: ldloc.s 15
             */
            if (!c.TryGotoNext(i => i.MatchLdcI4(75)))
            {
                ILLogger.LogILError("ldc.i4.s", "75", 1 + 1);
                return;
            }

            c.Index++;

            // Pop the normal values and replace them with the IDs for stone bricks and stone
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, TileID.GrayBrick);

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, TileID.Stone);

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, (int) WallID.GrayBrick);

            // Repeat the previous IL as there's a second call
            if (!c.TryGotoNext(i => i.MatchLdcI4(75)))
            {
                ILLogger.LogILError("ldc.i4.s", "75", 2 + 1);
                return;
            }

            c.Index++;

            // Ditto
            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, TileID.GrayBrick);

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, TileID.Stone);

            c.Index++;

            c.Emit(OpCodes.Pop);
            c.Emit(OpCodes.Ldc_I4, (int) WallID.GrayBrick);

            ILLogger.LogILCompletion("IL.CalamityMod.World.SmallBiomes.PlaceShrines");
        }
    }
}