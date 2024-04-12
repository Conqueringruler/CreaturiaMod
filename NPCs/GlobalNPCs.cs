using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Creaturia.NPCs.Town;
using Creaturia.NPCs.Enemies;
using Creaturia.NPCs.Enemies.Boss;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles.EnemyMelee;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Creaturia;
using Terraria.GameContent.ItemDropRules;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ReLogic.Content;
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Creaturia.NPCs.Enemies.GnomeEvent;
using Creaturia.Common;
using Creaturia.Common.Systems;

namespace Creaturia.NPCs
{
    public class GlobalNPCs : GlobalNPC
    {

        public int meleehit;
        int electricityDamageMult = 1;

        // I could probably just create lists without this Factory shit, but I'd rather just follow what vanilla does
        // public static SetFactory Factory = new SetFactory(NPCLoader.NPCCount);
/**        public static class Sets
        {


            public static bool[] Hallowed = NPCID.Sets.Factory.CreateBoolSet(NPCID.Unicorn, NPCID.Pixie, NPCID.SandsharkHallow, NPCID.HallowBoss, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue,
                                            NPCID.QueenSlimeMinionPink, NPCID.Gastropod, NPCID.LightMummy, NPCID.RainbowSlime, NPCID.FlyingFish,
                                        ModContent.NPCType<RainbowFish>(), NPCID.EmpressButterfly, NPCID.DesertGhoulHallow,
                                        NPCID.PigronHallow, NPCID.BigMimicHallow, NPCID.EnchantedSword, NPCID.IlluminantSlime, NPCID.IlluminantBat, NPCID.ChaosElemental);



            public static bool[] Evil = NPCID.Sets.Factory.CreateBoolSet(NPCID.CorruptBunny, NPCID.Corruptor, NPCID.EaterofSouls, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.DevourerBody
                       , NPCID.CorruptSlime, NPCID.Slimer, NPCID.Slimeling, NPCID.DarkMummy, NPCID.CorruptGoldfish, NPCID.Crimslime, NPCID.CrimsonBunny, NPCID.CrimsonGoldfish, NPCID.Crimera
                        , NPCID.BigCrimera, NPCID.CrimsonAxe, NPCID.BigCrimslime, NPCID.Herpling, NPCID.Creeper, NPCID.BrainofCthulhu, NPCID.BloodMummy, NPCID.BloodJelly
                         , NPCID.BloodFeeder, NPCID.BloodCrawler, NPCID.BloodCrawlerWall, NPCID.FaceMonster, NPCID.FloatyGross, NPCID.IchorSticker, NPCID.BloodCrawlerWall
                          , NPCID.DesertGhoulCrimson, NPCID.DesertGhoulCorruption, NPCID.CursedHammer, NPCID.SeekerBody, NPCID.Clinger, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.ServantofCthulhu,
                NPCID.EyeofCthulhu, NPCID.SkeletronHand, NPCID.SkeletronHead);




            public static bool[] Jungle = NPCID.Sets.Factory.CreateBoolSet(NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.PlanterasTentacle, NPCID.PlanterasHook,
                NPCID.Hornet, NPCID.GiantTortoise, NPCID.JungleBat, NPCID.Snatcher, NPCID.AngryTrapper, NPCID.Arapaima, NPCID.JungleCreeper, NPCID.Derpling, NPCID.JungleCreeperWall
               , NPCID.TurtleJungle, NPCID.Frog, NPCID.DoctorBones, NPCID.BigMimicJungle, NPCID.Grubby, NPCID.Sluggy, NPCID.Buggy, NPCID.Moth, NPCID.HornetFatty, NPCID.HornetLeafy
               , NPCID.HornetSpikey, NPCID.GiantMossHornet, NPCID.LittleMossHornet, NPCID.Piranha, NPCID.LittleHornetFatty, NPCID.HornetStingy, NPCID.GiantMossHornet,
                NPCID.LittleHornetLeafy, NPCID.LittleHornetStingy, NPCID.LittleHornetSpikey, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree,
                NPCID.FlyingSnake, NPCID.Lihzahrd, NPCID.LihzahrdCrawler);

           

            public static bool[] HurtingBeesTest = NPCID.Sets.Factory.CreateBoolSet(210, 211, 222);
        } 
        int[] HallowedList = new int[] {NPCID.Unicorn, NPCID.Pixie, NPCID.SandsharkHallow, NPCID.HallowBoss, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue,
                                        NPCID.QueenSlimeMinionPink, NPCID.Gastropod, NPCID.LightMummy, NPCID.RainbowSlime, NPCID.FlyingFish,
                                    ModContent.NPCType<RainbowFish>(), NPCID.EmpressButterfly, NPCID.DesertGhoulHallow,
                                    NPCID.PigronHallow, NPCID.BigMimicHallow, NPCID.EnchantedSword, NPCID.IlluminantSlime, NPCID.IlluminantBat, NPCID.ChaosElemental };

            int[] EvilList = new int[] {NPCID.CorruptBunny, NPCID.Corruptor, NPCID.EaterofSouls, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.DevourerBody
                   , NPCID.CorruptSlime, NPCID.Slimer, NPCID.Slimeling, NPCID.DarkMummy, NPCID.CorruptGoldfish, NPCID.Crimslime, NPCID.CrimsonBunny, NPCID.CrimsonGoldfish, NPCID.Crimera
                    , NPCID.BigCrimera, NPCID.CrimsonAxe, NPCID.BigCrimslime, NPCID.Herpling, NPCID.Creeper, NPCID.BrainofCthulhu, NPCID.BloodMummy, NPCID.BloodJelly
                     , NPCID.BloodFeeder, NPCID.BloodCrawler, NPCID.BloodCrawlerWall, NPCID.FaceMonster, NPCID.FloatyGross, NPCID.IchorSticker, NPCID.BloodCrawlerWall
                      , NPCID.DesertGhoulCrimson, NPCID.DesertGhoulCorruption, NPCID.CursedHammer, NPCID.SeekerBody, NPCID.Clinger, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.ServantofCthulhu,
            NPCID.EyeofCthulhu, NPCID.SkeletronHand, NPCID.SkeletronHead }; */
        public override bool InstancePerEntity => true;
       
        /**
        public override bool? CanHitNPC(NPC npc, NPC target)
        {

            if (npc.type == NPCID.Shark || npc.type == NPCID.BlueJellyfish || npc.type == NPCID.GreenJellyfish || npc.type == NPCID.Squid || npc.type == NPCID.PinkJellyfish || npc.type == NPCID.BloodJelly || npc.type == NPCID.FlyingFish || npc.type == NPCID.EyeballFlyingFish || npc.type == NPCID.Crab || npc.type == NPCID.SeaSnail)
            {
                if ( target.type == ModContent.NPCType<Fishman>())
                {
                    return false;

                }
            }
            if (npc.type == NPCID.Unicorn || npc.type == NPCID.Pixie || npc.type == NPCID.SandsharkHallow || npc.type == NPCID.HallowBoss || npc.type == NPCID.QueenSlimeBoss || npc.type == NPCID.QueenSlimeMinionBlue || npc.type == NPCID.QueenSlimeMinionPink || npc.type == NPCID.Gastropod || npc.type == NPCID.LightMummy || npc.type == NPCID.RainbowSlime || npc.type == NPCID.FlyingFish)
            {
                if ( target.type == ModContent.NPCType<RainbowFish>() || target.type is NPCID.QueenSlimeBoss or NPCID.HallowBoss)
                {
                    return false;
                }

            }
            if (npc.type == NPCID.HallowBoss || npc.type == NPCID.QueenSlimeBoss)
            {
                
                if (target.type is NPCID.CorruptBunny or NPCID.Corruptor or NPCID.EaterofSouls or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.DevourerBody or NPCID.CorruptSlime or NPCID.Slimer or NPCID.Slimeling or NPCID.DarkMummy or NPCID.CorruptGoldfish or NPCID.Crimslime or NPCID.CrimsonBunny or NPCID.CrimsonGoldfish or NPCID.Crimera or NPCID.BigCrimera or NPCID.CrimsonAxe or NPCID.BigCrimslime or NPCID.Herpling or NPCID.Creeper or NPCID.BrainofCthulhu or NPCID.BloodMummy or NPCID.BloodJelly or NPCID.BloodFeeder or NPCID.BloodCrawler or NPCID.BloodCrawlerWall or NPCID.FaceMonster or NPCID.FloatyGross or NPCID.IchorSticker or NPCID.BloodCrawlerWall or NPCID.DesertGhoulCrimson or NPCID.DesertGhoulCorruption or NPCID.CursedHammer or NPCID.SeekerBody or NPCID.Clinger or NPCID.BigMimicCorruption or NPCID.BigMimicCrimson)
                {
                    //npc.StrikeNPCNoInteraction(npc.damage, 6, -1);
                    npc.dontTakeDamageFromHostiles = false;
                    return true;

                }
                if (npc.type is NPCID.BlueSlime or NPCID.SlimeSpiked)
                {
                    if (target.type == ModContent.NPCType<Ninja>())
                    {
                        return false;
                    }
                }
            }
            return null;
        } */

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            
            if (!Main.hardMode)
            {
                electricityDamageMult = 1;
            }
            else if (Main.hardMode && !NPC.downedGolemBoss)
            {
                electricityDamageMult = 2;
            }
            else if (Main.hardMode && NPC.downedGolemBoss)
            {
                electricityDamageMult = 4;
            }
            if (npc.type is not NPCID.GreenJellyfish or NPCID.BlueJellyfish or NPCID.PinkJellyfish or NPCID.BloodJelly or NPCID.MartianDrone or NPCID.MartianEngineer or NPCID.MartianOfficer
                or NPCID.MartianProbe or NPCID.MartianSaucer or NPCID.MartianSaucerCannon or NPCID.MartianSaucerTurret or NPCID.MartianWalker or NPCID.GigaZapper or NPCID.BrainScrambler
                or NPCID.RayGunner)
            {

                if (!ModLoader.TryGetMod("CalamityMod", out Mod Calamity)) // Calamity does their own electricity damage, so I don't want mine to stack on top.
                {

                    if (npc.HasBuff(BuffID.Electrified)) //  ElectrifiedDamageValue = (int)(5 * (npc.velocity.X == 0 ? 1 : 4) * electricityDamageMult)
                    {
                        if (npc.lifeRegen > 0)
                        {
                            npc.lifeRegen = 0;
                        }
                        npc.lifeRegen -= 16;
                        damage = (int)(5 * ((MathF.Abs(npc.velocity.X) + MathF.Abs(npc.velocity.Y)) > 0.5f ? 1 : 40) * electricityDamageMult);

                        if (Main.rand.NextBool(3))
                        {
                            Dust dust = Dust.NewDustDirect(npc.position + new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), npc.width, npc.height, DustID.Electric, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, new Color(255, 0, 133), 0.4f);
                            dust.noGravity = true;
                            dust.fadeIn = 1f;
                            ;
                        }

                    }
                }
            }
        }
        float ProjectileWhoAmI;

      /**  private void GetHurtByOtherNPCs(NPC npc, bool[] acceptableNPCIDs)
        {

            if (npc.dontTakeDamage || npc.dontTakeDamageFromHostiles || npc.immortal)
            {
                return;
            }
            int specialHitSetter = 1;
            float damageMultiplier = 1f;
            if (npc.immune[255] != 0)
            {
                return;
            }
            Rectangle hitbox = npc.Hitbox;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (acceptableNPCIDs[nPC.type] && nPC.active && !nPC.friendly && nPC.damage > 0)
                {
                    Rectangle npcRect = nPC.Hitbox;
                    NPC.GetMeleeCollisionData(hitbox, i, ref specialHitSetter, ref damageMultiplier, ref npcRect);
                    if (hitbox.Intersects(npcRect) && (npc.type != 453 || !NPCID.Sets.Skeletons[nPC.type]) && nPC.type != 624 && (npc.whoAmI != nPC.whoAmI))
                    {
                        BeHurtByOtherNPC(npc, i, nPC);
                        break;
                    }
                }
            }
        }
        private void BeHurtByOtherNPC(NPC npc, int npcIndex, NPC thatNPC)
        {
           
            int num = 30;
            if (npc.type == 548)
            {
                num = 20;
            }
            int NPCdamage = Main.DamageVar(thatNPC.damage);
            int npcKnockback = 6;
            int hitDirection = ((!(thatNPC.Center.X > npc.Center.X)) ? 1 : (-1));
            double num5 = npc.StrikeNPCNoInteraction(NPCdamage, npcKnockback, hitDirection);
            if (Main.netMode != 0)
            {
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, NPCdamage, npcKnockback, hitDirection);
            }
            npc.netUpdate = true;
            npc.immune[255] = num;
            if (npc.dryadWard)
            {
                NPCdamage = (int)num5 / 3;
                npcKnockback = 6;
                hitDirection *= -1;
                thatNPC.StrikeNPCNoInteraction(NPCdamage, npcKnockback, hitDirection);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(28, -1, -1, null, npcIndex, NPCdamage, npcKnockback, hitDirection);
                }
                thatNPC.netUpdate = true;
                thatNPC.immune[255] = num;
            }
            if (NPCID.Sets.HurtingBees[thatNPC.type])
            {
                NPCdamage = npc.damage;
                npcKnockback = 6;
                hitDirection *= -1;
                thatNPC.StrikeNPCNoInteraction(NPCdamage, npcKnockback, hitDirection);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(28, -1, -1, null, npcIndex, NPCdamage, npcKnockback, hitDirection);
                }
                thatNPC.netUpdate = true;
                thatNPC.immune[255] = num;
            }
            if (TestSets.Evil[thatNPC.type])
            {
                NPCdamage = npc.damage;
                npcKnockback = 6;
                hitDirection *= -1;
                thatNPC.StrikeNPCNoInteraction(NPCdamage, npcKnockback, hitDirection);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(28, -1, -1, null, npcIndex, NPCdamage, npcKnockback, hitDirection);
                }
                thatNPC.netUpdate = true;
                thatNPC.immune[255] = num;
            }
            if (NPCID.Sets.AllNPCs[thatNPC.type])
            {
                NPCdamage = npc.damage;
                npcKnockback = 6;
                hitDirection *= -1;
                thatNPC.StrikeNPCNoInteraction(NPCdamage, npcKnockback, hitDirection);
                if (Main.netMode != 0)
                {
                    NetMessage.SendData(28, -1, -1, null, npcIndex, NPCdamage, npcKnockback, hitDirection);
                }
                thatNPC.netUpdate = true;
                thatNPC.immune[255] = num;
            }
        } 
        private void Step2OfCheckNPCs(NPC npc)
        {


            // I give up. I tried for so, so long to make code that was clean and wasn't an ugly block of ineffectiveness, but now I see that was never a real possibility. I have been humbled. 
     /**       if (NPC.npcsFoundForCheckActive[EvilList[0]] || NPC.npcsFoundForCheckActive[EvilList[1]] || NPC.npcsFoundForCheckActive[EvilList[2]] || NPC.npcsFoundForCheckActive[EvilList[3]] ||
                NPC.npcsFoundForCheckActive[EvilList[4]] || NPC.npcsFoundForCheckActive[EvilList[5]] || NPC.npcsFoundForCheckActive[EvilList[6]] || NPC.npcsFoundForCheckActive[EvilList[7]]
                 || NPC.npcsFoundForCheckActive[EvilList[8]] || NPC.npcsFoundForCheckActive[EvilList[9]] || NPC.npcsFoundForCheckActive[EvilList[10]] || NPC.npcsFoundForCheckActive[EvilList[11]]
                  || NPC.npcsFoundForCheckActive[EvilList[12]] || NPC.npcsFoundForCheckActive[EvilList[13]] || NPC.npcsFoundForCheckActive[EvilList[14]] || NPC.npcsFoundForCheckActive[EvilList[15]]
                   || NPC.npcsFoundForCheckActive[EvilList[16]] || NPC.npcsFoundForCheckActive[EvilList[17]] || NPC.npcsFoundForCheckActive[EvilList[18]] || NPC.npcsFoundForCheckActive[EvilList[19]]
                    || NPC.npcsFoundForCheckActive[EvilList[20]] || NPC.npcsFoundForCheckActive[EvilList[21]] || NPC.npcsFoundForCheckActive[EvilList[22]] || NPC.npcsFoundForCheckActive[EvilList[23]]
                     || NPC.npcsFoundForCheckActive[EvilList[24]] || NPC.npcsFoundForCheckActive[EvilList[25]] || NPC.npcsFoundForCheckActive[EvilList[26]] || NPC.npcsFoundForCheckActive[EvilList[27]]
                      || NPC.npcsFoundForCheckActive[EvilList[28]] || NPC.npcsFoundForCheckActive[EvilList[29]] || NPC.npcsFoundForCheckActive[EvilList[30]] || NPC.npcsFoundForCheckActive[EvilList[31]]
                       || NPC.npcsFoundForCheckActive[EvilList[32]] || NPC.npcsFoundForCheckActive[EvilList[33]] || NPC.npcsFoundForCheckActive[EvilList[34]] || NPC.npcsFoundForCheckActive[EvilList[35]]
                        || NPC.npcsFoundForCheckActive[EvilList[36]] || NPC.npcsFoundForCheckActive[EvilList[37]] || NPC.npcsFoundForCheckActive[EvilList[38]] || NPC.npcsFoundForCheckActive[EvilList[39]]
                         || NPC.npcsFoundForCheckActive[EvilList[40]] || NPC.npcsFoundForCheckActive[EvilList[41]] || NPC.npcsFoundForCheckActive[EvilList[42]] && !Sets.Evil[npc.type])
            {
                GetHurtByOtherNPCs(npc, Sets.Evil);
            } */



                
           
                
           

        
        /**
        private void CheckOtherNPCs(NPC npc)
        {
           for (int i = 0; i < HallowedList.Count(); i++)
            {
                int value = (int)HallowedList.GetValue(i); 
                if (value >= 0)
                {
                    if (NPC.npcsFoundForCheckActive[value])
                    {
                        for (int j = 0; j < EvilList.Count(); j++)
                        {
                            int value2 = (int)EvilList.GetValue(j); 
                            if (value2 >= 0)
                            {
                                if (NPC.npcsFoundForCheckActive[value2])
                                {
                                    if (npc.type == (int)HallowedList.GetValue(i) || npc.type == (int)EvilList.GetValue(j))
                                    {
                                        if (npc.type != 37)
                                    {
                                            if (Sets.Evil != null)
                                            {

                                                if (Sets.Hallowed != null)
                                                {
                                                    Step2OfCheckNPCs(npc);
                                                }
                                            }
                                        
                                    }
                                    }
                                   
                                    
                                    
                                }
                            }
                        }

                    }
                    else
                    {
                        
                    }
                }
               
            }
        } */ 
        public override void AI(NPC npc)
        {
       //     CheckOtherNPCs(npc);
            // if (Main.netMode != 1/* && npc.type != 37 && (npc.friendly || NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[npc.type]) */)
            //   {
            //       if (npc.townNPC)
            //       {
            //               npc.CheckDrowning();
            //      }

            //        GetHurtByOtherNPCs(npc, NPCID.Sets.AllNPCs);

            //      } 

            //if (Main.netMode != 1 && Evil[npc.type])
            // {
            //      GetHurtByOtherNPCs(npc, Hallowed);
            //       GetHurtByOtherNPCs(npc, Jungle);
            //    }
            /** if (npc != null && npc.active == true)
              {
                  if (Main.netMode != 1)
                  { 
                      if (NPC.npcsFoundForCheckActive[id] && NPC.npcsFoundForCheckActive[Evil.Length])
                          {
                              if (Hallowed[npc.type] && !Evil[npc.type])
                              {

                                  //  npc.CheckLifeRegen();
                                  GetHurtByOtherNPCs(npc, Evil);
                              }
                          }
                    
                     
                    
                    }
                  } */
            //   if (Main.netMode != 1 && (NPC.npcsFoundForCheckActive[210] || NPC.npcsFoundForCheckActive[211]) && !Hallowed[npc.type])
            //     {
            //         GetHurtByOtherNPCs(npc, Evil);
            //     }
/**

            if (Main.netMode != 1 && npc.type != 37 && (npc.friendly || NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[npc.type]))
            {
                if (npc.townNPC)
                {
                    npc.CheckDrowning();
                }
              //  npc.CheckLifeRegen();
                GetHurtByOtherNPCs(npc, NPCID.Sets.AllNPCs);
            }


          
            if (Main.netMode != 1 && (NPC.npcsFoundForCheckActive[210] || NPC.npcsFoundForCheckActive[211]) && !NPCID.Sets.HurtingBees[npc.type])
            {
                GetHurtByOtherNPCs(npc, NPCID.Sets.HurtingBees);
            }
            if (Main.netMode != 1 && (NPC.npcsFoundForCheckActive[210] || NPC.npcsFoundForCheckActive[211]) && !Sets.HurtingBeesTest[npc.type])
            {
                GetHurtByOtherNPCs(npc, Sets.HurtingBeesTest);
            }


            // Since the method is private in source code, just going to recreate it here by porting code
            // GetHurtByOtherNPCs( NPCID.Sets.AllNPCs);

            if (npc.dontTakeDamage || npc.dontTakeDamageFromHostiles || npc.immortal)
            {
                return;
            } */
            





            if (npc.type == NPCID.QueenSlimeBoss)
                {
                    meleehit++;
                    if (meleehit > 18)
                    {
                   //     Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<QueenSlimeHit>(), 45, 1, Main.myPlayer);
                        meleehit = 0;
                    }
                    
                }
            if (npc.type == NPCID.HallowBoss)
            {
                meleehit++;
                if (meleehit > 18)
                {
                  //  Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<EmpressofLightHit>(), 65, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.EaterofWorldsBody)
            {
                meleehit++;
                if (meleehit > 18)
                {
                   // Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<EaterofWorldsHit>(), 28, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.VileSpitEaterOfWorlds || npc.type == NPCID.VileSpit)
            {
                meleehit++;
                if (meleehit > 18) // To Do: need to make the NPC check for the projectile still being active, and if not spawning another one
                {
               //     int npcMeleeProj = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<VileSpitHit>(), 25, 1, Main.myPlayer);
                  
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                meleehit++;
                if (meleehit > 18)
                {
                 //   Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<BrainofCthulhuHit>(), 28, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.Plantera)
            {
                meleehit++;
                if (meleehit > 18)
                {
                 //   Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<PlanteraHit>(), 60, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
        }


        public override void OnCaughtBy(NPC npc, Player player, Item item, bool failed)
        {
            base.OnCaughtBy(npc, player, item, failed);
            if (npc.type == NPCID.Duck)
            {
                npc.Transform(NPCType<DuckMaiden>());
            }
        }
       
        public override void SetDefaults(NPC npc)
        {

            if (npc.type == NPCID.Cyborg)
            {
                npc.defense = 20;
                
                npc.lifeMax = 550;
            }
            if (npc.type == NPCID.WitchDoctor)
            {
                npc.lifeMax = 300;
            }
            if (npc.type == NPCID.CorruptBunny || npc.type == NPCID.Corruptor || npc.type == NPCID.EaterofSouls || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.DevourerBody || npc.type == NPCID.CorruptSlime || npc.type == NPCID.Slimer || npc.type == NPCID.Slimeling || npc.type == NPCID.DarkMummy || npc.type == NPCID.CorruptGoldfish || npc.type == NPCID.Crimslime || npc.type == NPCID.CrimsonBunny || npc.type == NPCID.CrimsonGoldfish || npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.CrimsonAxe || npc.type == NPCID.BigCrimslime || npc.type == NPCID.Herpling || npc.type == NPCID.Creeper || npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.BloodMummy || npc.type == NPCID.BloodJelly || npc.type == NPCID.BloodFeeder || npc.type == NPCID.BloodCrawler || npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.FaceMonster || npc.type == NPCID.FloatyGross || npc.type == NPCID.IchorSticker || npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.DesertGhoulCrimson || npc.type == NPCID.DesertGhoulCorruption || npc.type == NPCID.CursedHammer || npc.type == NPCID.SeekerBody || npc.type == NPCID.Clinger || npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson)
            {
                npc.dontTakeDamageFromHostiles = false;
            }

        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.OnHitByItem(npc, player, item, damage, knockback, crit);
            if (npc.type == ModContent.NPCType<TheHellborneSkull>() || npc.type == ModContent.NPCType<HellborneSkullMinion>() || npc.type == ModContent.NPCType<HellborneGuardian>())
            {
                if (item.type == ItemID.TheHorsemansBlade || item.type == ItemID.ChristmasTreeSword)
                {
                    damage *= 2;
                }
            }
            else
            {
                item.damage = item.OriginalDamage;
            }
        }
       
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

            
            if (npc.type == ModContent.NPCType<TheHellborneSkull>() || npc.type == ModContent.NPCType<HellborneSkullMinion>() || npc.type == ModContent.NPCType<HellborneGuardian>())
            {
                if (projectile.type == ProjectileID.CandyCorn || projectile.type == ProjectileID.JackOLantern || projectile.type == ProjectileID.FlamingJack ||
                projectile.type == ProjectileID.Stake || projectile.type == ProjectileID.Bat || projectile.type == ProjectileID.PineNeedleFriendly || projectile.type == ProjectileID.PineNeedleHostile 
                || projectile.type == ProjectileID.Raven || projectile.type == ProjectileID.OrnamentFriendly || projectile.type == ProjectileID.OrnamentStar
                || projectile.type == ProjectileID.ClusterSnowmanRocketI || projectile.type == ProjectileID.RocketSnowmanI || projectile.type == ProjectileID.RocketSnowmanIII
                || projectile.type == ProjectileID.MiniNukeSnowmanRocketI || projectile.type == ProjectileID.ScytheWhip || projectile.type == ProjectileID.ScytheWhipProj || projectile.type == ProjectileID.Blizzard)
                {
                    //projectile.damage = 0;
                    
                }
            }  // This is done in GlobalProjectiles now since it refused to work correctly here.
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            
           // if (projectile.type ==  && npc.type == NPCID.Werewolf)
          //  {
          //      npc.life -= 200; I guess silver bullets and musket balls are the same projectile.
          //  }
            if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.Squirrel || 
                npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack)
            {
                if (Main.rand.NextBool(6000))
                {
                    npc.Transform(NPCType<SkinWalker>());
                }
              
            }
            if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.Salamander || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || 
                npc.type == NPCID.Squirrel || npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack || npc.type == NPCID.Unicorn || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.Owl)
            {
               if (Main.rand.NextBool(40000))
                {
                    npc.Transform(NPCType<SkinWalker>());
                    npc.netUpdate = true;
                }
            }
        }
        
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.Salamander || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.Squirrel || npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack || npc.type == NPCID.Unicorn || npc.type == NPCID.WalkingAntlion)
            {
                if (Main.rand.NextBool(10000))
                {
                    NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<SkinWalker>());
                }

            }
            if (npc.type == NPCID.KingSlime && npc.AnyInteractions())
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<Ninja>()))
                {
                    NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Ninja>());
                }
                
            }
            if (npc.type == NPCID.GolemFistLeft && npc.AnyInteractions())
            {
                NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GolemFistL>());
            }

            if (npc.type == NPCID.GolemFistRight && npc.AnyInteractions())
            {
                NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GolemFistR>());
            }
          
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.type == NPCID.Golem)
            {
                Color color = new Color(255, 255, 255, 0);
                SpriteEffects spriteEffects = SpriteEffects.None;
                Texture2D texture = Mod.Assets.Request<Texture2D>("NPCs/Enemies/golemhellbornecrest").Value;
                spriteBatch.Draw(texture, npc.Center - screenPos,
            npc.frame, color, npc.rotation, // reminder for later; the sprite is there, just under the other stuff
            new Vector2(TextureAssets.Npc[npc.type].Value.Width * 0.5f, TextureAssets.Npc[npc.type].Value.Height * 0.095f), npc.scale, spriteEffects, 0f);

                

            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        
      /*  public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            
            
           if (npc.type == NPCID.Golem)
            {
                Color color = new Color(255, 255, 255, 0);
                SpriteEffects spriteEffects = SpriteEffects.None;
                Texture2D texture = Mod.Assets.Request<Texture2D>("NPCs/Enemies/golemhellbornecrest").Value;
                spriteBatch.Draw(texture, npc.Center - screenPos,
            npc.frame, color, npc.rotation, // reminder for later; the sprite is there, just under the other stuff
            new Vector2(TextureAssets.Npc[npc.type].Value.Width * 0.5f, TextureAssets.Npc[npc.type].Value.Height * 0.098f), npc.scale, spriteEffects, 0f);

            

            } 
        } */


    }
}
        



