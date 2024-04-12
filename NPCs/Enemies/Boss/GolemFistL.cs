using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using System;
using System.Collections.Generic;

using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;
using System.IO;


using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.PlayerDrawLayer;


namespace Creaturia.NPCs.Enemies.Boss
{
	public class GolemFistL : ModNPC
	{
		int PumpkinDustTimer;
		bool SpeakInChat = false;
		bool DownwardPunch = false;
		bool DownwardPunchStart = false;
		int DownwardPunchStartTimer;

		int DownwardPunchingTimer;


		int PumpkinProjectileTimer;
		public override void SetStaticDefaults()
		{
			if (NPC.downedHalloweenKing)
            {
				DisplayName.SetDefault("Golem Fist");
			}
			
			if (NPC.downedHalloweenKing == false)
            {
				DisplayName.SetDefault("Fist of Fright");
			}
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true
			};
		}

		public override void SetDefaults()
		{
			
			NPC.width = 48;
			NPC.height = 40;
			NPC.damage = 80;
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
		public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
		{
			dataNPC.Entries.Remove(bestiaryEntry);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
		
			SpriteEffects spriteEffects = SpriteEffects.None;

			
			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
			//for (int i = 0; i < 4; i++)
			//     {

			if (NPC.downedHalloweenKing == false)
			{
				for (int i = 0; i < 4; i++)
				{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
					Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
					spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(252, 190, 30, 1) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
				}
			}
			//	spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/GolemFistL").Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
		//	NPC.frame, new Color(252, 190, 30, 1) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation,
		//  new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
		//	}
			
			return false;
		}


		
		public override void AI()
		{
			if (NPC.downedHalloweenKing == false)
			{
				NPC.GivenName = "Fist of Fright";
			}
			if (NPC.downedHalloweenKing)
			{
				NPC.GivenName = "Golem Fist";
				
			}

			Player target = Main.player[NPC.target];
			DownwardPunchStartTimer++;

			if (NPC.downedHalloweenKing)
			{
				if (DownwardPunchStartTimer > 400)
				{
					if (Main.rand.NextBool(3))
					{
						Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Firefly, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						for (int i = 0; i < 8; i++)
                        {
							dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Firefly, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);

						}

						DownwardPunchStart = true;
					}
					DownwardPunchStartTimer = 0;
				}
				if (DownwardPunchStart == true)
				{
					Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12)), NPC.width, NPC.height, DustID.Firefly, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
					DownwardPunchingTimer++;
					NPC.position.X = Main.player[NPC.target].position.X;
					NPC.position.Y = Main.player[NPC.target].position.Y - 190;
					NPC.rotation = (float)Math.Atan2(1f, 0f) + 0.785f;
					if (DownwardPunchingTimer > 150)
                    {
						NPC.velocity.Y += 10f;
						DownwardPunchingTimer = 0;
						DownwardPunchStart = false;
						DownwardPunch = false;
						DownwardPunchStartTimer = 0;
						NPC.netUpdate = true;
					}

				}
				if (DownwardPunchStart == false && DownwardPunch == false)
				{
					// One thing I've always wondered about decompiled code is whether all these var names are actually what was used. There's no way Relogic are actually calling this shit "num336, 337, 338", right?


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
			if (NPC.downedHalloweenKing == false)
            {
				if (DownwardPunchStartTimer > 250)
				{
					if (Main.rand.NextBool(2))
					{
						DownwardPunchStart = true;

						Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
						dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
					}
					DownwardPunchStartTimer = 0;
				}
				if (DownwardPunchStart == true)
				{
					DownwardPunchingTimer++;
					NPC.position.X = Main.player[NPC.target].position.X;
					NPC.position.Y = Main.player[NPC.target].position.Y - 190;
					NPC.rotation = (float)Math.Atan2(1f, 0f) + 0.785f;
					if (DownwardPunchingTimer > 100)
					{
						NPC.velocity.Y += 20f;
						DownwardPunchingTimer = 0;
						DownwardPunchStart = false;
						DownwardPunch = false;
						DownwardPunchStartTimer = 0;
					}

				}
				if (DownwardPunchStart == false && DownwardPunch == false)
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

						Vector2 directionshoot = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

						PumpkinProjectileTimer++;
						if (PumpkinProjectileTimer > 80)
                        {
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, directionshoot, ProjectileID.FlamingScythe, 95, 1f);
							}
							PumpkinProjectileTimer = 0;
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
			











			if (NPC.downedHalloweenKing == false)
            {
				Lighting.AddLight(NPC.Center, Color.Orange.ToVector3() * 0.4f);
				if (SpeakInChat == false)
                {
					if (Main.netMode == NetmodeID.Server) 
					{
						//NetMessage.
						ChatHelper.BroadcastChatMessage(NetworkText.FromKey("The Pumpking endows his power unto this fist..."), Color.Orange);
						
					}
					else if (Main.netMode == NetmodeID.SinglePlayer)
					{
						Main.NewText(Language.GetTextValue("The Pumpking endows his power unto this fist..."), Color.Orange);
					}

					SpeakInChat = true;
                }

				
				PumpkinDustTimer++;

				NPC.damage *= 2;
				
			}
			if (PumpkinDustTimer > 15)
            {
				Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.FlameBurst, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0));
				dust.noGravity = true;
				PumpkinDustTimer = 0;
            }				
			//Dust.NewDustPerfect(NPC.Center + PolarVector(30, NPC.rotation), DustID.HeatRay, NPC.velocity, 0, default, 1);
			if (!Main.expertMode && !Main.masterMode)
            {
				NPC.damage = 80;
			}
			if (Main.expertMode)
            {
				NPC.damage = 110;
			}
			if (Main.masterMode)
            {
				NPC.damage = 140;
            }
			if (NPC.AnyNPCs(NPCID.Golem) == false)
			{
				NPC.life = 0;
			}

			
			Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);


		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.OnFire3, 1600);
			target.AddBuff(BuffID.OnFire, 1600);

			if (Main.expertMode || Main.masterMode)
            {
				if (Main.rand.NextBool(3))
				{ 
					target.AddBuff(BuffID.Slow, 300);
				}

			}
          
        }

    }
}
