using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creaturia.Items.Consumables.Fishing
{
	public class CorruptBait : ModItem
	{
		public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Rotted Bait");
			/* Tooltip.SetDefault("'!'\n" +
												  "Use in the Corruption"); */
		}

		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.maxStack = 99;
			Item.consumable = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.bait = 1;
			
		}
       

    }
}