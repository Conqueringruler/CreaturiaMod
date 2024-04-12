using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

using Terraria.GameContent.Creative;

using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace Creaturia.Items.Consumables.Fishing
{
	public class GoldenFishDevBait : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.GoldWorm;
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Magic Worm");
			Tooltip.SetDefault("'An aura of power radiates, but not in an intimidating way' \n" +
								"Use in the Ocean");
		}
		Rectangle frame;
		public override void SetDefaults()
		{
			Item.width = Item.height = 8;
			Item.rare = ItemRarityID.Green;
			Item.maxStack = 9;
			Item.consumable = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.bait = 1;
			Item.color = Color.Gold;
		}
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
			// new Color(252, 190, 30, 1) * (0.7f + 0.4f * ((25 - Item.alpha) / 255f))

			Texture2D texture = TextureAssets.Item[ItemID.GoldWorm].Value;
			frame = texture.Frame();
			Vector2 frameOrigin = frame.Size() / 2f;
			// I'll see if the Pumpking Golem Fist color goes good with this
			spriteBatch.Draw(texture, Item.position - Main.screenPosition + new Vector2(8, 6), frame, new Color(252, 190, 30, 1) * (0.7f + 0.4f * ((255 - Item.alpha) / 255f)), rotation, frameOrigin, scale, SpriteEffects.None, 1);

			spriteBatch.Draw(texture, Item.position - Main.screenPosition + new Vector2(8, 6), frame, new Color(252, 190, 30, 1) * (0.7f + 0.4f * ((255 - Item.alpha) / 255f)), rotation, frameOrigin, scale, SpriteEffects.None, 1);
			return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			Texture2D texture = TextureAssets.Item[ItemID.GoldWorm].Value;
			frame = texture.Frame();
			Vector2 frameOrigin = frame.Size() / 2f;
			// I'll see if the Pumpking Golem Fist color goes good with this
			spriteBatch.Draw(texture, position + new Vector2(9f, 8f), frame, new Color(252, 190, 30, 10) * (1.7f + 0.4f * ((0 - Item.alpha) / 255f)), default, frameOrigin, scale, SpriteEffects.None, 1);
			spriteBatch.Draw(texture, position + new Vector2(9f, 8f), frame, Color.Gold * ((200 - Item.alpha) / 255f), default, frameOrigin, scale, SpriteEffects.None, 1);
			spriteBatch.Draw(texture, position + new Vector2(9f, 8f), frame, new Color(252, 190, 30, 10) * (1.7f + 0.4f * ((0 - Item.alpha) / 255f)), default, frameOrigin, scale, SpriteEffects.None, 1);

			//			spriteBatch.Draw(texture, position + new Vector2(9f, 8f), frame, new Color(252, 190, 30, 1) * (0.7f + 0.4f * ((255 - Item.alpha) / 255f)), default, frameOrigin, scale, SpriteEffects.None, 1);
			return false;
		}





	}
}