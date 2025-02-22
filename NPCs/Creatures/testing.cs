using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Creaturia;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Creaturia.Items;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Achievements;

namespace Creaturia.NPCs.Creatures
{
    internal class testing : ModNPC
    {
		public override string Texture => "Terraria/Images/NPC_" + NPCID.FlyingFish;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fae"); // Testing porting source code
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.FlyingFish];
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
		}

        public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 2;
			NPC.lifeMax = 45;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[BuffID.Poisoned] = true;
			NPC.catchItem = (short)ItemType<HoopSnakeItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = -1;
			AnimationType = NPCID.FlyingFish;
			NPC.dontTakeDamageFromHostiles = true;
		}
		// Basically all I wanted to do here was see if I could port the Fairy code. 
		private Vector2 GetFairyCircleOffset(float elapsedTime, float circleRotation, float circleHeight)
		{
			return ((((float)Math.PI * 2f * elapsedTime + (float)Math.PI / 2f).ToRotationVector2() + new Vector2(0f, -1f)) * new Vector2(6 * -NPC.direction, circleHeight)).RotatedBy(circleRotation);
		}

		private void GetBirdFlightRecommendation(int downScanRange, int upRange, Point tCoords, out bool goDownwards, out bool goUpwards)
		{
			tCoords.X += NPC.direction;
			goDownwards = true;
			goUpwards = false;
			int x = tCoords.X;
			for (int i = tCoords.Y; i < tCoords.Y + downScanRange && WorldGen.InWorld(x, i); i++)
			{
				Tile tile = Main.tile[x, i];
				if (tile == null)
				{
					break;
				}
				if ((tile.HasUnactuatedTile && Main.tileSolid[tile.TileType]) || tile.LiquidType > 0)
				{
					if (i < tCoords.Y + upRange)
					{
						goUpwards = true;
					}
					goDownwards = false;
					break;
				}
			}
		}

		 bool GetFairyTreasureCoords(out Point treasureCoords)
		{
			treasureCoords = default(Point);
			Point point = NPC.Center.ToTileCoordinates();
			Rectangle value = new Rectangle(point.X, point.Y, 1, 1);
			value.Inflate(75, 50);
			int num = 40;
			Rectangle value2 = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
			value2.Inflate(-num, -num);
			value = Rectangle.Intersect(value, value2);
			int num2 = -1;
			float num3 = -1f;
			for (int i = value.Left; i <= value.Right; i++)
			{
				for (int j = value.Top; j <= value.Bottom; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null || !tile.HasTile || !TileID.Sets.FriendlyFairyCanLureTo[tile.TileType] || !SceneMetrics.IsValidForOreFinder(tile))
					{
						continue;
					}
					short num4 = Main.tileOreFinderPriority[tile.TileType];
					if (TileID.Sets.Ore[tile.TileType])
					{
						int num5 = 3;
						int num6 = 3;
						int num7 = 40;
						int num8 = 0;
						for (int k = i - num5; k <= i + num5; k++)
						{
							for (int l = j - num6; l <= j + num6; l++)
							{
								if (Main.tile[k, l].HasTile && Main.tile[k, l].TileType == tile.TileType)
								{
									num8++;
								}
							}
						}
						if (num8 < num7)
						{
							num4 = -1;
						}
					}
					if (num2 <= num4)
					{
						float num9 = NPC.Distance(new Vector2(i * 16 + 8, j * 16 + 8));
						if (num2 != num4 || !(num9 >= num3))
						{
							num2 = num4;
							num3 = num9;
							treasureCoords.X = i;
							treasureCoords.Y = j;
						}
					}
				}
			}
			return num2 != -1;
		}

		public override void AI()
        {
            
        
        bool flag = false;
		NPC.lavaImmune = true;
		if (Main.netMode != 1 && NPC.ai[2] > 1f)
		{
			int num = 18000;
			NPC.localAI[1] += 1f;
			if (NPC.localAI[1] >= (float)num)
			{
				NPC.ai[2] = 7f;
				if (Main.player[NPC.target].Center.X < NPC.Center.X)
				{
					NPC.direction = 1;
				}
				else
				{
					NPC.direction = -1;
				}
				NPC.netUpdate = true;
			}
		}
		switch ((int)NPC.ai[2])
		{
			case 0:
				{
					NPC.lavaImmune = false;
					NPC.noTileCollide = false;
					if (NPC.ai[0] == 0f && NPC.ai[1] == 0f)
					{
						NPC.ai[0] = NPC.Center.X;
						NPC.ai[1] = NPC.Center.Y;
					}
					if (NPC.localAI[0] == 0f)
					{
						NPC.localAI[0] = 1f;
						NPC.velocity = new Vector2(MathHelper.Lerp(2f, 4f, Main.rand.NextFloat()) * (float)(Main.rand.Next(2) * 2 - 1), MathHelper.Lerp(1f, 2f, Main.rand.NextFloat()) * (float)(Main.rand.Next(2) * 2 - 1));
						NPC.velocity *= 0.7f;
						NPC.netUpdate = true;
					}
					Vector2 vector4 = new Vector2(NPC.ai[0], NPC.ai[1]) - NPC.Center;
					if (vector4.Length() > 20f)
					{
						Vector2 vector5 = new Vector2((vector4.X > 0f) ? 1 : (-1), (vector4.Y > 0f) ? 1 : (-1));
						NPC.velocity += vector5 * 0.04f;
						if (Math.Abs(NPC.velocity.Y) > 2f)
						{
							NPC.velocity.Y *= 0.95f;
						}
					}
					NPC.TargetClosest();
					Player player = Main.player[NPC.target];
					if (!player.dead && player.Distance(NPC.Center) < 250f)
					{
						NPC.ai[2] = 1f;
						NPC.direction = ((!(player.Center.X > NPC.Center.X)) ? 1 : (-1));
						if (NPC.velocity.X * (float)NPC.direction < 0f)
						{
							NPC.velocity.X = NPC.direction * 2;
						}
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
					}
					break;
				}
			case 1:
				{
					NPC.lavaImmune = false;
					NPC.noTileCollide = false;
					if (NPC.collideX)
					{
						NPC.direction *= -1;
						NPC.velocity.X = NPC.direction * 2;
					}
					if (NPC.collideY)
					{
						NPC.velocity.Y = ((NPC.oldVelocity.Y > 0f) ? 1 : (-1));
					}
					float num12 = 4.5f;
					if (Math.Sign(NPC.velocity.X) != NPC.direction || Math.Abs(NPC.velocity.X) < num12)
					{
						NPC.velocity.X += (float)NPC.direction * 0.04f;
						if (NPC.velocity.X * (float)NPC.direction < 0f)
						{
							if (Math.Abs(NPC.velocity.X) > num12)
							{
								NPC.velocity.X += (float)NPC.direction * 0.4f;
							}
							else
							{
								NPC.velocity.X += (float)NPC.direction * 0.2f;
							}
						}
						else if (Math.Abs(NPC.velocity.X) > num12)
						{
							NPC.velocity.X = (float)NPC.direction * num12;
						}
					}
					int num13 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f);
					int num14 = 20;
					if (NPC.direction < 0)
					{
						num13 -= num14;
					}
					int num15 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
					bool flag5 = true;
					int num16 = 8;
					bool flag6 = false;
						
					for (int i = num13; i <= num13 + num14; i++)
					{
						for (int j = num15; j < num15 + num16; j++)
						{
								Tile tile = Main.tile[i, j];
								if (tile == null)
							{
								tile = new Tile();
							}
							if ((tile.HasUnactuatedTile && Main.tileSolid[Main.tile[i, j].TileType]) || Main.tile[i, j].LiquidType > 0)
							{
								if (j < num15 + 5)
								{
									flag6 = true;
								}
								flag5 = false;
								break;
							}
						}
					}
					if (flag5)
					{
						NPC.velocity.Y += 0.05f;
					}
					else
					{
						NPC.velocity.Y -= 0.2f;
					}
					if (flag6)
					{
						NPC.velocity.Y -= 0.3f;
					}
					if (NPC.velocity.Y > 3f)
					{
						NPC.velocity.Y = 3f;
					}
					if (NPC.velocity.Y < -5f)
					{
						NPC.velocity.Y = -5f;
					}
					break;
				}
			case 2:
				{
					NPC.noTileCollide = true;
					NPCAimedTarget targetData = NPC.GetTargetData();
					bool flag3 = false;
					if (targetData.Type == NPCTargetType.Player)
					{
						flag3 = Main.player[NPC.target].dead;
					}
					if (flag3)
					{
						NPC.ai[2] = 1f;
						NPC.direction = ((!(targetData.Center.X > NPC.Center.X)) ? 1 : (-1));
						if (NPC.velocity.X * (float)NPC.direction < 0f)
						{
							NPC.velocity.X = NPC.direction * 2;
						}
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
						break;
					}
					Rectangle r = Utils.CenteredRectangle(targetData.Center, new Vector2(targetData.Width + 60, targetData.Height / 2));
					if (Main.netMode != 1 && NPC.Hitbox.Intersects(r))
					{
						if (GetFairyTreasureCoords(out var treasureCoords2))
						{
							NPC.ai[0] = treasureCoords2.X;
							NPC.ai[1] = treasureCoords2.Y;
							NPC.ai[2] = 3f;
							NPC.ai[3] = 0f;
							NPC.netUpdate = true;
						}
						else
						{
							NPC.ai[2] = 6f;
							NPC.ai[3] = 0f;
							NPC.netUpdate = true;
						}
						break;
					}
					Vector2 vector3 = r.ClosestPointInRect(NPC.Center);
					Vector2 value = NPC.DirectionTo(vector3) * 2f;
					float num8 = NPC.Distance(vector3);
					if (num8 > 150f)
					{
						value *= 2f;
					}
					else if (num8 > 80f)
					{
						value *= 1.5f;
					}
					NPC.velocity = Vector2.Lerp(NPC.velocity, value, 0.07f);
					Point point = NPC.Center.ToTileCoordinates();
					if (NPC.ai[3] < 300f)
					{
						GetBirdFlightRecommendation(6, 3, point, out var goDownwards, out var goUpwards);
						if (goDownwards)
						{
							NPC.velocity.Y += 0.05f;
						}
						if (goUpwards)
						{
							NPC.velocity.Y -= 0.02f;
						}
						if (NPC.velocity.Y > 2f)
						{
							NPC.velocity.Y = 2f;
						}
						if (NPC.velocity.Y < -4f)
						{
							NPC.velocity.Y = -4f;
						}
					}
					if (WorldGen.InWorld(point.X, point.Y))
					{
						if (WorldGen.SolidTile(point))
						{
							NPC.ai[3] = Math.Min(NPC.ai[3] + 2f, 400f);
						}
						else
						{
							NPC.ai[3] = Math.Max(NPC.ai[3] - 1f, 0f);
						}
					}
					break;
				}
			case 3:
				NPC.noTileCollide = true;
				if (NPC.ai[3] == 15f)
				{
						SoundEngine.PlaySound(SoundID.Item27);
					}
				if (NPC.ai[3] <= 15f)
				{
					NPC.velocity *= 0.9f;
				}
				else
				{
					if (Main.player[NPC.target].Center.X > NPC.Center.X)
					{
						NPC.spriteDirection = -1;
					}
					else
					{
						NPC.spriteDirection = 1;
					}
					flag = true;
					float num6 = 0f;
					float num7 = NPC.ai[3] - 15f;
					float circleHeight = 22f;
					if (num7 <= 65f)
					{
						num6 = (float)Math.PI / 8f;
						circleHeight = 14f;
					}
					else if (num7 <= 130f)
					{
						num6 = -(float)Math.PI / 8f;
						circleHeight = 18f;
					}
					num6 *= (float)NPC.direction;
					Vector2 fairyCircleOffset3 = GetFairyCircleOffset(num7 / 65f, num6, circleHeight);
					Vector2 fairyCircleOffset4 = GetFairyCircleOffset(num7 / 65f + 0.0153846154f, num6, circleHeight);
					NPC.velocity = fairyCircleOffset4 - fairyCircleOffset3;
				}
				NPC.ai[3] += 1f;
				if (NPC.ai[3] >= 210f)
				{
					NPC.ai[2] = 4f;
					NPC.TargetClosest();
					NPC.ai[3] = 0f;
					NPC.netUpdate = true;
				}
				break;
			case 6:
				{
					NPC.noTileCollide = true;
					Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
					if (vector.Length() > 100f)
					{
						NPC.ai[2] = 2f;
						NPC.TargetClosest();
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
						break;
					}
					if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
					{
						NPC.noTileCollide = false;
						if (NPC.collideX)
						{
							NPC.velocity.X *= -1f;
						}
						if (NPC.collideY)
						{
							NPC.velocity.Y *= -1f;
						}
					}
					if (vector.Length() > 20f)
					{
						Vector2 vector2 = new Vector2((vector.X > 0f) ? 1 : (-1), (vector.Y > 0f) ? 1 : (-1));
						NPC.velocity += vector2 * 0.04f;
						if (Math.Abs(NPC.velocity.Y) > 2f)
						{
							NPC.velocity.Y *= 0.95f;
						}
					}
					if (Main.netMode != 1 && GetFairyTreasureCoords(out var treasureCoords))
					{
						NPC.ai[0] = treasureCoords.X;
						NPC.ai[1] = treasureCoords.Y;
						NPC.ai[2] = 3f;
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
					}
					break;
				}
			case 4:
				{
					NPC.noTileCollide = true;
					NPCAimedTarget targetData2 = NPC.GetTargetData();
					bool flag4 = false;
					if (targetData2.Type == NPCTargetType.Player)
					{
						flag4 = Main.player[NPC.target].dead;
					}
					if (flag4)
					{
						NPC.ai[2] = 1f;
						NPC.direction = ((!(targetData2.Center.X > NPC.Center.X)) ? 1 : (-1));
						if (NPC.velocity.X * (float)NPC.direction < 0f)
						{
							NPC.velocity.X = NPC.direction * 2;
						}
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
						break;
					}
					Rectangle r2 = Utils.CenteredRectangle(new Vector2(NPC.ai[0] * 16f + 8f, NPC.ai[1] * 16f + 8f), Vector2.One * 5f);
					if (NPC.Hitbox.Intersects(r2))
					{
						NPC.ai[2] = 5f;
						NPC.ai[3] = 0f;
						NPC.netUpdate = true;
						break;
					}
					float num9 = NPC.Distance(targetData2.Center);
					float num10 = 300f;
					if (num9 > num10)
					{
						if (num9 < num10 + 100f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
						{
							NPC.noTileCollide = false;
							if (NPC.collideX)
							{
								NPC.velocity.X *= -1f;
							}
							if (NPC.collideY)
							{
								NPC.velocity.Y *= -1f;
							}
						}
						flag = true;
						if (Main.player[NPC.target].Center.X > NPC.Center.X)
						{
							NPC.spriteDirection = -1;
						}
						else
						{
							NPC.spriteDirection = 1;
						}
						Vector2 vector6 = NPC.DirectionFrom(targetData2.Center);
						if (num9 > num10 + 60f)
						{
							NPC.velocity += vector6 * -0.1f;
							if (Main.rand.Next(30) == 0)
							{
									SoundEngine.PlaySound(SoundID.Item27);
								}
						}
						else if (num9 < num10 + 30f)
						{
							Vector2 destination = r2.ClosestPointInRect(NPC.Center);
							Vector2 vector7 = NPC.DirectionTo(destination);
							NPC.velocity += vector7 * 0.1f;
						}
						if (NPC.velocity.Length() > 1f)
						{
							NPC.velocity *= 1f / NPC.velocity.Length();
						}
						break;
					}
					Vector2 vector8 = r2.ClosestPointInRect(NPC.Center);
					Vector2 value2 = NPC.DirectionTo(vector8);
					float num11 = NPC.Distance(vector8);
					if (num11 > 150f)
					{
						value2 *= 3f;
					}
					else if (num11 > 80f)
					{
						value2 *= 2f;
					}
					Point point2 = NPC.Center.ToTileCoordinates();
					if (NPC.ai[3] < 300f)
					{
						NPC.velocity = Vector2.Lerp(NPC.velocity, value2, 0.07f);
						GetBirdFlightRecommendation(4, 2, point2, out var goDownwards2, out var goUpwards2);
						if (goDownwards2)
						{
							NPC.velocity.Y += 0.05f;
						}
						if (goUpwards2)
						{
							NPC.velocity.Y -= 0.05f;
						}
						if (NPC.velocity.Y > 1f)
						{
							NPC.velocity.Y = 1f;
						}
						if (NPC.velocity.Y < -1f)
						{
							NPC.velocity.Y = -1f;
						}
					}
					else
					{
						NPC.velocity = Vector2.Lerp(NPC.velocity, value2, 0.07f);
					}
					if (WorldGen.SolidTile(point2))
					{
						NPC.ai[3] = Math.Min(NPC.ai[3] + 2f, 400f);
					}
					else
					{
						NPC.ai[3] = Math.Max(NPC.ai[3] - 1f, 0f);
					}
					break;
				}
			case 5:
				{
					NPC.localAI[1] = 0f;
					NPC.noTileCollide = true;
					bool flag2 = false;
					Tile tileSafely = Framing.GetTileSafely(new Point((int)NPC.ai[0], (int)NPC.ai[1]));
					if (!tileSafely.HasTile || !SceneMetrics.IsValidForOreFinder(tileSafely))
					{
						flag2 = true;
					}
					if (NPC.ai[3] == 15f)
					{
							SoundEngine.PlaySound(SoundID.Item27);
					}
					if (NPC.ai[3] <= 15f)
					{
						NPC.velocity *= 0.9f;
					}
					else
					{
						flag = true;
						float num2 = 0f;
						float num3 = NPC.ai[3] - 15f;
						float num4 = 22f;
						int num5 = (int)(num3 / 50f);
						num2 = (float)Math.Cos((float)num5 * 1f) * ((float)Math.PI * 2f) / 16f;
						num4 = (float)Math.Cos((float)num5 * 2f) * 10f + 8f;
						num2 *= (float)NPC.direction;
						Vector2 fairyCircleOffset = GetFairyCircleOffset(num3 / 50f, num2, num4);
						Vector2 fairyCircleOffset2 = GetFairyCircleOffset(num3 / 50f + 0.02f, num2, num4);
						NPC.velocity = fairyCircleOffset2 - fairyCircleOffset;
						if (Main.player[NPC.target].Center.X > NPC.Center.X)
						{
							NPC.spriteDirection = -1;
						}
						else
						{
							NPC.spriteDirection = 1;
						}
					}
					NPC.ai[3] += 1f;
					if (Main.netMode != 1 && ((NPC.ai[3] > 200f) ? true : false))
					{
						NPC.active = false;
						if (Main.netMode == 0)
						{
							NPC.FairyEffects(NPC.Center, NPC.type - 583);
						}
						else if (Main.netMode == 2)
						{
							NPC.netSkip = -1;
							NPC.life = 0;
							NetMessage.SendData(23, -1, -1, null, NPC.whoAmI);
							NetMessage.SendData(112, -1, -1, null, 2, (int)NPC.Center.X, (int)NPC.Center.Y, 0f, NPC.type - 583);
						}
					}
					break;
				}
			case 7:
				NPC.noTileCollide = true;
				NPC.velocity.X += 0.05f * (float)NPC.direction;
				NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -10f, 10f);
				NPC.velocity.Y -= 0.025f;
				NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -5f, 5f);
				NPC.EncourageDespawn(10);
				break;
		}
		NPC.dontTakeDamage = (NPC.dontTakeDamageFromHostiles = NPC.ai[2] > 1f);
		for (int k = 0; k < 200; k++)
		{
			if (k != NPC.whoAmI && Main.npc[k].active && Main.npc[k].aiStyle == 112 && Math.Abs(NPC.position.X - Main.npc[k].position.X) + Math.Abs(NPC.position.Y - Main.npc[k].position.Y) < (float)NPC.width * 1.5f)
			{
				if (NPC.position.Y < Main.npc[k].position.Y)
				{
					NPC.velocity.Y -= 0.05f;
				}
				else
				{
					NPC.velocity.Y += 0.05f;
				}
			}
		}
		if (!flag)
		{
			NPC.direction = ((NPC.velocity.X >= 0f) ? 1 : (-1));
			NPC.spriteDirection = -NPC.direction;
		}
		Color value3 = Color.HotPink;
		Color value4 = Color.LightPink;
		int num17 = 4;
		if (NPC.type == 584)
		{
			value3 = Color.LimeGreen;
			value4 = Color.LightSeaGreen;
		}
		if (NPC.type == 585)
		{
			value3 = Color.RoyalBlue;
			value4 = Color.LightBlue;
		}
		if ((int)Main.timeForVisualEffects % 2 == 0)
		{
			NPC.position += NPC.netOffset;
			Dust dust = Dust.NewDustDirect(NPC.Center - new Vector2(num17) * 0.5f, num17 + 4, num17 + 4, 278, 0f, 0f, 200, Color.Lerp(value3, value4, Main.rand.NextFloat()), 0.65f);
			dust.velocity *= 0f;
			dust.velocity += NPC.velocity * 0.3f;
			dust.noGravity = true;
			dust.noLight = true;
			NPC.position -= NPC.netOffset;
		}
		Lighting.AddLight(NPC.Center, value3.ToVector3() * 0.7f);
		if (Main.netMode != 2)
		{
			Player localPlayer = Main.LocalPlayer;
			if (!localPlayer.dead && localPlayer.HitboxForBestiaryNearbyCheck.Intersects(NPC.Hitbox))
			{
				AchievementsHelper.HandleSpecialEvent(localPlayer, 22);
			}
		}
	}

}
}
