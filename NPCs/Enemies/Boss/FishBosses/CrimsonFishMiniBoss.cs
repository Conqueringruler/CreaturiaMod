using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

using System.Collections.Generic;
using Terraria.Graphics.CameraModifiers; // I wanna check out Camera Modifiers cause that sounds cool, maybe for corruption fish


namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{
	internal class CrimsonFishMiniBoss : ModNPC
	{


		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Lumpsucker");

			NPCID.Sets.TrailCacheLength[NPC.type] = 10; //Higher numbers mean longer trails
			NPCID.Sets.TrailingMode[NPC.type] = 0;


			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Bleeding,
					BuffID.Ichor,
					BuffID.WeaponImbueIchor,
					BuffID.CursedInferno,
					BuffID.Confused 
				}
			};

			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Creaturia/NPCs/Enemies/Boss/FishBosses/Lumpsucker_Bestiary",
				//Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				//Direction = 1, // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
				//SpriteDirection = 1
				PortraitScale = 0.30f,
				Scale = 0.15f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifiers);
		}
		

		int startsprayattacktimer;
		bool startsprayattack = false;
		int sprayingtime;
		int timeuntilsprayends;
		bool playsoundforichorspam = false;
		bool didsound = false;


		int babyshootingtimer;

		bool startshootingoutbabies = false;

		int shootingoutbabies;

		int shoottimelengthforbabies;

		int slowlybigger = 1;
		int slowlysmaller;
		int slowlytomid;
		public override void SetDefaults()
		{

			NPC.width = 76;
			NPC.height = 74; // Change ALL defaults 
			NPC.damage = 50;
			NPC.defense = 25;
			NPC.lifeMax = 7500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 500;
			NPC.lavaImmune = false;
			NPC.aiStyle = 2;
			NPC.noGravity = true;
			NPC.friendly = false;
			NPC.despawnEncouraged = false;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.npcSlots = 10f;
			NPC.value = Item.buyPrice(gold: 4, silver: 80);

			NPC.boss = true;
			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/underwater_fishy_terrariaminiboss");
			}
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

	
		


		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{

				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			// new CommonDrop(int itemId, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1)
			//new CommonDrop(ItemID.Torch, 5, 10, 15, 2); Drop a stack of 10 to 15 torches with 2 in 5 chance (40% chance), also this new method sucks
			//npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 4, 8));
			//if (Main.rand.NextBool(5))
			//{
				//	Item.NewItem(NPC.getRect(), ItemID.Leather);
				npcLoot.Add(ItemDropRule.Common(ItemID.CrimsonKey, 5, 1, 1));
			//}

			var parameters = new DropOneByOne.Parameters()
			{
				ChanceNumerator = 1,
				ChanceDenominator = 1,
				MinimumStackPerChunkBase = 1,
				MaximumStackPerChunkBase = 1,
				MinimumItemDropsCount = 6,
				MaximumItemDropsCount = 130,
			};

			//new DropOneByOne(ItemID.SoulofNight, parameters);
			npcLoot.Add(new DropOneByOne(ItemID.SoulofNight, parameters));
			npcLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.PirateShip));

		}




		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
				//BestiaryDatabaseNPCsPopulator.CrownosIconIndexes.
				new FlavorTextBestiaryInfoElement("The great ichorous beast of the Crimson waters," +
												  " vicious and unforgiving. The babies of the Lumpsucker are often so devoted to their mothers that they're more than happy to blow themselves up for her.")
				
			});
		}
        public override bool PreKill()
        {
            return base.PreKill();
        }

        int spawndust;
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			if (Main.expertMode || Main.masterMode)
			{
				target.AddBuff(BuffID.Bleeding, 90, true, false);
			}
		}

		/*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		  {
			  Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			  Player target = Main.player[NPC.target];
			  //NPC.spriteDirection = -1;
			  SpriteEffects spriteEffects = SpriteEffects.None;
			  //SpriteEffects effects = SpriteEffects.None;

			  //SpriteEffects spriteEffects = (NPC.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.None;
				  //spriteEffects = (NPC.spriteDirection == 2) ? SpriteEffects.FlipVertically : SpriteEffects.FlipHorizontally;
			  //effects = SpriteEffects.FlipHorizontally;
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
			  //int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
			  //int startY = frameHeight * 1;
			  //Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

			  //Vector2 origin = sourceRectangle.Size() / 2f;
			  Vector2 Offset = new Vector2(35, 35);
			  spriteBatch.Draw(texture, NPC.Center - screenPos + Offset, null, NPC.GetAlpha(drawColor), NPC.rotation, new Vector2(NPC.width, NPC.height), NPC.scale, spriteEffects, 0);

			  return base.PreDraw(spriteBatch, screenPos, drawColor);


		  }  */

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

				for (int i = 0; i < 4; i++)
				{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
					Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2); 
					spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(150, 100, 70, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
				}


				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
				NPC.frame, drawColor, NPC.rotation,
				new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

				spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/CrimsonFishMiniBoss_EyeGlow").Value, NPC.Center - screenPos,
				NPC.frame, Color.White, NPC.rotation,
				new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
				return false;
		}
			
		
		public override void AI()
        {
			if (slowlybigger >= 1) // if 1 or bigger then get bigger
            {
				slowlybigger++;
				slowlytomid = 0;
            }
			if (slowlybigger > 107) // higher than 107 then stop, start slowlysmaller 
            {
				slowlybigger = -1;
				slowlytomid = 107;
            }
			if (slowlytomid <= 107) // starts at 107, shrinks until -105 
			{
				slowlytomid--;
			}
			if (slowlytomid < 0) // once lower than -105 then it stops and starts going to mid
			{
				slowlytomid = 0;
				slowlybigger = 1;
			}

			if (!Main.expertMode && !Main.masterMode)
            {
				shoottimelengthforbabies = 1700;
            }
			if (Main.expertMode)
            {
				shoottimelengthforbabies = 1800;
            }
			if (Main.masterMode)
            {
				shoottimelengthforbabies = 1900;
            }
			Player target = Main.player[NPC.target];
			NPC.FaceTarget();
			NPC.rotation = NPC.AngleTo(target.position);

			if (!target.active || target.dead)
			{
				NPC.velocity = new Vector2(0f, -10f);
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
				return;
			}

			if (spawndust > 15)
			{
				for (int i = 0; i < Main.rand.Next(1, 5); i++)
				{
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10)), 4, 4, DustID.Blood, NPC.velocity.X, NPC.velocity.Y, 30);
				}
				spawndust = 0;
			}


			
				//	else if (NPC.position.Y < target.position.Y)
				// {
				//NPC.spriteDirection = NPC.direction = (NPC.velocity.X > 0).ToDirectionInt();
				//NPC.spriteDirection = 1;
			
			Vector2 directionshoot = (target.Center - NPC.Center + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-95, 95))).SafeNormalize(Vector2.UnitX);
			/*if (Math.Abs(NPC.velocity.Y) > 35)
            {
				NPC.velocity.Y = 35;
            }
			if (Math.Abs(NPC.velocity.Y) < -35)
			{
				NPC.velocity.Y = -35;
			}  */
			if (Math.Abs(NPC.velocity.X) < 9 && (Math.Abs(NPC.velocity.X) < 12) && (Math.Abs(NPC.velocity.X) > -12)) // I gotta learn what Math.Abs does // edit from future me: how did me from 2.5 years ago not just hover over it and see it's just the absolute value lol
			{
				NPC.velocity += new Vector2(NPC.direction * (float)Math.Sin(MathHelper.PiOver4 * 0.1), NPC.directionY * (float)Math.Atan(MathHelper.PiOver4 * 0.1));

			}
			startsprayattacktimer++;
			if (startsprayattacktimer > 650)
            {
				startsprayattack = true;
				didsound = false;
            }
			if (startsprayattack == true)
            {
				NPC.velocity.Y /= 2;
				playsoundforichorspam = true;
				
				sprayingtime++;
            }
			if (playsoundforichorspam == true && didsound == false)
            {
				SoundEngine.PlaySound(SoundID.Zombie78, NPC.Center);
				playsoundforichorspam = false;
				didsound = true;
			}
			if (sprayingtime > 6)
            {
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					timeuntilsprayends++;
					NPC.velocity.Y -= 8;
					int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerHostile, 35, 0);
					projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerHostile, 35, 0);
					if (Main.expertMode)
					{
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerHostile, 35, 0);
					}
					if (Main.masterMode)
					{
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerHostile, 35, 0);
					}
					sprayingtime = 0;
					NPC.netUpdate = true;
				}
            }
			if (timeuntilsprayends > 35)
            {
				startsprayattack = false;
				timeuntilsprayends = 0;
				startsprayattacktimer = 0;
				didsound = false;
				
            }

			if (sprayingtime != 0 && startsprayattack == true)
			{
				NPC.scale = 1f + (float)sprayingtime / 50;
			}
			if (startsprayattack == false && startshootingoutbabies == false)
            {
				NPC.scale = 1f + ((float)slowlybigger / 60) + ((float)slowlytomid / 60);
            }
			
			// FISH BABY ATTACK






			babyshootingtimer++;

			if (babyshootingtimer > 1550)
			if (babyshootingtimer > 1550)
			{

				startshootingoutbabies = true;
				startsprayattack = false;
				timeuntilsprayends = 0;
				startsprayattacktimer = 0;
				
			}
			
			if(babyshootingtimer > shoottimelengthforbabies)

{

				babyshootingtimer = 0;

				shootingoutbabies = 0;

				startshootingoutbabies = false;


			}

			if(startshootingoutbabies == true)

{

				shootingoutbabies++;
				sprayingtime = 0;
				NPC.scale = 1f + (float)shootingoutbabies / 90;

			}
			if (shootingoutbabies > 30)
            {
				NPC.velocity /= 1.5f;
			}
			if(shootingoutbabies > 40)

{

				// blood dusts

				// shit out baby
				if (NPC.position.X < target.position.X)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X, (int)NPC.Right.Y, NPCType<BabyLumpFish>(), 0, NPC.whoAmI);
					}
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					SoundEngine.PlaySound(SoundID.NPCDeath53, NPC.position);
					NPC.velocity.X -= 6f;
					shootingoutbabies = 0;
					NPC.netUpdate = true;

				}
				if (NPC.position.X > target.position.X)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Left.X, (int)NPC.Left.Y, NPCType<BabyLumpFish>(), 0, NPC.whoAmI);
					}
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Blood, NPC.direction + Main.rand.Next(-5, 5), NPC.direction + Main.rand.Next(-5, 5));
					SoundEngine.PlaySound(SoundID.NPCDeath53, NPC.position);
					NPC.velocity.X += 6f;
					shootingoutbabies = 0;
					NPC.netUpdate = true;
				}
				
			}
		

		}
		
    }


}