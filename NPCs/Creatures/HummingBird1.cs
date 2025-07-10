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
using Creaturia.Tiles.Furniture;

namespace Creaturia.NPCs.Creatures
{

    internal class HummingBird1 : ModNPC
    {
        


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruby-Throated Hummingbird");
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;
            NPC.catchItem = (short)ItemType<HummingBird1Item>();
            //Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlackDragonfly];
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;

        }

        public override void SetDefaults()
        {
            //AIType = NPCID.BlackDragonfly;
            //NPC.CloneDefaults(NPCID.BlackDragonfly);
            NPC.width = 30;
            NPC.height = 28;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.catchItem = (short)ItemType<HummingBird1Item>();
            NPC.lavaImmune = false;
            NPC.aiStyle = -1;
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

        /* public override void FindFrame(int frameHeight)
		{


			NPC.frameCounter++;

			if (NPC.frameCounter < 1)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			else if (NPC.frameCounter < 3)
			{
				NPC.frame.Y = 1 * frameHeight;
			}
			else if (NPC.frameCounter < 4)
			{
				NPC.frame.Y = 2 * frameHeight;
			}
			else if (NPC.frameCounter < 5)
			{
				NPC.frame.Y = 3 * frameHeight;
			}
			else
			{
				NPC.frameCounter = 0;
			}







		} */
        bool FlutterMode = true;

        public bool FindFlowersTop(int landX, int landY, out int flowerX, out int flowerY)
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
                        || tile.TileType == 227 && tile.TileFrameX > 70 && tile.TileFrameX < 130 || tile.TileType == 227 && tile.TileFrameX > 270 && tile.TileFrameX < 410 || tile.TileType == ModContent.TileType<HummingBirdFeederTile>())
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
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;
            int WindTimer = 0;
            //	Vector3 rgb = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.3f % 0.4f, 0.4f, 0.5f).ToVector3() * 0.3f;

            //NPC.spriteDirection = 1;

            //		Lighting.AddLight(NPC.Center, rgb);
            //	Lighting.AddLight(NPC.Center, Color.DeepPink.ToVector3() * 1f);

          

            // NPC.ai[2] and NPC.ai[3] are respectively X coordinate and Y coordinate 
            if (Main.IsItDay() && Math.Abs(Main.windSpeedCurrent) < 7 && Main.time < 40000 && !Main.raining)
            {

                NPC.catchItem = (short)ItemType<HummingBird1Item>();

                WindTimer++;
                if (WindTimer >= 5 && NPC.direction == 1)
                {

                    int dust = Dust.NewDust(NPC.Left, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y + 1, 10, Color.LightGray, Main.rand.NextFloat(0.3f, 0.6f));

                    WindTimer = 0;



                }
                if (WindTimer >= 5 && NPC.direction == 0)
                {

                    int dust = Dust.NewDust(NPC.Right, NPC.width, NPC.height, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y + 1, 10, Color.LightGray, Main.rand.NextFloat(0.3f, 0.6f));
                    WindTimer = 0;
                }

                if (NPC.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.localAI[0] = 1f;
                    Vector2 center = NPC.Center;
                    NPC.ai[2] = center.X;
                    NPC.ai[3] = center.Y;
                    NPC.velocity = (Main.rand.NextVector2Circular(5f, 3f) + Main.rand.NextVector2CircularEdge(5f, 3f)) * 0.6f; // originally 0.4f
                    NPC.ai[1] = 0f;
                    NPC.ai[0] = 1f;
                    NPC.netUpdate = true;
                }
                switch ((int)NPC.ai[0])
                {
                    case 0:
                        NPC.velocity *= 0.94f;
                        if (Main.netMode != 1 && (NPC.ai[1] += 1f) >= (float)(60 + Main.rand.Next(60)))
                        {
                            Vector2 vector = new Vector2(NPC.ai[2], NPC.ai[3]);
                            if (NPC.Distance(vector) > 96f)
                            {
                                NPC.velocity = NPC.DirectionTo(vector) * 3f;
                            }
                            else if (NPC.Distance(vector) > 16f)
                            {
                                NPC.velocity = NPC.DirectionTo(vector) * 1f + Main.rand.NextVector2Circular(1f, 0.5f);
                            }
                            else
                            {
                                NPC.velocity = (Main.rand.NextVector2Circular(5f, 3f) + Main.rand.NextVector2CircularEdge(5f, 3f)) * 0.6f; // changed
                            }
                            NPC.ai[1] = 0f;
                            NPC.ai[0] = 1f;
                            NPC.netUpdate = true;
                        }
                        break;
                    case 1:
                        {
                            int num = 4;
                            Vector2 other = new Vector2(NPC.ai[2], NPC.ai[3]);
                            if (NPC.Distance(other) > 112f)
                            {
                                num = 200;
                            }
                            if ((NPC.ai[1] += 1f) >= (float)num)
                            {
                                NPC.ai[1] = 0f;
                                NPC.ai[0] = 0f;
                                NPC.netUpdate = true;
                            }
                            int num2 = (int)NPC.Center.X / 16;
                            int num3 = (int)NPC.Center.Y / 16;
                            int num4 = 3;
                            for (int i = num3; i < num3 + num4; i++)
                            {
                                if (Main.tile[num2, i] != null && ((Main.tile[num2, i].HasTile && Main.tileSolid[Main.tile[num2, i].TileType]) || Main.tile[num2, i].LiquidAmount > 0) && Main.tile[num2, i].TileType != TileID.Platforms)
                                {
                                    if (NPC.velocity.Y > 0f)
                                    {
                                        NPC.velocity.Y *= 0.9f;
                                    }
                                    NPC.velocity.Y -= 0.2f;
                                    NPC.noTileCollide = false;
                                }
                                else if (Main.tile[num2, i].TileType == TileID.Platforms)
                                {
                                    NPC.noTileCollide = true;
                                }
                            }
                            if (!(NPC.velocity.Y < 0f))
                            {
                                break;
                            }
                            int num5 = 30;
                            bool flag = false;
                            for (int j = num3; j < num3 + num5; j++)
                            {
                                if (Main.tile[num2, j] != null && Main.tile[num2, j].HasTile && Main.tileSolid[Main.tile[num2, j].TileType])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag && NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y *= 0.9f;
                            }
                            break;
                        }
                }
                if (NPC.velocity.X != 0f)
                {
                    NPC.direction = ((NPC.velocity.X > 0f) ? 1 : (-1));
                }
                if (NPC.wet)
                {
                    NPC.velocity.Y = -4f;
                }
                if (NPC.localAI[1] > 0f)
                {
                    NPC.localAI[1] -= 1f;
                    return;
                }
                NPC.localAI[1] = 15f;
                float num6 = 0f;
                Vector2 zero = Vector2.Zero;
                for (int k = 0; k < 200; k++)
                {
                    NPC nPC = Main.npc[k];
                    if (nPC.active && nPC.damage > 0 && !nPC.friendly && nPC.Hitbox.Distance(NPC.Center) <= 200f)
                    {
                        num6 += 1f;
                        zero += NPC.DirectionFrom(nPC.Center);
                    }
                }
                for (int l = 0; l < 255; l++)
                {
                    Player player = Main.player[l];
                    if (player.active && player.Hitbox.Distance(NPC.Center) <= 200f)
                    {
                        num6 += 1f;
                        zero += NPC.DirectionFrom(player.Center);
                    }
                }
                if (num6 > 0f)
                {
                    float num7 = 2f;
                    zero /= num6;
                    zero *= num7;
                    NPC.velocity += zero;
                    if (NPC.velocity.Length() > 24f)
                    {
                        NPC.velocity = NPC.velocity.SafeNormalize(Vector2.Zero) * 24f;
                    }
                    Vector2 vector2 = NPC.Center + zero * 10f;
                    NPC.ai[1] = -10f;
                    NPC.ai[0] = 1f;
                    NPC.ai[2] = vector2.X;
                    NPC.ai[3] = vector2.Y;
                    NPC.netUpdate = true;
                }
                else
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient || !((new Vector2(NPC.ai[2], NPC.ai[3]) - NPC.Center).Length() < 16f))
                    {
                        return;
                    }
                    int maxValue = 60;
                    if (Main.tile[(int)NPC.ai[2] / 16, (int)NPC.ai[3] / 16].TileType is not TileID.DyePlants or TileID.Plants) // CHANGE THIS, OBV
                    {
                        maxValue = 4;
                    }
                    if (!Main.rand.NextBool(maxValue))
                    {
                        return;
                    }
                    int flowerX = (int)NPC.ai[2];
                    int flowerY = (int)NPC.ai[2];

                    //
                    // DETECT GRASS BOOL BELOW!
                    //


                    if (FindFlowersTop((int)NPC.ai[2] / 16, (int)NPC.ai[3] / 16, out flowerX, out flowerY))
                    {
                        NPC.ai[2] = flowerX * 16;
                        NPC.ai[3] = flowerY * 16;
                        NPC.netUpdate = true;
                        return;
                    }




                    int num8 = (int)(NPC.Center.X / 16f);
                    int m;
                    for (m = (int)(NPC.Center.Y / 16f); !WorldGen.SolidTile(num8, m) && (double)m < Main.worldSurface; m++)
                    {
                    }
                    m -= Main.rand.Next(3, 6);
                    NPC.ai[2] = num8 * 16;
                    NPC.ai[3] = m * 16;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                NPC.timeLeft = 10;
                if (NPC.direction != 1 && NPC.direction != -1)
                {
                   
                    NPC.direction = Main.rand.NextBool(2) ? 1 : -1;
                    
                }
                float num343 = 5f;


                //NPC.direction = 1;

                if (NPC.collideX)
                {
                    NPC.direction *= -1;
                    NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < num343 - 1f)
                        NPC.velocity.X = num343 - 1f;

                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > 0f - num343 + 1f)
                        NPC.velocity.X = 0f - num343 + 1f;
                }

                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                        NPC.velocity.Y = 1f;

                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                        NPC.velocity.Y = -1f;
                }

                if (NPC.direction == -1 && NPC.velocity.X > 0f - num343)
                {
                    NPC.velocity.X -= 0.1f;
                    if (NPC.velocity.X > num343)
                        NPC.velocity.X -= 0.1f;
                    else if (NPC.velocity.X > 0f)
                        NPC.velocity.X -= 0.05f;

                    if (NPC.velocity.X < 0f - num343)
                        NPC.velocity.X = 0f - num343;
                }
                else if (NPC.direction == 1 && NPC.velocity.X < num343)
                {
                    NPC.velocity.X += 0.1f;
                    if (NPC.velocity.X < 0f - num343)
                        NPC.velocity.X += 0.1f;
                    else if (NPC.velocity.X < 0f)
                        NPC.velocity.X += 0.05f;

                    if (NPC.velocity.X > num343)
                        NPC.velocity.X = num343;
                }
                int num344 = (int)((NPC.position.X + (float)(NPC.width / 2)) / 16f) + NPC.direction;
                int num345 = (int)((NPC.position.Y + (float)NPC.height) / 16f);
                bool flag23 = true;
                int num346 = 15;
                bool flag24 = false;
                for (int num347 = num345; num347 < num345 + num346; num347++)
                {
                    if (!WorldGen.InWorld(num344, num347))
                        continue;

                  // if (Main.tile[num344, num347] == null)
                        //Main.tile[num344, num347] = new Tile();

                    if ((Main.tile[num344, num347].HasTile && Main.tileSolid[Main.tile[num344, num347].TileType]) || Main.tile[num344, num347].LiquidType > 0)
                    {
                        if (num347 < num345 + 5)
                            flag24 = true;

                        flag23 = false;
                        break;
                    }
                }

                if (flag23)
                    NPC.velocity.Y += 0.05f;
                else
                    NPC.velocity.Y -= 0.1f;

                if (flag24)
                    NPC.velocity.Y -= 0.2f;

                if (NPC.velocity.Y > 2f)
                    NPC.velocity.Y = 2f;

                if (NPC.velocity.Y < -4f)
                    NPC.velocity.Y = -4f;
            }
        }

    

		
		

		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if (Main.time > 15000 && Main.time < 30000 && Main.dayTime && Math.Abs(Main.windSpeedCurrent) < 3f)
            {
                return SpawnCondition.OverworldDayBirdCritter.Chance * 7.85f;
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
                NPC.Opacity = 0;
                NPC.alpha = 255;
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
            /*bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
                
				new FlavorTextBestiaryInfoElement("A pretty little bird on the search for nectar and forest friends!")
                
				
			}) ; */
        }
	}

	internal class HummingBird1Item : ModItem
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
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 35, 0);
            
			Item.makeNPC = (short)NPCType<HummingBird1>();
		}
	}
}