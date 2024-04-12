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
	public class HallowBait : ModItem
	{
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Glistening Lolly");
			Tooltip.SetDefault("'Lolly is short for lollipop. I won't let you degenerates ruin this candy' \n" +
												  "Use in the Hallow");

		}

		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.maxStack = 9;
			Item.consumable = true;
			Item.value = Item.sellPrice(0, 15, 0, 0);
			Item.bait = 1;
			
		}
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			Item.color = Main.DiscoColor;
        }
        public override void UpdateInventory(Player player)
        {
			Item.color = Main.DiscoColor;
		}

    }
}