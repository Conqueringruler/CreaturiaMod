using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;

namespace Creaturia.Tiles.Trees
{
	public class plsworkorIcry : ModCactus
	{
		public override void SetStaticDefaults()
		{
			// Makes Example Cactus grow on ExampleOre
			GrowsOnTileId = new int[1] { TileID.ArgonMossBrick };
		}

		public override Asset<Texture2D> GetTexture()
		{
			return ModContent.Request<Texture2D>("Creaturia/Tiles/Plants/Trees/plsworkorIcry");
		}

		// This would be where the Cactus Fruit Texture would go, if we had one.
		public override Asset<Texture2D> GetFruitTexture()
		{
			return null;
		}
	}
}