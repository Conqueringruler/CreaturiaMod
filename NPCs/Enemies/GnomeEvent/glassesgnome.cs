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
    public class sunglasses : ModNPC
    {
       
        public static int gnomeType()
        {
            return ModContent.NPCType<glassesgnome>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("sunglasses");
            
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Ichor,
                    BuffID.WeaponImbueIchor,
                    BuffID.CursedInferno,
                    BuffID.Confused,
                    BuffID.Poisoned,
                    BuffID.Burning,
                    BuffID.Frostburn,
                    BuffID.Frostburn2,
                    BuffID.Frozen,
                    BuffID.Electrified,
                    BuffID.OnFire,
                    BuffID.OnFire3
                }
            };
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            }
        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
        {
            dataNPC.Entries.Remove(bestiaryEntry);
        }
        public override void SetDefaults()
        {

            NPC.width = 16;
            NPC.height = 16;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.lifeMax = 35;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.stepSpeed = 500;
            NPC.lavaImmune = false;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            
        }
        public bool idk = true;
        
        public override void AI()
        {
            

            if (NPC.type == ModContent.NPCType<sunglasses>())
            {
                int GlassesGnomeIdentity = (int)NPC.ai[0]; // Glasses gnome already set NPC.ai[0] as being that NPC, here we just turn it into something easier to read
                Main.npc[GlassesGnomeIdentity].ai[0] = NPC.whoAmI; // Now we do the same for Sunglasses

                if (Main.npc[GlassesGnomeIdentity].active && (Main.npc[GlassesGnomeIdentity].type == ModContent.NPCType<glassesgnome>() || Main.npc[GlassesGnomeIdentity].type == ModContent.NPCType<strongerglassesgnome>()))
                {
                    NPC.velocity = Vector2.Zero;
                    NPC.position = Main.npc[GlassesGnomeIdentity].Top + new Vector2(-5, -8);
                    NPC.spriteDirection = Main.npc[GlassesGnomeIdentity].direction;
                    NPC.ai[3] = Main.npc[GlassesGnomeIdentity].target;
                    return;
                }
                NPC.StrikeNPCNoInteraction(9999, 0f, 0); // it'll do NPC if it doesn't return above
                    NPC.netUpdate = true;
                
            }
           
        }
       

        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCProjectiles.Add(index);
        }

    }

        class glassesgnome : ModNPC
    {



        
        public static int glassestype()
        {
            return ModContent.NPCType<sunglasses>();
        }
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Gnome;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnome");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Gnome];
            
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
            NPC.damage = 10;
            NPC.defense = 5;
            NPC.lifeMax = 80;
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
        int WhoIsNPCGnome;
        public void spawnglasses()
        {
            if (NPC.localAI[0] == 0f)
            {
                NPC.localAI[0] = 1f; // So we only spawn it once
                WhoIsNPCGnome = NPC.whoAmI;
                newNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<sunglasses>(), NPC.whoAmI);
                Main.npc[newNPC].ai[0] = WhoIsNPCGnome; // newNPC is the one we just spawned. ai[0] is a variable in it. We then set it as being NPC NPC, so it knows who its owner is.
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
        int StartingHealTime = 60;
        bool IsWalking = false;
        bool noXMovement = false;
        public override void AI()
        {
            // I wish my skills were good enough to figure out what all NPC shit does. I get most of it, but there's so much 
            // Originally transcribed from Fighter AI
            if (ExposedToDeadlySun == false)
            {


                
                if (NPC.velocity.X == 0f)
                {
                    noXMovement = true;
                }
                if (NPC.justHit)
                {
                    noXMovement = false;
                }
                
                int backUpTimer = 60;
                if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
                {
                    IsWalking = true;
                }
                if ((NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)backUpTimer) | IsWalking)
                {
                    NPC.ai[3] += 1f;
                    
                }
                else if ((double)Math.Abs(NPC.velocity.X) > 0.9 && NPC.ai[3] > 0f)
                {
                    NPC.ai[3] -= 1f;
                }
                if (NPC.ai[3] > (float)(backUpTimer * 10))
                {
                    NPC.ai[3] = 0f;
                }
                if (NPC.justHit)
                {
                    NPC.ai[3] = 0f; // When hit resets the NPC to attacking the player I think, meaning ai[3] is the counter for running away
                    HealTime = StartingHealTime;
                }
                if (NPC.ai[3] == (float)backUpTimer)
                {
                    NPC.netUpdate = true;
                }
                if (NPC.ai[3] < (float)backUpTimer)
                {
                    NPC.TargetClosest(true);
                }
                else if (NPC.ai[2] <= 0f)
                {
                    if (NPC.velocity.X == 0f)
                    {
                        if (NPC.velocity.Y == 0f)
                        {
                            NPC.ai[0] += 1f;
                            if (NPC.ai[0] >= 2f)
                            {
                                NPC.direction *= -1;
                                NPC.spriteDirection = NPC.direction;
                                NPC.ai[0] = 0f;

                                // NPC part is what makes the NPC run away when not able to reach the player. I think. 
                                HealTime = 15;
                                //NPC.active = false;
                            }
                        }
                    }
                    else
                    {
                        NPC.ai[0] = 0f;
                        
                    }
                    if (NPC.direction == 0)
                    {
                        NPC.direction = 1;
                        HealTime = StartingHealTime;
                    }
                }

                
                
              if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) < 16f)
                {
                    NPC.ai[3] = 0f;
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                    if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                        NPC.velocity.X = 0f;
                    return;
                }
                float maxVelocity = 3f; // keep in mind I can change and multiply NPC super easy,
                float acceleration = 0.1f; // so if I make a gnome rallier or something I could have regular gnomes
                                           // speed up
                if (NPC.velocity.X < -maxVelocity || NPC.velocity.X > maxVelocity)
                {
                    if (NPC.velocity.Y == 0f)
                        NPC.velocity *= 0.7f;
                }
                else if (NPC.velocity.X < maxVelocity && NPC.direction == 1)
                {
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                    if (NPC.velocity.X > maxVelocity)
                        NPC.velocity.X = maxVelocity;
                }
                else if (NPC.velocity.X > -maxVelocity && NPC.direction == -1)
                {
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.X < -maxVelocity)
                        NPC.velocity.X = -maxVelocity;
                }
                bool isOnSolidTile = false;
                if (NPC.velocity.Y == 0f)
                {
                    int yTile = (int)(NPC.position.Y + (float)NPC.height + 7f) / 16;
                    int initialXTile = (int)NPC.position.X / 16;
                    int maxXTile = (int)(NPC.position.X + (float)NPC.width) / 16;
                    for (int xTile = initialXTile; xTile <= maxXTile; xTile++)
                    {
                        if (Main.tile[xTile, yTile] == null)
                        {
                            return;
                        }
                        if (Main.tile[xTile, yTile].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[xTile, yTile].TileType])
                        {
                            isOnSolidTile = true;
                            break;
                        }
                    }
                }
                if (NPC.velocity.Y >= 0f)
                {
                    int fallFaceDirection = 0;
                    if (NPC.velocity.X < 0f)
                    {
                        fallFaceDirection = -1;
                    }
                    if (NPC.velocity.X > 0f)
                    {
                        fallFaceDirection = 1;
                    }
                    Vector2 npcPosition = NPC.position;
                    npcPosition.X += NPC.velocity.X;
                    int xTileBelow = (int)((npcPosition.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * fallFaceDirection)) / 16f);
                    int yTileBelow = (int)((npcPosition.Y + (float)NPC.height - 1f) / 16f);
                    if ((float)(xTileBelow * 16) < npcPosition.X + (float)NPC.width && (float)(xTileBelow * 16 + 16) > npcPosition.X && ((Main.tile[xTileBelow, yTileBelow].HasUnactuatedTile && !Main.tile[xTileBelow, yTileBelow].TopSlope && !Main.tile[xTileBelow, yTileBelow - 1].TopSlope && Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow].TileType] && !Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow].TileType]) || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile)) && (!Main.tile[xTileBelow, yTileBelow - 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 1].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 1].TileType] || (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock && (!Main.tile[xTileBelow, yTileBelow - 4].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 4].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 4].TileType]))) && (!Main.tile[xTileBelow, yTileBelow - 2].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 2].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 2].TileType]) && (!Main.tile[xTileBelow, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow, yTileBelow - 3].TileType] || Main.tileSolidTop[(int)Main.tile[xTileBelow, yTileBelow - 3].TileType]) && (!Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[xTileBelow - fallFaceDirection, yTileBelow - 3].TileType]))
                    {
                        float yPixelDistance = (float)(yTileBelow * 16);
                        if (Main.tile[xTileBelow, yTileBelow].IsHalfBlock)
                        {
                            yPixelDistance += 8f;
                        }
                        if (Main.tile[xTileBelow, yTileBelow - 1].IsHalfBlock)
                        {
                            yPixelDistance -= 8f;
                        }
                        if (yPixelDistance < npcPosition.Y + (float)NPC.height)
                        {
                            float percentageTileRisen = npcPosition.Y + (float)NPC.height - yPixelDistance;
                            float fullTileAmt = 16.1f;
                            if (percentageTileRisen <= fullTileAmt)
                            {
                                NPC.gfxOffY += NPC.position.Y + (float)NPC.height - yPixelDistance;
                                NPC.position.Y = yPixelDistance - (float)NPC.height;
                                if (percentageTileRisen < 9f)
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
                if (isOnSolidTile)
                {
                    int doorCheckX = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)(15 * NPC.direction)) / 16f);
                    int doorCheckY = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
                    if ((Main.tile[doorCheckX, doorCheckY - 1].HasUnactuatedTile && (Main.tile[doorCheckX, doorCheckY - 1].TileType == 10 || Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)))
                    {
                        NPC.ai[2] += 1f; 
                        NPC.ai[3] = 0f;
                        if (NPC.ai[2] >= 60f)
                        {
                            NPC.velocity.X = 0.5f * (float)-(float)NPC.direction;
                            int doorOpenInc = 5;
                            if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)
                            {
                                doorOpenInc = 2;
                            }
                            NPC.ai[1] += (float)doorOpenInc;
                            NPC.ai[2] = 0f;
                            bool letMeIn = false;
                            if (NPC.ai[1] >= 10f)
                            {
                                letMeIn = true;
                                NPC.ai[1] = 10f;
                            }
                            WorldGen.KillTile(doorCheckX, doorCheckY - 1, true, false, false);
                            if ((Main.netMode != NetmodeID.MultiplayerClient || !letMeIn) && letMeIn && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 10)
                                {
                                    bool canOpenDoor = WorldGen.OpenDoor(doorCheckX, doorCheckY - 1, NPC.direction);
                                    if (!canOpenDoor)
                                    {
                                        NPC.ai[3] = (float)backUpTimer;
                                        NPC.netUpdate = true;
                                    }
                                    if (Main.netMode == NetmodeID.Server & canOpenDoor)
                                    {
                                        NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, 0, (float)doorCheckX, (float)(doorCheckY - 1), (float)NPC.direction, 0, 0, 0);
                                    }
                                }
                                if (Main.tile[doorCheckX, doorCheckY - 1].TileType == 388)
                                {
                                    bool canOpenTallGate = WorldGen.ShiftTallGate(doorCheckX, doorCheckY - 1, false);
                                    if (!canOpenTallGate)
                                    {
                                        NPC.ai[3] = (float)backUpTimer;
                                        NPC.netUpdate = true;
                                    }
                                    if (Main.netMode == NetmodeID.Server & canOpenTallGate)
                                    {
                                        NetMessage.SendData(MessageID.ToggleDoorState, -1, -1, null, 4, (float)doorCheckX, (float)(doorCheckY - 1), 0f, 0, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int faceDirection = NPC.spriteDirection;
                        if ((NPC.velocity.X < 0f && faceDirection == -1) || (NPC.velocity.X > 0f && faceDirection == 1))
                        {
                            if (NPC.height >= 32 && Main.tile[doorCheckX, doorCheckY - 2].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[doorCheckX, doorCheckY - 2].TileType])
                            {
                                if (Main.tile[doorCheckX, doorCheckY - 3].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[doorCheckX, doorCheckY - 3].TileType])
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
                            else if (Main.tile[doorCheckX, doorCheckY - 1].HasUnactuatedTile && Main.tileSolid[(int)Main.tile[doorCheckX, doorCheckY - 1].TileType])
                            {
                                NPC.velocity.Y = -6f;
                                NPC.netUpdate = true;
                            }
                            else if (NPC.position.Y + (float)NPC.height - (float)(doorCheckY * 16) > 20f && Main.tile[doorCheckX, doorCheckY].HasUnactuatedTile && !Main.tile[doorCheckX, doorCheckY].TopSlope && Main.tileSolid[(int)Main.tile[doorCheckX, doorCheckY].TileType])
                            {
                                NPC.velocity.Y = -5f;
                                NPC.netUpdate = true;
                            }
                            else if (NPC.directionY < 0 && (!Main.tile[doorCheckX, doorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[doorCheckX, doorCheckY + 1].TileType]) && (!Main.tile[doorCheckX + NPC.direction, doorCheckY + 1].HasUnactuatedTile || !Main.tileSolid[(int)Main.tile[doorCheckX + NPC.direction, doorCheckY + 1].TileType]))
                            {
                                NPC.velocity.Y = -12f;
                                NPC.velocity.X = NPC.velocity.X * 1.5f;
                                NPC.netUpdate = true;
                            }
                            
                            if ((NPC.velocity.Y == 0f & noXMovement) && NPC.ai[3] == 1f)
                            {
                                NPC.velocity.Y = -5f;
                            }
                        }
                    }
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
                    ExposedToDeadlySun = true; // In the case that the above doesn't return, NPC happens
                }
                
              
               // NPC.life -= 1;
            }

            if (ExposedToDeadlySun)
            {

                NPC.ai[0] = 101f; // The reason I'm doing NPC bullshit of assigning a random number is
                //because otherwise NPC shit breaks. I have no idea why, and I've tried so long
                //to fix it, but it keeps fucking bnreaking AND NPC IS ALL THAT WORKS

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


                if (NPC.life <= 0) // All of NPC is here too because the NPC dying from its own damage
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
            
           
            if (ExposedToDeadlySun && NPC.type == NPCType<glassesgnome>() && WhoIsNPCGnome == NPC.whoAmI)
            {
                  Main.instance.PrepareDrawnEntityDrawing(NPC, shaderID);  
                  Main.EntitySpriteDraw(texture, NPC.position - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, NPC.rotation,// NPC shit broken. gotta fix NPC tmrw. 
                      new Vector2(NPC.Center.X, NPC.Center.Y), NPC.scale, spriteEffects, 0);  // Well I guess NPC shit is gonna stay broken. idk how to get it to only do NPC NPC
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

            if (ExposedToDeadlySun && NPC.type == NPCType<glassesgnome>() && WhoIsNPCGnome == NPC.whoAmI)
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

        

        public override void HitEffect(int hitDirection, double damage)
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




        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
                new FlavorTextBestiaryInfoElement("The gnomes have finally become smart enough to start wearing glasses. \n" +
                                                  "Too bad they aren't smart enough to invade when the sun isn't out!")
            });
        }
    }
}
	