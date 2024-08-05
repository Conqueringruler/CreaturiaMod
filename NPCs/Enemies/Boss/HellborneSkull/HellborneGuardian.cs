using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;

namespace Creaturia.NPCs.Enemies.Boss.HellborneSkull
{

	public class HellborneGuardian : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hellborne Guardian");

			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				//Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				//Direction = 1, // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
				//SpriteDirection = 1
				Scale = 0.85f
			};
		}
		private int Timer;
		public override void SetDefaults()
		{
			NPC.width = 136;
			NPC.height = 120;
			NPC.damage = 200;
			NPC.defense = 12;
			NPC.lifeMax = 1;
			NPC.HitSound = SoundID.NPCHit22;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.value = 60f;
			NPC.knockBackResist = 1f;
			NPC.aiStyle = 97;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.dontTakeDamage = true;
		
		}
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Moon,
				new FlavorTextBestiaryInfoElement("Some few Lihzahrds were chosen by their deity to enter a higher state of existence, given the task of protecting him for eternity.")
			});
		}
        public TheHellborneSkull Boss
		{
			get
			{
				return (TheHellborneSkull)Main.npc[(int)NPC.ai[0]].ModNPC;
			}
		}
		/* private void CreateDust()
		{
			if (NPC.AnyNPCs(mod.NPCType("TheHellborneSkull")))
			{
				Vector2 target = Boss.npc.Center;
				target += new Vector2(1 * 60f, 60f);
				Vector2 offset = target - npc.Center;
				float length = offset.Length();
				if (offset != Vector2.Zero)
				{
					offset.Normalize();
				}
				for (float k = 0f; k < length - 10f; k += 4f)
				{
					if (Main.rand.Next(10) == 0)
					{
						int dust = Dust.NewDust(npc.Center + offset * k, 0, 0, DustID.Smoke, npc.velocity.X, npc.velocity.Y, 10, Color.DodgerBlue, 1);
						
						
						Main.dust[dust].alpha = 100;
					}
				}
			} 
	} */
		public override void AI()
		{
			
			Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 4f);
			if (!NPC.AnyNPCs(ModContent.NPCType<TheHellborneSkull>()))
			{
				NPC.life = 0;
			}
			
			Player target = Main.player[NPC.target];
			Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
			Timer++;
			if (Timer > 300)
			{

				int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, direction * 60, ProjectileID.CultistBossFireBallClone, 5, 0, Main.myPlayer);

				Timer = 0;
			}

		}
	}
}