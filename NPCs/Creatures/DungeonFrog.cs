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

namespace Creaturia.NPCs.Creatures
{
	internal class DungeonFrog : ModNPC
	{


		public override string Texture => "Terraria/Images/NPC_" + NPCID.Frog;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dungeon Frog");
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Frog];
			NPCID.Sets.CountsAsCritter[Type] = true;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // frog runs now in bestiary!! yay :D
				Velocity = -1f
			};
		}

		public override void SetDefaults()
		{
			NPC.width = 20;
			NPC.height = 30;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.scale = 2f;
			NPC.stepSpeed = 500;
			//NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 7;
			NPC.friendly = true;
			NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Frog;
			NPC.ShowNameOnHover = false;
			NPC.color = Color.SlateBlue;
		}
		public override void AI()
		{
			base.AI();
			NPC.ShowNameOnHover = false;
			Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 1f);

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
				Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos  + new Vector2(0, NPC.gfxOffY) + circular , NPC.frame, new Color(20, 50, 200, 0) * (0.2f + 0.5f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.019f), NPC.scale * 1.05f, spriteEffects, 0f);
			}


			
			return false;
		} 

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			if (NPC.downedPlantBoss == true)
            {
				return SpawnCondition.Dungeon.Chance * 0.015f;
			}
			else return SpawnCondition.Dungeon.Chance * 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 10; i++)
                {
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.TintableDustLighted, NPC.velocity.X, NPC.velocity.Y, 30);
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				
				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.,
				new FlavorTextBestiaryInfoElement("A mysterious inhabitant of the dungeon,\n" +
												  "subsisting off things that it really shouldn't.")
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