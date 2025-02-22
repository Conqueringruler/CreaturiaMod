using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;


using Terraria.Chat;


using static Terraria.ModLoader.ModContent;

using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.PlayerDrawLayer;


using System;
using System.Collections.Generic;


using System.IO;



namespace Creaturia.NPCs.Enemies.Boss
{
	public class GolemFistR : ModNPC
	{
		bool SpeakInChat = false;
		int IceQueenDustTimer;



		int IceProjectileTimer; 

		int UpwardPunchStartTimer;
		bool UpwardPunchStart;
		int UpwardPunchingTimer;
		bool UpwardPunching;
		bool UpwardPunch;
		public override void SetStaticDefaults()
		{
			if (NPC.downedChristmasIceQueen)
			{
				// DisplayName.SetDefault("Golem Fist");
			}
			
			if (NPC.downedChristmasIceQueen == false)
            {
				// DisplayName.SetDefault("Fist of Frost");
			}
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
		}
		public override void SetDefaults()
		{
			
			NPC.width = 48;
			NPC.height = 40;
			NPC.damage = 40;
			NPC.defense = 12;
			NPC.lifeMax = 1;
			NPC.HitSound = SoundID.NPCHit22;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.value = 0f;
			NPC.knockBackResist = 1f;
			//NPC.aiStyle = 23;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.dontTakeDamage = true;
		}
		
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;

			SpriteEffects spriteEffects = SpriteEffects.None;


				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
				new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
			//	for (int i = 0; i < 4; i++)
			//	{
			if (NPC.downedChristmasIceQueen == false)
			{
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
NPC.frame, drawColor, NPC.rotation,
new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
				for (int i = 0; i < 2; i++)
				{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
					Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
					spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/GolemFistRFrosty").Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(119, 187, 217, 1) * (0.4f + 0.7f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
				}
				return false;
			}
            //	spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/GolemFistR").Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
            //	NPC.frame, new Color(119, 187, 217, 10) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation,
            //  new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
            //	}
            else
            {
				return true;
            }
		}
		public override void AI()
		{
			if (NPC.downedChristmasIceQueen == false)
            {
				NPC.GivenName = "Fist of Frost";
            }
			if (NPC.downedChristmasIceQueen)
            {
				NPC.GivenName = "Golem Fist";
            }

			Player target = Main.player[NPC.target];
			UpwardPunchStartTimer++;
			if (NPC.downedChristmasIceQueen)
			{



				if (UpwardPunchStartTimer > 400)
				{
					if (Main.rand.NextBool(3))
					{
					//	Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					//	dust.noGravity = true;
						UpwardPunchStart = true;
					}
					UpwardPunchStartTimer = 0;
				}
				if (UpwardPunchStart == true)
				{
					Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
					UpwardPunchingTimer++;
					NPC.position.X = Main.player[NPC.target].position.X;
					NPC.position.Y = Main.player[NPC.target].position.Y + 190;
					NPC.rotation = (float)Math.Atan2(-1f, 0f) + 0.785f;
					if (UpwardPunchingTimer > 150)
					{
						NPC.velocity.Y -= 10f;
						UpwardPunchingTimer = 0;
						UpwardPunchStart = false;
						UpwardPunch = false;
						UpwardPunchStartTimer = 0;
					}

				}
				if (UpwardPunchStart == false && UpwardPunch == false)
				{

					if (NPC.ai[0] == 0f)
					{
						float num335 = 9f;
						Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num336 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector37.X;
						float num337 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector37.Y;
						float num338 = (float)Math.Sqrt(num336 * num336 + num337 * num337);
						float num339 = num338;
						num338 = num335 / num338;
						num336 *= num338;
						num337 *= num338;
						NPC.velocity.X = num336;
						NPC.velocity.Y = num337;
						NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 0.785f;
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}


					else if (NPC.ai[0] == 1f)
					{
						if (NPC.justHit)
						{
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
						}
						NPC.velocity *= 0.99f;
						NPC.ai[1] += 1f;
						if (NPC.ai[1] >= 100f)
						{
							NPC.netUpdate = true;
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
							NPC.velocity.X = 0f;
							NPC.velocity.Y = 0f;
						}
						else
						{
							NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 0.785f;
						}
					}
					else
					{
						if (NPC.justHit)
						{
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
						}
						NPC.velocity *= 0.96f;
						NPC.ai[1] += 1f;
						float num340 = NPC.ai[1] / 120f;
						num340 = 0.1f + num340 * 0.4f;
						NPC.rotation += num340 * (float)NPC.direction;
						if (NPC.ai[1] >= 120f)
						{
							NPC.netUpdate = true;
							NPC.ai[0] = 0f;
							NPC.ai[1] = 0f;
						}
					}
					if (NPC.localAI[0] == 0f)
					{
						NPC.TargetClosest();
						NPC.ai[0] = 1f;
						NPC.localAI[0] = 1f;
						NPC.netUpdate = true;
					}
				}
			}

			// Hyper mode!!!
			if (NPC.downedChristmasIceQueen == false)
			{
				if (UpwardPunchStartTimer > 250)
				{
					if (Main.rand.NextBool(2))
					{
						UpwardPunchStart = true;

						Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1.5f);
						dust.noGravity = true;
					}
					UpwardPunchStartTimer = 0;
				}
				if (UpwardPunchStart == true)
				{
					UpwardPunchingTimer++;
					NPC.position.X = Main.player[NPC.target].position.X;
					NPC.position.Y = Main.player[NPC.target].position.Y + 190;
					NPC.rotation = (float)Math.Atan2(-1f, 0f) + 0.785f;
					if (UpwardPunchingTimer > 100)
					{
						NPC.velocity.Y -= 20f;
						UpwardPunchingTimer = 0;
						UpwardPunchStart = false;
						UpwardPunch = false;
						UpwardPunchStartTimer = 0;
					}

				}
				if (UpwardPunchStart == false && UpwardPunch == false)
				{

					if (NPC.ai[0] == 0f)
					{
						float num335 = 9f;
						Vector2 vector37 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num336 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector37.X;
						float num337 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector37.Y;
						float num338 = (float)Math.Sqrt(num336 * num336 + num337 * num337);
						float num339 = num338;
						num338 = num335 / num338;
						num336 *= num338;
						num337 *= num338;
						NPC.velocity.X = num336 * 3;
						NPC.velocity.Y = num337 * 3;
						NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 0.785f;
						NPC.ai[0] = 1f;
						NPC.ai[1] = 0f;
						NPC.netUpdate = true;
					}


					else if (NPC.ai[0] == 1f)
					{
						if (NPC.justHit)
						{
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
						}
						NPC.velocity *= 0.96f;
						NPC.ai[1] += 1f;
						if (NPC.ai[1] >= 80f)
						{
							NPC.netUpdate = true;
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
							NPC.velocity.X = 0f;
							NPC.velocity.Y = 0f;
						}
						else
						{
							NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 0.785f;
						}
					}
					else
					{
						if (NPC.justHit)
						{
							NPC.ai[0] = 2f;
							NPC.ai[1] = 0f;
						}
						NPC.velocity *= 0.9f;
						NPC.ai[1] += 1f;
						float num340 = NPC.ai[1] / 120f;
						num340 = 0.3f + num340 * 0.5f;
						NPC.rotation += num340 * (float)NPC.direction;

						IceProjectileTimer++;
						if (IceProjectileTimer > 15)
						{
							int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-6, 6), Main.rand.Next(-6, 6)), ProjectileID.IceSpike, 25, 1f);
							if (Main.expertMode)
							{
								projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-6, 6), Main.rand.Next(-6, 6)), ProjectileID.IceSpike, 25, 1f);
							}

							if (Main.masterMode)
							{
								projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(Main.rand.Next(-6, 6), Main.rand.Next(-6, 6)), ProjectileID.IceSpike, 25, 1f);
							}
							IceProjectileTimer = 0;
						}

						if (NPC.ai[1] >= 50f)
						{
							NPC.netUpdate = true;
							NPC.ai[0] = 0f;
							NPC.ai[1] = 0f;
						}
					}
					if (NPC.localAI[0] == 0f)
					{
						NPC.TargetClosest();
						NPC.ai[0] = 1f;
						NPC.localAI[0] = 1f;
						NPC.netUpdate = true;
					}
				}
			}
			//Dust.NewDustPerfect(NPC.Center, DustID.HeatRay, default, 0, default, 1);
			if (NPC.downedChristmasIceQueen == false)
			{
				Lighting.AddLight(NPC.Center, Color.LightBlue.ToVector3() * 0.4f);
				if (SpeakInChat == false)
				{
					if (Main.netMode == NetmodeID.Server)
					{
						//NetMessage.
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey("The Ice Queen endows her power unto this fist..."), Color.LightBlue);

					}
					else if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(Language.GetTextValue("The Ice Queen endows her power unto this fist..."), Color.LightBlue);
					}

					SpeakInChat = true;
				}

				
				IceQueenDustTimer++;

				NPC.damage *= 2;

			}
			if (IceQueenDustTimer > 15)
			{
				Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0));
				dust.noGravity = true;
				IceQueenDustTimer = 0;
			}

			if (!Main.expertMode && !Main.masterMode)
            {
				NPC.damage = 30;
			}
			if (Main.expertMode)
            {
				NPC.damage = 40;
			}
			if (Main.masterMode)
            {
				NPC.damage = 65;
            }
			if (NPC.AnyNPCs(NPCID.Golem) == false)
			{
				NPC.life = 0;
			}

			
			Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);


		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(BuffID.Chilled, 600);
			base.OnHitPlayer(target, hurtInfo);

			if (Main.expertMode || Main.masterMode)
			{
				if (Main.rand.NextBool(3))
				{
					target.AddBuff(BuffID.Slow, 100);
				}

			}
		}
	}
}
