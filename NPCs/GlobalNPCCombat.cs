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
using Creaturia.Common.Systems;
using Creaturia.Common;


namespace Creaturia.NPCs
{
    public class GlobalNPCCombat : GlobalNPC
    {

        public int meleehit;
        int electricityDamageMult = 1;

        // I could probably just create lists without this Factory shit, but I'd rather just follow what vanilla does
        // public static SetFactory Factory = new SetFactory(NPCLoader.NPCCount);
      //  public static class Sets
      //  {


         /**   public static bool[] Hallowed = NPCID.Sets.Factory.CreateBoolSet(NPCID.Unicorn, NPCID.Pixie, NPCID.SandsharkHallow, NPCID.HallowBoss, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue,
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
                NPCID.FlyingSnake, NPCID.Lihzahrd, NPCID.LihzahrdCrawler); */



          //  public static bool[] HurtingBeesTest = NPCID.Sets.Factory.CreateBoolSet(210, 211, 222);
       // }
        int[] HallowedList = new int[] {NPCID.Unicorn, NPCID.Pixie, NPCID.SandsharkHallow, NPCID.HallowBoss, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue,
                                        NPCID.QueenSlimeMinionPink, NPCID.Gastropod, NPCID.LightMummy, NPCID.RainbowSlime, NPCID.FlyingFish,
                                    ModContent.NPCType<RainbowFish>(), NPCID.EmpressButterfly, NPCID.DesertGhoulHallow,
                                    NPCID.PigronHallow, NPCID.BigMimicHallow, NPCID.EnchantedSword, NPCID.IlluminantSlime, NPCID.IlluminantBat, NPCID.ChaosElemental };

        int[] EvilList = new int[] {NPCID.CorruptBunny, NPCID.Corruptor, NPCID.EaterofSouls, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.DevourerBody
                   , NPCID.CorruptSlime, NPCID.Slimer, NPCID.Slimeling, NPCID.DarkMummy, NPCID.CorruptGoldfish, NPCID.Crimslime, NPCID.CrimsonBunny, NPCID.CrimsonGoldfish, NPCID.Crimera
                    , NPCID.BigCrimera, NPCID.CrimsonAxe, NPCID.BigCrimslime, NPCID.Herpling, NPCID.Creeper, NPCID.BrainofCthulhu, NPCID.BloodMummy, NPCID.BloodJelly
                     , NPCID.BloodFeeder, NPCID.BloodCrawler, NPCID.BloodCrawlerWall, NPCID.FaceMonster, NPCID.FloatyGross, NPCID.IchorSticker, NPCID.BloodCrawlerWall
                      , NPCID.DesertGhoulCrimson, NPCID.DesertGhoulCorruption, NPCID.CursedHammer, NPCID.SeekerBody, NPCID.Clinger, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.ServantofCthulhu,
            NPCID.EyeofCthulhu, NPCID.SkeletronHand, NPCID.SkeletronHead };
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

       

        /*private void GetHurtByOtherNPCs(NPC npc, bool[] acceptableNPCIDs)
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
            //  if (SetsSystem.HurtingBeesTest[thatNPC.type])
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
            if (NPC.npcsFoundForCheckActive[EvilList[0]] || NPC.npcsFoundForCheckActive[EvilList[1]] || NPC.npcsFoundForCheckActive[EvilList[2]] || NPC.npcsFoundForCheckActive[EvilList[3]] ||
               NPC.npcsFoundForCheckActive[EvilList[4]] || NPC.npcsFoundForCheckActive[EvilList[5]] || NPC.npcsFoundForCheckActive[EvilList[6]] || NPC.npcsFoundForCheckActive[EvilList[7]]
                || NPC.npcsFoundForCheckActive[EvilList[8]] || NPC.npcsFoundForCheckActive[EvilList[9]] || NPC.npcsFoundForCheckActive[EvilList[10]] || NPC.npcsFoundForCheckActive[EvilList[11]]
                 || NPC.npcsFoundForCheckActive[EvilList[12]] || NPC.npcsFoundForCheckActive[EvilList[13]] || NPC.npcsFoundForCheckActive[EvilList[14]] || NPC.npcsFoundForCheckActive[EvilList[15]]
                  || NPC.npcsFoundForCheckActive[EvilList[16]] || NPC.npcsFoundForCheckActive[EvilList[17]] || NPC.npcsFoundForCheckActive[EvilList[18]] || NPC.npcsFoundForCheckActive[EvilList[19]]
                   || NPC.npcsFoundForCheckActive[EvilList[20]] || NPC.npcsFoundForCheckActive[EvilList[21]] || NPC.npcsFoundForCheckActive[EvilList[22]] || NPC.npcsFoundForCheckActive[EvilList[23]]
                    || NPC.npcsFoundForCheckActive[EvilList[24]] || NPC.npcsFoundForCheckActive[EvilList[25]] || NPC.npcsFoundForCheckActive[EvilList[26]] || NPC.npcsFoundForCheckActive[EvilList[27]]
                     || NPC.npcsFoundForCheckActive[EvilList[28]] || NPC.npcsFoundForCheckActive[EvilList[29]] || NPC.npcsFoundForCheckActive[EvilList[30]] || NPC.npcsFoundForCheckActive[EvilList[31]]
                      || NPC.npcsFoundForCheckActive[EvilList[32]] || NPC.npcsFoundForCheckActive[EvilList[33]] || NPC.npcsFoundForCheckActive[EvilList[34]] || NPC.npcsFoundForCheckActive[EvilList[35]]
                       || NPC.npcsFoundForCheckActive[EvilList[36]] || NPC.npcsFoundForCheckActive[EvilList[37]] || NPC.npcsFoundForCheckActive[EvilList[38]] || NPC.npcsFoundForCheckActive[EvilList[39]]
                        || NPC.npcsFoundForCheckActive[EvilList[40]] || NPC.npcsFoundForCheckActive[EvilList[41]] && !TestSets.Evil[npc.type])
            { 
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
        }
        }
        private void Step2OfCheckNPCs(NPC npc)
        {


            // I give up. I tried for so, so long to make code that was clean and wasn't an ugly block of ineffectiveness, but now I see that was never a real possibility. I have been humbled. 
            if (NPC.npcsFoundForCheckActive[EvilList[0]] || NPC.npcsFoundForCheckActive[EvilList[1]] || NPC.npcsFoundForCheckActive[EvilList[2]] || NPC.npcsFoundForCheckActive[EvilList[3]] ||
                NPC.npcsFoundForCheckActive[EvilList[4]] || NPC.npcsFoundForCheckActive[EvilList[5]] || NPC.npcsFoundForCheckActive[EvilList[6]] || NPC.npcsFoundForCheckActive[EvilList[7]]
                 || NPC.npcsFoundForCheckActive[EvilList[8]] || NPC.npcsFoundForCheckActive[EvilList[9]] || NPC.npcsFoundForCheckActive[EvilList[10]] || NPC.npcsFoundForCheckActive[EvilList[11]]
                  || NPC.npcsFoundForCheckActive[EvilList[12]] || NPC.npcsFoundForCheckActive[EvilList[13]] || NPC.npcsFoundForCheckActive[EvilList[14]] || NPC.npcsFoundForCheckActive[EvilList[15]]
                   || NPC.npcsFoundForCheckActive[EvilList[16]] || NPC.npcsFoundForCheckActive[EvilList[17]] || NPC.npcsFoundForCheckActive[EvilList[18]] || NPC.npcsFoundForCheckActive[EvilList[19]]
                    || NPC.npcsFoundForCheckActive[EvilList[20]] || NPC.npcsFoundForCheckActive[EvilList[21]] || NPC.npcsFoundForCheckActive[EvilList[22]] || NPC.npcsFoundForCheckActive[EvilList[23]]
                     || NPC.npcsFoundForCheckActive[EvilList[24]] || NPC.npcsFoundForCheckActive[EvilList[25]] || NPC.npcsFoundForCheckActive[EvilList[26]] || NPC.npcsFoundForCheckActive[EvilList[27]]
                      || NPC.npcsFoundForCheckActive[EvilList[28]] || NPC.npcsFoundForCheckActive[EvilList[29]] || NPC.npcsFoundForCheckActive[EvilList[30]] || NPC.npcsFoundForCheckActive[EvilList[31]]
                       || NPC.npcsFoundForCheckActive[EvilList[32]] || NPC.npcsFoundForCheckActive[EvilList[33]] || NPC.npcsFoundForCheckActive[EvilList[34]] || NPC.npcsFoundForCheckActive[EvilList[35]]
                        || NPC.npcsFoundForCheckActive[EvilList[36]] || NPC.npcsFoundForCheckActive[EvilList[37]] || NPC.npcsFoundForCheckActive[EvilList[38]] || NPC.npcsFoundForCheckActive[EvilList[39]]
                         || NPC.npcsFoundForCheckActive[EvilList[40]] || NPC.npcsFoundForCheckActive[EvilList[41]] && !TestSets.Evil[npc.type])
            {
                GetHurtByOtherNPCs(npc, TestSets.Evil);
            }








        }
        private void CheckOtherNPCs(NPC npc)
        {
           
        }
        public override void AI(NPC npc)
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
                                            
                                                    Step2OfCheckNPCs(npc);
                                                
                                            

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
           // CheckOtherNPCs(npc);


            // if (Main.netMode != 1/* && npc.type != 37 && (npc.friendly || NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[npc.type]))
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
           
            //   if (Main.netMode != 1 && (NPC.npcsFoundForCheckActive[210] || NPC.npcsFoundForCheckActive[211]) && !Hallowed[npc.type])
            //     {
            //         GetHurtByOtherNPCs(npc, Evil);
            //     }


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
        //    if (Main.netMode != 1 && (NPC.npcsFoundForCheckActive[210] || NPC.npcsFoundForCheckActive[211]) && !SetsSystem.HurtingBeesTest[npc.type])
       //     {
       //         GetHurtByOtherNPCs(npc, SetsSystem.HurtingBeesTest);
      //      }


            // Since the method is private in source code, just going to recreate it here by porting code
            // GetHurtByOtherNPCs( NPCID.Sets.AllNPCs);

            if (npc.dontTakeDamage || npc.dontTakeDamageFromHostiles || npc.immortal)
            {
                return;
            }
            




         

        }
    */
    }
}
        



