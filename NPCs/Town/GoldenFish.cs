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
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Creative;
using System.IO;
using Terraria.ModLoader.IO;
using Creaturia;
using NVorbis.Contracts;
using System.Net.Sockets;
using static Humanizer.In;


namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class GoldenFish : ModNPC
    {
        //public override string Texture => "Terraria/Images/NPC_" + NPCID.FlyingFish;

        private bool pulledup = true;


        public bool ChoosedWishes = false;

        public int RichesWish; 
                               
                               
        public int FishesWish;
        public int WishesWish;

        public int SoulsWish = 0;
        public int WarWish;
        public int OresWish;

        private bool FourthButton1 = false;

        private bool EvilWishes = false;
        public int EvilCalculator;

        private bool firstbuttonchosen;


        public bool WishGranted = false;
        private int Poof;
        public int PunishmentChooser = 1;
        private bool TurnRed = false;

        public bool Button1IsRiches = false;
        public bool Button1IsFishes = false;
        public bool Button1IsWishes = false;
        public bool Button1IsDishes = false;

        public bool Button2IsWar = false;
        public bool Button2IsSouls = false;
        public bool Button2IsOres = false;

        public bool TryDoWish = false; // Need to do this for networking

        public bool TwoButtonsPacket = false;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            //    NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            //   NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            //   NPCID.Sets.DangerDetectRange[NPC.type] = 70;
            //     NPCID.Sets.AttackType[NPC.type] = 5;
            //     NPCID.Sets.AttackTime[NPC.type] = 15;
            //     NPCID.Sets.AttackAverageChance[NPC.type] = 2;
            //     NPCID.Sets.HatOffsetY[NPC.type] = 4;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;
            //    NPCID.Sets.SpawnsWithCustomName[Type] = true; // So it chooses a name like a townnpc since it isnt actually one
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
            // DisplayName.SetDefault("Mysterious Golden Fish");
            
            //NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value); // for whatever reason, I can
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.WishingFish")

            });

            ContentSamples.NpcBestiaryRarityStars[ModContent.NPCType<GoldenFish>()] = 4;
        }
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        { // Influences NPC in Bestiary
            PortraitPositionXOverride = -10f, //15f
            PortraitPositionYOverride = 0, // 8f
            Velocity = 1f,
            Scale = 0.95f
           // SpriteDirection = -1,
           // Direction = -1

        };
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 70;
            NPC.height = 44;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 2f;
            AnimationType = NPCID.FlyingFish;
            NPC.color = Color.Gold;
            NPC.noGravity = true;
            NPC.dontTakeDamageFromHostiles = true;
            
            


        }
        int DustTimer;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Player target = Main.LocalPlayer;
            SpriteEffects spriteEffects = SpriteEffects.FlipVertically;
            int bluecolor = 245;
            int greencolor = 80;
            if (NPC.position.X > target.position.X)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.None;
            }

            if (NPC.position.X < target.position.X)
            {
                //NPC.spriteDirection = 1;
                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

                //SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            }
            if (TurnRed == true)
            {
                drawColor = Color.DarkRed;
                bluecolor = 0;
                greencolor = 0;
            }
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
            NPC.frame, drawColor, NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.105f), NPC.scale, spriteEffects, 0f);

            //        spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Town/GoldenFish").Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
            //        NPC.frame, new Color(255, bluecolor, greencolor, 0) * (0.5f + 0.5f * ((10 - NPC.alpha) / 255f)), NPC.rotation,
            //      new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.105f), NPC.scale, spriteEffects, 0f);
            /*spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
			NPC.frame, drawColor, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */

            // spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Misc/GravPlant_glowmask").Value, NPC.Center - screenPos,
            //  NPC.frame, Color.White, NPC.rotation,
            //   new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);



            // for (int i = 0; i < 4; i++)
            // { // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
            //  Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
            // spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(186, 85, 211, 0) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale + (Math.Abs(NPC.velocity.Y / 10f)), spriteEffects, 0f);
            // }





            return false;
        }
        bool SentPacketYet = false;
        int waitasec;
        public override void AI()
        {
            /*  if (Main.netMode == NetmodeID.Server)
              {
                  if (waitasec <= 3)
                  {
                      waitasec++;
                  }
                  if (waitasec >= 3 && SentPacketYet == false)
                  {
                      SentPacketYet = true;
                       ModPacket packet = Mod.GetPacket(); // use this instead of other
                      packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
                      packet.Write((byte)NPC.whoAmI); // NPC identity
                      packet.Write((bool)ChoosedWishes); // WishesChosen
                      packet.Write((bool)WishGranted); // Wish Granted
                      packet.Write((byte)EvilCalculator); // Evil Calc
                      packet.Write((byte)PunishmentChooser); // Punshment Chosen
                      packet.Write((byte)RichesWish);
                      packet.Write((byte)WishesWish);
                      packet.Write((byte)FishesWish);
                      packet.Write((byte)OresWish);
                      packet.Write((byte)SoulsWish);
                      packet.Write((byte)WarWish);
                      packet.Write((bool)TryDoWish);
                      packet.Send();
                  }
              } */
            // ^^^ the point of this is to sync the buttons, idk if it will work though. Edit: Except I don't need to sync it here
            if (TryDoWish)
            {
                
                DoWish(!secondButton, secondButton); // I can do the opposite of secondButton, this is actually genius 
                TryDoWish = false;
            }

            if (pulledup == true)
            {
                NPC.velocity.Y -= 15;
                pulledup = false;
            }
            NPC.dontTakeDamageFromHostiles = true;
            NPC.velocity *= 0.8f;
            DustTimer++;
            if (DustTimer > 15 && EvilCalculator != 1)
            {
                Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.GoldFlame, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0));
                dust.noGravity = true;
                dust.fadeIn = 0.9f;
                DustTimer = 0;
            }
            if (DustTimer > 15 && EvilCalculator == 1 && WishGranted)
            {
                Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.RedTorch, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0));
                dust.noGravity = true;
                dust.fadeIn = 0.9f;
                DustTimer = 0;
            }

            // For anyone reading further, I warn you now to stop while your mind is still intact. This was written in a time before knowledge of 'switch{}' existed, when all that was had was 'else if{}' statements.
            // If you ignore my warning and proceed further, I am in no way responsible for what wretched state it may leave you in.



            if (ChoosedWishes == false)
            {
               // if (Main.netMode != NetmodeID.MultiplayerClient)
              //  {


                    RichesWish = Main.rand.Next(2);
                    FishesWish = Main.rand.Next(2);
                    WishesWish = Main.rand.Next(2);
                    FourthButton1 = Main.rand.NextBool(5); // I can do one more wish
                    if (Main.hardMode)
                    {
                        SoulsWish = Main.rand.Next(2);
                    }

                    WarWish = Main.rand.Next(2);
                if (Main.hardMode)
                {
                    SoulsWish = Main.rand.Next(2);
                }
                    OresWish = Main.rand.Next(2);
                    WishesWish = Main.rand.Next(2);
                    PunishmentChooser = Main.rand.Next(1, 5);
                    EvilCalculator = Main.rand.Next(8);
             //   }
                
                // PunishmentChooser
                if (PunishmentChooser != 0) // int defaults to value of 0, so when this is not 0 is when values above happened. Might not even need this check but idc, sue me
                {
                    if (Main.netMode == NetmodeID.Server)
                    {
                        SendPacketNowThatValuesAreSet();
                    }
                    ChoosedWishes = true;
                }
                
            }


            if (WishGranted == true)
            {

                if (SentPacketYet == false)
                {

                }
                Poof++;
                if (Poof > 180)
                {
                    if (EvilCalculator != 1)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-3, 3)), NPC.width, NPC.height, DustID.Cloud, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, Color.Gold);
                        }
                    }



                    if (EvilCalculator == 1)
                    {
                        if (Main.GraveyardVisualIntensity < 0.8f)
                        {
                            Main.GraveyardVisualIntensity += 0.2f;
                        }


                        for (int i = 0; i < 16; i++)
                        {
                            Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-3, 3)), NPC.width, NPC.height, DustID.Cloud, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, Color.Red);
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {

                            if (Main.bloodMoon)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X, (int)NPC.Right.Y, NPCID.BloodNautilus, 0, NPC.whoAmI);
                            }
                            if (PunishmentChooser == 1)
                            {
                                int projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0, -140), NPC.velocity, ProjectileID.Boulder, 130, 0);

                                for (int i = 0; i < 5; i++)
                                {
                                    projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0 + (i * 5), -140 + (i * -5)), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                }
                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(10, -180), NPC.velocity, ProjectileID.BeeHive, 130, 0);
                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-10, -180), NPC.velocity, ProjectileID.BeeHive, 130, 0);

                                // High up boulders
                                for (int i = 0; i < 5; i++)
                                {
                                    projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(20 + (i * -40), -540), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                }
                            }
                            if (PunishmentChooser == 2)
                            {
                                /*  for (int i = 0; i < 10; i++) // My current theory on why this isn't working is that the for statement needs more time to complete rather than just 1 frame or whatever
                                  {
                                      NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X, (int)NPC.Right.Y, NPCID.CaveBat, 0, NPC.whoAmI);
                                  }
                                  for (int i = 0; i < 3; i++)
                                  {
                                      NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-10, 10), (int)NPC.Right.Y + Main.rand.Next(-10, 10), NPCID.GiantBat, 0, NPC.whoAmI);
                                      NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-10, 10), (int)NPC.Right.Y + Main.rand.Next(-10, 10), NPCID.JungleBat, 0, NPC.whoAmI);
                                      NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-10, 10), (int)NPC.Right.Y + Main.rand.Next(-10, 10), NPCID.IceBat, 0, NPC.whoAmI);
                                      NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-10, 10), (int)NPC.Right.Y + Main.rand.Next(-10, 10), NPCID.SporeBat, 0, NPC.whoAmI);
                                  } */
                                for (int i = 0; i < 10; i++)
                                {
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-100, 100), (int)NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.CaveBat, 0, NPC.whoAmI);
                                }
                                for (int i = 0; i < 3; i++)
                                {

                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-100, 100), (int)NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.GiantBat, 0, NPC.whoAmI);
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-100, 100), (int)NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.JungleBat, 0, NPC.whoAmI);
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-100, 100), (int)NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.IceBat, 0, NPC.whoAmI);
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X + Main.rand.Next(-100, 100), (int)NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.SporeBat, 0, NPC.whoAmI);
                                }



                            }
                            if (PunishmentChooser == 3)
                            {
                                if (Main.bloodMoon == false && Main.dayTime == false)
                                {
                                    Main.bloodMoon = true;
                                }
                                else
                                {
                                    if (Main.netMode != NetmodeID.MultiplayerClient)
                                    {


                                        int projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0, -140), NPC.velocity, ProjectileID.Boulder, 130, 0);

                                        for (int i = 0; i < 5; i++)
                                        {
                                            projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0 + (i * 5), -140 + (i * -5)), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        }
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(10, -180), NPC.velocity, ProjectileID.BeeHive, 130, 0);
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-10, -180), NPC.velocity, ProjectileID.BeeHive, 130, 0);

                                        // High up boulders
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(20, -540), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-20, -540), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(60, -540), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-60, -540), NPC.velocity, ProjectileID.Boulder, 130, 0);
                                    }
                                }
                            }
                            if (PunishmentChooser == 4)
                            {
                                Main.LocalPlayer.statLife = 1;

                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y - 190, NPCID.KingSlime, 0, NPC.whoAmI);
                                Main.LocalPlayer.AddBuff(BuffID.Cursed, 800);
                            }



                        }
                       
                    }
                    NPC.active = false;
                    NPC.netUpdate = true;
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.GoldFlame, NPC.velocity.X + Main.rand.Next(-5, 5), NPC.velocity.Y + Main.rand.Next(-5, 5));
                        dust.noGravity = true;
                    }

                }
            }
        }
        public void SendPacketNowThatValuesAreSet()
        {
            ModPacket packet = Mod.GetPacket(); // use this instead of other
            packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
            packet.Write((Int32)NPC.whoAmI); // NPC identity
            packet.Write((bool)ChoosedWishes); // WishesChosen
            packet.Write((bool)WishGranted); // Wish Granted
            packet.Write((Int32)EvilCalculator); // Evil Calc
            packet.Write((Int32)PunishmentChooser); // Punshment Chosen
            packet.Write((Int32)RichesWish);
            packet.Write((Int32)WishesWish);
            packet.Write((Int32)FishesWish);
            packet.Write((Int32)OresWish);
            packet.Write((Int32)SoulsWish);
            packet.Write((Int32)WarWish);
            packet.Write((bool)TryDoWish);
            packet.Send();
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

        /*    public override List<string> SetNPCNameList()
            {
                return new List<string>() {
                    "Finley",
                    "Marlin",
                    "Anchove",
                    "Flipper",
                    "Flotsom",
                    "Walleye",
                    "Shellton",
                    "Scaly Pete",
                    "Triton",
                    "Fishstick",
                    "Buckets",
                    "Filbert",
                    "Gilbert",
                    "Gillie",
                    "Gillian",
                }; */
        // } 

        public override string GetChat()
        {
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            int pirate = NPC.FindFirstNPC(NPCID.Pirate);
            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.WishDia"));

           
            string chosenChat = chat;

            return chosenChat;
        }
        /*  public void Send(int toWho, int fromWho)
          {
              ModPacket packet = Mod.GetPacket(); // use this instead of other
              if (Main.netMode == NetmodeID.Server)
              {
                  packet.Write(fromWho);
              }
              packet.Write((bool)ChoosedWishes); // WishesChosen
              packet.Write((bool)WishGranted); // Wish Granted
              packet.Write(EvilCalculator); // Evil Calc
              packet.Write(PunishmentChooser); // Punshment Chosen
              packet.Send(toWho, fromWho);
          }

          public void Receive(BinaryReader reader, int fromWho)
          {
              if (Main.netMode == NetmodeID.MultiplayerClient)
              {
                  fromWho = reader.ReadInt32();
              }
              ChoosedWishes = reader.ReadBoolean();
              WishGranted = reader.ReadBoolean();
              EvilCalculator = reader.ReadInt32();
              PunishmentChooser = reader.ReadInt32();
              if (Main.netMode == NetmodeID.Server)
              {
                  Send(-1, fromWho);
              }

          } */ // You know what, I'm just gonna use SendExtraAI cause it really doesn't matter that much tbh

        public bool ButtonsChosenDontChangePleasePlease = false;

       
        public override void SetChatButtons(ref string button, ref string button2)
        {


            // This will all be rewritten from the ground up in an upcoming update. Please don't read ahead, this is the worst thing I've made, and is clearly a product of younger, inexperienced me. 


            if (FourthButton1 != true)
            {
               // if (ButtonsChosenDontChangePleasePlease == false)
               // {


                    if (RichesWish == 1 && WishesWish != 1 && FishesWish != 1) // O X X
                    {
                        button = Language.GetTextValue("Wish for Riches");
                        Button1IsRiches = true;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                        Button1IsFishes = false;
                    }
                    if (RichesWish != 1 && WishesWish == 1 && FishesWish != 1) // X O X
                    {
                        button = Language.GetTextValue("Wish for Wishes");
                        Button1IsWishes = true;

                        Button1IsRiches = false;
                        Button1IsDishes = false;
                        Button1IsFishes = false;
                    }
                    if (RichesWish != 1 && WishesWish != 1 && FishesWish == 1) // X X O
                    {
                        button = Language.GetTextValue("Wish for Fishes");
                        Button1IsFishes = true;
                        Button1IsRiches = false;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                    }
                    if (RichesWish == 1 && WishesWish != 1 && FishesWish == 1) // O X O
                    {
                        button = Language.GetTextValue("Wish for Riches");
                        Button1IsRiches = true;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                        Button1IsFishes = false;
                    }
                    if (RichesWish == 1 && WishesWish == 1 && FishesWish != 1) // O O X
                    {
                        button = Language.GetTextValue("Wish for Wishes");
                        Button1IsWishes = true;
                        Button1IsRiches = false;
                        Button1IsDishes = false;
                        Button1IsFishes = false;
                    }
                    if (RichesWish != 1 && WishesWish == 1 && FishesWish == 1) // X O O
                    {
                        button = Language.GetTextValue("Wish for Fishes");
                        Button1IsFishes = true;
                        Button1IsRiches = false;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                    }
                    if (RichesWish != 1 && WishesWish != 1 && FishesWish != 1) // X X X
                    {
                        button = Language.GetTextValue("Wish for Riches");
                        Button1IsRiches = true;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                        Button1IsFishes = false;
                    }
                    if (RichesWish == 1 && WishesWish == 1 && FishesWish == 1) // O O O
                    {
                        button = Language.GetTextValue("Wish for Fishes");
                        Button1IsFishes = true;
                        Button1IsRiches = false;
                        Button1IsDishes = false;
                        Button1IsWishes = false;
                    }// I think that's every combo
               // }
            }

            if (FourthButton1 == true)
            {
                button = Language.GetTextValue("Wish for Dishes");
                Button1IsDishes = true;
                Button1IsRiches = false;
                Button1IsWishes = false;
                Button1IsFishes = false;
            }









            //if (FourthButton2 != true)
            //{
            //if (ButtonsChosenDontChangePleasePlease == false)
           // {


                if (SoulsWish == 1 && WarWish != 1 && OresWish != 1) // O X X
                {

                    button2 = Language.GetTextValue("Wish for Souls");
                    Button2IsSouls = true;

                    Button2IsOres = false;
                    Button2IsWar = false;
                }
                if (SoulsWish != 1 && WarWish == 1 && OresWish != 1) // X O X
                {
                    button2 = Language.GetTextValue("Wish for War");
                    Button2IsWar = true;

                    Button2IsOres = false;
                    Button2IsSouls = false;
                }
                if (SoulsWish != 1 && WarWish != 1 && OresWish == 1) // X X O
                {
                    button2 = Language.GetTextValue("Wish for Ores");
                    Button2IsOres = true;
                    Button2IsSouls = false;
                    Button2IsWar = false;
                }
                if (SoulsWish == 1 && WarWish != 1 && OresWish == 1) // O X O
                {
                    button2 = Language.GetTextValue("Wish for Souls");
                    Button2IsSouls = true;
                    Button2IsOres = false;
                    Button2IsWar = false;
                }
                if (SoulsWish == 1 && WarWish == 1 && OresWish != 1) // O O X
                {
                    button2 = Language.GetTextValue("Wish for War");
                    Button2IsWar = true;
                    Button2IsOres = false;
                    Button2IsSouls = false;
                }
                if (SoulsWish != 1 && WarWish == 1 && OresWish == 1) // X O O
                {
                    button2 = Language.GetTextValue("Wish for Ores");
                    Button2IsOres = true;
                    Button2IsSouls = false;
                    Button2IsWar = false;
                }
                if (SoulsWish != 1 && WarWish != 1 && OresWish != 1) // X X X
                {
                    button2 = Language.GetTextValue("Wish for Souls");
                    Button2IsSouls = true;
                    Button2IsOres = false;
                    Button2IsWar = false;
                }
                if (SoulsWish == 1 && WarWish == 1 && OresWish == 1) // O O O
                {
                    button2 = Language.GetTextValue("Wish for Ores");
                    Button2IsOres = true;
                    Button2IsSouls = false;
                    Button2IsWar = false;
                }
                ButtonsChosenDontChangePleasePlease = true;
           // }
            // I think that's every combo
             //}

            //    if (FourthButton1 == true)
            //     {
            //         button = Language.GetTextValue("Wish for Dishes");
            //         Button1IsDishes = true;
            //     }
            // button = Language.GetTextValue("LegacyInterface.28");
            //   button2 = "Custom";
            if (WishGranted == true)
            {
                button = Language.GetTextValue("Wish Granted!");
                button2 = Language.GetTextValue("Wish Granted!");
            }

        }


        bool secondButton = false;
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (WishGranted == false)
            {
                if (firstButton)
                {
                    //  firstbuttonchosen = true;


                    //if (EvilCalculator != 1)
                    //{
                        //if (Main.netMode != NetmodeID.MultiplayerClient)
                       // {
                            DoWish(firstButton, secondButton);
                        //}
                        TryDoWish = true;

                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                         /*   ModPacket packet = Mod.GetPacket(); // use this instead of other
                            packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
                            packet.Write((Int32)NPC.whoAmI); // NPC identity
                            packet.Write((bool)ChoosedWishes); // WishesChosen
                            packet.Write((bool)WishGranted); // Wish Granted
                            packet.Write((Int32)EvilCalculator); // Evil Calc
                            packet.Write((Int32)PunishmentChooser); // Punshment Chosen
                            packet.Write((Int32)RichesWish);
                            packet.Write((Int32)WishesWish);
                            packet.Write((Int32)FishesWish);
                            packet.Write((Int32)OresWish);
                            packet.Write((Int32)SoulsWish);
                            packet.Write((Int32)WarWish);
                            packet.Write((bool)TryDoWish);
                            packet.Send(); */
                        }



                    //}

                   // WishGranted = true;
                }
                else
                {
                    secondButton = true;
                    firstButton = false;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        DoWish(firstButton, secondButton);
                    }
                    TryDoWish = true;

                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        ModPacket packet = Mod.GetPacket(); // use this instead of other
                        packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
                        packet.Write((Int32)NPC.whoAmI); // NPC identity
                        packet.Write((bool)ChoosedWishes); // WishesChosen
                        packet.Write((bool)WishGranted); // Wish Granted
                        packet.Write((Int32)EvilCalculator); // Evil Calc
                        packet.Write((Int32)PunishmentChooser); // Punshment Chosen
                        packet.Write((Int32)RichesWish);
                        packet.Write((Int32)WishesWish);
                        packet.Write((Int32)FishesWish);
                        packet.Write((Int32)OresWish);
                        packet.Write((Int32)SoulsWish);
                        packet.Write((Int32)WarWish);
                        packet.Write((bool)TryDoWish);
                        packet.Send();
                    }
                }


            }
        }
        public void DoWish(bool firstbutton, bool secondbutton)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                   ModPacket packet = Mod.GetPacket(); // use this instead of other
                   packet.Write((byte)Creaturia.MessageType.WishMsg); // id
                   packet.Write((Int32)NPC.whoAmI); // NPC identity
                   packet.Write((bool)ChoosedWishes); // WishesChosen
                   packet.Write((bool)WishGranted); // Wish Granted
                   packet.Write((Int32)EvilCalculator); // Evil Calc
                   packet.Write((Int32)PunishmentChooser); // Punshment Chosen
                   packet.Write((bool)Button1IsRiches);
                   packet.Write((bool)Button1IsWishes);
                   packet.Write((bool)Button1IsFishes);
                   packet.Write((bool)Button2IsOres);
                   packet.Write((bool)Button2IsSouls);
                   packet.Write((bool)Button2IsWar);
                   packet.Write((bool)TryDoWish);
                   packet.Write((bool)firstbutton);
                   packet.Write((bool)secondbutton);
                   packet.Send();

             /*   ModPacket packet = Mod.GetPacket(); // use this instead of other
                packet.Write((byte)Creaturia.MessageType.WishMsg); // id
                packet.Write((Int32)NPC.whoAmI); // NPC identity
                packet.Write((bool)ChoosedWishes); // WishesChosen
                packet.Write((bool)WishGranted); // Wish Granted
                packet.Write((bool)TryDoWish);
                packet.Write((bool)firstbutton);
                packet.Write((bool)secondbutton);
                packet.Send();  */


            }
            if (WishGranted == false)
            {
                if (firstbutton)
                {


                    if (EvilCalculator != 1)
                    {



                        

                            if (Button1IsWishes != true)
                            {
                                Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.WishGrantedDia");
                            }

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (Button1IsDishes != true)
                                {

                                    if (Button1IsFishes == true)
                                {
                                    if (Main.rand.NextBool(3) && Main.hardMode)
                                    {

                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.ChaosFish, Main.rand.Next(0, 3));

                                    }
                                    if (Main.rand.NextBool(3))
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Ebonkoi, Main.rand.Next(0, 3));
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Honeyfin, Main.rand.Next(0, 3));
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CrimsonTigerfish, Main.rand.Next(0, 5));
                                    }
                                    if (Main.rand.NextBool(5))
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Fish, 1);
                                    }
                                    if (Main.rand.NextBool(4))
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.BalloonPufferfish, 1);
                                    }
                                    if (Main.rand.NextBool(3) && Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CrystalSerpent, 1);
                                    }
                                    if (Main.rand.NextBool(3) && Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.ObsidianSwordfish, 1);
                                    }
                                    if (Main.rand.NextBool(5) && !Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PurpleClubberfish, 1);
                                    }
                                    if (Main.rand.NextBool(5) && !Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Swordfish, 1);
                                    }
                                    if (Main.rand.NextBool(3) && !Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.ReaverShark, 1);
                                    }
                                    if (Main.rand.NextBool(1))
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.GoldenCarp, 1);
                                    }
                                    //  Main.LocalPlayer.QuickSpawnItem(EntitySource_Loot, ItemID.AtlanticCod); 
                                    if (Main.rand.NextBool(2) && Main.hardMode)
                                    {
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PrincessFish, Main.rand.Next(0, 4));
                                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Prismite, Main.rand.Next(0, 3));
                                    }
                                    Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Tuna, Main.rand.Next(0, 7));
                                    Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.FrostMinnow, Main.rand.Next(0, 3));
                                    Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.DoubleCod, Main.rand.Next(0, 6));
                                    Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.RedSnapper, Main.rand.Next(0, 6));
                                    Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.BombFish, Main.rand.Next(2, 8));

                                }

                                // holy carp louis i frickin love nestled if statements and repeating rand.NextBool!
                                // thats great petah cause we have plenty!





                                if (Button1IsRiches == true)
                                {
                                    int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(0f, -42f), NPC.velocity, ProjectileID.CoinPortal, 0, 0);
                                    projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(0f, -42f), NPC.velocity, ProjectileID.CoinPortal, 0, 0);
                                }
                                //    else
                                //   {

                                //     }
                            }
                            if (Button1IsDishes == true)
                        {
                            Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.WishGrantedDia");
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CookedFish, Main.rand.Next(1, 4));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Escargot, Main.rand.Next(0, 3));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.FroggleBunwich, Main.rand.Next(0, 3));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.FruitSalad, Main.rand.Next(0, 3));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.GrubSoup, Main.rand.Next(0, 3));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SeafoodDinner, Main.rand.Next(1, 3));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PeachSangria, Main.rand.Next(0, 2));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PinaColada, Main.rand.Next(0, 2));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PrismaticPunch, Main.rand.Next(0, 2));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.BananaSplit, Main.rand.Next(0, 2));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.FlaskofGold, 1);
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.RedPotion, Main.rand.Next(0, 2));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.ManaPotion, Main.rand.Next(0, 4));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.HealingPotion, Main.rand.Next(0, 4));
                        }
                            }
                       
                    }
                    if (Button1IsWishes == true || EvilCalculator == 1)
                    {
                        //Main.NewText("Bad Option Chosen!");
                        if (EvilCalculator == 1)
                        {
                            //Main.NewText("Evil Calc = 1!");
                            Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.EvilWish1");
                          
                            
                        }
                        if (Button1IsWishes == true)
                        {
                            //Main.NewText("Wishes Wish Wished!");
                            Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.CheatWish");
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                EvilCalculator = 1;
                            }
                        }
                        //  Main.PlaySound(SoundID.Item8);
                        TurnRed = true;
                        NPC.color = Color.Red;
                    }
                    /* if (EvilCalculator == 1)
                     {
                        // Main.PlaySound(SoundID.Item8);

                         Main.npcChatText = "'You really thought I would grant you a wish? Muahahaha!'";
                         NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Right.X, (int)NPC.Right.Y, NPCID.BloodNautilus, 0, NPC.whoAmI);
                         NPC.color = Color.Red;

                     } */

                }
                WishGranted = true;
            }

            if (secondbutton)
            {




            
                
                    if (EvilCalculator == 1)
                    {
                        if (EvilCalculator == 1)
                        {
                            Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.EvilWish1");
                        NPC.color = Color.Red;
                            TurnRed = true;
                        }

                        //  Main.PlaySound(SoundID.Item8);

                    }

                if (EvilCalculator != 1)
                {
                    Main.npcChatText = Language.GetTextValue("Mods.Creaturia.Dialogue.GoldenWishingFish.WishGrantedDia");
                    Button1IsWishes = false;

                    if (Button2IsSouls == true)
                    {
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofFlight, Main.rand.Next(0, 13));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofNight, Main.rand.Next(0, 13));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofLight, Main.rand.Next(0, 13));
                        if (NPC.downedMechBoss3)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofFright, Main.rand.Next(0, 9));
                        }
                        if (NPC.downedMechBoss2)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofSight, Main.rand.Next(0, 9));
                        }
                        if (NPC.downedMechBoss1)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SoulofMight, Main.rand.Next(0, 9));
                        }
                        //         if (ModLoader.TryGetMod("Consolaria", out Mod Consolaria) && Consolaria.TryFind("SoulofBlight", out ModItem SoulOfBlight))// && (bool)ModLoader.GetMod("Consolaria").Call("DownedBossSystem", "downedOcram"))
                        //         {
                        // This shit ALMOST works, I just need to figure out how to check if Ocram is downed. fuck



                        /*     if (ModLoader.TryGetMod("Consolaria", out Mod Consolaria))
                             {
                                 if (Consolaria.TryFind("SoulofBlight", out ModItem SoulOfBlight))
                                 {
                                     if (Consolaria.TryFind("DownedBossSystem", out ModSystem ConsolariaBossDowned))
                                     {
                                         if (ConsolariaBossDowned.GetType("gjhg"))
                                            // if ((bool)Consolaria.Call("DownedBossSystem", "DownedOcram"))
                                         {
                                             Item.NewItem(NPC.GetSource_Loot(), NPC.Center, SoulOfBlight.Type, Main.rand.Next(1, 9));
                                         }
                                     }
                                 }

                         */
                        // I really want to add support for Souls of Blight from Consolaria mod, but I do NOT want it so badly that I'll go through the process of creating a weakRefence just for getting if Ocram is downed

                    }// && (bool)ModLoader.GetMod("Consolaria").Call("DownedBossSystem", "downedOcram"))
                     //         {
                     //               Item.NewItem(NPC.GetSource_Loot(), NPC.Center, SoulOfBlight.Type, Main.rand.Next(1, 9));
                     //            }

                    //   if (ModLoader.GetMod("Consolaria") != null && NPC.downedMechBossAny)
                    //    {

                    //     }
                    //ModLoader.TryGetMod("Consolaria", out Mod Consolaria);

                    //Mod Consolaria = ModLoader.GetMod("Consolaria");
                    // Consolaria.TryFind("DownedBossSystem", out ModSystem DownedBossSystem);
                    //    Consolaria.TryFind("downedOcram", out bool downedOcram);
                    //    if (Creaturia.ConsolariaLoaded)
                    //   {

                    // if ((Consolaria != null) && (Consolaria.Call("DownedBossSystem", "downedOcram") is true))
                    //  {
                    //        if (DownedBossSystem.boo)
                    //        Consolaria.TryFind("SoulofBlight", out ModItem SoulOfBlight);
                    //         Item.NewItem(NPC.GetSource_Loot(), NPC.Center, SoulOfBlight.Type, Main.rand.Next(5, 9));
                    //      } 

                    //    if (Consolar)
                    //    {
                    //        if (Consolaria.Call("Downed", "downedOcram"))
                    //        {
                    //            Consolaria.TryFind("SoulofBlight", out ModItem SoulOfBlight);
                    //             Item.NewItem(NPC.GetSource_Loot(), NPC.Center, SoulOfBlight.Type, Main.rand.Next(1, 9));
                    //        }
                    //    }


                    //Consolaria.TryFind("DownedBossSystem", out ModSystem DownedBossSystem);
                    // public bool ConsolariaDownedOcram
                    //    {
                    //          get { return Consolaria.DownedBossSystem.downedOcram; }
                    //      }


                    if (Button2IsWar == true)
                    {
                        if (!Main.hardMode)
                        {
                            Main.StartInvasion(InvasionID.GoblinArmy);
                        }

                        if (Main.hardMode && Main.LocalPlayer.statLifeMax > 200 && !NPC.downedPlantBoss)
                        {
                            Main.StartInvasion(InvasionID.PirateInvasion);
                        }
                        if (NPC.downedPlantBoss && Main.hardMode && Main.LocalPlayer.statLifeMax > 200)
                        {
                            Main.StartInvasion(InvasionID.MartianMadness);
                        }
                    }
                    if (Button2IsOres == true)
                    {
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CopperOre, Main.rand.Next(0, 6));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.TinOre, Main.rand.Next(0, 6));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SilverOre, Main.rand.Next(0, 6));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.TungstenOre, Main.rand.Next(0, 6));
                        if (Main.rand.NextBool(2) && !Main.hardMode)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CopperOre, Main.rand.Next(0, 13));
                        }
                        if (Main.rand.NextBool(2) && !Main.hardMode)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.TinOre, Main.rand.Next(0, 13));
                        }
                        if (Main.rand.NextBool(2) && !Main.hardMode)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.SilverOre, Main.rand.Next(0, 11));
                        }
                        if (Main.rand.NextBool(2) && !Main.hardMode)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.TungstenOre, Main.rand.Next(0, 11));
                        }

                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.IronOre, Main.rand.Next(0, 11));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.LeadOre, Main.rand.Next(0, 11));

                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.GoldOre, Main.rand.Next(0, 11));
                        Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PlatinumOre, Main.rand.Next(0, 11));
                        if (NPC.downedBoss1)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CrimtaneOre, Main.rand.Next(0, 13));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.DemoniteOre, Main.rand.Next(0, 13));
                        }

                        if (NPC.downedBoss2)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Meteorite, Main.rand.Next(0, 17));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PlatinumOre, Main.rand.Next(0, 11)); // Want it to give more of lesser tier ores as you progress
                        }
                        if (NPC.downedBoss3 && NPC.downedBoss2)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.Hellstone, Main.rand.Next(6, 21));
                        }
                        if (Main.hardMode)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.CobaltOre, Main.rand.Next(5, 13));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.PalladiumOre, Main.rand.Next(5, 13));
                        }

                        if (NPC.downedMechBossAny || NPC.downedQueenSlime)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.MythrilOre, Main.rand.Next(5, 13));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.AdamantiteOre, Main.rand.Next(5, 13));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.OrichalcumOre, Main.rand.Next(5, 13));
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.TitaniumOre, Main.rand.Next(5, 13));
                        }
                        if (NPC.downedPlantBoss)
                        {
                            Item.NewItem(NPC.GetSource_Loot(), NPC.Center, ItemID.ChlorophyteOre, Main.rand.Next(5, 21));
                        }
                        //  if (ModLoader.GetMod("Consolaria") != null && NPC.downedMechBossAny && )
                        //  {

                        //   }

                    }
                }
            }
        




WishGranted = true;
            }
        public void Send(int toWho, int fromWho)
        {
            ModPacket packet = Mod.GetPacket(); // use this instead of other
            packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
            packet.Write((Int32)NPC.whoAmI); // NPC identity
            packet.Write((bool)ChoosedWishes); // WishesChosen
            packet.Write((bool)WishGranted); // Wish Granted
            packet.Write((Int32)EvilCalculator); // Evil Calc
            packet.Write((Int32)PunishmentChooser); // Punshment Chosen
            packet.Write((Int32)RichesWish);
            packet.Write((Int32)WishesWish);
            packet.Write((Int32)FishesWish);
            packet.Write((Int32)OresWish);
            packet.Write((Int32)SoulsWish);
            packet.Write((Int32)WarWish);
            packet.Write((bool)TryDoWish);
            packet.Send();
        }
            
        }
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







/*     public class ExamplePersonProfile : ITownNPCProfile
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
     } */




