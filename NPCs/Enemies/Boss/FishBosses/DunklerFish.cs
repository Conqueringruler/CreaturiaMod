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
using Creaturia.Items;

using System.Collections.Generic;
using Terraria.Graphics.CameraModifiers;
using System.IO;
using Creaturia.NPCs.Creatures;

namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{
	public class DunklerFish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dunkle");
			Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifiers);
        }
		bool Spawned = true;
		bool StartUpwards;

		public override void SetDefaults()
		{

			NPC.width = 290;
			NPC.height = 108; // Change ALL defaults 
			NPC.damage = 85;
			NPC.defense = 80;
			NPC.lifeMax = 7000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.lavaImmune = true;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.friendly = false;
			NPC.despawnEncouraged = false;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.npcSlots = 10f;
			NPC.value = Item.buyPrice(gold: 9, silver: 80);
            
            NPC.boss = true;
			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/FishBoss2");
			}
		}
		Player target;
		NPCAimedTarget targetData;

		float RotationSpeed = 0.04f;
		bool CircleModeActive = true;
		bool GoAbove = false;
		int SwitchFromCircleMode;

		int RainCursedFlameTimer;
		int CursedFlameDropTimer;

		int ResetAllOfCursedFlame;

		bool SwitchFromCircleToAbove = false;

		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			Position = new Vector2 ( 90f, 0f ),
            PortraitPositionXOverride = 45f,
            PortraitScale = 0.90f,
            Scale = 0.8f
        };

        public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(SwitchFromCircleToAbove); // only called on the server, don't forget
            writer.Write(GoAbove); // All sent in order, so reader must be in same order
            writer.Write(ResetAllOfCursedFlame);
			writer.Write(NPC.target);
        }

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			SwitchFromCircleToAbove = reader.ReadBoolean();
            GoAbove = reader.ReadBoolean();
            ResetAllOfCursedFlame = reader.ReadInt32();
			NPC.target = reader.ReadInt32();
        }
		public override void AI()
		{
           

            //if (NPC.rotation < NPC.AngleTo(Main.player[NPC.target].Center))
            //{
            //NPC.rotation = MathHelper.Lerp(NPC.rotation, NPC.AngleTo(Main.player[NPC.target].Center), 0.09f);
            //}



            NPC.knockBackResist = 0f;
			targetData = NPC.GetTargetData();
            target = Main.player[NPC.target];
            if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				target = Main.player[NPC.target];
				if (target == null || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
				{
					NPC.TargetClosest();
				}
			}
			//NPC.rotation = (target.Center - NPC.Center).ToRotation();
			if (NPC.position.Y < target.position.Y)
            {
			//	NPC.rotation = MathHelper.Lerp(NPC.rotation, (target.Center - NPC.Center).ToRotation(), 0.08f);
			}
			if (NPC.position.Y > target.position.Y)
            {
			//	NPC.rotation = MathHelper.Lerp(NPC.rotation, (NPC.Center - target.Center).ToRotation(), 0.08f);
			}
			
			if (CircleModeActive)
            {
				CircleMode();
			}
			if (GoAbove)
            {
				ChaseAndDash();
            }
			
			if (Spawned == true)
			{
				UpwardsOnSpawn();
			}

			if (target.dead || Vector2.Distance(target.Center, NPC.Center) > 8600f)
			{
				//NPC.velocity.Y -= 0.4f;
				NPC.EncourageDespawn(10);

			}
			

			if (Main.expertMode)
            {
				if (NPC.life < NPC.lifeMax/3)
                {
					RainCursedFlameTimer++;
                }
            }
			if (!Main.expertMode)
			{
				if (NPC.life < NPC.lifeMax / 2)
				{
					RainCursedFlameTimer++;
				}
			}
			if (RainCursedFlameTimer > 500)
            {
				CursedFlameRain();
            }

		}
		int EndUpwardsTimer;
		float num;
		float num2;

		void UpwardsOnSpawn()
		{
			
			if (Spawned == true)
			{
				EndUpwardsTimer++;
				Spawned = false;
				StartUpwards = true;
				//NPC.velocity.Y += 15f;
			}

			if (StartUpwards == true)
			{
				EndUpwardsTimer++;
			}

			//NPC.velocity.Y /= 1.5f;

			if (EndUpwardsTimer > 200)
			{
				StartUpwards = false;
				CircleMode();
			}
		}
		int CirclingTime;
		float OscillateMovement;
		int FlamesTimer;
		int FlamesChargeTimer;
		int FlamesChargeLength = 20;
		bool CornerDashing;
		void CircleMode()
		{
			
			if (NPC.life < NPC.lifeMax/2)
            {
				if (!Main.expertMode)
                {
					FlamesChargeLength = 15;
                }
				if (Main.expertMode)
				{
					FlamesChargeLength = 10;
				}
			}
            
			if (SwitchFromCircleMode > 3)
            {
				ChaseAndDash();
				CircleModeActive = false;
				GoAbove = true;
				SwitchFromCircleMode = 0;
            }
			Vector2 ShootSpot = NPC.Left;
			Vector2 directionshoot = (target.Center - NPC.Center + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5))).SafeNormalize(Vector2.UnitX);
			//NPC.position = Vector2.Lerp(NPC.position, (Main.player[NPC.target].position + new Vector2((70 * MathF.Cos(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi), (70 * MathF.Sin(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi))), 0.4f);

			//NPC.rotation = (float)Math.Atan2((double)num5, (double)num4);
			//Utils.GetLerpValue(NPC.position, (Main.player[NPC.target].position + new Vector2((70 * MathF.Cos(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi), (70 * MathF.Sin(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi))), (Main.GlobalTimeWrappedHourly / 100), false);
			FlamesTimer++;
			if (FlamesTimer > 200)
            {
				FlamesChargeTimer++;
				if (FlamesChargeTimer >= FlamesChargeLength)
                {
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						
						if (NPC.position.X < target.position.X)
                        {
							 ShootSpot = NPC.Left;
                        }
						if (NPC.position.X > target.position.X)
						{
							 ShootSpot = NPC.Right;
						}


						//	int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(90f, 0f).RotatedBy(NPC.rotation), directionshoot * (float)Main.rand.Next(5, 9), ProjectileID.CursedFlameHostile, 35, 0);
						// ^^^^^ Rotation version
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							if (target != null)
							{
								if (target.position.X < NPC.position.X)
								{
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), ShootSpot + new Vector2(-280, 0), directionshoot * (float)Main.rand.Next(7, 10), ProjectileID.CursedFlameHostile, 40, 0.3f);
								}
								if (target.position.X > NPC.position.X)
								{
									int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), ShootSpot + new Vector2(280, 0), directionshoot * (float)Main.rand.Next(7, 10), ProjectileID.CursedFlameHostile, 40, 0.3f);
								}

							}
						}

					}
					FlamesChargeTimer = 0;
				}
				
            }
			if (FlamesTimer > 260)
            {
				FlamesTimer = 0;
				FlamesChargeTimer = 0;
				SwitchFromCircleMode++;
            }				
			
			num = 3.5f;
			num2 = 0.021f;

			//if ((double)(NPC.position.Y / 16f) < Main.worldSurface) Part of the Hornet's AI. I have no idea what it does tbh
			//{
			if (NPC.velocity.X > 8f)
            {
				NPC.velocity.X *= 0.97f;
            }
			if (NPC.velocity.X < -8f)
			{
				NPC.velocity.X *= 0.97f;
			}

			if ((Main.player[NPC.target].position.Y - NPC.position.Y) > 300f && NPC.velocity.Y < 0f)
			{
				NPC.velocity.Y *= 0.975f;
			}
			if ((Main.player[NPC.target].position.Y - NPC.position.Y) < 80f && NPC.velocity.Y > 0f)
			{
				NPC.velocity.Y *= 0.975f;
			}
			NPC.velocity.X *= 0.99f;
				
				if (MathHelper.Distance(NPC.position.X, Main.player[NPC.target].position.X) > 800 && NPC.position.X > target.position.X)
            {
				if (NPC.velocity.X < 1f)
				{
					NPC.velocity.X += -1f;
				}
            }
			if (MathHelper.Distance(NPC.position.X, Main.player[NPC.target].position.X) > 1000 && NPC.position.X < target.position.X)
			{
				if (NPC.velocity.X > -1f)
				{
					NPC.velocity.X += 1f;
				}
				
			}
			//}

			OscillateMovement += 1f;
				if (OscillateMovement > 0f)
				{
				if (NPC.position.Y < Main.player[NPC.target].position.Y)
                {
					NPC.velocity.Y += 0.11f;
				}
					else
                {
					NPC.velocity.Y += 0.08f;
				}
				}
				else
				{
				if (NPC.position.Y > Main.player[NPC.target].position.Y)
				{
					NPC.velocity.Y -= 0.11f;
				}
				else
				{
					NPC.velocity.Y -= 0.08f;
				}
			}
				if (OscillateMovement < -50f || OscillateMovement > 50f)
				{
				if (NPC.position.X < Main.player[NPC.target].position.X)
				{
					NPC.velocity.X += 0.11f;
				}
				else
				{
					NPC.velocity.X += 0.08f;
				}
			}
				else
				{
				if (NPC.position.X > Main.player[NPC.target].position.X)
				{
					NPC.velocity.X -= 0.11f;
				}
				else
				{
					NPC.velocity.X -= 0.08f;
				}
			}
				if (OscillateMovement > 100f)
				{
					OscillateMovement = -100f;
				}
			
			float num15 = 0.7f;
			
			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -num15;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				{
					NPC.velocity.X = 1.8f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				{
					NPC.velocity.X = -1.8f;
				}
			}
			if (NPC.collideY)
			{
				NPC.netUpdate = true;
				NPC.velocity.Y = NPC.oldVelocity.Y * -num15;
				if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
				{
					NPC.velocity.Y = 1.8f;
				}
				if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
				{
					NPC.velocity.Y = -1.8f;
				}
			}

			if (NPC.wet)
			{
				if (NPC.velocity.Y > 0f)
				{
					NPC.velocity.Y *= 1.05f;
				}
				
				
				NPC.TargetClosest();
			}
			if (NPC.ai[1] == 101f)
			{
				SoundEngine.PlaySound(SoundID.Item17, NPC.position);
				NPC.ai[1] = 0f;
			}
			// SWITCH BACK IF IT BREAKS THE BOSS!!!

			/* if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[1] += (float)Main.rand.Next(5, 20) * 0.1f * NPC.scale;
				
					Player player = Main.player[NPC.target];
					if (player != null && player.stealth == 0f && player.itemAnimation == 0)
					{
						NPC.ai[1] = 0f;
					}
				
				if (NPC.ai[1] >= 130f)
				{
					if (targetData.Type != 0 && Collision.CanHit(NPC, targetData)) 
					{
						float num20 = 8f;
						Vector2 vector5 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)(NPC.height / 2));
						float num21 = targetData.Center.X - vector5.X + (float)Main.rand.Next(-20, 21);
						float num22 = targetData.Center.Y - vector5.Y + (float)Main.rand.Next(-20, 21);
						if ((num21 < 0f && NPC.velocity.X < 0f) || (num21 > 0f && NPC.velocity.X > 0f))
						{
							float num23 = (float)Math.Sqrt(num21 * num21 + num22 * num22);
							num23 = num20 / num23;
							num21 *= num23;
							num22 *= num23;
							int num24 = (int)(10f * NPC.scale);
							
							int num25 = 55;
							//int num26 = Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), vector5.X, vector5.Y, num21, num22, num25, num24, 0f, Main.myPlayer);
							//Main.projectile[num26].timeLeft = 300;
							NPC.ai[1] = 101f;
							NPC.netUpdate = true;
						}
						else
						{
							NPC.ai[1] = 0f;
						}
					}
					else
					{
						NPC.ai[1] = 0f;
					} 
				}
			} */
			//NPC.position += NPC.netOffset;


			//NPC.position -= NPC.netOffset;
			
		}
		int QuickDashingTime;
		bool GoingToCorner = true;
		int GoingToCornerTimer;
		bool HoldingBeforeDash;
		int HoldingBeforeDashTimer;
		Vector2 CornerTargetPosition;
		int CornerDashingTimer;

		
		void ChaseAndDash()
        {
			


			// Gonna copy Duke's go to corner move, then change it around until I reach something unique that I like
			if (GoingToCorner)
			{
				CornerTargetPosition = target.Center;
				GoingToCornerTimer++;

				if (GoingToCornerTimer > 300)
                {
					GoingToCornerTimer = 0;
					GoingToCorner = false;
					HoldingBeforeDash = true;
                }
				//NPC.velocity = NPC.DirectionTo(target.TopRight + new Vector2(0, 45)) * 1f;
				NPC.TargetClosestUpgraded();
				
				NPC.ai[0] = 2f;
				float num21 = (float)Math.Atan2(target.Center.Y - NPC.Center.Y, target.Center.X - NPC.Center.X);
				if (NPC.spriteDirection == -1)
				{
					num21 += (float)Math.PI;
				}
				if (num21 < 0f)
				{
					num21 += (float)MathHelper.TwoPi;
				}
				if (num21 > (float)MathHelper.TwoPi)
				{
					num21 -= (float)MathHelper.TwoPi;
				}
				if (NPC.ai[0] == -1f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 3f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 4f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 8f)
				{
					num21 = 0f;
				}
				float num22 = 0.04f;
				if (NPC.ai[0] == 1f || NPC.ai[0] == 6f)
				{
					num22 = 0f;
				}
				if (NPC.ai[0] == 7f)
				{
					num22 = 0f;
				}
				if (NPC.ai[0] == 3f)
				{
					num22 = 0.01f;
				}
				if (NPC.ai[0] == 4f)
				{
					num22 = 0.01f;
				}
				if (NPC.ai[0] == 8f)
				{
					num22 = 0.01f;
				}
				if (NPC.rotation < num21)
				{
					if ((double)(num21 - NPC.rotation) > Math.PI)
					{
				//		NPC.rotation -= num22;
					}
					else
					{
					//	NPC.rotation += num22;
					}
				}
				if (NPC.rotation > num21)
				{
					if ((double)(NPC.rotation - num21) > Math.PI)
					{
					//	NPC.rotation += num22;
					}
					else
					{
					//	NPC.rotation -= num22;
					}
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
				//	NPC.rotation = num21;
				}
				if (NPC.rotation < 0f)
				{
				//	NPC.rotation += (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > (float)MathHelper.TwoPi)
				{
				//	NPC.rotation -= (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
				//	NPC.rotation = num21;
				}
				float num3 = 0.3f;
				float num4 = 6f;
				Vector2 vector3 = Vector2.Normalize(target.Center + new Vector2(NPC.ai[1], 0f) - NPC.Center - NPC.velocity) * num4;
				if (NPC.velocity.X < vector3.X)
				{
					NPC.velocity.X += num3;
					if (NPC.velocity.X < 0f && vector3.X > 0f)
					{
						NPC.velocity.X += num3;
					}
				}
				else if (NPC.velocity.X > vector3.X)
				{
					NPC.velocity.X -= num3;
					if (NPC.velocity.X > 0f && vector3.X < 0f)
					{
						NPC.velocity.X -= num3;
					}
				}
				if (NPC.velocity.Y < vector3.Y)
				{
					NPC.velocity.Y += num3;
					if (NPC.velocity.Y < 0f && vector3.Y > 0f)
					{
						NPC.velocity.Y += num3;
					}
				}
				else if (NPC.velocity.Y > vector3.Y)
				{
					NPC.velocity.Y -= num3;
					if (NPC.velocity.Y > 0f && vector3.Y < 0f)
					{
						NPC.velocity.Y -= num3;
					}
				}
				
			}
            if (HoldingBeforeDash)
            {
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0;
				HoldingBeforeDashTimer++;
				if (Main.expertMode || Main.masterMode)
                {
					if (HoldingBeforeDashTimer > 8)
                    {
						HoldingBeforeDashTimer = 0;
						HoldingBeforeDash = false;
						CornerDashing = true;
                    }
                }
				else
                {
					if (HoldingBeforeDashTimer > 15)
					{
						HoldingBeforeDashTimer = 0;
						HoldingBeforeDash = false;
						CornerDashing = true;
					}
				}
				
            }
			if (CornerDashing)
            {
				CornerDashingTimer++;
				if (CornerDashingTimer <= 1)
                {
					NPC.velocity = NPC.DirectionTo(CornerTargetPosition) * 22f;
					NPC.netUpdate = true;
				}
				if (CornerDashingTimer > 30)
                {

					CornerDashingTimer = 0;
					CornerDashing = false;
					GoingToCorner = true;

					if (Main.rand.NextBool(3))
                    {

						SwitchFromCircleToAbove = true;
					}
					
                }
				
            }
			if (SwitchFromCircleToAbove)
			{
				SwitchFromCircleToAbove = false;
				CircleModeActive = true;
				GoAbove = false;
				NPC.netUpdate = true;
			}
		}
		void QuickDashesMode()
		{

		}
		void ArmorCrackOrSmthn()
		{

		}
		int SecondCursedFlameDropTimer; // This naming sucks dude why did I do this
		void CursedFlameRain()
		{
			CircleModeActive = false;
			GoAbove = false;
			GoingToCorner = true;

			CursedFlameDropTimer++;
			SecondCursedFlameDropTimer++;
			ResetAllOfCursedFlame++;
			//if (SecondCursedFlameDropTimer < 60)
			//{
				if (CursedFlameDropTimer > 10)
				{
					if (target != null)
					{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						if (NPC.position.X < target.position.X)
						{
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position + new Vector2(270, 50), new Vector2(Main.rand.NextFloat(-0.005f, 0.005f), 0), ProjectileID.CursedDartFlame, 30, 1f);
							

						}
						if (NPC.position.X > target.position.X)
						{
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position + new Vector2(-20, 50), new Vector2(Main.rand.NextFloat(-0.005f, 0.005f), 0), ProjectileID.CursedDartFlame, 30, 1f);
						}
					}

					var PukeSound = SoundID.DD2_OgreSpit;
					PukeSound.Pitch = 1.6f;
					SoundEngine.PlaySound(PukeSound, NPC.position);
					PukeSound.Volume = 0.6f;
					PukeSound.PitchVariance = 0.2f;


				}
					CursedFlameDropTimer = 0;
				//}
			}
			if (SecondCursedFlameDropTimer > 61)
            {
				SecondCursedFlameDropTimer = 0;
				CursedFlameDropTimer = 0;
				
            }
			if (ResetAllOfCursedFlame > 3500)
            {
				ResetAllOfCursedFlame = 0;
				SecondCursedFlameDropTimer = 0;
				CursedFlameDropTimer = 0;
				RainCursedFlameTimer = 0;
				GoAbove = true;
			}
			
			if (GoingToCorner)
			{
			
				if (NPC.position.X < target.position.X)
				{
					CornerTargetPosition = target.Center + new Vector2(200, -280);
				}
				if (NPC.position.X > target.position.X)
				{
					CornerTargetPosition = target.Center + new Vector2(-200, -280);
				}
				GoingToCornerTimer++;

				if (GoingToCornerTimer > 300)
				{
					GoingToCornerTimer = 0;
					//GoingToCorner = false;
					//HoldingBeforeDash = true;
				}
				//NPC.velocity = NPC.DirectionTo(target.TopRight + new Vector2(0, 45)) * 1f;
				NPC.TargetClosestUpgraded();

				NPC.ai[0] = 2f;
				float num21 = (float)Math.Atan2(target.Center.Y - NPC.Center.Y, target.Center.X - NPC.Center.X);
				if (NPC.spriteDirection == -1)
				{
					num21 += (float)Math.PI;
				}
				if (num21 < 0f)
				{
					num21 += (float)MathHelper.TwoPi;
				}
				if (num21 > (float)MathHelper.TwoPi)
				{
					num21 -= (float)MathHelper.TwoPi;
				}
				if (NPC.ai[0] == -1f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 3f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 4f)
				{
					num21 = 0f;
				}
				if (NPC.ai[0] == 8f)
				{
					num21 = 0f;
				}
				float num22 = 0.04f;
				if (NPC.ai[0] == 1f || NPC.ai[0] == 6f)
				{
					num22 = 0f;
				}
				if (NPC.ai[0] == 7f)
				{
					num22 = 0f;
				}
				if (NPC.ai[0] == 3f)
				{
					num22 = 0.01f;
				}
				if (NPC.ai[0] == 4f)
				{
					num22 = 0.01f;
				}
				if (NPC.ai[0] == 8f)
				{
					num22 = 0.01f;
				}
				if (NPC.rotation < num21)
				{
					if ((double)(num21 - NPC.rotation) > Math.PI)
					{
						//		NPC.rotation -= num22;
					}
					else
					{
						//	NPC.rotation += num22;
					}
				}
				if (NPC.rotation > num21)
				{
					if ((double)(NPC.rotation - num21) > Math.PI)
					{
						//	NPC.rotation += num22;
					}
					else
					{
						//	NPC.rotation -= num22;
					}
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
					//	NPC.rotation = num21;
				}
				if (NPC.rotation < 0f)
				{
					//	NPC.rotation += (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > (float)MathHelper.TwoPi)
				{
					//	NPC.rotation -= (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
					//	NPC.rotation = num21;
				}
				float num3 = 0.6f;
				float num4 = 8.5f;
				Vector2 vector3 = Vector2.Normalize(target.Center + new Vector2(NPC.ai[1], -280f) - NPC.Center - NPC.velocity) * num4;
				
			//	Vector2 vector3 = Vector2.Normalize(target.Center + new Vector2(NPC.ai[1], -280f) - NPC.Center - NPC.velocity) * num4;
				if (NPC.velocity.X < vector3.X)
				{
					NPC.velocity.X += num3;
					if (NPC.velocity.X < 0f && vector3.X > 0f)
					{
						NPC.velocity.X += num3;
					}
				}
				else if (NPC.velocity.X > vector3.X)
				{
					NPC.velocity.X -= num3;
					if (NPC.velocity.X > 0f && vector3.X < 0f)
					{
						NPC.velocity.X -= num3;
					}
				}
				if (NPC.velocity.Y < vector3.Y)
				{
					NPC.velocity.Y += num3;
					if (NPC.velocity.Y < 0f && vector3.Y > 0f)
					{
						NPC.velocity.Y += num3;
					}
				}
				else if (NPC.velocity.Y > vector3.Y)
				{
					NPC.velocity.Y -= num3;
					if (NPC.velocity.Y > 0f && vector3.Y < 0f)
					{
						NPC.velocity.Y -= num3;
					}
				}

			}
			CornerDashingTimer++;
			if (CornerDashingTimer <= 1)
			{
				NPC.velocity = NPC.DirectionTo(CornerTargetPosition) * 20f;
				NPC.netUpdate = true;
			}
			if (CornerDashingTimer > 20)
			{

				CornerDashingTimer = 0;

				

			}

			if (NPC.life <= 0)
            {
				NPC.netUpdate = true;
            }
		}
		public override void HitEffect(NPC.HitInfo hit)
        {
           if (NPC.life <= 0)
            {
				if (NPC.direction == -1)
                {
					Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(-10, 0), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore4").Type, 1f);
				}
				if (NPC.direction == 1)
				{
					Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(10, 0), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore4").Type, 1f);
				}
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(0, -8), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore3").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(0, 1), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(0, -1), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore").Type, 1f);

				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(-2, 0), NPC.velocity * hit.HitDirection, Mod.Find<ModGore>("DunkGore5").Type, 1f);
			}
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

           
            //npcLoot.Add(ItemDropRule.Common(ItemID.HallowedKey, 15, 1, 1));

            //npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 0, 2));
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowScale2>(), 50 , 0, 7)); //\This new method is cock and balls, don't forget to use terraria.lootshit so stuff can drop and also 1 = 100% chance of dropping, 100 = 1% chance of dropping for some stupid reason
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 0, 3));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DunkleVertebrae>(), 1, 10, 16));
		}

		public override void FindFrame(int frameHeight)
		{


			NPC.frameCounter++;
			if (GoAbove)
            {
				NPC.frameCounter++;
            }

			if (NPC.frameCounter < 5)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			else if (NPC.frameCounter < 10)
			{
				NPC.frame.Y = 1 * frameHeight;
			}
			else if (NPC.frameCounter < 15)
			{
				NPC.frame.Y = 2 * frameHeight;
			}
			else if (NPC.frameCounter < 20)
			{
				NPC.frame.Y = 3 * frameHeight;
			}
			else if (NPC.frameCounter < 25)
			{
				NPC.frame.Y = 4 * frameHeight;
			}
			else if (NPC.frameCounter < 30)
			{
				NPC.frame.Y = 5 * frameHeight;
			}

			else
			{
				NPC.frameCounter = 0;
			}
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;



			/*
						if (NPC.position.X > target.position.X)
						{




							if (NPC.position.Y < target.position.Y)
							{
								spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
							}
							else
							{
								spriteEffects = SpriteEffects.None | SpriteEffects.None;
							}
						}

						if (NPC.position.X < target.position.X)
						{
							//NPC.spriteDirection = 1;
							if (NPC.position.Y < target.position.Y)
							{
								spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
							}
							else
							{
								spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.None;
							}

							// If I choose to give him rotation again, then use this 

						}*/
			if (NPC.position.X < target.position.X)
			{
				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
			}
			if (NPC.position.X > target.position.X)
			{
				spriteEffects = SpriteEffects.None | SpriteEffects.None;
			}
				for (int i = 0; i < 4; i++)
			{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
				Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(60, 53, 157, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0f + 144f, TextureAssets.Npc[NPC.type].Value.Height * 0f + 48f), NPC.scale * 1.03f * (1 + (MathF.Abs(NPC.velocity.X) / 150 + MathF.Abs(NPC.velocity.Y) / 150)), spriteEffects, 0f);
			}


			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation, // adjusted this shit to be EXACT!
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0f + 144f, TextureAssets.Npc[NPC.type].Value.Height * 0f + 48f), NPC.scale, spriteEffects, 0f);

		/*	spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/DunklerFish_Tail").Value, (NPC.Center + new Vector2(0f, 0f).RotatedBy(NPC.rotation)) - screenPos,
			NPC.frame, new Color(60, 53, 157, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0f + 144f, TextureAssets.Npc[NPC.type].Value.Height * 0f + 48f), NPC.scale, spriteEffects, 0f);
		*/
			
			/*spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/CrimsonFishMiniBoss_EyeGlow").Value, NPC.Center - screenPos,
			NPC.frame, Color.White, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */
			// ^ Replace with Dunkle glow
			return false;
		}
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
				//BestiaryDatabaseNPCsPopulator.CrownosIconIndexes.
				new FlavorTextBestiaryInfoElement("The great scaled beast of the Corruption waters," +
                                                  " brutal and destructive. Its physical build gives it incredible strength. ")

            });
        }
    }

	
}

