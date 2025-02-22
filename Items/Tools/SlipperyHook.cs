using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Creaturia.Items;
using Creaturia.NPCs.Creatures;
using Creaturia.Buffs;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent;
using System;
using Terraria.GameContent.ItemDropRules;

using System.Collections.Generic;
using Terraria.Graphics.Shaders;

namespace Creaturia.Items.Tools
{
	internal class SlipperyHook : ModItem // Don't forget SlipperyBuff and the hook proj are also in this file
	{ // REMINDER: Need to change the Slippery Hook sprites!
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slippery Hook");
			/* Tooltip.SetDefault("50% chance to dodge attacks while being dragged by the hook'\n" +
				"'There's slime everywhere!'"); */

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // Reminder that I need to do this with every other item
		}

		public override void SetDefaults()
		{
									// Amethyst Hook has the worst stats in the game, so I should use that as a base to upgrade off of
			Item.CloneDefaults(ItemID.AmethystHook);
			Item.value = Item.sellPrice(0, 2, 50, 0);
			Item.shootSpeed = 11.5f; //how quickly the hook is shot
			Item.shoot = ModContent.ProjectileType<SlipperyHookProj>(); 
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<SlipperySlugItem>(5)
				.AddIngredient(ItemID.Hook)
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe()
				.AddIngredient<SlipperySlugItem>(4)
				.AddIngredient(ItemID.GrapplingHook)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

	internal class SlipperyHookProj : ModProjectile
	{
		private static Asset<Texture2D> chainTexture;

		public override void Load()
		{ 
		  // I probably want to use a vanilla texture w/ some changes
			chainTexture = ModContent.Request<Texture2D>("Creaturia/Items/Tools/SlipperyHookChain");
		}

		public override void Unload()
		{ 
		  // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
		  // ^^^^^^
			chainTexture = null;
		}
        public override void AI()
        {

		if (Projectile.velocity == Vector2.Zero)
            {
				if (Math.Abs(Main.player[Main.myPlayer].velocity.X) > 0 && Math.Abs(Main.player[Main.myPlayer].velocity.Y) > 0)
				{
					Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<SlipperyBuff>(), 2);
				}
			}
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = Projectile.Center;
			if (Math.Abs(Projectile.velocity.X) > 0 || Math.Abs(Projectile.velocity.Y) > 0)
            {
				if (Main.rand.NextBool(2))
                {
					var dust = Dust.NewDustDirect(Projectile.Center, Projectile.width + Main.rand.Next(-5, 5), Projectile.height + Main.rand.Next(-5, 5), DustID.Water, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.DarkGray, 1);
					dust.velocity.Y /= 20;
					//dust.color = new Color(180, 180, 180);
					dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(55, Main.LocalPlayer);
                }
				
			}
		}
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slippery Hook");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Hook);
		}

		// Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
		public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
				{
					hooksOut++;
					Player owner = Main.LocalPlayer;
				}
			}

			return hooksOut <= 0;
		}

		// Return true if it is like: Hook, CandyCaneHook, BatHook, GemHooks
		// public override bool? SingleGrappleHook(Player player)
		// {
		//	return true;
		// }

		// Use this to kill oldest hook. For hooks that kill the oldest when shot, not when the newest latches on: Like SkeletronHand
		// You can also change the projectile like: Dual Hook, Lunar Hook
		// public override void UseGrapple(Player player, ref int type)
		// {
		//	int hooksOut = 0;
		//	int oldestHookIndex = -1;
		//	int oldestHookTimeLeft = 100000;
		//	for (int i = 0; i < 1000; i++)
		//	{
		//		if (Main.projectile[i].active && Main.projectile[i].owner == projectile.whoAmI && Main.projectile[i].type == projectile.type)
		//		{
		//			hooksOut++;
		//			if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
		//			{
		//				oldestHookIndex = i;
		//				oldestHookTimeLeft = Main.projectile[i].timeLeft;
		//			}
		//		}
		//	}
		//	if (hooksOut > 1)
		//	{
		//		Main.projectile[oldestHookIndex].Kill();
		//	}
		// }

		// Amethyst Hook is 300, Static Hook is 600.
		public override float GrappleRange()
		{
			return 400f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 1;
		}

		// default is 11, Lunar is 24
		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 16f; // How fast the grapple returns to you after meeting its max shoot distance
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 9; // How fast you get pulled to the grappling hook projectile's landing position
		}

		// Adjusts the position that the player will be pulled towards. This will make them hang 50 pixels away from the tile being grappled.
		public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
		{
			Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
			float hangDist = 20f;
			grappleX += dirToPlayer.X * hangDist;
			grappleY += dirToPlayer.Y * hangDist;
		}

		// Draws the grappling hook's chain.
		public override bool PreDrawExtras()
		{
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 directionToPlayer = playerCenter - Projectile.Center;
			float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
			float distanceToPlayer = directionToPlayer.Length();

			while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
			{
				directionToPlayer /= distanceToPlayer; // get unit vector
				directionToPlayer *= chainTexture.Height(); // multiply by chain link length

				center += directionToPlayer; // update draw position
				directionToPlayer = playerCenter - center; // update distance
				distanceToPlayer = directionToPlayer.Length();

				//Color drawColor = (Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16))); // It's kinda funny the only thing the example hook didn't explain was why the draw color was using lighting, when that's
				// The only thing I don't understand.

				Color drawColor = new Color(255, 255, 255);
				// Draw chain
				Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
					chainTexture.Value.Bounds, drawColor, chainRotation,
					chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			}
			// Stop vanilla from drawing the default chain.
			return false;
		}
	}
}