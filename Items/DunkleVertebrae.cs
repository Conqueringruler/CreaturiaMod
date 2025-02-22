using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class DunkleVertebrae : ModItem
	{
		public override void SetStaticDefaults()
		{
			 // DisplayName.SetDefault("Dunkle Vertebrae"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("'The density of the bone makes it nearly unbreakable'");
		}

		public override void SetDefaults()
		{
			
			Item.width = 16;
			Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            Item.rare = ItemRarityID.Pink;
			Item.material = true;
			Item.maxStack = 99;
			

		}
		
		
	}
}