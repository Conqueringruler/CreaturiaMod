using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Creaturia;
using Terraria.GameContent.ItemDropRules;

namespace Creaturia.NPCs.Creatures
{

	internal class HoopSnake : ModNPC
	{


		public int SnakeSand;
		public int PoisonTimer;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hoop Snake");
			Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 2;
			NPC.lifeMax = 45;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.buffImmune[BuffID.Poisoned] = true;
			NPC.catchItem = (short)ItemType<HoopSnakeItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 26;
			AnimationType = NPCID.Tumbleweed;
			NPC.dontTakeDamageFromHostiles = true;
		}
		/* public void ReflectProjectile(Projectile proj)
		{
			if (NPC.velocity.X <= 0)
            {
				SoundEngine.PlaySound(SoundID.Item150, proj.position);
				for (int i = 0; i < 3; i++)
				{
					int num = Dust.NewDust(proj.position, proj.width, proj.height, 31);
					Main.dust[num].velocity *= 0.3f;
				}
				proj.hostile = true;
				proj.friendly = false;
				Vector2 vector = Main.player[proj.owner].Center - proj.Center;
				vector.Normalize();
				vector *= proj.oldVelocity.Length();
				proj.velocity = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
				proj.velocity.Normalize();
				proj.velocity *= vector.Length();
				proj.velocity += vector * 20f;
				proj.velocity.Normalize();
				proj.velocity *= vector.Length();
				proj.damage /= 2;
				proj.penetrate = 1;
			}
			
		} */

		public override void AI()
        {
			
            base.AI();
			NPC.rotation += NPC.velocity.X;
			Player target = Main.player[NPC.target];
			Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
			SnakeSand++;
			if (SnakeSand > 10)
			{
				int dust = Dust.NewDust(NPC.position - new Vector2(4f, 3f), NPC.width, NPC.height, DustID.Sand, NPC.velocity.X, NPC.velocity.Y, 99);
				SnakeSand = 0;
			}
			if (NPC.velocity.X > 0)
            {
				NPC.spriteDirection *= -1;
            }
			if (Main.masterMode)
			{
				NPC.friendly = false;
				NPC.buffImmune[BuffID.Poisoned] = true;
				NPC.reflectsProjectiles = true;
				PoisonTimer++;
				if (PoisonTimer >= 205)
				{
					NPC.dontTakeDamage = true;
			//		int projectile = Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.position, direction * 4, ModContent.ProjectileType<HoopSnakePoison>(), 3, 0);
					PoisonTimer = 0;
				}
				if (PoisonTimer < 205)
				{
					NPC.dontTakeDamage = false;
				}
			}
			if (!Main.masterMode)
            {
				//NPC.friendly = true;
			}

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.WindyDay,
				new FlavorTextBestiaryInfoElement("'Terrarians have long lived in fear of the dreaded Hoop Snake.' Despite its bad rep, " +
				"the incredibly rare Hoop Snake just wants to enjoy your companionship as it rolls around. When rolling, it has a chance of reflecting projectiles right off it's scales.")
			});
			bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[NPC.type], quickUnlock: true);
		}

		

		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			
			if (Main.IsItAHappyWindyDay)
			{
				//return SpawnCondition.OverworldDayDesert.Chance * 0.2f; Disabled for now
				return SpawnCondition.OverworldDayDesert.Chance * 0.0005f; // Since I'm still having it as the mod icon for the Bestiary, I'm just gonna have it be ultra rare.
			}
			else
				return SpawnCondition.OverworldDayDesert.Chance * 0.0001f;
			
		}

		public override void HitEffect(int hitDirection, double damage)
		{

			if (NPC.life <= 0)
			{
				int PoopSnakeGore1 = Mod.Find<ModGore>("PoopSnakeGore1").Type;
				int PoopSnakeGore2 = Mod.Find<ModGore>("PoopSnakeGore2").Type;
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, PoopSnakeGore1);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, PoopSnakeGore2);
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
		
	}
        
	
	internal class HoopSnakeItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hoop Snake");
			Tooltip.SetDefault("'Terrarians have long lived in fear of the dreaded Hoop Snake.'");
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
			Item.width = 30;
			Item.height = 28;
			//item.makeNPC = 360;
			//item.noUseGraphic = true;
			//item.bait = 15;

			Item.CloneDefaults(ItemID.GlowingSnail);
			Item.makeNPC = (short)NPCType<HoopSnake>();
		}
	}
}