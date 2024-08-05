using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace Creaturia.Items
{
	public class BlankItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault(""); 
			// Tooltip.SetDefault("Lore on this item: basically I just needed an item with a blank sprite for rendering the Spectral Watchman's life values");
		}

		public override void SetDefaults()
		{

			Item.width = 30;
			Item.height = 34;
			Item.value = 2000;
			Item.rare = ItemRarityID.Purple;
			Item.material = true;
			Item.maxStack =1;


		}
		public override void PostUpdate()
		{
		//	Item.color = Main.DiscoColor;
		}
		

	}
}