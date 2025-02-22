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
	public class CrimsonBait : ModItem
	{
		public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Ichorous Steak");
			/* Tooltip.SetDefault("'!'\n" +
												  "Use in the Crimson"); */ 
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