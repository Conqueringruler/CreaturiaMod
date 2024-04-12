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

namespace Creaturia.NPCs.Creatures
{
	internal class Jackrabbit : ModNPC
	{



		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jackrabbit");
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Bunny];
			NPCID.Sets.CountsAsCritter[NPC.type] = true;
			NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[NPC.type] = true;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{

			NPC.width = 8;
			NPC.height = 28;
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.CloneDefaults(NPCID.Bunny);
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 500;
			NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 7;
			AnimationType = NPCID.Bunny;
		}
		int JumpTimer;
		public override void AI()
		{
			base.AI();
			//NPC.ai[0] = 1;
			if (((NPC.velocity.X < 2) && (NPC.velocity.X > -2)) && NPC.ai[0] == 1)
            {
				NPC.velocity.X *= 1.3f;
            }
			if (((NPC.velocity.X > 1.7f) || (NPC.velocity.X < -1.7f)) && (NPC.ai[0] == 1) && (NPC.velocity.Y == 0))
            {
				JumpTimer++;
            }
			if (JumpTimer > 20)
            {
				NPC.velocity.Y -= 4 +(2 * (MathF.Abs(NPC.velocity.X)));
				NPC.velocity.X *= 1.05f;
				JumpTimer = 0;
            }
			if (NPC.ai[2] == -1)
            {
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X + Main.rand.Next(-3, 3), NPC.velocity.Y + Main.rand.Next(-3, 3), default, Color.White, Main.rand.NextFloat(0.5f, 0.8f));
			}
		}
		
		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{




			return SpawnCondition.OverworldDayDesert.Chance * 0.2f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{

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

		public override void OnCaughtBy(Player player, Item item, bool failed)
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
		}


		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
				new FlavorTextBestiaryInfoElement("This wiry rabbit thrives in the desert dunes," +
												  "always on the search for snacks.")
			});
		}
	}

	internal class JackrabbitItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jackrabbit");
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
			Item.makeNPC = (short)NPCType<Jackrabbit>();
		}
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}