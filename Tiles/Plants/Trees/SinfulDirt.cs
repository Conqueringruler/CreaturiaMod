using Terraria;
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
            
            ItemDrop = ModContent.ItemType<SinfulSoil>();

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.RosyBrown);
            name.SetDefault("Sinful Soil");
            
        }
       
       
    }
}
