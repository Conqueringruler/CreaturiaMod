using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;

namespace Creaturia.NPCs.Creatures
{
	public class MantisShrimp : ModNPC
	{
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;


		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mantis Shrimp"); //canceled this NPC since Calamity already has a fucking mantis shrimp. and theirs is mid. im so malding rn
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true
			};
		}

		public override void SetDefaults()
		{

			NPC.width = 30;
			NPC.height = 28; // Change ALL defaults 
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.lifeMax = 25;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//NPC.stepSpeed = 500; Speed of frames going by
			NPC.lavaImmune = false;
			NPC.aiStyle = 7; 
			NPC.friendly = false;
		}
		
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return true;
		}

		

		public override void HitEffect(NPC.HitInfo hit)
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

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
		}


		/*	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
			{
				// Use AddRange instead of calling Add multiple times
				bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
					BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
					BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
					new FlavorTextBestiaryInfoElement("The mantis shrimp ," +
													  ".")
				});
			} */
	}

	
}