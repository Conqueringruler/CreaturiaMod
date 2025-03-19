using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Creaturia.NPCs.Enemies
{
	internal class GreatPixie : ModNPC
	{


		

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Great Pixie");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Pixie];
		}

		public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie;
		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 30;
			NPC.damage = 100;
			NPC.defense = 20;
			NPC.lifeMax = 2000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.scale = 2.5f;
			NPC.stepSpeed = 500;
			//NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = -1;
			NPC.value = 30000;
			NPC.friendly = false;
			//NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Pixie;
			NPC.ShowNameOnHover = true;
			NPC.knockBackResist = 0f;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
		
		}

		int TeleportTimerMax;
		int TeleportTimer;
		Vector2 TeleportLocation = new Vector2(0, 0);
		int WaitUntilTeleportMax = 60;
		int WaitUntilTeleport;
		bool GotTeleportSpot = false;

		public override void AI()
		{

			// REMINDER FUTURE SELF: DON'T DELETE THE EXTRA "if (NPC.type ==)" BECAUSE I WANT TO MAKE MORE
			// BASED OFF SOME OF THE OTHER'S STUFF

			NPC.ShowNameOnHover = true;

			bool StartFalling = false;
				bool TimeToLeave = NPC.type == NPCID.Poltergeist && !Main.pumpkinMoon;
				if (NPC.type == 253 && !Main.eclipse)
				{
					TimeToLeave = true;
				}
				if (NPC.type == 490 && Main.dayTime)
				{
					TimeToLeave = true;
				}
				if (NPC.justHit)
				{
					NPC.ai[2] = 0f;
				}
				if (NPC.type == NPCID.Ghost && (Main.player[NPC.target].dead || Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 6000f))
				{
					NPC.TargetClosest();
					if (Main.player[NPC.target].dead || Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 6000f)
					{
						NPC.EncourageDespawn(10);
						StartFalling = true;
						TimeToLeave = true;
					}
				}
				if (TimeToLeave)
				{
					if (NPC.velocity.X == 0f)
					{
						NPC.velocity.X = (float)Main.rand.Next(-1, 2) * 1.5f;
						NPC.netUpdate = true;
					}
				}
				else if (NPC.ai[2] >= 0f)
				{
					int num298 = 16;
					bool flag18 = false;
					bool flag19 = false;
					if (NPC.position.X > NPC.ai[0] - (float)num298 && NPC.position.X < NPC.ai[0] + (float)num298)
					{
						flag18 = true;
					}
					else if ((NPC.velocity.X < 0f && NPC.direction > 0) || (NPC.velocity.X > 0f && NPC.direction < 0))
					{
						flag18 = true;
					}
					num298 += 24;
					if (NPC.position.Y > NPC.ai[1] - (float)num298 && NPC.position.Y < NPC.ai[1] + (float)num298)
					{
						flag19 = true;
					}
					if (flag18 && flag19)
					{
						NPC.ai[2] += 1f;
						if (NPC.ai[2] >= 30f && num298 == 16)
						{
							StartFalling = true;
						}
						if (NPC.ai[2] >= 60f)
						{
							NPC.ai[2] = -200f;
							NPC.direction *= -1;
							NPC.velocity.X *= -1f;
							NPC.collideX = false;
						}
					}
					else
					{
						NPC.ai[0] = NPC.position.X;
						NPC.ai[1] = NPC.position.Y;
						NPC.ai[2] = 0f;
					}
					NPC.TargetClosest();
				}
				else if (NPC.type == NPCID.Reaper)
				{
					NPC.TargetClosest();
					NPC.ai[2] += 2f;
				}
				else
				{
				if (NPC.type == NPCID.Poltergeist)
					{
						NPC.ai[2] += 0.1f;
					}
					else
					{
						NPC.ai[2] += 1f;
					}
					if (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) > NPC.position.X + (float)(NPC.width / 2))
					{
						NPC.direction = -1;
					}
					else
					{
						NPC.direction = 1;
					}
				}
				int tileX = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction * 2;
				int tileY = (int)((NPC.position.Y + (float)NPC.height) / 16f);
				bool Fall = true;
				bool IdkFlag = false;
				int num301 = 3;
				if (NPC.type == NPCID.Gastropod)
				{
					if (NPC.justHit)
					{
						NPC.ai[3] = 0f;
						NPC.localAI[1] = 0f;
					}
					if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[3] == 32f && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
					{
						float num302 = 7f;
						Vector2 vector33 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num303 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector33.X;
						float num304 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector33.Y;
						float num305 = (float)Math.Sqrt(num303 * num303 + num304 * num304);
						float num306 = num305;
						num305 = num302 / num305;
						num303 *= num305;
						num304 *= num305;
						float num307 = 0.0125f;
						Vector2 vector34 = new Vector2(num303, num304).RotatedByRandom(num307 * ((float)Math.PI * 2f));
						num303 = vector34.X;
						num304 = vector34.Y;
						int num308 = 25;
						int num309 = 84;
						int num310 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector33.X, vector33.Y, num303, num304, num309, num308, 0f, Main.myPlayer);
					}
					num301 = 8;
					if (NPC.ai[3] > 0f)
					{
						NPC.ai[3] += 1f;
						if (NPC.ai[3] >= 64f)
						{
							NPC.ai[3] = 0f;
						}
					}
					if (Main.netMode != 1 && NPC.ai[3] == 0f)
					{
						NPC.localAI[1] += 1f;
						if (NPC.localAI[1] > 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
						{
							NPC.localAI[1] = 0f;
							NPC.ai[3] = 1f;
							NPC.netUpdate = true;
						}
					}
				}



				// Teleport code --------------


			if (!Main.expertMode && !Main.masterMode)
			{
				TeleportTimerMax = 1000 + ((NPC.life - NPC.lifeMax) / (NPC.lifeMax / 100));
			}
			if (Main.expertMode && !Main.masterMode)
			{
				TeleportTimerMax = 800 + ((NPC.life - NPC.lifeMax) / (NPC.lifeMax / 110)); // need to increase the division as health goes up from difficulty because the proportion gets a lot bigger
			}
			if (Main.masterMode)
			{
				TeleportTimerMax = 550 + ((NPC.life - NPC.lifeMax) / (NPC.lifeMax / 140));
			}
			TeleportTimer++;
			if (TeleportTimer > TeleportTimerMax)
			{
				TimeToLeave = true;
				NPC.velocity.X *= 0.9f;
				NPC.scale = (2.5f + MathF.Sin(1 + Main.GlobalTimeWrappedHourly * 9f));
				//NPC.alpha = (int)(2 + MathF.Sin(100 + Main.GlobalTimeWrappedHourly * 9f));
				WaitUntilTeleport++;
				if (GotTeleportSpot == false)
                {
					
					TeleportLocation = new Vector2(Main.player[NPC.target].position.X + Main.rand.Next(-100, 100), Main.player[NPC.target].position.Y + Main.rand.Next(-100, 100));
					GotTeleportSpot = true;
					SoundStyle ZapSound = SoundID.DD2_LightningAuraZap;
					ZapSound.Pitch = 1.0f;
					
					SoundEngine.PlaySound(ZapSound, TeleportLocation);
					

				}
				if (TeleportLocation != new Vector2(0, 0))
                {
					if (Main.rand.NextBool(3))
					{
						int num311 = Dust.NewDust(TeleportLocation + new Vector2(Main.rand.Next(-60, 60), Main.rand.Next(-60, 60)), 60, 60, DustID.Pixie, 0f, 0f, 200, new Color(190, (120 + (float)(NPC.life - NPC.lifeMax) / 10), 0 + (float)(NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f)));
						Dust dust = Main.dust[num311];
						dust.color = new Color(190, (120 + (NPC.life - NPC.lifeMax) / 100), 0 + (NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f));
						dust.velocity *= 0.3f;
						Lighting.AddLight(TeleportLocation, Color.Lerp(Color.Yellow, Color.DeepPink, (float)MathF.Abs((float)(NPC.life - NPC.lifeMax) / NPC.lifeMax)).ToVector3() * 1f);
						//dust.noLight = false;
						
						//dust.noLightEmittence = true;
					}
				}
			}
			
			if (WaitUntilTeleport > (WaitUntilTeleportMax + (NPC.life - NPC.lifeMax) / 110))
            {
				NPC.position = TeleportLocation;
				WaitUntilTeleport = 0;
				TeleportTimer = 0;
				GotTeleportSpot = false;
				NPC.alpha = 0;
				NPC.scale = 2.5f;
				TimeToLeave = false;
            }













			//else if (NPC.type == NPCID.Pixie)
			//{
			num301 = 4;
					NPC.position += NPC.netOffset;
					if (Main.rand.Next(6) == 0)
					{																// using NPC.color is actually really smart, if it worked lmao. also DIVIDED BY 100 FOR DUST COLOR, BY 10 FOR SPRITES
						int num311 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Pixie, 0f, 0f, 200, new Color(190, (120 + (float)(NPC.life - NPC.lifeMax) / 10), 0 + (float)(NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f)));
						Dust dust = Main.dust[num311];
				dust.color = new Color(190, (120 + (NPC.life - NPC.lifeMax) / 100), 0 + (NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f));
						dust.velocity *= 0.3f;
				dust.noLight = false;
				
				dust.noLightEmittence = true; 
					}
			
			Lighting.AddLight(NPC.Center, Color.Lerp(Color.Yellow, Color.DeepPink, (float)MathF.Abs((float)(NPC.life - NPC.lifeMax) / NPC.lifeMax)).ToVector3() * 1f);
			if (Main.rand.NextBool(40))
					{
						SoundEngine.PlaySound(SoundID.Pixie, new Vector2((int)NPC.position.X, (int)NPC.position.Y));
					// That sound hurts, gotta change it
			}
					NPC.position -= NPC.netOffset;
				//}

				if (NPC.type == NPCID.IceElemental)
				{
					NPC.position += NPC.netOffset; //  new Color(190, (120 + (NPC.life - NPC.lifeMax) / 100), 0 + (NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f))
				//Lighting.AddLight(new Vector2((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f)), Color.Lerp(Color.Yellow, Color.HotPink, MathF.Abs((NPC.life - NPC.lifeMax) / NPC.lifeMax)).ToVector3() * 1.1f);
			//	Lighting.AddLight(new Vector2((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f)), Color.Violet.ToVector3() * 10.1f);
					//Lighting.AddLight(NPC.Center, .19f, 0.12f + ((float)(NPC.life - NPC.lifeMax) / 1000), 0f + ((NPC.lifeMax - NPC.life) / 1100));
				NPC.alpha = 30;
					if (Main.rand.Next(3) == 0)
					{
						int num312 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 92, 0f, 0f, 200);
						Dust dust = Main.dust[num312];
						dust.velocity *= 0.3f;
						Main.dust[num312].noGravity = true;
					
					
				}
					NPC.position -= NPC.netOffset;
					if (NPC.justHit)
					{
						NPC.ai[3] = 0f;
						NPC.localAI[1] = 0f;
					}
					float num313 = 5f;
					Vector2 vector35 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num314 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector35.X;
					float num315 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector35.Y;
					float num316 = (float)Math.Sqrt(num314 * num314 + num315 * num315);
					float num317 = num316;
					num316 = num313 / num316;
					num314 *= num316;
					num315 *= num316;
					if (num314 > 0f)
					{
						NPC.direction = 1;
					}
					else
					{
						NPC.direction = -1;
					}
					NPC.spriteDirection = NPC.direction;
					if (NPC.direction < 0)
					{
						NPC.rotation = (float)Math.Atan2(0f - num315, 0f - num314);
					}
					else
					{
						NPC.rotation = (float)Math.Atan2(num315, num314);
					}
					if (Main.netMode != 1 && NPC.ai[3] == 16f)
					{
						int num318 = 45;
						int num319 = 128;
						int num320 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector35.X, vector35.Y, num314, num315, num319, num318, 0f, Main.myPlayer);
					}
					num301 = 10;
					if (NPC.ai[3] > 0f)
					{
						NPC.ai[3] += 1f;
						if (NPC.ai[3] >= 64f)
						{
							NPC.ai[3] = 0f;
						}
					}
					if (Main.netMode != 1 && NPC.ai[3] == 0f)
					{
						NPC.localAI[1] += 1f;
						if (NPC.localAI[1] > 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
						{
							NPC.localAI[1] = 0f;
							NPC.ai[3] = 1f;
							NPC.netUpdate = true;
						}
					}
				}

				/*else if (NPC.type == NPCID.IchorSticker)
				{
					NPC.rotation = NPC.velocity.X * 0.1f;
					num301 = ((!(Main.player[NPC.target].Center.Y < NPC.Center.Y)) ? 6 : 12);
					if (Main.netMode != 1 && !NPC.confused)
					{
						NPC.ai[3] += 1f;
						if (NPC.justHit)
						{
							NPC.ai[3] = -45f;
							NPC.localAI[1] = 0f;
						}
						if (Main.netMode != 1 && NPC.ai[3] >= (float)(60 + Main.rand.Next(60)))
						{
							NPC.ai[3] = 0f;
							if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
							{
								float num321 = 10f;
								Vector2 vector36 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f - 4f, NPC.position.Y + (float)NPC.height * 0.7f);
								float num322 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector36.X;
								float num323 = Math.Abs(num322) * 0.1f;
								float num324 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector36.Y - num323;
								num322 += (float)Main.rand.Next(-10, 11);
								num324 += (float)Main.rand.Next(-30, 21);
								float num325 = (float)Math.Sqrt(num322 * num322 + num324 * num324);
								float num326 = num325;
								num325 = num321 / num325;
								num322 *= num325;
								num324 *= num325;
								int num327 = 40;
								int num328 = 288;
								int num329 = Projectile.NewProjectile(NPC.GetSource_None(), vector36.X, vector36.Y, num322, num324, num328, num327, 0f, Main.myPlayer);
							}
						}
					}
				} */

				// ADAPTATION STUFF: .type TURNS TO .TileType, .nactive() TURNS TO .HasUnactuatedTile


				/*if (NPC.type == NPCID.Drippler)
				{
					num301 = 4;
					if (NPC.target >= 0)
					{
						float num330 = (Main.player[NPC.target].Center - NPC.Center).Length();
						num330 /= 70f;
						if (num330 > 8f)
						{
							num330 = 8f;
						}
						num301 += (int)num330;
					}
				} */
				if (NPC.position.Y + (float)NPC.height > Main.player[NPC.target].position.Y)
				{

				Fall = false; // Wanted to make it just have a really high flying range, but couldn't get it to work right so ig this is it

				if (NPC.type == NPCID.Poltergeist)
					{
						Fall = false;
					}
					/*else
					{
						for (int TileY2 = tileY; TileY2 < tileY + num301; TileY2++)
						{
							if (Main.tile[tileX, TileY2] == null)
							{
							//Main.tile[tileX, TileY2] = new Tile();
							return;
							}
							if ((Main.tile[tileX, TileY2].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, TileY2].TileType]) || Main.tile[tileX, TileY2].LiquidType > 0)
							{
								if (TileY2 <= tileY + 100)
								{
									IdkFlag = true;
								}
								Fall = false;
								break;
							}
						}
					} */
				}
				if (Main.player[NPC.target].npcTypeNoAggro[NPC.type])
				{
					bool flag22 = false;
					for (int num332 = tileY; num332 < tileY + num301 - 2; num332++)
					{
						if (Main.tile[tileX, num332] == null)
						{
						//Main.tile[tileX, num332] = new Tile();
						return; // for all of the "new Tile()" just remove and replace with return; like this, I guess.
						}
						if ((Main.tile[tileX, num332].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, num332].TileType]) || Main.tile[tileX, num332].LiquidType > 0)
						{
							flag22 = true;
							break;
						}
					}
					NPC.directionY = (!flag22).ToDirectionInt();
				}
				/*if (NPC.type == NPCID.IceElemental || NPC.type == NPCID.IchorSticker)
				{
					for (int num333 = tileY - 1; num333 < tileY; num333++)
					{
						if (Main.tile[tileX, num333] == null)
						{
						//	Main.tile[tileX, num333] = new Tile();
						return;
						}
					if ((Main.tile[tileX, num333].HasUnactuatedTile && Main.tileSolid[Main.tile[tileX, num333].TileType] && !TileID.Sets.Platforms[Main.tile[tileX, num333].TileType]) || Main.tile[tileX, num333].LiquidType > 0)
						{
							IdkFlag = false;
							StartFalling = true;
							break;
						}
					}
				} */
				if (StartFalling)
				{
					IdkFlag = false;
					Fall = true;
					if (NPC.type == NPCID.IchorSticker)
					{
						NPC.velocity.Y += 2f;
					}
				}
				if (Fall)
				{
					//if (NPC.type == NPCID.Pixie || NPC.type == NPCID.IceElemental)
					//{
						NPC.velocity.Y += 0.2f;
						if (NPC.velocity.Y > 2f)
						{
							NPC.velocity.Y = 2f;
						}
					//}
					/*else if (NPC.type == NPCID.Drippler)
					{
						NPC.velocity.Y += 0.03f;
						if ((double)NPC.velocity.Y > 0.75)
						{
							NPC.velocity.Y = 0.75f;
						}
					}
					else
					{
						NPC.velocity.Y += 0.1f;
						if (NPC.type == 316 && TimeToLeave)
						{
							NPC.velocity.Y -= 0.05f;
							if (NPC.velocity.Y > 6f)
							{
								NPC.velocity.Y = 6f;
							}
						}
						else if (NPC.velocity.Y > 3f)
						{
							NPC.velocity.Y = 3f;
						}
					}
					*/
				}
				else
				{
					//if (NPC.type == 75 || NPC.type == 169)
					//{
						if ((NPC.directionY < 0 && NPC.velocity.Y > 0f) || IdkFlag)
						{
							NPC.velocity.Y -= 0.2f;
						}
					//}
					else if (NPC.type == NPCID.Drippler)
					{
						if ((NPC.directionY < 0 && NPC.velocity.Y > 0f) || IdkFlag)
						{
							NPC.velocity.Y -= 0.075f;
						}
						if (NPC.velocity.Y < -0.75f)
						{
							NPC.velocity.Y = -0.75f;
						}
					}
					else if (NPC.directionY < 0 && NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y -= 0.1f;
					}
					if (NPC.velocity.Y < -4f)
					{
						NPC.velocity.Y = -4f;
					}
				}
			if (NPC.type == NPCID.Pixie && NPC.wet)
			{
				if (NPC.wet)
				{
					NPC.velocity.Y -= 0.2f;
					if (NPC.velocity.Y < -2f)
					{
						NPC.velocity.Y = -2f;
					}
				}
			}
				if (NPC.collideX)
				{
					NPC.velocity.X = NPC.oldVelocity.X * -0.4f;
					if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 1f)
					{
						NPC.velocity.X = 1f;
					}
					if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -1f)
					{
						NPC.velocity.X = -1f;
					}
				}
				if (NPC.collideY)
				{
					NPC.velocity.Y = NPC.oldVelocity.Y * -0.25f;
					if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
					{
						NPC.velocity.Y = 1f;
					}
					if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
					{
						NPC.velocity.Y = -1f;
					}
				}
				float VelocityMax = 2f;
			
								   //if (NPC.type == 75)
								   //{
								   //	VelocityMax = 3f;
			VelocityMax = 9f;
				

				if (!TimeToLeave)
					{
						NPC.TargetClosest();
					}
			
				/*if (NPC.type == NPCID.Reaper)
				{
					VelocityMax = 4f;
				}
				if (NPC.type == 490)
				{
					VelocityMax = 1.5f;
				}
				if (NPC.type == NPCID.Poltergeist)
				{
					NPC.alpha = 0;
					VelocityMax = 4f;
					if (!TimeToLeave)
					{
						NPC.TargetClosest();
					}
					else
					{
						NPC.EncourageDespawn(10);
					}
					if (NPC.direction < 0 && NPC.velocity.X > 0f)
					{
						NPC.velocity.X *= 0.9f;
					}
					if (NPC.direction > 0 && NPC.velocity.X < 0f)
					{
						NPC.velocity.X *= 0.9f;
					}
				}
			*/
				if (NPC.direction == -1 && NPC.velocity.X > 0f - VelocityMax)
				{
					NPC.velocity.X -= 0.2f;
					if (NPC.velocity.X > VelocityMax)
					{
						NPC.velocity.X -= 0.2f;
					}
					else if (NPC.velocity.X > 0f) 
					{
						NPC.velocity.X += 0.11f;
					}
					if (NPC.velocity.X < 0f - VelocityMax)
					{
						NPC.velocity.X = 0f - VelocityMax;
					}
				}
				else if (NPC.direction == 1 && NPC.velocity.X < VelocityMax)
				{
					NPC.velocity.X += 0.2f;
					if (NPC.velocity.X < 0f - VelocityMax)
					{
						NPC.velocity.X += 0.2f;
					}
					else if (NPC.velocity.X < 0f)
					{
						NPC.velocity.X -= 0.11f;
					}
					if (NPC.velocity.X > VelocityMax)
					{
						NPC.velocity.X = VelocityMax;
					}
				}
				VelocityMax = ((NPC.type != NPCID.Drippler) ? 1.5f : 1f);
				if (NPC.directionY == -1 && NPC.velocity.Y > 0f - VelocityMax)
				{
					NPC.velocity.Y -= 0.04f;
					if (NPC.velocity.Y > VelocityMax)
					{
						NPC.velocity.Y -= 0.05f;
					}
					else if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y += 0.03f;
					}
					if (NPC.velocity.Y < 0f - VelocityMax)
					{
						NPC.velocity.Y = 0f - VelocityMax;
					}
				}
				else if (NPC.directionY == 1 && NPC.velocity.Y < VelocityMax)
				{
					NPC.velocity.Y += 0.04f;
					if (NPC.velocity.Y < 0f - VelocityMax)
					{
						NPC.velocity.Y += 0.05f;
					}
					else if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y -= 0.03f;
					}
					if (NPC.velocity.Y > VelocityMax)
					{
						NPC.velocity.Y = VelocityMax;
					}
				}
			/*
			if (NPC.type == NPCID.Gastropod)
			{
				Lighting.AddLight((int)NPC.position.X / 16, (int)NPC.position.Y / 16, 0.4f, 0f, 0.25f);
			}
			*/
			

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;





			if (NPC.spriteDirection == 1)
			{
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//NPC.spriteDirection = -1;
				
				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
			}

			if (NPC.spriteDirection == 0)
			{
				//NPC.spriteDirection = 1;
				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

				//SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

			}









			for (int i = 0; i < 4; i++)
			{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
				Vector2 circular = new Vector2(3, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(190, (120 + (NPC.life - NPC.lifeMax)/10), 0 + (NPC.lifeMax - NPC.life)/110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.12f), NPC.scale * 1.05f, spriteEffects, 0f);
			}



			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			//if (NPC.downedPlantBoss == true)
			//{
			//	return SpawnCondition..Chance * 0.015f;
		//	}
			return SpawnCondition.Dungeon.Chance * 0f;
		}


		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//npcLoot.Add(ItemDropRule.Common(ItemID.HallowedKey, 15, 1, 1));

			//npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 0, 2));
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowScale2>(), 50 , 0, 7)); //\This new method is cock and balls, don't forget to use terraria.lootshit so stuff can drop and also 1 = 100% chance of dropping, 100 = 1% chance of dropping for some stupid reason
			npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 0, 3));
			npcLoot.Add(ItemDropRule.Common(ItemID.PixieDust, 1, 6, 12));
		}



		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					var dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Pixie, NPC.velocity.X, NPC.velocity.Y, 30, new Color(190, (120 + (NPC.life - NPC.lifeMax) / 100), 0 + (NPC.lifeMax - NPC.life) / 110, 0) * (0.2f + 0.5f * ((200 - NPC.alpha) / 255f)));
					if (Main.rand.NextBool(2))
                    {
						dust.scale *= (1 + (Main.rand.Next(40) * 0.01f));
						dust.noGravity = true;
						dust.noLightEmittence = true;
						dust.noLight = true;
					}
				}

				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
			}
		}
		/*public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			if (Main.rand.NextBool(5))
			{
				//	Item.NewItem(NPC.getRect(), ItemID.Leather);
			}
		} */

		/*public override void OnCaughtBy(Player player, Item item, bool failed)
		{
			item.stack = 1;

			try
			{
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0)
				{
					//	Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType = (byte)Main.rand.Next(50, 150);
					WorldGen.SquareTileFrame(NPCCenter.X, NPCCenter.Y, true);
				}
			}
			catch
			{
				return;
			}
		} */


		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times

			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow,
				
				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.,
				new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.GreatPixieLine1" + "\n" + "Mods.Creaturia.Bestiary.GreatPixieLine2")

              
			});
		}
	}

	/*internal class DungeonFrogItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DungeonFrogItem");
		}

		public override void SetDefaults()
		{
			//item.useStyle = 1;
			//item.autoReuse = true;
			//item.useTurn = true;
			//item.useAnimation = 15;
			//item.useTime = 10;
			//item.maxStack = 999;
			//item.consumable = true;
			Item.width = 28;
			Item.height = 36;
			//item.makeNPC = 360;
			//item.noUseGraphic = true;
			//item.bait = 15;

			Item.CloneDefaults(ItemID.GlowingSnail);
			Item.makeNPC = (short)NPCType<DungeonFrog>();
		}
	} */
}