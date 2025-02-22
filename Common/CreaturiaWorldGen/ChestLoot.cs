using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System.Linq;
using Creaturia.Items.Consumables.Fishing;
using Creaturia.Items.Weapon;

namespace Creaturia.Common.CreaturiaWorldGen;

public class ChestLoot : ModSystem
{
    public override void PostWorldGen()
    {
        for (int chestIndex = 0; chestIndex < 1000; chestIndex++) // So it just tries the first 1000 chests it finds (which is a value a world would most likely never get past)
        {
            Chest chest = Main.chest[chestIndex];
            
            if (chest != null)
            {
                if (WorldGen.genRand.NextBool(6) &&
                (Main.tile[chest.x, chest.y].TileType == TileID.Containers && // https: //terraria.wiki.gg/wiki/Tile_IDs
                (Main.tile[chest.x, chest.y].TileFrameX == 17 * 36 ||          // Each chest is seperated by 36 pixels. Therefore, 0 would be Chest 0, aka Wooden, 13 would be Web, 17 would be Water, etc.
                Main.tile[chest.x, chest.y].TileFrameX == 13 * 36)))
                {  // I'm not sure what the difference between Containers and Containers2 is so I won't touch that. I think containers2 is trapped chests but idk
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++) // This just tries the first 40 slots of the chest, looking for an empty one.
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<GoldenFishDevBait>());
                            break; // Break ends the loop, preventing the rest of the empty slots from being filled by the item
                        }
                    }
                }

                if (WorldGen.genRand.NextBool(5) &&
               (Main.tile[chest.x, chest.y].TileType == TileID.Containers &&  // I'm hoping the Electric Eel isn't too OP of a weapon to find underground
               (Main.tile[chest.x, chest.y].TileFrameX == 17 * 36)))
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<ElectricEel>());
                            break;
                        }
                    }
                }
                if (Main.tile[chest.x, chest.y].TileType == TileID.Containers &&
               (Main.tile[chest.x, chest.y].TileFrameX == 17 * 36))
                {

                }
            }
        }
    }
}
    
