using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class AncientDartGunPieces : ModItem
	{
		public override void SetStaticDefaults()
		{
			 DisplayName.SetDefault("Ancient Dart Gun Pieces"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			
		}

		public override void SetDefaults()
		{
			
			Item.width = 44;
			Item.height = 24;
			Item.value = 2000;
			Item.rare = ItemRarityID.Blue;
			Item.material = true;
			Item.maxStack = 1;
			

		}
		
		
	}
}