using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.ItemDropRules;

namespace Creaturia.NPCs.Creatures
{

    internal class PurpleHeadedHummingbird : ModNPC
    {
        


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruby-Throated Hummingbird");
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;
            NPC.catchItem = (short)ItemType<PurpleHeadedHummingbirdItem>();
           // Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlackDragonfly];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }

        public override void SetDefaults()
        {
           // AIType = ModContent.NPCType<HummingBird1>();
            //NPC.CloneDefaults(NPCID.BlackDragonfly);
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.catchItem = (short)ItemType<PurpleHeadedHummingbirdItem>();
            NPC.lavaImmune = false;
          
            //NPC.aiStyle = -1;
            //AnimationType = NPCID.BlackDragonfly;
            NPC.friendly = true;


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

       
        bool FlutterMode = true;

       /* public bool FindFlowersTop(int landX, int landY, out int flowerX, out int flowerY)
        {
            flowerX = landX;
            flowerY = landY;
            if (!WorldGen.InWorld(landX, landY, 31))
                return false;

            int num = 1;
            for (int i = landX - 30; i <= landX + 30; i++)
            {
                for (int j = landY - 20; j <= landY + 20; j++)
                {
                    Tile tile = Main.tile[i, j]; // CHANGE TILE TYPE TO GRASSESFLOWERS
                    if (tile != null && tile.HasTile &&
                        (tile.TileType == 3 && tile.TileFrameX > 107 && tile.TileFrameX < 142 || tile.TileType == 3 && tile.TileFrameX >= 162 || tile.TileType == 27 || tile.TileType == 84
                        || tile.TileType == 227 && tile.TileFrameX > 70 && tile.TileFrameX < 130 || tile.TileType == 227 && tile.TileFrameX > 270 && tile.TileFrameX < 410)
                        && Main.rand.NextBool(num))
                    {
                        flowerX = i;
                        flowerY = j;
                        num++;
                    }
                }
            }

            if (flowerX != landX || flowerY != landY)
                return true;

            return false;
        } */
        public override void AI()
        {
			HummingBirdBase.HummingBirdAI(NPC, Mod);
            NPC.spriteDirection = NPC.direction;
        }

    

		
		

		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if ((Main.time > 20000) && (Main.time < 35000) && Main.dayTime && Math.Abs(Main.windSpeedCurrent) < 3f)
            {
                return SpawnCondition.OverworldDayBirdCritter.Chance * 4.5f;
            }
            else
            {
                return 0;
            }
        }

		public override void HitEffect(NPC.HitInfo hit)
		{

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, GoreID.ChumBucketFloatingChunks, 0.8f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(-2, 2), NPC.velocity, Mod.Find<ModGore>("HummingbirdGore").Type, 1f);

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-2, 2)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2));
                }
            }


        }
        public override void OnKill()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<HummingbirdBestiary>(), 0, NPC.whoAmI);
        }
        public override void OnCaughtBy(Player player, Item item, bool failed)
        {
			item.stack = 1;

			try
			{
				// I made the hummingbird so long ago that I don't remember where this came from, whether it's ExampleMod or ported from source code.
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0)
				{

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
            database.Entries.Remove(bestiaryEntry);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            if (NPC.frameCounter < 2)
            {
                NPC.frame.Y = 0 * frameHeight;
            }
            else if (NPC.frameCounter < 4)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 6)
            {
                NPC.frame.Y = 2 * frameHeight;
            }

            else
            {
                NPC.frameCounter = 0;
            }
        }
    }

	internal class PurpleHeadedHummingbirdItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruby-Throated Hummingbird");
			// Tooltip.SetDefault("'Pretty!'");
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
            //item.width = 12;
            //item.height = 12;
            //item.makeNPC = 360;
            //item.noUseGraphic = true;
            //item.bait = 15;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 30, 0);
			Item.makeNPC = (short)NPCType<PurpleHeadedHummingbird>();
		}
	}
}