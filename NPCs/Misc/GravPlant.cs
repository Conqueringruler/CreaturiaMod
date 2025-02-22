using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Creaturia.Projectiles;
using Terraria.Graphics.Shaders;

namespace Creaturia.NPCs.Misc
{

	internal class GravPlant : ModNPC
	{
		public int PurpledustTimer;


		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gravity Flower");

		}

		public override void SetDefaults()
		{
			NPC.width = 68;
			NPC.height = 38;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 1;
			//NPC.HitSound = SoundID.Null;
			//NPC.DeathSound = SoundID.Item;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.rarity = 2;
			NPC.knockBackResist = 1f;
			NPC.ShowNameOnHover = false;
			NPC.lavaImmune = false;
			NPC.aiStyle = -1;
			NPC.friendly = false;
			NPC.dontTakeDamageFromHostiles = true;
			NPC.value = 0f;
			

		}
		

		/* public override void FindFrame(int frameHeight)
		{


			NPC.frameCounter++;

			if (NPC.frameCounter < 1)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			else if (NPC.frameCounter < 3)
			{
				NPC.frame.Y = 1 * frameHeight;
			}
			else if (NPC.frameCounter < 4)
			{
				NPC.frame.Y = 2 * frameHeight;
			}
			else if (NPC.frameCounter < 5)
			{
				NPC.frame.Y = 3 * frameHeight;
			}
			else
			{
				NPC.frameCounter = 0;
			}







		} */
		int counting;
		int counting2;
		float SinEnlarge;
		public override void AI()
		{

			//	Vector3 rgb = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.3f % 0.4f, 0.4f, 0.5f).ToVector3() * 0.3f;

			counting++;
			
			if (counting > 45)
			{
				counting2--;
			}
			if (counting2 < -45)
            {
				counting = -45;
				counting2 = 45;
            }
			//		Lighting.AddLight(NPC.Center, rgb);
			if (!NPC.collideY && counting < 45)
			{
				SinEnlarge = -0.4f * (float)Math.Sin(MathHelper.ToRadians(counting));
				NPC.velocity.Y = -0.4f * (float)Math.Sin(MathHelper.ToRadians(counting));
			}

			if (!NPC.collideY && counting > 45)
			{
				SinEnlarge = -0.4f * (float)Math.Sin(MathHelper.ToRadians(counting2));
				NPC.velocity.Y = -0.4f * (float)Math.Sin(MathHelper.ToRadians(counting2));
			}

			Lighting.AddLight(NPC.Center, Color.Violet.ToVector3() * 0.2f);

			PurpledustTimer++;
			if (PurpledustTimer >= 6)
			{
			//	Main.SetCameraLerp(10f, 100);
				//new EnvironmentMapEffect(IEffectFog);
				int dust = Dust.NewDust(NPC.Top - new Vector2 (8, 0), NPC.width, NPC.height, DustID.PurificationPowder, NPC.velocity.X, NPC.velocity.Y + 1, 10, Color.Purple, 0.4f);
				PurpledustTimer = 0;
				
			}
		}
		
		
		int scaleincrease;
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;


			/*spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */

			spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Misc/GravPlant_glowmask").Value, NPC.Center - screenPos,
			NPC.frame, Color.White, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

			spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Misc/GravPlant_glowmask").Value, NPC.Center - screenPos,
			NPC.frame, new Color(255, 255, 255, 10), NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale + ((Math.Abs(NPC.velocity.Y / 5)) * Math.Abs(SinEnlarge * 20)), spriteEffects, 0f);

			for (int i = 0; i < 4; i++)
			{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
				Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(186, 85, 211, 0) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale + (Math.Abs(NPC.velocity.Y / 10f)), spriteEffects, 0f);
			}





			return false;
		}



		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{                                                      // Did I come up with these spawn parameters or did I have it there temporarily for reference? 
			if (!Main.hardMode && (spawnInfo.SpawnTileY <= Main.maxTilesY - 200 && spawnInfo.SpawnTileY > (Main.rockLayer + Main.maxTilesY - 200) / 2))
			{
				return SpawnCondition.Cavern.Chance * (spawnInfo.Water ? 0 : 0.02f); // Can't believe I didn't know how to use the ternary conditional operator until now
			}
			else return SpawnCondition.Cavern.Chance * (spawnInfo.Water ? 0 : 0.012f);
			
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				
				new FlavorTextBestiaryInfoElement("This ancient plant grows anti-gravity juice. Awesome!")
			});
		}
		public override void HitEffect(NPC.HitInfo hit)
		{

			if (NPC.life <= 0)
			{
				for (int i = 0; i < 8; i++)
                {
					//int dust = Dust.NewDust(NPC.Top - new Vector2(8, 0), NPC.width, NPC.height, DustID.PurificationPowder, NPC.velocity.X + Main.rand.Next(-8, 8), NPC.velocity.Y + Main.rand.Next(-10, 1), 10, Color.Purple, 1f);
				}
				for (int i = 0; i < 6; i++)
                {
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(4, -10), new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(1, 5)), ModContent.ProjectileType<GravityGloop>(), 0, 0, default);
				}
				var Ssound = SoundID.Item87;
				Ssound.Pitch = -1f;
				
				SoundEngine.PlaySound(Ssound, NPC.position);

				int dustType = 244;
				var dust2 = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20)), NPC.width, NPC.height, dustType, 0, 0, 100, Color.MediumPurple);
				dust2.shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
				dust2.noGravity = true;


				var grassdust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height - 2, DustID.Cloud, 0f, 0f, 0, new Color(255, 255, 255), 1.2f);
				grassdust.shader = GameShaders.Armor.GetSecondaryShader(92, Main.LocalPlayer);
				grassdust.fadeIn = 0.75f;
				grassdust.scale = 1.2f;

				for (int i = 0; i < 15; i++)
                {
					grassdust = Dust.NewDustDirect(NPC.position, NPC.width + Main.rand.Next(-7, -1), NPC.height - 2, DustID.Cloud, 0f, 0f, 100, new Color(255, 255, 255), 1.2f);
					grassdust.shader = GameShaders.Armor.GetSecondaryShader(92, Main.LocalPlayer);
				}
				for (int i = 0; i < 15; i++)
				{
					grassdust = Dust.NewDustDirect(NPC.position, NPC.width + Main.rand.Next(1, 7), NPC.height - 2, DustID.Cloud, 0f, 0f, 100, new Color(255, 255, 255), 1.2f);
					grassdust.shader = GameShaders.Armor.GetSecondaryShader(92, Main.LocalPlayer);
				}
				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PoopSnakeGore1"), 2f); // No idea why GetGoreSlot no longer exists
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PoopSnakeGore2"), 1f);
			}


		}
		



	}
}