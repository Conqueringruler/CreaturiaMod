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

namespace Creaturia.NPCs.Creatures
{

	internal class HummingBird1 : ModNPC
	{
        public int WindTimer;
		

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby-Throated Hummingbird");

			Main.npcCatchable[NPC.type] = true;
			NPC.catchItem = (short)ItemType<HummingBird1Item>();
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlackDragonfly];
		}

		public override void SetDefaults()
		{
			AIType = NPCID.BlackDragonfly;
			NPC.CloneDefaults(NPCID.BlackDragonfly);
			NPC.width = 30;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.catchItem = (short)ItemType<HummingBird1Item>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 114;
			AnimationType = NPCID.BlackDragonfly;
			NPC.friendly = true;
			
			
		}
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return true;
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

		public override void AI()
		{
			//	Vector3 rgb = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.3f % 0.4f, 0.4f, 0.5f).ToVector3() * 0.3f;
			


			//		Lighting.AddLight(NPC.Center, rgb);
		//	Lighting.AddLight(NPC.Center, Color.DeepPink.ToVector3() * 1f);
			NPC.catchItem = (short)ItemType<HummingBird1Item>();

			WindTimer++;
			if (WindTimer >= 3 && NPC.direction == 1)
			{

				int dust = Dust.NewDust(NPC.Left, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y + 1, 10, Color.LightGray, Main.rand.NextFloat(0.2f, 0.4f));
				
				WindTimer = 0;
				
				WindTimer++;
				
			}
			if (WindTimer >= 3 && NPC.direction == 0)
				{

				int dust = Dust.NewDust(NPC.Right, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y + 1, 10, Color.LightGray, Main.rand.NextFloat(0.2f, 0.4f));
					WindTimer = 0;
				}
			
		}
		
		

		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayBirdCritter.Chance * 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{

			if (NPC.life <= 0)
			{
				
				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PoopSnakeGore1"), 2f); // No idea why GetGoreSlot no longer exists
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/PoopSnakeGore2"), 1f);
			}


		}
        public override void OnCaughtBy(Player player, Item item, bool failed)
        {
			item.stack = 1;

			try
			{
				// I made the hummingbird so long ago that I don't remember where this came from, whether it's ExampleMod or ported from source code.
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0)
				{

					WorldGen.SquareTileFrame(NPCCenter.X, NPCCenter.Y, true);
				}
			}
			catch
			{
				return;
			}
		}
        
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
				new FlavorTextBestiaryInfoElement("A pretty little bird on the search for nectar and forest friends!"),
				
			}) ;
		}
	}

	internal class HummingBird1Item : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby-Throated Hummingbird");
			Tooltip.SetDefault("'Pretty!'");
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
			//item.width = 12;
			//item.height = 12;
			//item.makeNPC = 360;
			//item.noUseGraphic = true;
			//item.bait = 15;

			Item.CloneDefaults(ItemID.GlowingSnail);
			Item.makeNPC = (short)NPCType<HummingBird1>();
		}
	}
}