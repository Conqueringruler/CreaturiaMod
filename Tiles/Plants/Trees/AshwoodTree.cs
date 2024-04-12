
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Creaturia.Tiles.Plants.Trees
{
	public class AshwoodTree : ModTree
	{
		private Mod mod => ModLoader.GetMod("Creaturia");


		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
		{
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
			
		};
		
		public override void SetStaticDefaults()
		{
			// Makes Example Tree grow on ExampleBlock
			GrowsOnTileId = new int[1] { ModContent.TileType<SinfulDirt>() };
		}
		
		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<AshwoodSapling>();
		}

		public override int DropWood()
		{
			return ItemID.AshBlock;
		}
		public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
		{
			// This is where fancy code could go, but let's save that for an advanced example
		}
		
		public override bool Shake(int x, int y, ref bool createLeaves)
		{
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ItemID.Hellstone) ;
			return false;
		}
		public override Asset<Texture2D> GetTexture()
		{
			return ModContent.Request<Texture2D>("Creaturia/Tiles/Plants/Trees/AshwoodTree");
		}

		public override Asset<Texture2D> GetTopTextures()
		
			{
			return ModContent.Request<Texture2D>("Creaturia/Tiles/Plants/Trees/AshwoodTree_Tops");

		}

		public override Asset<Texture2D> GetBranchTextures()
		{
			return ModContent.Request<Texture2D>("Creaturia/Tiles/Plants/Trees/AshwoodTree_Branches");
		}
		
		public override bool CanDropAcorn()
		{
			return true;
		}
		public override int TreeLeaf()
		{
			return GoreID.TreeLeaf_GemTreeRuby;
		}

	}
}