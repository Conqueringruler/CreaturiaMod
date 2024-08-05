using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ReLogic.Content;




namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{

	public class BabyLumpFish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Baby Lumpsucker");

		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
							   //Direction = 1, // -1 is left and 1 is right. NPCs are drawn facing the left by default
			Rotation = 110
			//SpriteDirection = 1,
			//Scale = 1f + (float)Main.time / 50 - (MathF.Acos((float)Main.GlobalTimeWrappedHourly / 50)) // I need to figure out how to oscillate their movement
		};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifiers);
		}
		
		int explodesoontimer;
		int exploding;
		public override void SetDefaults()
		{
			NPC.aiStyle = 44;
			NPC.width = 34;
			NPC.height = 66;
			NPC.damage = 30;
			NPC.defense = 6;
			NPC.lifeMax = 400;
			NPC.HitSound = SoundID.NPCHit22;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.value = 0f;
			NPC.knockBackResist = 0.1f;
			//NPC.aiStyle = 44;

			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = NPCID.FlyingFish;

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
				//BestiaryDatabaseNPCsPopulator.CrownosIconIndexes.
				new FlavorTextBestiaryInfoElement("The babies of the Lumpsucker are often so devoted to their mother that they're more than happy to blow themselves up for her.")

			});
		}
		/*public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
				new FlavorTextBestiaryInfoElement("The babies of the Lumpsucker are often so devoted to their mothers that they're more than happy to blow themselves up for her.")
												  
			});
		} */
		int jitter;
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 1400;
			NPC.defense = 25;
		}
	
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;

			if (NPC.position.X > target.position.X)
			{
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//NPC.spriteDirection = -1;

				spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
			}

			if (NPC.position.X < target.position.X)
			{
				//NPC.spriteDirection = 1;
				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

				//SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

			}

			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
			spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/BabyLumpFish_EyeGlow").Value, NPC.Center - screenPos,
			NPC.frame, Color.White, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
			return false;
		}
       
        public override void AI()
		{

			if (Math.Abs(NPC.velocity.X) < 7 && Math.Abs(NPC.velocity.Y) < 7) // I gotta learn what Math.Abs does
			{
				NPC.velocity += new Vector2(NPC.direction * 1.06f, NPC.directionY * 2.5f);
			}




			NPC.spriteDirection = 1;
			Player target = Main.player[NPC.target];

			NPC.FaceTarget();
			NPC.rotation = NPC.AngleTo(target.position);
			Lighting.AddLight(NPC.Center, Color.Yellow.ToVector3() * 0.5f);
			explodesoontimer++;
			if (explodesoontimer > 350)
            {
				exploding++;
				jitter++;
				
			}
			
			if (jitter > 3)
            {
				jitter = 0;
            }
			if (explodesoontimer > 350)
            {
				NPC.scale = 1f + (float)exploding / 50 - (float)jitter / 50;
				NPC.netUpdate = true;
			}
			
			if (exploding > 30) 
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), ProjectileID.GoldenShowerHostile, 25, 0);


					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerHostile, 25, 0);
					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerHostile, 25, 0);
					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerHostile, 25, 0);
					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerHostile, 25, 0);
					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerHostile, 25, 0);
					if (Main.expertMode)
					{
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-5, 6), Main.rand.Next(-5, 6)), ProjectileID.GoldenShowerHostile, 25, 0);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-5, 6), Main.rand.Next(-5, 6)), ProjectileID.GoldenShowerHostile, 25, 0);
					}
					if (Main.masterMode)
					{
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), ProjectileID.GoldenShowerHostile, 25, 0);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), ProjectileID.GoldenShowerHostile, 25, 0);
					}
				}
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 16)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-7, 7), Main.rand.Next(-7, 7));
				
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 6)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));


				SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, NPC.position);
				NPC.life = 0;
			}
		
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Bleeding, 500);

		}
		



	}
}
