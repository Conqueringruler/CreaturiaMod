using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class LumpsuckerHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("Ichorous Spines"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'Even though the spine is razor clean the smell is still atrocious");
		}

		public override void SetDefaults()
		{
			
			Item.width = 32;
			Item.height = 38;
			Item.value = 2000;
			Item.rare = ItemRarityID.Pink;
			Item.material = true;
			Item.maxStack = 99;
			

		}
		
		
	}
}