using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class HellborneIcon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hellborne Skull & Golem are weak to this weapon!"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			/* Tooltip.SetDefault("I wonder if\n" +
									"this tooltip will be there too"); */
		}

		public override void SetDefaults()
		{

			Item.width = 30;
			Item.height = 34;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.material = true;
			Item.maxStack =1;


		}
		public override void PostUpdate()
		{
		//	Item.color = Main.DiscoColor;
		}
		

	}
}