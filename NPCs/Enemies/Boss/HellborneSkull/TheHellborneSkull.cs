using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.IO;
using System;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Terraria.GameContent;
using Creaturia.Items;
using Creaturia.Common.Systems;
using Terraria.GameContent.ItemDropRules;

namespace Creaturia.NPCs.Enemies.Boss.HellborneSkull
{
	[AutoloadBossHead]
	public class TheHellborneSkull : ModNPC
	{


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hellborne Skull");
			Main.npcFrameCount[NPC.type] = 6;

		}
		public int Timer;
		public int Timer2;
		public int Timer3;
		public int Timer4;
		public int TeleportTimer;
		public int DashTimer;
		public int DustTimer;
		public int DustLocation1;
		public int DustLocation2;
		public int Roar;
		public bool Enraged = false;
		public bool ChatMessageSaid = false;
		public override void SetDefaults()
		{
			NPC.width = 108;
			NPC.height = 169;
			NPC.damage = 35;
			NPC.defense = 40;
			NPC.lifeMax = 55000;
			NPC.HitSound = SoundID.NPCHit21;
			NPC.DeathSound = new SoundStyle("Creaturia/Assets/Sounds/hellborne_death");
			NPC.value = 60f;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 10;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.alpha = 240;
			NPC.boss = true;
			AIType = NPCID.CursedSkull;

			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/HellborneSkull");
			}


		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Moon,
				new FlavorTextBestiaryInfoElement("This once great being of light was worshipped by the Lihzards long ago, long before Cthulhu had descended upon Terraria.\n" +
												  "At some point during that time the armies of the King of Pumpkins and Queen of Frost had betrayed the Lihzahrds,\n"+
												  "nearly killing Hellborne before the Lihzarhds built the Golem to channel his remaining energy. Until the world's guardian\n" +
												   "was defeated, these spirits had been trapped, and have long ago forgotten their battle.\n")
			});
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenBone>(), 50, 0, 7));
			npcLoot.Add(ItemDropRule.Common(ItemID.AshBlock, 10, 16, 32));
			npcLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 40, 8, 32));
			npcLoot.Add(ItemDropRule.Common(ItemID.ManaPotion, 40, 8, 32));
			Item.NewItem(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.ManaPotion, Main.rand.Next(8, 12));


			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenBone>(), 50, 0, 7));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenBone>(), 50, 0, 7));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenBone>(), 50, 0, 7));

		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.ShadowFlame, 350);

		}
		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < .1000f)
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 30 * NPC.direction, (int)NPC.Center.Y + 14, ModContent.NPCType<HellborneSkullMinion>(), ai0: NPC.direction);
			
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < .1000f)
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 30 * NPC.direction, (int)NPC.Center.Y + 14, ModContent.NPCType<HellborneSkullMinion>(), ai0: NPC.direction);
			
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.damage = 100;
			NPC.lifeMax = 80000;
			NPC.defense = 45;
		}
		bool ChatMessageSaidYet = false;
		
		public override void AI()
		{
			Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 10f);
			if (ChatMessageSaidYet == false)
            {
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Main.NewText("As you can probably tell, this is an old boss from my 1.3 mod. It's only here as an unobtainable placeholder while the rework stays in stasis. ", Color.BlueViolet);
				}
				ChatMessageSaidYet = true;

            }
			Player player = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active)
			{
				NPC.TargetClosest(false);
				NPC.direction = 1;
				NPC.velocity.Y = NPC.velocity.Y - 99.1f;
				if (NPC.timeLeft > 20)
				{
					NPC.timeLeft = 20;
					return;
				}
			}
			if (NPC.life <= NPC.lifeMax / 1.5 || NPC.life >= NPC.lifeMax / 2)
			{
				NPC.alpha = 200;
			}
			if (NPC.life <= NPC.lifeMax / 2 || NPC.life >= NPC.lifeMax / 3)
			{
				NPC.alpha = 160;
			}
			if (NPC.life <= NPC.lifeMax / 3 || NPC.life >= NPC.lifeMax / 5)
			{
				NPC.alpha = 120;
			}
			if (NPC.life <= NPC.lifeMax / 5 || NPC.life >= NPC.lifeMax / 8)
			{
				NPC.alpha = 80;
			}

			if (Enraged == false)
			{
				if (NPC.life <= 3000)
				{
					Enraged = true;
				}
				DustTimer++;
				if (DustTimer >= 2)
				{
					DustLocation1 = Main.rand.Next(-200, 50); /*+ DustLocation1 */ /*+ DustLocation2 */
					DustLocation2 = Main.rand.Next(-280, -10);
					DustTimer = 0;
				}
				if (DustTimer >= 1)
				{
					int dust = Dust.NewDust(NPC.Center, NPC.width + DustLocation1, NPC.height + DustLocation2, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y, 10, Color.DodgerBlue, 1);
				}



				if (NPC.life > 40000)
				{
					NPC.aiStyle = 10;
				}

				NPC.knockBackResist = 0f;

				if (NPC.life < 40000)
				{
					TeleportTimer++;
					if (TeleportTimer < 20)
					{
						NPC.aiStyle = 97;
					}
					else if (TeleportTimer < 51)
					{
						NPC.aiStyle = 10;
					}
					else if (TeleportTimer > 75)
					{
						TeleportTimer = 0;
					}
				}
				if (NPC.lifeMax <= 45000)
				{
					NPC.knockBackResist = 0f;
				}
				Player target = Main.player[NPC.target];
				Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
				Vector2 directionangle = (target.Center - NPC.Left).SafeNormalize(Vector2.UnitX);
				Vector2 directionangle2 = (target.Center - NPC.Right).SafeNormalize(Vector2.UnitX);

				Timer2++;
				if (Timer2 > 400)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 16, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 17, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 18, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 15, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 14, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 13, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 12, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 11, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 10, ModContent.ProjectileType<HellborneSkullProj>(), 15, 0, Main.myPlayer);
					}

					Timer2 = 0;
				}
				if (NPC.life < 45000)
				{
					DashTimer++;
					if (DashTimer > 600)
					{
						int dust = Dust.NewDust(NPC.position - new Vector2(6f, 3f), NPC.width + 1, NPC.height + 18, DustID.SparksMech, 3f, 6f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(9f, 1f), NPC.width + 2, NPC.height + 11, DustID.SparksMech, 2f, 4f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(4f, 3f), NPC.width + 1, NPC.height + 19, DustID.SparksMech, 3f, 3f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(5f, 3f), NPC.width + 5, NPC.height + 14, DustID.SparksMech, 1f, 5f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(6f, 3f), NPC.width + 6, NPC.height + 3, DustID.SparksMech, 3f, 6f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(2f, 4f), NPC.width + 8, NPC.height + 9, DustID.SparksMech, 2f, 4f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(8f, 8f), NPC.width + 4, NPC.height + 4, DustID.SparksMech, 3f, 3f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(3f, 6f), NPC.width + 14, NPC.height + 5, DustID.SparksMech, 1f, 5f, 99, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(9f, 1f), NPC.width + 2, NPC.height + 11, DustID.SparksMech, 2f, 4f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(4f, 3f), NPC.width + 1, NPC.height + 19, DustID.SparksMech, 3f, 3f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(5f, 3f), NPC.width + 5, NPC.height + 14, DustID.SparksMech, 1f, 5f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(6f, 3f), NPC.width + 6, NPC.height + 3, DustID.SparksMech, 3f, 6f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(2f, 4f), NPC.width + 8, NPC.height + 9, DustID.SparksMech, 2f, 4f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(8f, 8f), NPC.width + 4, NPC.height + 4, DustID.SparksMech, 3f, 3f, 0, Color.DeepSkyBlue);
						dust = Dust.NewDust(NPC.position - new Vector2(3f, 6f), NPC.width + 14, NPC.height + 5, DustID.SparksMech, 1f, 5f, 0, Color.DeepSkyBlue);

						NPC.netUpdate = true;
						NPC.velocity.X *= 5;
						NPC.velocity.Y *= 5;
						DashTimer = 0;

					}

				}
				Timer3++;
				if (Timer3 > 300)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 46, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle * 45, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle2 * 45, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
					}
					Timer3 = 0;
				}
				Timer4++;
				if (Timer4 > 120)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 46, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle * 45, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle2 * 44, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
					}
					SoundEngine.PlaySound(SoundID.Item15);

					Timer4 = 0;
				}
				Timer++;
				if (Timer > 130)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, direction * 46, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle * 45, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
						projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom, directionangle2 * 45, ProjectileID.CultistBossFireBallClone, 25, 0, Main.myPlayer);
					}

					Timer = 0;
				}

				if (NPC.life <= 25500)
				{

					if (NPC.AnyNPCs(ModContent.NPCType<HellborneGuardian>()))
					{

					}
					else
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 30 * NPC.direction, (int)NPC.Center.Y + 14, ModContent.NPCType<HellborneGuardian>(), ai0: NPC.direction);
							NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 30 * NPC.direction, (int)NPC.Center.Y + 14, ModContent.NPCType<HellborneGuardian>(), ai0: NPC.direction);
						}
					}



				}
			}

			if (Enraged == true)
			{

				Timer = 0;
				Timer2 = 0;
				Timer3 = 0;
				Timer4 = 0;
				Roar++;
				if (Roar == 1)
				{
					SoundEngine.PlaySound(SoundID.ForceRoar);
				}
				else
				{
					Roar = 2;
				}
				//NPC.velocity *= 0.95f; Idk wtf I was on when I made it do this
				//NPC.rotation = 90;

				NPC.alpha = 60;

			}


		}
		public override void FindFrame(int frameHeight)
		{


			NPC.frameCounter++;

			if (NPC.frameCounter < 10)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			else if (NPC.frameCounter < 20)
			{
				NPC.frame.Y = 1 * frameHeight;
			}
			else if (NPC.frameCounter < 30)
			{
				NPC.frame.Y = 2 * frameHeight;
			}
			else if (NPC.frameCounter < 40)
			{
				NPC.frame.Y = 3 * frameHeight;
			}
			else if (NPC.frameCounter < 50)
			{
				NPC.frame.Y = 4 * frameHeight;
			}
			else if (NPC.frameCounter < 60)
			{
				NPC.frame.Y = 5 * frameHeight;
			}

			else
			{
				NPC.frameCounter = 0;
			}







		}

        public override void OnKill()
        {
			NPC.SetEventFlagCleared(ref CreaturiaSystem.downedHellborne, -1);
			string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<HellborneGuardian>()];
			Main.BestiaryTracker.Kills.SetKillCountDirectly(persistentId, 10); // I wonder if kills will work for a critter?
		}



    }






	public class HellborneFireball : ModProjectile
	{
		// Since the texture is useless and not drawn, we can reuse the vanilla texture
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.DD2BetsyFireball;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellborne Skull Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.alpha = 220;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 4;
			Projectile.CloneDefaults(ProjectileID.DD2BetsyFireball);
			AIType = ProjectileID.DD2BetsyFireball;
			Projectile.extraUpdates = 2;
		}
		public int DustTimer;
		public override void AI()
		{
			/*	DustTimer++;
				if (DustTimer >= 5)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.BlueTorch, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
					DustTimer = 0;
				} */


		}
		public Color GetColor()
		{
			return new Color(10, 30, 255);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.ShadowFlame, 180, true);
		}


	}

	public class HellborneSkullProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Skull");

		}

		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.width = 26;
			Projectile.height = 28;
			Projectile.CloneDefaults(ProjectileID.CannonballHostile);
			AIType = ProjectileID.CannonballHostile;
			Projectile.damage = 120;
			Projectile.Opacity = 0f;
		}
        public override void AI()
        {
			if (Projectile.Opacity < 1f)
            {
				Projectile.Opacity += 0.2f;

			}
			if (Projectile.Opacity > 1f)
            {
				Projectile.Opacity = 1f;
            }
			
        }

    }
}





















