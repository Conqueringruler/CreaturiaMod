using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;

namespace Creaturia.NPCs.Enemies
{
	internal class HyperrealisticEyeball : ModNPC
	{
		


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Eye");
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true
			};
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
		}
		public override void SetDefaults()
		{

			NPC.width = 40;
			NPC.height = 48; // Change ALL defaults 
			NPC.damage = 10;
			NPC.defense = 40;
			NPC.lifeMax = 25000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 500;
			NPC.CloneDefaults(NPCID.DemonEye);
			NPC.lavaImmune = false;
			NPC.aiStyle = 2;
			NPC.noGravity = true;
			NPC.friendly = false;
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




		
	}


}