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
using Terraria.Graphics.Shaders;
using Creaturia;

namespace Creaturia.NPCs.Creatures
{
	internal class Jackrabbit : ModNPC
	{



		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jackrabbit");
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Bunny];
			NPCID.Sets.CountsAsCritter[NPC.type] = true;
			//NPCID.Sets.DangerDetectRange[NPC.type] = 450;
			//NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[NPC.type] = true;
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 1.5f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
           // NPC.CloneDefaults(NPCID.Bunny);
            NPC.width = 8;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.friendly = true;
			
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 500;
			NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = NPCAIStyleID.Passive;
			AnimationType = NPCID.Bunny;
		}
		int JumpTimer;

		bool BurrowOnGrounded = false;
		public override void AI()
		{
			if (NPC.velocity.X > 0)
			{
				NPC.direction = 1;
			}
			if (NPC.velocity.X < 0)
			{
				NPC.direction = -1;
			}

			base.AI();
			//NPC.ai[0] = 1;
			int yTile = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
			int initialXTile = (int)NPC.position.X / 16;
			int maxXTile = (int)(NPC.position.X + (float)NPC.width) / 16;
			for (int xTile = initialXTile; xTile <= maxXTile; xTile++) // I understand like 60% of this, which is higher than half so good enough for me. Right here it just changes when the X position changes
			{
				if (Main.tile[xTile, yTile] == null)
				{
					return;
				}
				if (Main.tile[xTile, yTile].TileType == TileID.LargePiles2) // like it disappears under the brush/skulls
				{
					NPC.velocity.X = 0;
					JumpTimer = 21;
					BurrowOnGrounded = true;
					
				}
				if (Main.tile[xTile, yTile].TileType == TileID.OasisPlants && Main.tile[xTile, yTile].TileFrameX < 90 && Main.tile[xTile, yTile].TileFrameY < 10) // like it disappears under the brush/skulls
				{
					NPC.velocity.X = 0;
					JumpTimer = 21;
					BurrowOnGrounded = true;

				}
				
			}
			if (BurrowOnGrounded && NPC.velocity.Y == 0)
			{
				for (int i = 0; i < 24; i++)
				{
					int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 125, Color.PaleGoldenrod, 1.25f);
					Main.dust[num].position.X += Main.rand.Next(-10, 10);
					Main.dust[num].position.Y += Main.rand.Next(-6, 6);
					Main.dust[num].velocity *= 0.42f;
					Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
					//Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, NPC);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f; // I can't believe I've never thought of using Main.rand.next like this before
						Main.dust[num].noGravity = true;
					}
					NPC.active = false;
					NPC.netUpdate = true;
				}
			}
			if (BurrowOnGrounded == false)
			{
				if (((NPC.velocity.X < 2) && (NPC.velocity.X > -2)) && NPC.ai[0] == 1)
				{
					NPC.velocity.X *= 1.4f;
					NPC.netUpdate = true;
				}
				if (((NPC.velocity.X > 1.7f) || (NPC.velocity.X < -1.7f)) && (NPC.ai[0] == 1) && (NPC.velocity.Y == 0))
				{
					JumpTimer++;
				}
				if (NPC.velocity.Y != 0)
				{
					JumpTimer = 0;
				}
				if (JumpTimer > 20)
				{
					NPC.netUpdate = true;
					NPC.velocity.Y -= 4.5f + (2 * (MathF.Abs(NPC.velocity.X)));
					NPC.velocity.X *= 1.1f;
					JumpTimer = 0;
					for (int i = 0; i < 4; i++)
					{
						int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 31, 0f, 0f, 100, default(Color), 0.8f);

						Main.dust[num].position.X += Main.rand.Next(-4, 4);
						Main.dust[num].position.Y += Main.rand.Next(-2, 2);
						Main.dust[num].velocity *= 0.4f;
						Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
						//Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, NPC);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
							Main.dust[num].noGravity = true;
						}
					}
				}
			}
				if (NPC.ai[2] == -1)
				{
					Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X + Main.rand.Next(-3, 3), NPC.velocity.Y + Main.rand.Next(-3, 3), default, Color.White, Main.rand.NextFloat(0.5f, 0.8f));
				}
			
		}
		
		
		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			return (Main.remixWorld ? SpawnCondition.DesertCave.Chance : SpawnCondition.OverworldDayDesert.Chance) * 0.2f;
            //return SpawnCondition.OverworldDayDesert.Chance * 0.2f;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(0, -3), NPC.velocity, Mod.Find<ModGore>("JackrabbitGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(2, 0), NPC.velocity, Mod.Find<ModGore>("JackrabbitGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(-2, 0), NPC.velocity, Mod.Find<ModGore>("JackrabbitGore2").Type, 1f);
				//for (int i = 0; i < 6; i++)
				//{
				//	Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), NPC.velocity, GoreID.BloodZombieChunk2, Main.rand.NextFloat(0.9f, 1.1f));
				//}

				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
			}
		}
	
        public override bool? CanBeHitByItem(Player player, Item item)
        {

            if (player.dontHurtCritters)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            Player owner = Main.player[projectile.owner];

            if (owner != null)
            {
                if (owner.dontHurtCritters)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
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
				new FlavorTextBestiaryInfoElement("This wiry rabbit thrives in the desert dunes, " +
												  "always on the search for snacks.")
			});
		}
	}

	internal class JackrabbitItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jackrabbit");
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.Bunny);
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

            Item.value = Item.sellPrice(0, 0, 18, 0);
            Item.makeNPC = (short)NPCType<Jackrabbit>();
		}
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}