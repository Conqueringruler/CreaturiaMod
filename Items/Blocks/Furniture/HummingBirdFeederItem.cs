using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Creaturia;
using Creaturia.Tiles;
using Creaturia.Tiles.Plants.Trees;
using Creaturia.Tiles.Furniture;

namespace Creaturia.Items.Blocks.Furniture
{


    public class HummingBirdFeederItem : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<HummingBirdFeederTile>());
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 12;
            Item.height = 12;




            Item.maxStack = 9999;
            Item.value = 75000;
            Item.rare = ItemRarityID.Green;
        }
    }
}