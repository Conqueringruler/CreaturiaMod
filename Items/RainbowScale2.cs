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
			 // DisplayName.SetDefault("Rainbow Scales"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("The prismatic colors are mesmerizing!");
		}

		public override void SetDefaults()
		{
			
			Item.width = 26;
			Item.height = 28;
			Item.value = 2000;
			Item.rare = ItemRarityID.Pink;
			Item.material = true;
			Item.maxStack = 999;
			

		}
		public override void PostUpdate()
        {
            float red = (float)Main.DiscoR / 200f;
            float green = (float)Main.DiscoG / 200f;
            float blue = (float)Main.DiscoB / 200f;
            Color ReducedRainbow = Color.Lerp(new Color(red, green, blue), Color.White, 0.75f);
            Item.color = ReducedRainbow;
        }
        public override void UpdateInventory(Player player)
        {
            float red = (float)Main.DiscoR / 200f;
            float green = (float)Main.DiscoG / 200f;
            float blue = (float)Main.DiscoB / 200f;
			Color ReducedRainbow = Color.Lerp(new Color(red, green, blue), Color.White, 0.75f);
			Item.color = ReducedRainbow;
        }
		
	}
}