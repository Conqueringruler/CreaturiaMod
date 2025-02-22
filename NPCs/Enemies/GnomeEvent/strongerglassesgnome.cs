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
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Shaders;


namespace Creaturia.NPCs.Enemies.GnomeEvent
{
    

        public class strongerglassesgnome : ModNPC
    {



        
        public static int glassestype()
        {
            return ModContent.NPCType<sunglasses>();
        }
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Gnome;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gnome");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Gnome];
            
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            database.Entries.Remove(bestiaryEntry);
        }

        private NPC Body = null;
        private int newNPC;
        private int HealTimer;
        private int DamageTimer;
        private bool ExposedToDeadlySun = false;
        public override void SetDefaults()
        {
            
            NPC.width = 30;
            NPC.height = 28;
            NPC.damage = 20;
            NPC.defense = 5;
            NPC.lifeMax = 120;
            NPC.knockBackResist = 0.6f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.stepSpeed = 500;
            NPC.lavaImmune = false;
            NPC.aiStyle = -1;
            AIType = NPCID.Mummy;
            NPC.friendly = false;
            
            AnimationType = NPCID.Gnome;
        }
        int WhoIsThisGnome;
        public void spawnglasses()
        {
            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f; // So we only spawn it once
                WhoIsThisGnome = NPC.whoAmI;
                newNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<sunglasses>(), NPC.whoAmI);
                Main.npc[newNPC].ai[0] = WhoIsThisGnome; // newNPC is the one we just spawned. ai[0] is a variable in it. We then set it as being this NPC, so it knows who its owner is.
                NPC.ai[0] = (int)Main.npc[newNPC].whoAmI;

                // if (Main.netMode == NetmodeID.Server && newNPC < Main.maxNPCs)
                // {
                //     NetMessage.SendData(MessageID.SyncNPC, number: newNPC);
                //  }
                //  NPC.ai[0] = (float)newNPC;
                //     NPC.netUpdate = true;
                //    NPC.localAI[0] = 1f;
            }
        }
        NPC glassesgnomeNPC;
        bool onstart = false;

        //  float NPCai0Replacement;

        int HealTime = 60;
        bool IsWalking = false;
        int StartingHealTime = 60;
        public override void AI()
        {
            // I wish my skills were good enough to figure out what all this shit does. I get most of it, but there's so much 
            // Originally transcribed from Fighter AI 
            if (ExposedToDeadlySun == false)
            {
                if (ExposedToDeadlySun == false)
                {

                    int BackUpTimer = 60;
                    bool flag3 = false;
                    if (!(NPC.ai[2] > 0f))
                    {
                        if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
                        {
                            IsWalking = true;
                        }
                        if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)BackUpTimer || IsWalking)
                        {
                            NPC.ai[3] += 1f;
                        }
                        else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
                        {
                            NPC.ai[3] -= 1f;
                        }
                        if (NPC.ai[3] > (float)(BackUpTimer * 10))
                        {
                            NPC.ai[3] = 0f;
                        }
                        if (NPC.justHit)
                        {
                            NPC.ai[3] = 0f; // When hit resets the NPC to attacking the player I think, meaning ai[3] is the counter for running away
                            HealTime = StartingHealTime;
                        }
                        if (NPC.ai[3] == (float)BackUpTimer)
                        {
                            NPC.netUpdate = true;
                        }
                        if (NPC.ai[3] < (float)BackUpTimer)
                        {
                            NPC.TargetClosest(true);
                        }
                    }
                    if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) < 16f)
                    {
                        NPC.ai[3] = 0f;
                        //NPC.velocity.X = NPC.velocity.X * 0.9f; Undo this if I want it to stop on the player again
                        if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                            // NPC.velocity.X = 0f;Undo this if I want it to stop on the player again
                            return;
                    }



                    if (NPC.velocity.X < -2.5f || NPC.velocity.X > 2.5f)
                    {
                        if (NPC.velocity.Y == 0f)
                        {
                            NPC.velocity *= 0.8f;
                        }
                    }
                    else if (NPC.velocity.X < 2.5f && NPC.direction == 1)
                    {

                        NPC.velocity.X += 0.07f;
                        if (NPC.velocity.X > 2.5f)
                        {
                            NPC.velocity.X = 2.5f;
                        }
                    }
                    else if (NPC.velocity.X > -2.5f && NPC.direction == -1)
                    {

                        NPC.velocity.X -= 0.07f;
                        if (NPC.velocity.X < -2.5f)
                        {
                            NPC.velocity.X = -2.5f;
                        }
                    }




                    bool OnSolidTile = false;
                    if (NPC.velocity.Y == 0f)
                    {
                        int num29 = (int)(NPC.position.Y + (float)NPC.height + 8f) / 16;
                        int num30 = (int)NPC.position.X / 16;
                        int num31 = (int)(NPC.position.X + (float)NPC.width) / 16;
                        for (int l = num30; l <= num31; l++)
                        {
                            if (Main.tile[l, num29] == null)
                            {
                                return;
                            }
                            if (Main.tile[l, num29].HasUnactuatedTile && Main.tileSolid[Main.tile[l, num29].TileType])
                            {
                                OnSolidTile = true;
                                break;
                            }
                        }
                    }
                    if (OnSolidTile)
                    {
                        int DoorCheckX = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                        int DoorCheckY = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);

                        /* if (Main.tile[DoorCheckX, DoorCheckY] == null)
                         {
                             Main.tile[DoorCheckX, DoorCheckY] = new Tile();
                         }
                         if (Main.tile[DoorCheckX, DoorCheckY - 1] == null)
                         {
                             Main.tile[DoorCheckX, DoorCheckY - 1] = new Tile();
                         }
                         if (Main.tile[DoorCheckX, DoorCheckY - 2] == null)
                         {
                             Main.tile[DoorCheckX, DoorCheckY - 2] = new Tile();
                         }
                         if (Main.tile[DoorCheckX, DoorCheckY - 3] == null)
                         {
                             Main.tile[DoorCheckX, DoorCheckY - 3] = new Tile();
                         }
                         if (Main.tile[DoorCheckX, DoorCheckY + 1] == null)
                         {
                             Main.tile[DoorCheckX, DoorCheckY + 1] = new Tile();
                         }
                         if (Main.tile[DoorCheckX + NPC.direction, DoorCheckY - 1] == null)
                         {
                             Main.tile[DoorCheckX + NPC.direction, DoorCheckY - 1] = new Tile();
                         }
                         if (Main.tile[DoorCheckX + NPC.direction, DoorCheckY + 1] == null)
                         {
                             Main.tile[DoorCheckX + NPC.direction, DoorCheckY + 1] = new Tile();
                         } */

                        // What do I do with the above?

                        if (Main.tile[DoorCheckX, DoorCheckY - 1].HasUnactuatedTile && Main.tile[DoorCheckX, DoorCheckY - 1].TileType == 10 && flag3)
                        {
                            NPC.ai[2] += 1f;
                            NPC.ai[3] = 0f;
                            if (NPC.ai[2] >= 60f)
                            {

                                NPC.velocity.X = 0.5f * (float)(-NPC.direction);
                                NPC.ai[1] += 1f;
                                /*if (NPC.type == NPCID.GoblinThief)
                                {
                                    NPC.ai[1] += 1f;
                                }
                                if (NPC.type == NPCID.AngryBones)
                                {
                                    NPC.ai[1] += 6f;
                                } */
                                NPC.ai[2] = 0f;
                                bool flag5 = false;
                                if (NPC.ai[1] >= 10f)
                                {
                                    flag5 = true;
                                    NPC.ai[1] = 10f;
                                }
                                WorldGen.KillTile(DoorCheckX, DoorCheckY - 1, fail: true);
                                if ((Main.netMode != 1 || !flag5) && flag5 && Main.netMode != 1)
                                {
                                    if (NPC.type == 26)
                                    {
                                        WorldGen.KillTile(DoorCheckX, DoorCheckY - 1);
                                        if (Main.netMode == NetmodeID.Server)
                                        {
                                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, DoorCheckX, DoorCheckY - 1);
                                        }
                                    }
                                    else
                                    {
                                        bool flag6 = WorldGen.OpenDoor(DoorCheckX, DoorCheckY, NPC.direction);
                                        if (!flag6)
                                        {
                                            NPC.ai[3] = BackUpTimer;
                                            NPC.netUpdate = true;
                                        }
                                        if (Main.netMode == 2 && flag6)
                                        {
                                            NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, 0, DoorCheckX, DoorCheckY, NPC.direction);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((NPC.velocity.X < 0f && NPC.spriteDirection == -1) || (NPC.velocity.X > 0f && NPC.spriteDirection == 1))
                            {
                                if (Main.tile[DoorCheckX, DoorCheckY - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[DoorCheckX, DoorCheckY - 2].TileType])
                                {
                                    if (Main.tile[DoorCheckX, DoorCheckY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[DoorCheckX, DoorCheckY - 3].TileType])
                                    {
                                        NPC.velocity.Y = -8f;
                                        NPC.netUpdate = true;
                                    }
                                    else
                                    {
                                        NPC.velocity.Y = -7f;
                                        NPC.netUpdate = true;
                                    }
                                }
                                else if (Main.tile[DoorCheckX, DoorCheckY - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[DoorCheckX, DoorCheckY - 1].TileType])
                                {
                                    NPC.velocity.Y = -8f;
                                    int num198 = (int)(NPC.position.Y + (float)NPC.height) / 16; // Taken directly from Gnome special movement
                                    if (WorldGen.SolidTile((int)NPC.Center.X / 16, num198 - 8))
                                    {
                                        NPC.direction *= -1;
                                        NPC.spriteDirection = NPC.direction;
                                        NPC.velocity.X = 3 * NPC.direction;
                                    }
                                    NPC.netUpdate = true;
                                }
                                else if (Main.tile[DoorCheckX, DoorCheckY].HasUnactuatedTile && Main.tileSolid[Main.tile[DoorCheckX, DoorCheckY].TileType])
                                {
                                    NPC.velocity.Y = -5f;
                                    NPC.netUpdate = true;
                                }
                                else if (NPC.directionY < 0 && NPC.type != 67 && (!Main.tile[DoorCheckX, DoorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[DoorCheckX, DoorCheckY + 1].TileType]) && (!Main.tile[DoorCheckX + NPC.direction, DoorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[DoorCheckX + NPC.direction, DoorCheckY + 1].TileType]))
                                {
                                    NPC.velocity.Y = -8f;
                                    NPC.velocity.X *= 1.5f;
                                    NPC.netUpdate = true;
                                }
                                else if (flag3)
                                {
                                    NPC.ai[1] = 0f;
                                    NPC.ai[2] = 0f;
                                }
                            }
                            float PlayerXDistance = Math.Abs(NPC.position.X + (float)(NPC.width / 2) - (Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2)));
                            float PlayerYDistance = Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - (Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2)));

                            if (NPC.velocity.Y == 0f && PlayerXDistance < 70f && PlayerYDistance < 140f && PlayerYDistance > 40f && ((NPC.direction > 0 && NPC.velocity.X >= 1f) || (NPC.direction < 0 && NPC.velocity.X <= -1f)))
                            {

                                NPC.velocity.Y = -8f; // Jumps here!
                                NPC.netUpdate = true;
                            }
                            if (NPC.type == 120 && NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y *= 1.1f;
                            }
                        }
                    }
                    else if (flag3)
                    {
                        NPC.ai[1] = 0f;
                        NPC.ai[2] = 0f;
                    }

                    // How Chaos Elemental Teleports
                    /*
                    if (Main.netMode != NetmodeID.MultiplayerClient || NPC.type == NPCID.ChaosElemental || (NPC.ai[3] >= (float)BackUpTimer))
                    {
                        int num34 = (int)Main.player[NPC.target].position.X / 16;
                        int num35 = (int)Main.player[NPC.target].position.Y / 16;
                        int num36 = (int)NPC.position.X / 16;
                        int num37 = (int)NPC.position.Y / 16;
                        int num38 = 20;
                        int num39 = 0;
                        bool flag7 = false;

                        if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) + Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
                        {
                            num39 = 100;
                            flag7 = true;
                        }

                        while (!flag7 && num39 < 100)
                        {
                            num39++;
                            int num40 = Main.rand.Next(num34 - num38, num34 + num38);
                            int num41 = Main.rand.Next(num35 - num38, num35 + num38);
                            for (int m = num41; m < num35 + num38; m++)
                            {
                                if ((m < num35 - 4 || m > num35 + 4 || num40 < num34 - 4 || num40 > num34 + 4) && (m < num37 - 1 || m > num37 + 1 || num40 < num36 - 1 || num40 > num36 + 1) && Main.tile[num40, m].HasUnactuatedTile)
                                {
                                    bool flag8 = true;
                                    if (NPC.type == 32 && Main.tile[num40, m - 1].WallType == 0)
                                    {
                                        flag8 = false;
                                    }

                                    if (flag8 && Main.tileSolid[Main.tile[num40, m].TileType] && !Collision.SolidTiles(num40 - 1, num40 + 1, m - 4, m - 1))
                                    {
                                        NPC.position.X = num40 * 16 - NPC.width / 2;
                                        NPC.position.Y = m * 16 - NPC.height;
                                        NPC.netUpdate = true;
                                        NPC.ai[3] = -120f;
                                    }
                                }
                            }
                        }

                        if (NPC.velocity.Y >= 0f)
                        {
                            int FallDirection = 0;
                            if (NPC.velocity.X < 0f)
                            {
                                FallDirection = -1;
                            }
                            if (NPC.velocity.X > 0f)
                            {
                                FallDirection = 1;
                            }
                            Vector2 npcPosition = NPC.position;
                            npcPosition.X += NPC.velocity.X;
                            int xTileBelow = (int)((npcPosition.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * FallDirection)) / 16f);
                            int yTileBelow = (int)((npcPosition.Y + (float)NPC.height - 1f) / 16f);
                            if ((float)(xTileBelow * 16) < npcPosition.X + (float)NPC.width && (float)(xTileBelow * 16 + 16) > npcPosition.X && ((Main.tile[xTileBelow, yTileBelow].HasUnactuatedTile && !Main.tile[xTileBelow, yTileBelow].TopSlope && !Main.tile[xTileBelow, yTileBelow - 1].TopSlope && Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow].TileType] && !Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow].TileType]) || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile)) && (!Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 1].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 1].TileType] || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && (!Main.tile[xTileBelow, yTileBelow - 4].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 4].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 4].TileType]))) && (!Main.tile[xTileBelow, yTileBelow - 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 2].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 2].TileType]) && (!Main.tile[xTileBelow, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 3].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 3].TileType]) && (!Main.tile[xTileBelow - FallDirection, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow - FallDirection, yTileBelow - 3].TileType]))
                            {
                                float PixelDistanceY = (float)(yTileBelow * 16);
                                if (Main.tile[xTileBelow, yTileBelow].IsHalfBlock)
                                {
                                    PixelDistanceY += 8f;
                                }
                                if (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock)
                                {
                                    PixelDistanceY -= 8f;
                                }
                                if (PixelDistanceY < npcPosition.Y + (float)NPC.height)
                                {
                                    float num192 = npcPosition.Y + (float)NPC.height - PixelDistanceY;
                                    float num193 = 16.1f;
                                    if (num192 <= num193)
                                    {
                                        NPC.gfxOffY += NPC.position.Y + (float)NPC.height - PixelDistanceY;
                                        NPC.position.Y = PixelDistanceY - (float)NPC.height;
                                        if (num192 < 9f)
                                        {
                                            NPC.stepSpeed = 1f;
                                        }
                                        else
                                        {
                                            NPC.stepSpeed = 2f;
                                        }
                                    }
                                }
                            }
                        }
                    }

                  */



                }




            }

















            if (NPC.life > NPC.lifeMax)
            {
                NPC.life = NPC.lifeMax;
            }
            spawnglasses();
           
            if (ExposedToDeadlySun == false)
            {
                HealTimer++;
                NPC.ai[0] = (int)Main.npc[newNPC].whoAmI;

                if (HealTimer > HealTime)
                {
                    HealTimer = 0;
                    if (NPC.life < NPC.lifeMax)
                    {
                        NPC.life += 5;
                        NPC.HealEffect(5, true);
                    }

                }
            }    
            
            int sunglassescheck = (int)NPC.ai[0];
           

            if (Main.dayTime && WorldGen.InAPlaceWithWind(NPC.position, NPC.width, NPC.height))
            {
                if (Main.npc[sunglassescheck].active && NPC.ai[0] != 101f)
                {
                    return;
                }
                else
                {
                    ExposedToDeadlySun = true; // In the case that the above doesn't return, this happens
                }
                
              
               // NPC.life -= 1;
            }

            if (ExposedToDeadlySun)
            {

                NPC.ai[0] = 101f; // The reason I'm doing this bullshit of assigning a random number is
                //because otherwise this shit breaks. I have no idea why, and I've tried so long
                //to fix it, but it keeps fecking bnreaking AND THIS IS ALL THAT WORKS // Future me here: I'm more chill now I promise

                

                NPC.velocity.X *= 0;
                NPC.velocity.Y = 2;
                NPC.color = Color.Tan;
                NPC.aiStyle = -1;
                DamageTimer++;
                NPC.defense = 50;
                NPC.knockBackResist = 0.1f;
                NPC.HitSound = SoundID.NPCHit2;
                NPC.DeathSound = SoundID.Tink;

            }
            
         
            if (DamageTimer > 15)
            {
                DamageTimer = 0;
                NPC.life -= 10;
                SoundEngine.PlaySound(SoundID.NPCHit2, NPC.Center);
                int num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BorealWood, 0f, 0f, 2, Color.White, 0.9f);
                Main.dust[num3].velocity *= 0.2f;
                for (int i = 0; i < 8; i++)
                {
                    num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, (NPC.velocity.X + Main.rand.NextFloat(-1f, 1f)), (NPC.velocity.Y + Main.rand.Next(-1, 1)), 0, Color.White, Main.rand.NextFloat(0.8f, 1f));
                }


                if (NPC.life <= 0) // All of this is here too because the NPC dying from its own damage
                                   // won't spawn the gore and dust like being hit
                {
                    if (ExposedToDeadlySun == true)
                    {
						Main.BestiaryTracker.Kills.RegisterKill(NPC);
						for (int i = 0; i < 15; i++)
                        {

                            Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), 202, Main.rand.NextFloat(0.4f, 0.7f));
                        }
                        SoundEngine.PlaySound(SoundID.Tink, NPC.Center);
                        //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
                        //	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
                    }
                    else
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
                        }
                        Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome1, Main.rand.NextFloat(0.8f, 1.1f));
                        Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome2, Main.rand.NextFloat(0.8f, 1.1f));
                        Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome3, Main.rand.NextFloat(0.8f, 1.1f));
                        Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome4, Main.rand.NextFloat(0.8f, 1.1f));
                    }
                }
            }
            //  NPC sunglasses = Main.npc[(int)NPC.ai[1]];


            /*  if (NPCType<glassesgnome>() && Main.npc[n].active && Main.npc[n].ai[0] == NPC.ai[0])
              {

              }
              else
              {
                  NPC.velocity = Vector2.Zero;
              } */
        }
        int shaderID = ContentSamples.ItemsByType[ItemID.SilverDye].dye;
       
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
           /* Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (NPC.spriteDirection == 1)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
            }

            if (NPC.spriteDirection == 0)
            {
                //NPC.spriteDirection = 1;
                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

                //SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            }
            
           
            if (ExposedToDeadlySun && NPC.type == NPCType<glassesgnome>() && WhoIsThisGnome == NPC.whoAmI)
            {
                  Main.instance.PrepareDrawnEntityDrawing(NPC, shaderID);  
                  Main.EntitySpriteDraw(texture, NPC.position - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, NPC.rotation,// this shit broken. gotta fix this tmrw. 
                      new Vector2(NPC.Center.X, NPC.Center.Y), NPC.scale, spriteEffects, 0);  // Well I guess this shit is gonna stay broken. idk how to get it to only do this NPC
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
            NPC.frame, drawColor, NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.25f, TextureAssets.Npc[NPC.type].Value.Height * 0.055f), NPC.scale, spriteEffects, 0f);
            }
           */
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
            }

            if (NPC.spriteDirection == 0)
            {
                //NPC.spriteDirection = 1;
                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

                //SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            }

            if (ExposedToDeadlySun && NPC.type == NPCType<glassesgnome>() && WhoIsThisGnome == NPC.whoAmI)
            {
                
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
           NPC.frame, Color.Gray/*new Color(100, 100, 100, 0) * (0.3f + 0.3f * ((200 - NPC.alpha) / 255f) )*/, NPC.rotation,
           new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.05f), NPC.scale, spriteEffects, 0f);
                

              /*  for (int i = 0; i < 3; i++)
                {
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
           NPC.frame, new Color(100, 100, 100, 0) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f) ), NPC.rotation,
           new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.05f), NPC.scale, spriteEffects, 0f);
                } */
            }

            }
        
        
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return default;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return default;
        }

        

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (!ExposedToDeadlySun)
            {
                for (int i = 0; i < 5; i++)
                {
                    int num4 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
                }
                if (NPC.life <= 0)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        int num4 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
                    }
                    Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome1);
                    Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome2);
                    Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome3);
                    Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), GoreID.Gnome4);
                }
            }
            if (ExposedToDeadlySun == true)
            {
                int num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
                if (NPC.life <= 0)
                {
                        for (int i = 0; i < 15; i++)
                        {
                            Gore.NewGoreDirect(NPC.GetSource_Death(), NPC.position + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 4)), NPC.velocity + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), 202, Main.rand.NextFloat(0.4f, 0.7f));
                        }
                    for (int i = 0; i < 30; i++)
                    {
                        num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Stone, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.3f));
                    }
                    SoundEngine.PlaySound(SoundID.Tink, NPC.Center);
                }
            }
            
            
        }



        /*
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
                new FlavorTextBestiaryInfoElement("The gnomes have finally become smart enough to start wearing glasses. \n" +
                                                  "Too bad they aren't smart enough to invade when the sun isn't out")
            });
        }
        */
    }
}
	