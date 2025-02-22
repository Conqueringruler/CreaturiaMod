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
using Creaturia.NPCs.Enemies;
using Creaturia;
using System.IO;

namespace Creaturia.NPCs.Creatures
{
	internal class FallenPixie : ModNPC
	{


		public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fallen Pixie");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Pixie];
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;

		}

		public override void SetDefaults()
		{
			NPC.width = 25;
			NPC.height = 40;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//AnimationType = NPCID.Pixie;
			//NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = -1;
			NPC.value = 100;
			NPC.friendly = false;
			NPC.dontTakeDamageFromHostiles = true;
			NPC.ShowNameOnHover = false;
			
		}
		bool RevivedYet = false;
		int TransformTimer;

		public bool SyncTheShit = false;

		public int NewLife = 5;

		public bool FairyIsActivatedByPacket = false;

		public bool SyncSpawn = false;
		public void Activate() // hopefully activating the function with a packet will work. if it doesn't I'm fucked
		{
			FairyIsActivatedByPacket = true;
            NPC.lifeMax = 15;
            NPC.life = 15;

           
                NewLife = 15;
                NPC.lifeMax = 15;
                NPC.life = 15;
           
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
          if (projectile.type == ProjectileID.HolyWater)
			{
				if (Main.netMode != NetmodeID.SinglePlayer)
				{


					ModPacket packet = Mod.GetPacket(); // use this instead of other
					packet.Write((byte)Creaturia.MessageType.FallenPixieMsg); // id
					packet.Write((Int32)NPC.whoAmI); // NPC identity
					packet.Write((bool)true);
					// packet.Write((byte)15);
					//packet.Write((bool)true);
					packet.Send();
				}
                Activate();
			}
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
			writer.Write((bool)FairyIsActivatedByPacket);
			writer.Write((Int32)NPC.lifeMax);
			writer.Write((Int32)NPC.defense);
			writer.Write((Int32)TransformTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            FairyIsActivatedByPacket = reader.ReadBoolean();
			NPC.lifeMax = reader.ReadInt32();
			NPC.defense = reader.ReadInt32();
			TransformTimer = reader.ReadInt32();
        }
        public override void AI()
		{
			if (NPC.defense == 21)
			{
                for (int i = 0; i < 35; i++)
                {
                    int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)), 20, 30, DustID.Pixie, 10 * Main.rand.Next(-1, 2), 10 * Main.rand.Next(-1, 2), 200, Color.White);
                    Dust dust = Main.dust[num311];

                }
            }
			if (SyncSpawn == false)
			{
				NPC.netUpdate = true;
				SyncSpawn = true;
			}
			if (SyncTheShit == true)
			{
				//NPC.netUpdate = true;
			}
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				//NPC.lifeMax = NewLife;
				//NPC.life = NewLife;

				if (FairyIsActivatedByPacket)
				{
					NewLife = 15;
					NPC.lifeMax = 15;
					NPC.life = 15;
                    Activate();
					NPC.netUpdate = true;
                }
			}
			if (Main.rand.NextBool(40))
            {
				SoundEngine.PlaySound(SoundID.Pixie, new Vector2((int)NPC.position.X, (int)NPC.position.Y));
			}
			NPC.velocity.X = 0;
			if (NPC.lifeMax == 15 && RevivedYet == false)
            {
				RevivedYet = true;
				NPC.noGravity = true;
				NPC.velocity.Y = -0.55f;
				NPC.dontTakeDamage = true;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.netUpdate = true;
				}
			}
			if (RevivedYet == true)
            {
				TransformTimer++;
				Lighting.AddLight(NPC.Center, Color.Yellow.ToVector3() * 0.1f);
				if (Main.rand.NextBool(4))
                {
					int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), 25, 40, DustID.Pixie, 0f, 0f, 200, Color.White);
					Dust dust = Main.dust[num311];
					dust.velocity *= 0.35f;
				}
				if (Main.rand.NextBool(20))
				{
					int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-50, 50)), 25, 40, DustID.Pixie, 0f, 0f, 200, Color.Orange);
					Dust dust = Main.dust[num311];
					
					//dust.velocity *= 0.35f;
				}
			}
			if (TransformTimer > 100)
            {

				int FairyTypeGet = Main.rand.Next(1, 4);
				NPC.defense = 21;
				if (Main.rand.NextBool(4))
                {
					SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, new Vector2((int)NPC.position.X, (int)NPC.position.Y));
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.Transform(ModContent.NPCType<GreatPixie>()); // play sound
						//NPC.netUpdate = true;
					}
					for (int i = 0; i < 35; i++)
					{
						int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)), 20, 30, DustID.Pixie, 10 * Main.rand.Next(-1, 2), 10 * Main.rand.Next(-1, 2), 200, Color.White);
						Dust dust = Main.dust[num311];

					}
					
				}
                else
                {
					SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact, new Vector2((int)NPC.position.X, (int)NPC.position.Y));
					for (int j = 0; j < 255; j++)
                    {
						if (Main.player[j] != null && Main.player[j].active && !Main.player[j].dead)
						{
							if (Vector2.Distance(Main.player[j].Center, NPC.Center) < 480f)
							{
								if (Main.netMode != NetmodeID.MultiplayerClient)
								{
									if (FairyTypeGet == 1)
									{
										Main.player[j].AddBuff(BuffID.Regeneration, 18000);
										Main.player[j].AddBuff(BuffID.Lifeforce, 18000);
									}
									if (FairyTypeGet == 2)
									{
										Main.player[j].AddBuff(BuffID.Lucky, 18000);
										Main.player[j].AddBuff(BuffID.Swiftness, 18000);
									}
									if (FairyTypeGet == 3)
									{
										Main.player[j].AddBuff(BuffID.ManaRegeneration, 18000);
										Main.player[j].AddBuff(BuffID.MagicPower, 18000);
									}
								}
							}
						}
						
                    }
					if (FairyTypeGet == 1)
					{ // play sound for transformations
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.Transform(NPCID.FairyCritterPink);
						}
						for (int i = 0; i < 30; i++)
						{
							int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)), 20, 30, DustID.PinkFairy, 12 * Main.rand.Next(-1, 2), 10 * Main.rand.Next(-1, 2), 200, Color.White);
							Dust dust = Main.dust[num311];
							
						}
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.netUpdate = true;
						}
					}
					if (FairyTypeGet == 2)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.Transform(NPCID.FairyCritterGreen);
						}
						for (int i = 0; i < 30; i++)
						{
							int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)), 20, 30, DustID.GreenFairy, 12 * Main.rand.Next(-1, 2), 10 * Main.rand.Next(-1, 2), 200, Color.White);
							Dust dust = Main.dust[num311];
						}
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.netUpdate = true;
						}
					}
					if (FairyTypeGet == 3)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.Transform(NPCID.FairyCritterBlue);
						}
						for (int i = 0; i < 30; i++)
						{
							int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2)), 20, 30, DustID.BlueFairy, 12 * Main.rand.Next(-1, 2), 10 * Main.rand.Next(-1, 2), 200, Color.White);
							Dust dust = Main.dust[num311];
						}
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.netUpdate = true;
						}
					}
				}
            }
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Pixie];
			NPC.ShowNameOnHover = false;
			Lighting.AddLight(NPC.Center, Color.Yellow.ToVector3() * 0.02f);
			if (Main.rand.NextBool(8))
            {
				int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11)), 20, 30, DustID.Pixie, 0f, 0f, 200, Color.White);
				Dust dust = Main.dust[num311];
				dust.velocity *= 0.3f;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			if (Main.hardMode)
			{
				if (spawnInfo.Player.ZoneRockLayerHeight)
				{
					if (TileID.Sets.Conversion.Stone[spawnInfo.SpawnTileType]) // What's the difference here between Sets.Conversion.Stone and Sets.Stone?
					{
						return 0.185f;
					}
					else return 0f;
				}
				return SpawnCondition.OverworldHallow.Chance * 0.005f; // Ultra-rare surface spawning cause why not
			}
			else return 0f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int num311 = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), 20, 30, DustID.Pixie, 0f, 0f, 200, Color.White);
					Dust dust = Main.dust[num311];
				}

				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			var fairyDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Pixie, false); // false is important here!!
			foreach (var FairyDropRule in fairyDropRules)
			{ 
				npcLoot.Add(FairyDropRule);
			}
		}
        public override void FindFrame(int frameHeight)
        {
			NPC.frameCounter++;
			
			//NPC.frame.Y = 2 * frameHeight;
			if (NPC.frameCounter < 55)
			{
				NPC.frame.Y = 1 * frameHeight;
				if (Main.rand.NextBool(8))
				{
					NPC.frameCounter += 10;
				}
			}
			else if (NPC.frameCounter < 60)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			if (NPC.frameCounter > 63)
            {
				NPC.frameCounter = 0;
            }
		}
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
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow,

                    //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.,
                    new FlavorTextBestiaryInfoElement("A fairy that's lost it's power.\n" +
                                                      "Maybe you should try rejuvenating it!")
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