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
using Terraria.Graphics.CameraModifiers;

namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{
	public class DunklerFish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dunkle");
		}
		bool Spawned = true;
		bool StartUpwards;

		public override void SetDefaults()
		{

			NPC.width = 290;
			NPC.height = 108; // Change ALL defaults 
			NPC.damage = 70;
			NPC.defense = 80;
			NPC.lifeMax = 7500;
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
			NPC.value = Item.buyPrice(gold: 4, silver: 80);

			NPC.boss = true;
			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/underwater_fishy_terrariaminiboss");
			}
		}
		Player target;
		NPCAimedTarget targetData;

		float RotationSpeed = 0.04f;
		bool CircleModeActive = true;
		bool GoAbove = false;
		int SwitchFromCircleMode;
		public override void AI()
		{



			//if (NPC.rotation < NPC.AngleTo(Main.player[NPC.target].Center))
			//{
			//NPC.rotation = MathHelper.Lerp(NPC.rotation, NPC.AngleTo(Main.player[NPC.target].Center), 0.09f);
			//}

			

			NPC.knockBackResist = 0f;
			targetData = NPC.GetTargetData();
			
	
			target = Main.player[NPC.target];
			if (target == null || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest();
			}
			//NPC.rotation = (target.Center - NPC.Center).ToRotation();
			if (NPC.position.Y < target.position.Y)
            {
				NPC.rotation = MathHelper.Lerp(NPC.rotation, (target.Center - NPC.Center).ToRotation(), 0.08f);
			}
			if (NPC.position.Y > target.position.Y)
            {
				NPC.rotation = MathHelper.Lerp(NPC.rotation, (NPC.Center - target.Center).ToRotation(), 0.08f);
			}
			
			if (CircleModeActive)
            {
				CircleMode();
			}
			if (GoAbove)
            {
				GoAboveFromDukeFishron();
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
		bool CornerDashing;
		void CircleMode()
		{
			
			if (SwitchFromCircleMode > 3)
            {
				GoAboveFromDukeFishron();
				CircleModeActive = false;
				GoAbove = true;
				SwitchFromCircleMode = 0;
            }

			Vector2 directionshoot = (target.Center - NPC.Center + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5))).SafeNormalize(Vector2.UnitX);
			//NPC.position = Vector2.Lerp(NPC.position, (Main.player[NPC.target].position + new Vector2((70 * MathF.Cos(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi), (70 * MathF.Sin(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi))), 0.4f);

			//NPC.rotation = (float)Math.Atan2((double)num5, (double)num4);
			//Utils.GetLerpValue(NPC.position, (Main.player[NPC.target].position + new Vector2((70 * MathF.Cos(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi), (70 * MathF.Sin(Main.GlobalTimeWrappedHourly) * MathHelper.TwoPi))), (Main.GlobalTimeWrappedHourly / 100), false);
			FlamesTimer++;
			if (FlamesTimer > 200)
            {
				FlamesChargeTimer++;
				if (FlamesChargeTimer is 20)
                {
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(90f, 0f).RotatedBy(NPC.rotation), directionshoot * (float)Main.rand.Next(5, 9), ProjectileID.CursedFlameHostile, 35, 0);
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
			if (Main.netMode != 1)
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
			}
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

		
		void GoAboveFromDukeFishron()
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
						NPC.rotation -= num22;
					}
					else
					{
						NPC.rotation += num22;
					}
				}
				if (NPC.rotation > num21)
				{
					if ((double)(NPC.rotation - num21) > Math.PI)
					{
						NPC.rotation += num22;
					}
					else
					{
						NPC.rotation -= num22;
					}
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
					NPC.rotation = num21;
				}
				if (NPC.rotation < 0f)
				{
					NPC.rotation += (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > (float)MathHelper.TwoPi)
				{
					NPC.rotation -= (float)MathHelper.TwoPi;
				}
				if (NPC.rotation > num21 - num22 && NPC.rotation < num21 + num22)
				{
					NPC.rotation = num21;
				}
				float num3 = 0.5f;
				float num4 = 8f;
				Vector2 vector3 = Vector2.Normalize(target.Center + new Vector2(NPC.ai[1], -200f) - NPC.Center - NPC.velocity) * num4;
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
					NPC.velocity = NPC.DirectionTo(CornerTargetPosition) * 15f;
				}
				if (CornerDashingTimer > 30)
                {

					CornerDashingTimer = 0;
					CornerDashing = false;
					GoingToCorner = true;

					if (Main.rand.NextBool(3))
                    {
						CircleModeActive = true;
						GoAbove = false;
						
					}

                }
				
            }
		}
		void QuickDashesMode()
		{

		}
		void ArmorCrackOrSmthn()
		{

		}



		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;




			if (NPC.position.X > target.position.X)
			{
				

				//spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;

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

				//SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

			}
			

			for (int i = 0; i < 4; i++)
			{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
				Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(60, 53, 157, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale * 1.03f * (1 + (MathF.Abs(NPC.velocity.X) / 150 + MathF.Abs(NPC.velocity.Y) / 150)), spriteEffects, 0f);
			}


			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

			spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/DunklerFish_Tail").Value, (NPC.Center + new Vector2(0f, 0f).RotatedBy(NPC.rotation)) - screenPos,
			NPC.frame, new Color(60, 53, 157, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

			/*spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/CrimsonFishMiniBoss_EyeGlow").Value, NPC.Center - screenPos,
			NPC.frame, Color.White, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */
			// ^ Replace with Dunkle glow
			return false;
		}
	}
	
}

