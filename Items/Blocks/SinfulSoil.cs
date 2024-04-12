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

namespace Creaturia.Items.Blocks
{


	public class SinfulSoil : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.DirtBlock;
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<SinfulDirt>();
			Item.width = 12;
			Item.height = 12;
			Item.value = 3000;
		}
	}
}