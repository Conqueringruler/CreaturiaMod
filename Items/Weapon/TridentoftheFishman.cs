using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.DataStructures;
using Creaturia.Buffs;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creaturia.NPCs.Town;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace Creaturia.Items.Weapon
{
	public class TridentoftheFishman : ModItem // PROJECTILE IS IN HERE TOO
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trident of the Fishman"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			/* Tooltip.SetDefault("Chance of summoning ghostly tridents on hitting an enemy\n" +
							" Right-click to activate his blessing. \n" +
							   "Stats increased while wet \n" +
							   "'with goodly trident'"); */
			ItemID.Sets.Spears[Item.type] = true;
			
		}
		
		public override void SetDefaults()
		{
			
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 22; //time in ticks (60 ticks == 1 second.)
			Item.useTime = 22;
			Item.knockBack = 2.5f;
			Item.width = 32;
			Item.height = 32;
			Item.damage = 95;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<TridentProjectile>();
			Item.shootSpeed = 4f; // The speed of the projectile measured in pixels per frame.
			Item.UseSound = SoundID.Item1; // The sound that this item makes when used
			Item.rare = ItemRarityID.LightRed; // The color of the name of your item
			Item.value = Item.sellPrice(gold: 10, silver: 50);
			Item.DamageType = DamageClass.Melee; // Deals melee damage
			//Item.channel = true;
			Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
			
		}
		private int rightclickbufftimer;
		private bool rightclickplayedsound;
		private bool rightclickready;
        public override void UpdateInventory(Player player)
        {
			if (player.inventory[player.selectedItem].type == ModContent.ItemType<TridentoftheFishman>() && (!player.mount.Active || !player.mount.Cart) && player.wet)
			{
				player.trident = true;
			}
			if (rightclickbufftimer < 1999) // Confusing myself with this convoluted system lol
			{
				rightclickplayedsound = false;
			}
			if (rightclickbufftimer < 2002)
            {
				rightclickbufftimer++;
			}
			if (rightclickbufftimer > 2000)
            {
				
				rightclickready = true;
				

			}
			if (rightclickready == true && rightclickplayedsound == false)
            {
				SoundEngine.PlaySound(SoundID.Item9, player.position);
				CombatText.NewText(player.Hitbox, Color.LightBlue, "Fishman's Blessing is ready!");
				var dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.WaterCandle, player.velocity.X * Main.rand.Next(-2, 2), player.velocity.Y * Main.rand.Next(-2, 2), 60, Color.Gold, Main.rand.NextFloat(0.3f, 1.5f));

				for (int i = 0; i < 10; i++)
				{
					dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.WaterCandle, player.velocity.X * Main.rand.Next(-2, 2), player.velocity.Y * Main.rand.Next(-2, 2), 60, Color.Gold, Main.rand.NextFloat(0.3f, 1.5f));
				}


				rightclickplayedsound = true;
            }
		}
        public override bool AltFunctionUse(Player player)
        {
			return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			
			if (player.altFunctionUse == 2 && rightclickbufftimer > 2000)
			{
				player.AddBuff(ModContent.BuffType<FishmanBuff>(), 1050);
				SoundEngine.PlaySound(SoundID.Item119, player.position);
				rightclickbufftimer = 0;
				
				rightclickready = false;
				return false;
			}
            else
            {
				return true;
				//Projectile.NewProjectile(source, position, velocity, type, (int)(damage), (int)(knockback), player.whoAmI, player.altFunctionUse == 2 ? 1 : 0);
			}
			
			
        }
        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2 && rightclickbufftimer < 1200)
			{
				return false;
			}
			// Ensures no more than one spear can be thrown out, use this when using autoReuse. Also remember I can use this for other weapons where I only want a few projectiles out at a time.
			return player.ownedProjectileCounts[Item.shoot] < 1;
			
		}

        public override void PostUpdate()
        {
			Lighting.AddLight(Item.Center, Color.BlueViolet.ToVector3() * 0.55f * Main.essScale); // I have no idea what essScale means, but since the pros use it I'll just go with it.
																								  // Plus looking it up all I see is the Epworth Sleepiness Scale
		}

    }
	public class TridentProjectile : ModProjectile
	{
		
		protected virtual float HoldoutRangeMin => 12f;
		protected virtual float HoldoutRangeMax => 155f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trident of the Fishman");
		}
		private int DustTimer;
		public override void SetDefaults()
		{
			Projectile.damage = 10;
			AIType = ProjectileID.Gungnir;
			Projectile.CloneDefaults(ProjectileID.Spear);
			
			Projectile.friendly = true;
		}





		// My own spear code wasn't working great & was messy, so I'm gonna base it off of examplemod and then alter whatever I need to
        public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner]; // making a variable for player to simplify stuff
			int duration = player.itemAnimationMax; 

			player.heldProj = Projectile.whoAmI; 

			
			if (Projectile.timeLeft > duration)
			{
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

			float halfDuration = duration * 0.5f;
			float progress;

			// Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
			if (Projectile.timeLeft < halfDuration)
			{
				progress = Projectile.timeLeft / halfDuration;
			}
			else
			{
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			// Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

			// Apply proper rotation to the sprite.
			if (Projectile.spriteDirection == -1)
			{
				// If sprite is facing left, rotate 45 degrees
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else
			{
				// If sprite is facing right, rotate 135 degrees
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			// Avoid spawning dusts on dedicated servers
			if (!Main.dedServ)
			{
				
				if (Main.rand.NextBool(3))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WaterCandle, Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1f);
				}

				if (Main.rand.NextBool(4))
				{
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WaterCandle, Alpha: 128, Scale: 0.3f);
				}
			}

			return false; // Don't execute vanilla AI.
		}



		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.NextBool(12))
			{
				int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 3f, ModContent.ProjectileType<TridentGhostProjectile>(), 120, 0f, Projectile.owner);
			}
		}

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, Color.Violet.ToVector3() * 0.1f);
			DustTimer++;
			if (DustTimer > 15)
			{

			var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0, 60, Color.Blue, 1f);
			dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 - 10, Projectile.velocity.Y * 0.1f, 60, Color.Blue, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 2, Projectile.velocity.Y * 0 + 2, 60, Color.Blue, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 2, Projectile.velocity.Y * 0.1f - 2, 60, Color.Blue, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 5, Projectile.velocity.Y * 0, 60, Color.Blue, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 2, Projectile.velocity.Y * 0.1f + 8, 60, Color.Blue, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0, Projectile.velocity.Y * 0 - 10, 60, Color.Blue, 1f);
				//int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity, ProjectileID.Shuriken, 10, Projectile.knockBack, Projectile.owner);
				//projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 1.3f, ProjectileID.Shuriken, 10, Projectile.knockBack, Projectile.owner);
				//projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 0.8f, ProjectileID.Shuriken, 10, Projectile.knockBack, Projectile.owner);
				DustTimer = 0;
				//	Projectile.active = false;

			}


		}


		public class TridentGhostProjectile : ModProjectile
		{
			public override void SetStaticDefaults()
			{
				// DisplayName.SetDefault("Trident of the Fishman");
			}
			private int FadeTimer;
			private int dusttimer;
			public override void SetDefaults()
			{
				AIType = ProjectileID.MagnetSphereBall;
				
				Projectile.friendly = true;
				Projectile.ignoreWater = true;
				Projectile.tileCollide = false;
				Projectile.scale = 3f;
				Projectile.damage = 160;
				Projectile.penetrate = 8;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}
			
			public override void AI()
			{
				// The particles right below are taken straight from terraria source code lol
				if (Main.player[Projectile.owner].wet && Main.rand.NextBool(2))
				{
					int num10 = Dust.NewDust(Main.player[Projectile.owner].position, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height, DustID.MagicMirror, 0f, 0f, 100, default(Color), 0.8f);
					Main.dust[num10].velocity *= 0.1f;
				}

				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
				dusttimer++;
				if (dusttimer > 20)
                {
					for (int i = 0; i < 5; i++)
                    {
					var dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * -0.9f, Projectile.velocity.Y * -0.9f, 60, Color.Violet, Main.rand.NextFloat(0.3f, 1.1f));
                    }
					dusttimer = 0;
				}


				Projectile.velocity.X *= 0.995f;
				Projectile.velocity.Y *= 0.995f;

				Lighting.AddLight(Projectile.Center, Color.Violet.ToVector3() * 0.75f);
				FadeTimer++;
				if (FadeTimer > 350)
                {
					var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.WaterCandle, Projectile.velocity.X * 0.1f - 2, Projectile.velocity.Y * 0 + 2, 60, Color.Violet, 1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 2, Projectile.velocity.Y * 0.1f - 2, 60, Color.Violet, 1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 5, Projectile.velocity.Y * 0, 60, Color.Violet, 1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 - 2, Projectile.velocity.Y * 0.1f - 2, 60, Color.Violet, 0.8f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 - 5, Projectile.velocity.Y * 0, 60, Color.Violet, 1.1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f + 1, Projectile.velocity.Y * 0.1f - 2, 60, Color.Violet, 1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f + 6, Projectile.velocity.Y * 0, 60, Color.Violet, 1f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 1, Projectile.velocity.Y * 0.1f - 2, 60, Color.Violet, 0.8f);
					dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 6, Projectile.velocity.Y * 0, 60, Color.Violet, 1.1f);

					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * Main.rand.Next(-6, 6), Projectile.velocity.Y * Main.rand.Next(-6, 6), 60, Color.Violet, Main.rand.NextFloat(0.3f, 1.5f));
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * Main.rand.Next(-6, 6), Projectile.velocity.Y * Main.rand.Next(-6, 6), 60, Color.Violet, Main.rand.NextFloat(0.3f, 1.5f));
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * Main.rand.Next(-6, 6), Projectile.velocity.Y * Main.rand.Next(-6, 6), 60, Color.Violet, Main.rand.NextFloat(0.3f, 1.5f));
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * Main.rand.Next(-6, 6), Projectile.velocity.Y * Main.rand.Next(-6, 6), 60, Color.Violet, Main.rand.NextFloat(0.3f, 1.5f));
					SoundEngine.PlaySound(SoundID.Item109, Projectile.position);
					FadeTimer = 0;
					Projectile.active = false;
                }
				//new Color(150, 100, 70, 0) * (0.7f + 0.4f * ((255 - Projectile.alpha) / 255f));


			}
           /** public override bool PreDraw(ref Color lightColor)
            {
                return base.PreDraw(ref lightColor);
            } */
            public override Color? GetAlpha(Color lightColor)
			{
				return new Color(128, 0, 158, 0) * (0.5f + 0.5f * ((200 - Projectile.alpha - FadeTimer) / 255f));

			}
		}
	}
}