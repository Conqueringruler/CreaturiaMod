using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class RainbowScale2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("Rainbow Scale"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The prismatic colors could definitely fetch a high price\n" +
									"Can be traded with the Fishman");
		}

		public override void SetDefaults()
		{
			
			Item.width = 20;
			Item.height = 18;
			Item.value = 2000;
			Item.rare = ItemRarityID.Pink;
			Item.material = true;
			Item.maxStack = 999;
			

		}
		public override void PostUpdate()
        {
			Item.color = Main.DiscoColor;
        }
        public override void UpdateInventory(Player player)
        {
			Item.color = Main.DiscoColor;
		}
		
	}
}