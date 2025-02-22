using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System;
using System.Linq;
using Terraria.Audio;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using Creaturia.Items;
using Creaturia.Items.Consumables.Fishing;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles;
using Creaturia.Items.Consumables;
using Creaturia.Items.Weapon;
using Creaturia.Common.Players;
using Creaturia.Items.Ammo;
using Creaturia.Currencies.FishCurrencies;
using static Terraria.GameContent.Animations.IL_Actions.NPCs;

namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class Fishman : ModNPC
    {
        public override string Texture
        {
            get { return "Creaturia/NPCs/Town/Fishman"; }
        }
        private bool pulledup = true;


       
        int CheapFish1;
        int MediumExpensiveFish1;
        int MediumExpensiveFish2;
        int CostlyFish1;
        int CostlyFish2;



        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 70;
            NPCID.Sets.AttackType[NPC.type] = 5;
            NPCID.Sets.AttackTime[NPC.type] = 15;
            NPCID.Sets.AttackAverageChance[NPC.type] = 0;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;
            NPCID.Sets.SpawnsWithCustomName[Type] = true; // So it chooses a name like a townnpc since it isnt actually one
            NPCID.Sets.ActsLikeTownNPC[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { 
                Velocity = 1f,
                //Direction = -1

            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("The Fishman of the ocean depths prefers solitude, but is happy to trade when the opportunity for non-ocean fish arises.")

            });
        }
        public override bool CanGoToStatue(bool toKingStatue) => true;

        
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 34;
            NPC.height = 45;
            NPC.aiStyle = NPCAIStyleID.Passive; // Note: I cannot believe I never knew about NPCAIStyleID. This makes minor NPCs so much easier
            NPC.damage = 22;
            NPC.defense = 17;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 5f;

        }
        int eatinganimationtimer;
        bool endanimation = false;
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            //endanimation = true;
            //CreaturiaPlayer.playeatinganimation = false;
            if (endanimation == true)
               {
                  CreaturiaPlayer.playeatinganimation = false;
                  endanimation = false;
                }
            if (CreaturiaPlayer.playeatinganimation == true)
            {
               
                AnimationType = NPCID.None;
                NPC.velocity.X = 0f;
                //NPC.velocity.Y = 0f;
                
                if (NPC.frameCounter < 8 && NPC.frameCounter > 0)
                {
             //     ..  NPC.color = Color.Red;
                    NPC.frame.Y = 0 * frameHeight;
                    
                    // endanimation = true;
                }
                if (NPC.frameCounter < 16 && NPC.frameCounter > 8)
                {
               //     NPC.color = Color.Blue;
                    NPC.frame.Y = 20 * frameHeight;
                    if (NPC.spriteDirection == 1)
                    {
                       // NPC.color = Color.Green;
                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDustPerfect(NPC.Center + new Vector2(-10f, -10f), DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-0.8f, 0.8f), Main.rand.NextFloat(-0.6f, 0.6f)), default, Color.DarkRed, 1.5f);
                        }
                    }
                    if (NPC.spriteDirection == -1)
                    {
                     //   NPC.color = Color.Violet;
                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDustPerfect(NPC.Center + new Vector2(-10f, -10f), DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-0.8f, 0.8f), Main.rand.NextFloat(-0.6f, 0.6f)), default, Color.DarkRed, 1.5f);
                        }
                    }
                }
                if (NPC.frameCounter < 24 && NPC.frameCounter > 16)
                {
            //        NPC.color = Color.Green;
                    NPC.frame.Y = 0 * frameHeight;                  // It's probably obvious but I spent way more time than I should have debugging here
                }
                if (NPC.frameCounter < 32 && NPC.frameCounter > 24)
                {
               //     NPC.color = Color.Yellow;
                    NPC.frame.Y = 20 * frameHeight;
                    //  endanimation = true;
                    if (NPC.spriteDirection == 1)
                    {
                        //NPC.color = Color.Green;
                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDustPerfect(NPC.Center + new Vector2(10f, -10f), DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-0.8f, 0.8f), Main.rand.NextFloat(-0.6f, 0.6f)), default, Color.DarkRed, 1.5f);
                        }
                    }
                    if (NPC.spriteDirection == -1)
                    {
                        //NPC.color = Color.Violet;
                        if (Main.rand.NextBool(3))
                        {
                            Dust.NewDustPerfect(NPC.Center + new Vector2(-10f, -10f), DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-0.8f, 0.8f), Main.rand.NextFloat(-0.6f, 0.6f)), default, Color.DarkRed, 1.5f);
                        }
                    }
                }
                if (NPC.frameCounter < 40 && NPC.frameCounter > 32)
                {
                    NPC.frame.Y = 0 * frameHeight;

                    CreaturiaPlayer.playeatinganimation = false; // Doesn't work in pause but idc
                     endanimation = true;
                    AnimationType = NPCID.SkeletonMerchant;
                }
                if (NPC.frameCounter > 41)
                {
                    CreaturiaPlayer.playeatinganimation = false;
                    AnimationType = NPCID.SkeletonMerchant;
                    NPC.frameCounter = 0;
                }
                // if (NPC.frameCounter > 41)
                //  {
                //      NPC.frameCounter = 0;
                //       CreaturiaPlayer.playeatinganimation = false;
                //        NPC.frameCounter = 0;
                //       AnimationType = NPCID.SkeletonMerchant;
            }
          //  if (NPC.frameCounter > 41)
        //    {
        //        CreaturiaPlayer.playeatinganimation = false;
       //         AnimationType = NPCID.SkeletonMerchant;
       //         NPC.frameCounter = 0;
      //      }
            if (CreaturiaPlayer.playeatinganimation == false)
            {
                //NPC.color = default;
                AnimationType = NPCID.SkeletonMerchant;
                //NPC.frameCounter = 0;
                return;
            }
        }



        bool DetPrices = false;

        int PriceDet1;
        int PriceDet2;
        int PriceDet3;

        int OnetoThreePriceDet;
        int OnetoFourPriceDet;



        
        public override void AI()
        {
            NPC.breath += 2;
            if (DetPrices == false)
            {
                PriceDet1 = Main.rand.Next(1, 3); // don't forget that (1, 3) excludes the final value (so it's 1 & 2 as options)
                PriceDet2 = Main.rand.Next(1, 3);
                PriceDet3 = Main.rand.Next(1, 3); // Also the reason I'm doing this is because GetShop() resets every time you open it, so before the prices were changing each time I opened the shop
                OnetoThreePriceDet = Main.rand.Next(1, 4);
                OnetoFourPriceDet = Main.rand.Next(1, 5);
                DetPrices = true;
            }
            if (CreaturiaPlayer.playeatinganimation == true)
            {
                AnimationType = NPCID.None;
            }
            if (CreaturiaPlayer.playeatinganimation == false)
            {
                AnimationType = NPCID.SkeletonMerchant;
            }
            //Main.GraveyardVisualIntensity = 0.9f;
            //Main.ColorOfTheSkies = Color.Yellow;

            if (pulledup == true)
            {
                NPC.velocity.Y -= 10;
                pulledup = false;
            }
            NPC.dontTakeDamageFromHostiles = true;


            /*
                CheapFish1 = Creaturia.BassId;


            if (Main.rand.NextBool(2))
            {
                CostlyFish1 = Creaturia.FlarefinKoiId;
            }
            else
            {
                CostlyFish1 = Creaturia.ChaosFishId;
            }


            if (Main.rand.NextBool(2))
            {
                CostlyFish2 = Creaturia.FlarefinKoiId;
            }
            else
            {
                CostlyFish2 = Creaturia.ChaosFishId;
            }


            if (Main.rand.NextBool(3))
                {
                MediumExpensiveFish1 = Creaturia.FrostMinnowId;
                }
            else if (Main.rand.NextBool(2))
            {
                MediumExpensiveFish1 = Creaturia.VariegatedLardfishId;
            }
            else
            {
                MediumExpensiveFish1 = Creaturia.HoneyFishId;
            }


            if (Main.rand.NextBool(3))
            {
                MediumExpensiveFish2 = Creaturia.FrostMinnowId;
            }
            else if (Main.rand.NextBool(2))
            {
                MediumExpensiveFish2 = Creaturia.VariegatedLardfishId;
            }
            else
            {
                MediumExpensiveFish2 = Creaturia.HoneyFishId;
            }
            */
        }
        public override bool CanChat() // gotta add since isn't a real town npc
        {
            return true;
        }
      /*  public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach (Item item in player.inventory)
                {
                    if (NPC.downedSlimeKing)
                    {
                        return true;
                    }
                }
            }
            return false;
        } */
       
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Finley",
                "Marlin",
                "Anchove",
                "Flipper",
                "Flotsom",
                "Walleye",
                "Shellton",
               // "Scaly Pete",
                "Triton",
                "Fishstick",
                "Buckets",
                "Filbert",
                "Gilbert",
                "Gillie",
                "Gillian",
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            int pirate = NPC.FindFirstNPC(NPCID.Pirate);
            if (angler > 0)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.AnglerDia", Main.npc[angler].GivenName));
            }
            if (pirate > 0)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.PirateDia", Main.npc[pirate].GivenName));
            }
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue1")); // So glad ExampleMod has localization tutorials, would have never figured this out
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue4"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue5"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue6"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Fishman.StandardDialogue7"));

            string chosenChat = chat;

            return chosenChat;
            /*
            if (Main.hardMode && Main.rand.NextBool(4))
            {
                return "Yous seems like the adventurin' type, so maybees you'll be interested in my *Snort* magicals bait. Theys only work in powerful places, so I hear.";
            }
            switch (Main.rand.Next(7))
            {
                case 0:
                    return "I'vve no interest *snort* in your moneys, many a chests o' gold have me. ";
                case 1:
                    return "*Snort* whats havve ye pulled meself up for?";
                case 2:
                    return "Maybees you've interest in me booty? Mine treasure I mean, harharhar.";
                case 3:
                    return "I be a little squiffy, but me wares be fine!";
                case 4:
                    return "I's won't stick around for long, me sea legs havve no care for land.";
                case 5:
                    return "Fresh fish. Easy choose.";
                case 6:
                    return "Summs of those fish from the evil ain't the friendliest, so bees careful fishin' thurr.";
                    
                default:
                    return "*Snort* whats havve ye pulled meself up for?";
            } */
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
         //   button2 = "Custom";

        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
            else
            {
             //   Main.npcChatText = "oppa gangam style";    
            }
        }
        int checkwhichbait;
        public override void AddShops()
        {
            NPCShop npcShop = new NPCShop(Type)
            .Add(new Item(ModContent.ItemType<TridentoftheFishman>())
            {
                shopCustomPrice = 3,
                shopSpecialCurrency = Creaturia.GoldenCarpId

            }, Condition.Hardmode)
             
             .Add(new Item(ModContent.ItemType<ElectricEel>())
             {
                 shopCustomPrice = 1,
                 shopSpecialCurrency = Creaturia.GoldenCarpId

             })
             .Add(new Item(ModContent.ItemType<TroutCannon>())
             {
                 shopCustomPrice = 1,
                 shopSpecialCurrency = Creaturia.GoldenCarpId

             })


              .Add(new Item(ModContent.ItemType<BundleOfFishBullets>())
              {
                  shopCustomPrice = 1,
                  shopSpecialCurrency = Creaturia.BassId

              }, Condition.MoonPhasesHalf0)
              .Add(new Item(ModContent.ItemType<BundleOfFishBullets>())
              {
                  shopCustomPrice = 1,
                  shopSpecialCurrency = Creaturia.BassId

              }, Condition.MoonPhasesHalf1)



               .Add(new Item(ModContent.ItemType<BundleOfCoral>())
               {
                   shopCustomPrice = 5,
                   shopSpecialCurrency = Creaturia.BassId

               }, Condition.MoonPhasesHalf0)
            .Add(new Item(ModContent.ItemType<BundleOfCoral>())
            {
                shopCustomPrice = 5,
                shopSpecialCurrency = Creaturia.BassId

            }, Condition.MoonPhasesHalf1)
                // if (PriceDet1 == 1)
                // {
                .Add(new Item(ItemID.ApprenticeBait)
                {
                    shopCustomPrice = Main.rand.Next(1, 3), // 3 is not included btw
                    shopSpecialCurrency = Creaturia.FrostMinnowId

                }, Condition.MoonPhasesEven)
                // }
                //   else
                //  {
                .Add(new Item(ItemID.ApprenticeBait)
                {
                    shopCustomPrice = Main.rand.Next(1, 3),
                    shopSpecialCurrency = Creaturia.VariegatedLardfishId

                }, Condition.MoonPhasesOdd)
                // }
                //  if (OnetoThreePriceDet == 1)
                //  {
                .Add(new Item(ItemID.JourneymanBait)
                {
                    shopCustomPrice = 1,
                    shopSpecialCurrency = Creaturia.FlarefinKoiId

                }, Condition.MoonPhaseWaningGibbous)
                //  }
                //if (OnetoFourPriceDet == 1)
                //{
                .Add(new Item(ItemID.MasterBait)
                {
                    shopCustomPrice = 3,
                    shopSpecialCurrency = Creaturia.HoneyFishId

                }, Condition.MoonPhaseFull)
            //}
            //if (PriceDet2 == 1)
            //{
                .Add(new Item(ItemID.SonarPotion)
                {
                    shopCustomPrice = 4,
                    shopSpecialCurrency = Creaturia.HoneyFishId

                }, Condition.MoonPhases04)
                //}
                // else
                //{
                .Add(new Item(ItemID.FishingPotion)
                {
                    shopCustomPrice = 3,
                    shopSpecialCurrency = Creaturia.FrostMinnowId

                }, Condition.MoonPhases26)
                //}


                .Add(new Item(ItemID.AnglerEarring)
                {
                    shopCustomPrice = 1,
                    shopSpecialCurrency = Creaturia.GoldenCarpId

                }, Condition.MoonPhasesNearNew)


                .Add(new Item(ItemID.HighTestFishingLine)
                {
                    shopCustomPrice = 10,
                    shopSpecialCurrency = Creaturia.ChaosFishId

                }, Condition.MoonPhaseNew, Condition.Hardmode)

                .Add(new Item(ItemID.HighTestFishingLine)
                {
                    shopCustomPrice = 20,
                    shopSpecialCurrency = Creaturia.VariegatedLardfishId

                }, Condition.MoonPhaseNew)
                 .Add(new Item(ItemID.Trout)
                 {
                     shopCustomPrice = 1,
                     shopSpecialCurrency = Creaturia.BassId

                 }, Condition.MoonPhaseNew)



                    /*   npcShop.Add(new Item(ModContent.ItemType<TridentoftheFishman>())
                       {
                           shopCustomPrice = 15,
                           shopSpecialCurrency = Creaturia.RainbowScaleId

                       }); */

                    // checkwhichbait = Main.rand.Next(1, 5);

                    /* shop.item[nextSlot].SetDefaults(ItemID.PixelBox);
                     shop.item[nextSlot].shopCustomPrice = 2;
                     shop.item[nextSlot].shopSpecialCurrency = Creaturia.BassId;
                     nextSlot++;
                     shop.item[nextSlot].SetDefaults(ItemID.BoringBow);
                     shop.item[nextSlot].shopCustomPrice = 20;
                     shop.item[nextSlot].shopSpecialCurrency = Creaturia.ChaosFishId;
                     nextSlot++; */


                    //  if (checkwhichbait == 1)
                    //  {
                    .Add(new Item(ModContent.ItemType<HallowBait>())
                    {
                        shopCustomPrice = 6,
                        shopSpecialCurrency = MediumExpensiveFish1

                    }, Condition.Hardmode, Condition.MoonPhases37)
                    //  }
                    // else if (checkwhichbait == 2)
                    // {
                    .Add(new Item(ModContent.ItemType<CrimsonBait>())
                    {
                        shopCustomPrice = 6,
                        shopSpecialCurrency = MediumExpensiveFish2

                    }, Condition.Hardmode, Condition.MoonPhasesEvenQuarters)

                    //  }
                    // else if (checkwhichbait == 3)
                    // {
                    .Add(new Item(ModContent.ItemType<CorruptBait>())
                    {
                        shopCustomPrice = 4,
                        shopSpecialCurrency = Creaturia.FlarefinKoiId

                    }, Condition.Hardmode, Condition.MoonPhasesOddQuarters);
               // }
               // else if (checkwhichbait == 4)
               // {
                    
              //  }
            //}
            npcShop.Register();
            /*
             
              
             
             
             
             
               if (Main.hardMode)
               {
                   shop.item[nextSlot].SetDefaults(ModContent.ItemType<TridentoftheFishman>());
                   shop.item[nextSlot].shopCustomPrice = 3;
                   shop.item[nextSlot].shopSpecialCurrency = Creaturia.GoldenCarpId;
                   nextSlot++;
                   shop.item[nextSlot].SetDefaults(ModContent.ItemType<TridentoftheFishman>());
                   shop.item[nextSlot].shopCustomPrice = 15;
                   shop.item[nextSlot].shopSpecialCurrency = Creaturia.RainbowScaleId;
                   nextSlot++;
                   checkwhichbait = Main.rand.Next(1,5);
                   shop.item[nextSlot].SetDefaults(ItemID.PixelBox);
                   shop.item[nextSlot].shopCustomPrice = 2;
                   shop.item[nextSlot].shopSpecialCurrency = Creaturia.BassId;
                   nextSlot++;
                   shop.item[nextSlot].SetDefaults(ItemID.BoringBow);
                   shop.item[nextSlot].shopCustomPrice = 20;
                   shop.item[nextSlot].shopSpecialCurrency = Creaturia.ChaosFishId;
                   nextSlot++;


                   if (checkwhichbait == 1)
                   {
                       shop.item[nextSlot].SetDefaults(ModContent.ItemType<HallowBait>());
                       shop.item[nextSlot].shopCustomPrice = 6;
                       shop.item[nextSlot].shopSpecialCurrency = MediumExpensiveFish1;
                       nextSlot++;
                   }
                   else if (checkwhichbait == 2)
                   {
                       shop.item[nextSlot].SetDefaults(ModContent.ItemType<CrimsonBait>());
                       shop.item[nextSlot].shopCustomPrice = 6;
                       shop.item[nextSlot].shopSpecialCurrency = MediumExpensiveFish2;
                       nextSlot++;
                   }
                   else if (checkwhichbait == 3)
                   {
                       shop.item[nextSlot].SetDefaults(ModContent.ItemType<CorruptBait>());
                       shop.item[nextSlot].shopCustomPrice = 4;
                       shop.item[nextSlot].shopSpecialCurrency = Creaturia.FlarefinKoiId;
                       nextSlot++;
                   }
                   else if (checkwhichbait == 4)
                   {
                       shop.item[nextSlot].SetDefaults(ItemID.None);
                   }
               }
               else if (checkwhichbait == 5)
                   {
                       shop.item[nextSlot].SetDefaults(ItemID.None);
                   }

                   if (Main.moonPhase == 1 && checkwhichbait < 4)
                       {
                           shop.item[nextSlot].shopSpecialCurrency = Creaturia.GoldenCarpId;
                           shop.item[nextSlot].shopCustomPrice = 1;
                       }
                       else if (Main.moonPhase == 2 && checkwhichbait < 4)
                   {
                           shop.item[nextSlot].shopSpecialCurrency = CostlyFish1;
                           shop.item[nextSlot].shopCustomPrice = Main.rand.Next(4, 7);
                       }
                       else if (Main.moonPhase == 3 && checkwhichbait < 4)
                   {
                           shop.item[nextSlot].shopSpecialCurrency = MediumExpensiveFish1;
                           shop.item[nextSlot].shopCustomPrice = Main.rand.Next(4, 7);
                       }
                       else if (Main.moonPhase == 4 && checkwhichbait < 4)
                   {
                           shop.item[nextSlot].shopSpecialCurrency = MediumExpensiveFish2;
                           shop.item[nextSlot].shopCustomPrice = Main.rand.Next(4, 7);
                       }

                       else if (Main.moonPhase == 5 && checkwhichbait < 4)
                   {
                           shop.item[nextSlot].shopSpecialCurrency = CostlyFish2;
                           shop.item[nextSlot].shopCustomPrice = 3;
                       }

                       else if (Main.moonPhase == 6 && checkwhichbait < 4)
                   {
                           shop.item[nextSlot].shopSpecialCurrency = CostlyFish1;
                           shop.item[nextSlot].shopCustomPrice = 3;
                       }
                       else if (checkwhichbait < 4)
                       {
                           shop.item[nextSlot].shopSpecialCurrency = Creaturia.BassId;
                           shop.item[nextSlot].shopCustomPrice = Main.rand.Next(35, 51);
                       }
                   if (checkwhichbait != 4)
                   {
                       nextSlot++;
                   }




               /* if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.GuideVoodooDoll);
                    nextSlot++;
                    if (NPC.downedMechBossAny)
                    {

                        shop.item[nextSlot].SetDefaults(ItemID.MoonCharm);
                        nextSlot++;
                    }
                } 
                if (Main.LocalPlayer.HasBuff(BuffID.Slimed))
                {
                    shop.item[nextSlot].SetDefaults(ItemID.SlimeCrown);
                    shop.item[nextSlot].shopCustomPrice = 200;
                    nextSlot++;
                } */

        }
       


        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 4f;

        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!Main.hardMode)
            {
                projType = ProjectileID.Spear;
                attackDelay = 22;
            }
            else
            projType = ProjectileID.Spear;
            attackDelay = 20;

        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 11f;
            randomOffset = 2f;
           // gravityCorrection = -1f;
        }
        public class ExamplePersonProfile : ITownNPCProfile
        {
            public int RollVariation() => 0;
            public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

            public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
            {
                if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman");

                if (npc.altTexture == 1)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman_Party");

                return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman");
            }

            public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Creaturia/NPCs/Town/Fishman_Head");
        }
    }

}
