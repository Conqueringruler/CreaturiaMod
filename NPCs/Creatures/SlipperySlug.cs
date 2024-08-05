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
using Terraria.Audio;

using Terraria.GameContent.Shaders;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.GameContent;

namespace Creaturia.NPCs.Creatures
{
	internal class SlipperySlug : ModNPC
	{


		//public override string Texture => "Terraria/Images/NPC_" + NPCID.Sluggy;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slippery Sluggy");
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Sluggy];
			NPCID.Sets.CountsAsCritter[NPC.type] = true;
			NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[NPC.type] = true;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f,
				//Direction = -1
				Scale = 2,
				PortraitScale = 2
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		int driptimer;
		public override void SetDefaults()
		{

			NPC.width = 8;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.scale = 2;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.stepSpeed = 100;
			NPC.catchItem = (short)ItemType<SlipperySlugItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 7;
			AnimationType = NPCID.Sluggy;
		}
		int dripTime;
		public override void AI()
		{
			if (NPC.velocity.X > 0)
			{
				dripTime = 15;
			}
			else
            {
				dripTime = 50;
            }
			driptimer++;
			if (driptimer > dripTime)
            {
				driptimer = 0;
				var dust = Dust.NewDustDirect(NPC.Center, NPC.width + Main.rand.Next(-3, 3), NPC.height + Main.rand.Next(-1, 1), DustID.Water, NPC.velocity.X, NPC.velocity.Y, 10, Color.DarkGray, 1);
				dust.velocity.Y /= 20;
                dust.shader = GameShaders.Armor.GetSecondaryShader(55, Main.LocalPlayer);
            }
		}

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
			
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{




			return SpawnCondition.Cavern.Chance * 0.05f;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
                for (int i = 0; i < 8; i++)
                {
                   int dust = Dust.NewDust(NPC.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), NPC.width, NPC.height, DustID.Sluggy, (NPC.velocity.X + Main.rand.Next(-2, 2)) * hitDirection, NPC.velocity.Y + Main.rand.Next(-2, 2), 190, Color.Gray, Main.rand.NextFloat(0.6f, 0.9f));
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(55, Main.LocalPlayer);
                }
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


				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
				new FlavorTextBestiaryInfoElement("A rare, valuable, and (for fishies) delicious slug of the caverns. \n" +
												  "Luckily for the slug, its so hard to grab that nothing can get it for long!")
			});
		}
        
    }

	internal class SlipperySlugItem : ModItem
	{
		//public override string Texture => "Terraria/Images/Item_" + ItemID.Sluggy;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slippery Sluggy");
			// Tooltip.SetDefault("'Be careful not to drop it!'");
		}
		int DropTimer;
		int stacknumber;
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GlowingSnail);
			//item.useStyle = 1; 
			//item.autoReuse = true;
			//item.useTurn = true;
			//item.useAnimation = 15;
			//item.useTime = 10;
			//item.maxStack = 999;
			//item.consumable = true;
			Item.width = 28;
			Item.bait = 45;
			Item.maxStack = 20;
			
			Item.height = 36;
			Item.value = Item.buyPrice(0, 12, 50, 0);
			Item.rare = ItemRarityID.Orange;
			
			//item.makeNPC = 360;
			//item.noUseGraphic = true;
			//item.bait = 15;
			//Item.color = new Color(155, 155, 155);
			
			Item.makeNPC = (short)NPCType<SlipperySlug>();
			
		}
		int slugNPC;
		Player thisPlayer;
		public override void UpdateInventory(Player player)
		{
			thisPlayer = player;
			Item.color = new Color(155, 155, 155);
			if (Main.netMode != NetmodeID.Server)
			{


				if (Main.LocalPlayer.whoAmI == player.whoAmI)
				{
					if (Main.myPlayer == player.whoAmI)
					{




						DropTimer++;
						stacknumber = Item.stack;
						if (DropTimer > (200 / stacknumber))
						{
							if (Main.rand.NextBool(3))
							{

								for (int i = 0; i < 1; i++)
								{
									if (Main.netMode == NetmodeID.SinglePlayer)
									{

										slugNPC = NPC.NewNPC(player.GetSource_FromAI(), (int)player.Center.X + Main.rand.Next(-3, 3), (int)player.Center.Y + Main.rand.Next(-3, 3), ModContent.NPCType<SlipperySlug>());
									}
								}
								//Main.npc[slugNPC].netUpdate = true;


								//NetMessage.SendData(MessageID.SyncNPC, slugNPC);
								//NetMessage.SendData(MessageID.SyncItem, Item.whoAmI);
								DropTimer = 0;
								SoundEngine.PlaySound(SoundID.GlommerBounce, player.position);
								if (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.MultiplayerClient)
								{
                                    Item.stack -= 1;
                                }
								if (Main.netMode == NetmodeID.MultiplayerClient)
								{

									
									ModPacket packet = Mod.GetPacket(); // use this instead of other
									packet.Write((byte)Creaturia.MessageType.SlugMsg); // id
									packet.Write((Int32)player.whoAmI); // Player identity
									packet.Send();
								}

							}
							else
							{
								DropTimer = 0;
							}


						}
					}
				}
			}

		}
       

    }
       
    }
