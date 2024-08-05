using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Creaturia.Items.Blocks;


namespace Creaturia.Tiles.Plants.Trees
{
    public class SinfulDirt : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileLavaDeath[Type] = false;
            
           // ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<SinfulSoil>();

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.RosyBrown);
            // name.SetDefault("Sinful Soil");
            
        }
       
       
    }
}
