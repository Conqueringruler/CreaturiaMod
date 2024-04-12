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
	internal class DungeonFrogEmpty : ModNPC
	{


		public override string Texture => "Terraria/Images/NPC_" + NPCID.Frog;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dungeon Frog");
			Main.npcCatchable[NPC.type] = false;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Frog];
			NPCID.Sets.CountsAsCritter[Type] = true;
			NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[NPC.type] = true;
			//Main.npcCatchable[Type] = false;
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
			
			bestiaryEntry.AddTags();
		}
		public override void SetDefaults()
		{

			NPC.width = 8;
			NPC.height = 28;
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 500;
			//NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 7;
			NPC.friendly = true;
			//NPC.dontTakeDamageFromHostiles = true;
			AnimationType = NPCID.Frog;
			NPC.ShowNameOnHover = true;
            
			NPC.color = Color.SkyBlue;
		}
		public override void AI()
		{
			base.AI();
			Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 0.6f);
		}
		


		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			if (NPC.downedPlantBoss == true)
            {
				return SpawnCondition.Dungeon.Chance * 0.05f;
			}
			else return SpawnCondition.Dungeon.Chance * 0f;
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 10; i++)
                {
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.BlueTorch, NPC.velocity.X, NPC.velocity.Y, 30);
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