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
			DisplayName.SetDefault("Plubee");
			Main.npcCatchable[NPC.type] = false;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GemBunnyDiamond];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 2f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
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
			NPC.aiStyle = 3;
			NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Bunny;
			NPC.color = Color.DeepSkyBlue;
			//NPC.ShowNameOnHover = false;
			NPC.stepSpeed = 300;

			AIType = NPCID.DesertBeast;
			NPC.alpha = 200;
			NPC.Opacity = 200;
			NPC.target = -1;
		}
		public override void AI()
		{

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
	



		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			
			return SpawnCondition.OverworldNight.Chance * 0.01f;
		}

		public override void HitEffect(int hitDirection, double damage)
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
			if (Main.rand.NextBool(5))
			{
				//	Item.NewItem(NPC.getRect(), ItemID.Leather);
			}
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			// I'm just gonna steal the spritedraw that I made for Spectral Mirrorman for NPC guy cause why not
			{
				Texture2D texture = TextureAssets.Npc[NPC.type].Value;
				
				SpriteEffects spriteEffects = SpriteEffects.None;


				if (NPC.spriteDirection == 1)
				
				{
					//spriteEffects = SpriteEffects.FlipHorizontally;
					//spriteEffects = SpriteEffects.FlipHorizontally;
					//NPC.spriteDirection = -1;

					spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.None;
				}

				if (NPC.spriteDirection == -1)
				
				{
					//NPC.spriteDirection = 1;
					spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;

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
				new FlavorTextBestiaryInfoElement("A mischevious being known for taking the form of rabbits, \n" +
												  "awarding those lucky enough to capture it with treasures")
			});
		}
	}

	
	}
