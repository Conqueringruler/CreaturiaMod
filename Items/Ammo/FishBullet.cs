using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace Creaturia.Items.Ammo
{
	public class FishBullet : ModItem
	{

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
			// DisplayName.SetDefault("Fish Bullet");
			/* Tooltip.SetDefault("Transforms into a fish when in water or rain! \n" +
							   "When transformed +5% chance to critical hit \n" +
							   "When transformed has much higher knockback \n" +
							   "When transformed targets enemies"); */
                           //    "When transformed can penetrate once");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 0.2f;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ModContent.ProjectileType<FishBulletProj>();
			Item.shootSpeed = 7f;
			Item.ammo = AmmoID.Bullet;
		}

		
		

	}

	internal class FishBulletProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fish Bullet");
			
			Main.projFrames[Projectile.type] = 3;

		}

		public override void SetDefaults()
		{

			Projectile.width = 10; 
			Projectile.height = 3;
			Projectile.aiStyle = 1; 
			Projectile.friendly = true; 
			Projectile.hostile = false; 
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1; 
			Projectile.timeLeft = 600; 
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1; // I'm assuming I want more updates per frame because of how fast the bullets are?

			AIType = ProjectileID.Bullet;
		}
		bool FishMode = false;

		int AmountToCharge = 0;
		int WaterDustTimer;
		bool ActivateOnSpawnStuff = true;
        public override void AI()
        {
			
			if (ActivateOnSpawnStuff)
            {
				Projectile.velocity /= 1.7f;
				ActivateOnSpawnStuff = false;
            }
			Player player = Main.LocalPlayer;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Collision.WetCollision(Projectile.position, Projectile.width, Projectile.height))
            {
				FishMode = true;
            }
			if (Main.raining && player.ZoneOverworldHeight)
            {
				FishMode = true;
            }
			if (!FishMode)
            {
				Projectile.frame = 0;
			}
			if (FishMode)
            {
				Projectile.aiStyle = 0;

				if (AmountToCharge < 5)
                {
				AmountToCharge++;
				
				//Projectile.penetrate = 2;
				Projectile.CritChance += 1;
					Projectile.knockBack = 1.5f;
					//Projectile.velocity *= 1.01f;
                }

				WaterDustTimer++;
				if (WaterDustTimer > 5)
                {
					var dust = Dust.NewDustDirect(Projectile.Center, Projectile.width + Main.rand.Next(-5, 5), Projectile.height + Main.rand.Next(-5, 5), DustID.Water, Projectile.velocity.X, Projectile.velocity.Y, 250, Color.White, 0.8f);
					dust.velocity.Y /= 20;
				}

				if (++Projectile.frameCounter >= 3)
				{
					Projectile.frameCounter = 0;
					if (++Projectile.frame >= Main.projFrames[Projectile.type])
					{
						Projectile.frame = 1;
					}
				}

				NPC closestNPC = FindClosestNPC(500f);
				if (closestNPC == null)
					return;

				// If found, change the velocity of the projectile and turn it in the direction of the target
				// Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
				// idk what NaNs are but thanks G
				Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 6.5f;
				Projectile.rotation = Projectile.velocity.ToRotation();

			}
            
		}
		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy())
				{
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
		public override bool PreDraw(ref Color lightColor)
        {

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			SpriteEffects spriteEffects = SpriteEffects.None;

			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			Vector2 drawPos = (Projectile.position - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);

			/*	Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.White, Projectile.rotation,
				new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), Projectile.scale, spriteEffects, 0); */

			Main.EntitySpriteDraw(texture, drawPos, sourceRectangle, Color.White, Projectile.rotation, drawOrigin, Projectile.scale, spriteEffects, 0);
			
			return false;
        }

		public override void OnKill(int timeLeft)
		{
			
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}

}

