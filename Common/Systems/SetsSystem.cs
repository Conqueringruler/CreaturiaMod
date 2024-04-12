using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Events;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.IO;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.NPCs.Creatures;
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Creaturia.NPCs.Enemies;
using System.IO;
using Creaturia;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using Creaturia.NPCs.Enemies.Boss.FishBosses;

namespace Creaturia.Common.Systems
{

    public class SetsSystem : ModSystem
    {
        public override void PostSetupContent()
        {
         //   TestSets.InitializeEvil();
        }
        /**  public static bool[] Evil { get; set; } = new bool[0];
          public static bool[] Hallowed { get; set; } = new bool[1];
          public static bool[] Fiery { get; set; } = new bool[2];

          public override void PostSetupContent()
          {
              Evil  = NPCID.Sets.Factory.CreateBoolSet(NPCID.CorruptBunny, NPCID.Corruptor, NPCID.EaterofSouls, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail, NPCID.DevourerBody
                     , NPCID.CorruptSlime, NPCID.Slimer, NPCID.Slimeling, NPCID.DarkMummy, NPCID.CorruptGoldfish, NPCID.Crimslime, NPCID.CrimsonBunny, NPCID.CrimsonGoldfish, NPCID.Crimera
                      , NPCID.BigCrimera, NPCID.CrimsonAxe, NPCID.BigCrimslime, NPCID.Herpling, NPCID.Creeper, NPCID.BrainofCthulhu, NPCID.BloodMummy, NPCID.BloodJelly
                       , NPCID.BloodFeeder, NPCID.BloodCrawler, NPCID.BloodCrawlerWall, NPCID.FaceMonster, NPCID.FloatyGross, NPCID.IchorSticker, NPCID.BloodCrawlerWall
                        , NPCID.DesertGhoulCrimson, NPCID.DesertGhoulCorruption, NPCID.CursedHammer, NPCID.SeekerBody, NPCID.Clinger, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.ServantofCthulhu,
              NPCID.EyeofCthulhu, NPCID.SkeletronHand, NPCID.SkeletronHead);

              Hallowed = NPCID.Sets.Factory.CreateBoolSet(NPCID.Unicorn,
             NPCID.Pixie,
             NPCID.SandsharkHallow,
             NPCID.HallowBoss,
             NPCID.QueenSlimeBoss,
             NPCID.QueenSlimeMinionBlue,
                                            NPCID.QueenSlimeMinionPink,
                                            NPCID.Gastropod, NPCID.LightMummy,
                                            NPCID.RainbowSlime,
                                            NPCID.FlyingFish,
                                        ModContent.NPCType<RainbowFish>(),
                                        NPCID.EmpressButterfly,
                                        NPCID.DesertGhoulHallow,
                                        NPCID.PigronHallow,
                                        NPCID.BigMimicHallow,
                                        NPCID.EnchantedSword,
                                        NPCID.IlluminantSlime,
                                        NPCID.IlluminantBat,
                                        NPCID.ChaosElemental);

               Fiery = NPCID.Sets.Factory.CreateBoolSet(
         NPCID.FireImp,
         NPCID.LavaSlime,
         NPCID.Hellbat,
         NPCID.Demon,
         NPCID.VoodooDemon,
         NPCID.Lavabat,
         NPCID.RedDevil);

      } */

    } 
    } 

