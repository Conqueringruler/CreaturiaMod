using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Creaturia.Currencies;
using Creaturia.NPCs.Creatures;
using Creaturia.Common.Systems;

namespace Creaturia.Items.GlobalItems
{
	internal class ItemRecipes : ModSystem
	{


		
			public override void AddRecipes()
			{
			Recipe recipe = Recipe.Create(ItemID.MagicPowerPotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Moonglow, 2);
			recipe.AddIngredient(ModContent.ItemType<RainbowScale2>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

			 recipe = Recipe.Create(ItemID.ManaRegenerationPotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Daybloom, 2);
			recipe.AddIngredient(ModContent.ItemType<RainbowScale2>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

			



			recipe = Recipe.Create(ItemID.HallowedKey);

			recipe.AddIngredient(ModContent.ItemType<RainbowScale2>(), 10);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.TempleKey, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

			recipe = Recipe.Create(ItemID.WrathPotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddIngredient(ModContent.ItemType<LumpsuckerHeart>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

			recipe = Recipe.Create(ItemID.RagePotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Deathweed, 2);
			recipe.AddIngredient(ModContent.ItemType<LumpsuckerHeart>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

			recipe = Recipe.Create(ItemID.CrimsonKey);

			recipe.AddIngredient(ModContent.ItemType<LumpsuckerHeart>(), 10);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.TempleKey, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();




			recipe = Recipe.Create(ItemID.EndurancePotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.RottenChunk, 5);
			recipe.AddIngredient(ModContent.ItemType<DunkleVertebrae>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

			recipe = Recipe.Create(ItemID.ThornsPotion, 3);

			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Cactus, 5);
			recipe.AddIngredient(ModContent.ItemType<DunkleVertebrae>(), 2);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();

            recipe = Recipe.Create(ItemID.CorruptionKey);

            recipe.AddIngredient(ModContent.ItemType<DunkleVertebrae>(), 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.TempleKey, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();





            recipe = Recipe.Create(ItemID.BunnyStew);

            recipe.AddIngredient(ModContent.ItemType<JackrabbitItem>(), 10);
            recipe.AddTile(TileID.CookingPots);
			recipe.Register();

			recipe = Recipe.Create(ItemID.RoastedBird);

            recipe.AddRecipeGroup("Hummingbirds", 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
			

            /*	recipe = Recipe.Create(ItemID.CorruptionKey);

				recipe.AddIngredient(ModContent.ItemType<>(), 10);
				recipe.AddIngredient(ItemID.SoulofLight, 5);
				recipe.AddIngredient(ItemID.JungleKey, 1);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.Register(); */

            /*
			 * 
			recipe = Recipe.Create(ItemID.SoulofLight, 1);
			
			recipe.AddIngredient(ModContent.ItemType<RainbowScale2>(), 2);
			recipe.AddTile(TileID.CrystalBall);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();

			recipe = Recipe.Create(ItemID.SoulofNight, 1);

			recipe.AddIngredient(ModContent.ItemType<RainbowScale2>(), 2);
			recipe.AddTile(TileID.CrystalBall);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register(); */
        }

			
				
	}
}