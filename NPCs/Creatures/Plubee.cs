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
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;

namespace Creaturia.NPCs.Creatures
{
	internal class Plubee : ModNPC
	{

		public override string Texture => "Terraria/Images/NPC_" + NPCID.GemBunnyDiamond;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plubee");
			Main.npcCatchable[NPC.type] = false;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GemBunnyDiamond];
			ContentSamples.NpcBestiaryRarityStars[ModContent.NPCType<Plubee>()] = 5;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 2f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
        }
		int spawndust;
		public override void SetDefaults()
		{

			NPC.width = 28;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 1;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath52;
			NPC.stepSpeed = 30;
			NPC.aiStyle = -1;
			NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Bunny;
			NPC.color = Color.DeepSkyBlue;
			//NPC.ShowNameOnHover = false;
			NPC.stepSpeed = 300;
			NPC.value = 132232;
			NPC.rarity = 5;
			//AIType = NPCID.DesertBeast;
			NPC.alpha = 200;
			NPC.Opacity = 200;
			
		}
		public override void AI()
		{
				int num = 30;
				int num2 = 10;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
				{
					flag2 = true;
					NPC.ai[3] += 1f;
				}
			
				if (NPC.type == 315)
				{
					int num3 = 480;
					if (NPC.localAI[0]++ >= (float)num3)
					{
						NPC.localAI[0] = 0f;
						int num4 = NPC.target;
						if (Main.netMode != 1 && num4 != 255)
						{
							int attackDamage_ForProjectiles = NPC.GetAttackDamage_ForProjectiles(40f, 30f);
						//	Projectile.NewProjectile(NPC.GetSpawnSourceForNPCFromNPCAI(), NPC.Center + Main.rand.NextVector2Circular(40f, 40f), new Vector2(NPC.velocity.X, Main.rand.NextFloatDirection() * 3f), 1001, attackDamage_ForProjectiles, 0f, Main.myPlayer, num4);
						}
					}
					Lighting.AddLight(NPC.Center, 0.4f, 0.36f, 0.2f);
					int num5 = NPC.frame.Height;
					if (num5 < 1)
					{
						num5 = 1;
					}
					int num6 = NPC.frame.Y / num5;
					if (num6 >= 4 && num6 <= 7)
					{
						Vector2 vector = NPC.Bottom + new Vector2(-30f, -8f);
						Vector2 vector2 = new Vector2(60f, 8f);
						if (Main.rand.Next(3) != 0)
						{
							Dust dust = Dust.NewDustPerfect(vector + new Vector2(Main.rand.NextFloat() * vector2.X, Main.rand.NextFloat() * vector2.Y), 6, NPC.velocity);
							dust.scale = 0.6f;
							dust.fadeIn = 1.1f;
							dust.noGravity = true;
							dust.noLight = true;
						}
					}
				}
				if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num || flag2)
				{
					NPC.ai[3] += 1f;
					flag3 = true;
				}
				else if (NPC.ai[3] > 0f)
				{
					NPC.ai[3] -= 1f;
				}
				if (NPC.ai[3] > (float)(num * num2))
				{
					NPC.ai[3] = 0f;
				}
				if (NPC.justHit)
				{
					NPC.ai[3] = 0f;
				}
				if (NPC.ai[3] == (float)num)
				{
					NPC.netUpdate = true;
				}
				Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num7 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector3.X;
				float num8 = Main.player[NPC.target].position.Y - vector3.Y;
				float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
				if (num9 < 200f && !flag3)
				{
					NPC.ai[3] = 0f;
				}
				if (NPC.type == 410)
				{
					NPC.ai[1] += 1f;
				bool flag5 = NPC.ai[1] >= 240f;
			//	bool flag5 = false;
				/*	if (!flag5 && NPC.velocity.Y == 0f)
					{
						for (int j = 0; j < 255; j++)
						{
							if (Main.player[j].active && !Main.player[j].dead && Main.player[j].Distance(NPC.Center) < 800f && Main.player[j].Center.Y < NPC.Center.Y && Math.Abs(Main.player[j].Center.X - NPC.Center.X) < 20f)
							{
								flag5 = true;
								break;
							}
						}
					} 
					
					if (flag5 && Main.netMode != 1)
					{
						for (int k = 0; k < 3; k++)
						{
							Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center.X, NPC.Center.Y, (Main.rand.NextFloat() - 0.5f) * 2f, -4f - 10f * Main.rand.NextFloat(), 538, 50, 0f, Main.myPlayer);
						}
						NPC.HitEffect(9999);
						NPC.active = false;
						return;
					} */

					// Twinkle death explosion
				}



			NPC.EncourageDespawn(10);
			if (NPC.ai[3] < (float)num)
				{
					if ((NPC.type == 329 || NPC.type == 315) && !Main.pumpkinMoon)
					{
						NPC.EncourageDespawn(10);
					}
					else
					{
					//	NPC.TargetClosest();
					}
				}
				else
				{
					if (NPC.velocity.X == 0f)
					{
						if (NPC.velocity.Y == 0f)
						{
							NPC.ai[0] += 1f;
							if (NPC.ai[0] >= 2f)
							{
								NPC.direction *= -1;
								NPC.spriteDirection = NPC.direction;
								NPC.ai[0] = 0f;
							}
						}
					}
					else
					{
						NPC.ai[0] = 0f;
					}
					NPC.directionY = -1;
					if (NPC.direction == 0)
					{
						NPC.direction = 1;
					}
				}
				float num11 = 6f;
				float num12 = 0.07f;
				if (!flag && (NPC.velocity.Y == 0f || NPC.wet || (NPC.velocity.X <= 0f && NPC.direction < 0) || (NPC.velocity.X >= 0f && NPC.direction > 0)))
				{
					
					
					
					
					// This is how fast I want it to be I think
				//	 if (NPC.type == 410)
				//	{
						if (Math.Sign(NPC.velocity.X) != NPC.direction)
						{
							NPC.velocity.X *= 0.9f;
						}
						num11 = 7.5f;
						num12 = 0.2f;
				//	}
					 if (NPC.type == 423)
					{
						if (Math.Sign(NPC.velocity.X) != NPC.direction)
						{
							NPC.velocity.X *= 0.85f;
						}
						num11 = 10f;
						num12 = 0.2f;
					}
					
					if (NPC.velocity.X < 0f - num11 || NPC.velocity.X > num11)
					{
						if (NPC.velocity.Y == 0f)
						{
							NPC.velocity *= 0.8f;
						}
					}
					else if (NPC.velocity.X < num11 && NPC.direction == 1)
					{
						NPC.velocity.X += num12;
						if (NPC.velocity.X > num11)
						{
							NPC.velocity.X = num11;
						}
					}
					else if (NPC.velocity.X > 0f - num11 && NPC.direction == -1)
					{
						NPC.velocity.X -= num12;
						if (NPC.velocity.X < 0f - num11)
						{
							NPC.velocity.X = 0f - num11;
						}
					}
				}
				if (NPC.velocity.Y >= 0f)
				{
					int num14 = 0;
					if (NPC.velocity.X < 0f)
					{
						num14 = -1;
					}
					if (NPC.velocity.X > 0f)
					{
						num14 = 1;
					}
					Vector2 vector8 = NPC.position;
					vector8.X += NPC.velocity.X;
					int num15 = (int)((vector8.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num14)) / 16f);
					int num16 = (int)((vector8.Y + (float)NPC.height - 1f) / 16f);
					if (Main.tile[num15, num16] == null)
					{
					return;
					}
					if (Main.tile[num15, num16 - 1] == null)
					{
					return;
				}
					if (Main.tile[num15, num16 - 2] == null)
					{
					return;
				}
					if (Main.tile[num15, num16 - 3] == null)
					{
					return;
				}
					if (Main.tile[num15, num16 + 1] == null)
					{
					return;
				}
					if ((float)(num15 * 16) < vector8.X + (float)NPC.width && (float)(num15 * 16 + 16) > vector8.X && ((Main.tile[num15, num16].HasUnactuatedTile && !Main.tile[num15, num16].TopSlope && !Main.tile[num15, num16 - 1].TopSlope && Main.tileSolid[Main.tile[num15, num16].TileType] && !Main.tileSolidTop[Main.tile[num15, num16].TileType]) || (Main.tile[num15, num16 - 1].IsHalfBlock && Main.tile[num15, num16 - 1].HasUnactuatedTile)) && (!Main.tile[num15, num16 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num15, num16 - 1].TileType] || Main.tileSolidTop[Main.tile[num15, num16 - 1].TileType] || (Main.tile[num15, num16 - 1].IsHalfBlock && (!Main.tile[num15, num16 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[num15, num16 - 4].TileType] || Main.tileSolidTop[Main.tile[num15, num16 - 4].TileType]))) && (!Main.tile[num15, num16 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num15, num16 - 2].TileType] || Main.tileSolidTop[Main.tile[num15, num16 - 2].TileType]) && (!Main.tile[num15, num16 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num15, num16 - 3].TileType] || Main.tileSolidTop[Main.tile[num15, num16 - 3].TileType]) && (!Main.tile[num15 - num14, num16 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num15 - num14, num16 - 3].TileType]))
					{
						float num17 = num16 * 16;
						if (Main.tile[num15, num16].IsHalfBlock)
						{
							num17 += 8f;
						}
						if (Main.tile[num15, num16 - 1].IsHalfBlock)
						{
							num17 -= 8f;
						}
						if (num17 < vector8.Y + (float)NPC.height)
						{
							float num18 = vector8.Y + (float)NPC.height - num17;
							if ((double)num18 <= 16.1)
							{
								NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num17;
								NPC.position.Y = num17 - (float)NPC.height;
								if (num18 < 9f)
								{
									NPC.stepSpeed = 1f;
								}
								else
								{
									NPC.stepSpeed = 2f;
								}
							}
						}
					}
				}
				if (NPC.velocity.Y == 0f)
				{
					bool flag6 = true;
					int num19 = (int)(NPC.position.Y - 7f) / 16;
					int num20 = (int)(NPC.position.X - 7f) / 16;
					int num21 = (int)(NPC.position.X + (float)NPC.width + 7f) / 16;
					for (int m = num20; m <= num21; m++)
					{
						if (Main.tile[m, num19] != null && Main.tile[m, num19].HasUnactuatedTile && Main.tileSolid[Main.tile[m, num19].TileType])
						{
							flag6 = false;
							break;
						}
					}
					if (flag6)
					{
						int num22 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 2) * NPC.direction) + NPC.velocity.X * 5f) / 16f);
						int num23 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
						if (Main.tile[num22, num23] == null)
						{
						return;
					}
						if (Main.tile[num22, num23 - 1] == null)
						{
						return;
					}
						if (Main.tile[num22, num23 - 2] == null)
						{
						return;
					}
						if (Main.tile[num22, num23 - 3] == null)
						{
						return;
					}
						if (Main.tile[num22, num23 + 1] == null)
						{
						return;
					}
						if (Main.tile[num22 + NPC.direction, num23 - 1] == null)
						{
						return;
					}
						if (Main.tile[num22 + NPC.direction, num23 + 1] == null)
						{
						return;
					}
						if (Main.tile[num22 - NPC.direction, num23 + 1] == null)
						{
						return;
					}
						if (Main.tile[num22 + NPC.direction, num23 + 3] == null)
						{
						return;
					}
						int num24 = NPC.spriteDirection;
						
						if ((NPC.velocity.X < 0f && num24 == -1) || (NPC.velocity.X > 0f && num24 == 1))
						{
						//bool flag7 = NPC.type == 410 || NPC.type == 423;
						bool flag7 = true;
							float num25 = 3f;
							if (Main.tile[num22, num23 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[num22, num23 - 2].TileType])
							{
								if (Main.tile[num22, num23 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[num22, num23 - 3].TileType])
								{
									NPC.velocity.Y = -11.5f;
									NPC.netUpdate = true;
								}
								else
								{
									NPC.velocity.Y = -9.5f;
									NPC.netUpdate = true;
								}
							}
							else if (Main.tile[num22, num23 - 1].HasUnactuatedTile && !Main.tile[num22, num23 - 1].TopSlope && Main.tileSolid[Main.tile[num22, num23 - 1].TileType])
							{
								NPC.velocity.Y = -11f;
								NPC.netUpdate = true;
							}
							else if (NPC.position.Y + (float)NPC.height - (float)(num23 * 16) > 20f && Main.tile[num22, num23].HasUnactuatedTile && !Main.tile[num22, num23].TopSlope && Main.tileSolid[Main.tile[num22, num23].TileType])
							{
								NPC.velocity.Y = -9f;
								NPC.netUpdate = true;
							}
							else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > num25) && (!flag7 || !Main.tile[num22, num23 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[num22, num23 + 1].TileType]) && (!Main.tile[num22, num23 + 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[num22, num23 + 2].TileType]) && (!Main.tile[num22 + NPC.direction, num23 + 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[num22 + NPC.direction, num23 + 3].TileType]))
							{
								NPC.velocity.Y = -11f;
								NPC.netUpdate = true;
							}
						}
					}
				}
				if (NPC.type == 423 && Math.Abs(NPC.velocity.X) >= num11 * 0.95f)
				{
					Rectangle hitbox = NPC.Hitbox;
					for (int n = 0; n < 2; n++)
					{
                    if (Main.rand.NextBool(3))
						{
							Dust obj3 = Main.dust[Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 242)];
							obj3.velocity = Vector2.Zero;
							obj3.noGravity = true;
							obj3.fadeIn = 1f;
							obj3.scale = 0.5f + Main.rand.NextFloat();
						}
					}
				}
				
			

			/**
			NPC.rotation = NPC.velocity.ToRotation();
			NPC.EncourageDespawn(1);
			NPC.TargetClosest(true);
			Player target = Main.player[NPC.target];
			NPC.WithinRange(target.position + new Vector2(500, 0), 1);
			Vector3 coloring = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 1.9f % 0.4f, 0.4f, 0.5f).ToVector3() * 0.3f; // idk what hslToRgb is good for but it always gives me the best lighting so idc!
			spawndust++;
			if (spawndust > 15)
            {
				for (int i = 0; i < Main.rand.Next(1, 5); i++)
				{
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.BlueTorch, NPC.velocity.X, NPC.velocity.Y, 30);
				}
				spawndust = 0;
			}


					Lighting.AddLight(NPC.Center, coloring);
			if (Main.masterMode)
            {
				NPC.damage = 999;
            } */



			// Terraria.NPC
		}

        public override void OnKill()
        {
			Main.BestiaryTracker.Kills.RegisterKill(NPC);
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
		if (Main.hardMode)
            {
				return SpawnCondition.OverworldNight.Chance * 0.008f;
			}
		else
            {
				return SpawnCondition.OverworldNight.Chance * 0.015f;
			}
			
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < Main.rand.Next(12, 18); i++)
                {
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.XenonMoss, NPC.velocity.X + Main.rand.Next(-5, 5), NPC.velocity.Y + Main.rand.Next(-5, 5), 30);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.BlueFairy, NPC.velocity.X + Main.rand.Next(-5, 5), NPC.velocity.Y + Main.rand.Next(-5, 5), 30);
				}
				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//if (Main.rand.NextBool(5))
			//{
			//	npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 80, 1, 2));
			//	npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 2, 9));
			//	npcLoot.Add(ItemDropRule.Common(ItemID.SilverCoin, 1, 22, 91));
			//}
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			// I'm just gonna steal the spritedraw that I made for Spectral Mirrorman for NPC guy cause why not
			{
				Texture2D texture = TextureAssets.Npc[NPC.type].Value;
				
				SpriteEffects spriteEffects = SpriteEffects.None;


				if (NPC.direction == 1)
				
				{
					//spriteEffects = SpriteEffects.FlipHorizontally;
					//spriteEffects = SpriteEffects.FlipHorizontally;
					//NPC.spriteDirection = -1;

					spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.None;
				}

				if (NPC.direction == -1)
				
				{
					//NPC.spriteDirection = 1;
					spriteEffects = SpriteEffects.None | SpriteEffects.None;

					//SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

				}

				/*spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
                NPC.frame, drawColor, NPC.rotation,
                new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */

				//  spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchman_glowmask").Value, NPC.Center - screenPos,
				//    NPC.frame, Color.White, NPC.rotation,
				//   new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);


				// ^^^^ Want to add a glowmask cause that would be awesome
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, new Color(3, 20, 227, 1) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.50f, TextureAssets.Npc[NPC.type].Value.Height * 0.07f), NPC.scale, spriteEffects, 0f);

				
				/*
				for (int i = 0; i < 1; i++)
				{ // NPC is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
					//Vector2 RotateEffect = new Vector2(1, 1).RotatedBy((5 * MathF.Sin(Main.GlobalTimeWrappedHourly)) + i); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
					spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) , NPC.frame, new Color(3, 20, 227, 1) * (0.5f + 0.5f * ((250 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.11f), NPC.scale, spriteEffects, 0f);
				}
				*/
				for (int i = 0; i < 1; i++)
				{ // NPC is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
					//Vector2 RotateEffect = new Vector2(1, 1).RotatedBy((-5 * MathF.Sin(Main.GlobalTimeWrappedHourly)) + i); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
					spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, new Color(3, 20, 227, 1) * (0.5f + 0.5f * ((250 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.07f), NPC.scale, spriteEffects, 0f);
				}



				return false;
			}
		}

		public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Moon,
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.Plubee")
            });
		}
	}

	
	}
