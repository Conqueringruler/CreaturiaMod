using Microsoft.Xna.Framework;
using System;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
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
using Terraria.GameContent;
using Creaturia.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;

namespace Creaturia.NPCs.Enemies
{

	internal class SkinWalker : ModNPC
	{

		//public override string Texture => "Terraria/Images/NPC_" + NPCID.DesertBeast;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Skinwalker");
			Main.npcFrameCount[NPC.type] = 10;
			NPCID.Sets.BossBestiaryPriority.Remove(Type);
			//	NPCID.Sets.TrailCacheLength[NPC.type] = 5; // 
			//	NPCID.Sets.TrailingMode[NPC.type] = 0; // The recording mode or something, idk what that means
			//	NPCID.Sets.HurtingBees[NPC.type] = true;


			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Ichor,
					BuffID.WeaponImbueIchor,
					BuffID.CursedInferno,
					BuffID.Confused
				}
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifiers);

		}
		NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
            PortraitScale = 1f,
			Scale = 1.10f,
			Position = new Vector2(5f, 75f),
			PortraitPositionXOverride = 0f,
			PortraitPositionYOverride = 10f,
			Velocity = 5f
		};
		private bool screamed = false;
		
		public override void SetDefaults()
		{
			NPC.width = 45;
			NPC.height = 100;
			NPC.damage = 45;
			NPC.defense = 32;
			NPC.lifeMax = 900;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCDeath11;
			NPC.noGravity = false;
			NPC.boss = true;
		//	NPC.color = Color.PeachPuff;
			NPC.DeathSound = SoundID.NPCDeath25;
			NPC.lavaImmune = false;
			NPC.immortal = true;
			NPC.aiStyle = 3;
			NPC.timeLeft = 200;
			NPC.rarity = 5;
			//NPC.stepSpeed = 200f;
			AnimationType = NPCID.SolarDrakomire;
			AIType = NPCID.DesertBeast;
			if (!Main.dedServ)
			{
			Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/Heartbeat");
			}
			
			

		}
		int dusttimer = 0;
		int dusttimer2 = 0;
		int breathtimer = 0;

		int lifetimer = 0;
        public override bool PreKill()
        {
			NPC.boss = false;
			return true;
        }
        public override void AI()
		{
			
			lifetimer++;
			if (lifetimer >= 15000)
            {
				NPC.active = false;
				SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Shadowflame, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Shadowflame, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Shadowflame, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Shadowflame, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
			}
			Player player = Main.player[NPC.target];
			player.AddBuff(BuffID.Blackout, 10, true);
			player.AddBuff(BuffID.Darkness, 10, true);
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					NPC.velocity = new Vector2(0f, -100f);
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
					}
					return;
				}

			}
			if (dusttimer < 15)
            {
				dusttimer++;
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Blood, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Blood, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Blood, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.Blood, NPC.velocity.X + Main.rand.Next(-1, 1), NPC.velocity.Y + Main.rand.Next(-1, 1));
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-25, 25)), NPC.velocity + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), GoreID.ChumBucketFloatingChunks, 0.8f);
			}
			if (dusttimer2 < 8)
			{
				dusttimer2++;
				Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.GreenFairy, NPC.velocity.X, NPC.velocity.Y, 100, Color.Red);
				
			}



			breathtimer++;
			if (breathtimer > 97)
            {
				SoundEngine.PlaySound(new SoundStyle("Creaturia/Assets/Sounds/SkinwalkerBreathing"), NPC.Center);

				breathtimer = 0;
            }


				if (screamed == false)
				{
				NPC.life = NPC.lifeMax;
				NPC.immortal = false;
					SoundEngine.PlaySound(SoundID.DD2_BetsyScream, NPC.position);
					screamed = true;
				}
			
			}
		
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
			target.AddBuff(BuffID.Obstructed, 400, true);
			
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.SkinWalker")
            });
		}


        public override void HitEffect(NPC.HitInfo hit)
        {
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 15; i++)
				{
					Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-25, 25)), NPC.velocity + new Vector2(Main.rand.Next(-6, 6), Main.rand.Next(-6, 6)), GoreID.ChumBucketFloatingChunks, Main.rand.NextFloat(0.9f, 1.3f));
				}
			}
		}
        public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return null;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowScale2>(), 50, 0, 7)); // This new method is cock and balls, don't forget to use terraria.lootshit so stuff can drop and also 1 = 100% chance of dropping, 100 = 1% chance of dropping for some stupid reason

		}
		
        public override void OnKill()
        {
            NPC.boss = false;
		}





    }


}