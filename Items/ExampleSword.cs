using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creaturia.Items
{
	public class ExampleSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// I might as well leave 
			Tooltip.SetDefault("Every mod needs to start with the Example Sword.");
		}

		public override void SetDefaults() 
		{
			Item.damage = 1;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Guitar;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}
		
		//public override void AddRecipes() 
		//{
		//	Recipe recipe = CreateRecipe();
			//recipe.AddIngredient(ItemID.DirtBlock, 10);
		//	recipe.AddTile(TileID.WorkBenches);
		//	recipe.Register();
		//}
	}
}