using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Creaturia.Items.Ammo;

namespace Creaturia.Items.Consumables
{
	// Basic code for a boss treasure bag
	public class BundleOfFishBullets : ModItem
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bundle of Fish Bullets");
			/* Tooltip.SetDefault("Contains 10-20 Fish Bullets \n" +
						"{$CommonItemTooltip.RightClickToOpen}"); */ // References a language key that says "Right Click To Open" in the language of the game

		

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2; // Reminder to do this for other items
		}

		public override void SetDefaults()
		{
			Item.maxStack = 99;
			Item.consumable = true;
			Item.width = 24;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.height = 24;
			Item.rare = ItemRarityID.Green;
			
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{

			itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FishBullet>(), 1, 10, 20));
		}

		
		
		
		
		public override void PostUpdate()
		{
			// Spawn some light and dust when dropped in the world


			//if (Item.timeSinceItemSpawned % 12 == 0)
			//{
			//Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);


			//Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
			//float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
			//Vector2 velocity = new Vector2(0f, Main.rand.NextFloat() * 0.3f - 1.5f);
			
			//}
		}

		
	}
}