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
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.Utilities;
using System.IO;
using Ionic.Zlib;

namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class SpectralWatchman : ModNPC
    {
       // public override string Texture
      //  {

        //    get { return "Terraria/Images/NPC_" + NPCID.SkeletonMerchant; }
       //  }
        private bool pulledup = true;


        // Idea for Spectral Watchman: track how many trades are made for low, medium, and high value value materials, and if enough are gotten then have him give a gift
        // ex.) rod of discord
        
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 0; // I want him to not run from danger.
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 15;
            NPCID.Sets.AttackAverageChance[NPC.type] = 8;
            // NPCID.Sets.HatOffsetY[NPC.type] = 4;
            // DisplayName.SetDefault("Spectral Mirrorman");
           // NPCID.Sets.SpawnsWithCustomName[Type] = false; // So it chooses a name like a townnpc since it isnt actually one. I want to try this with a hostile NPC and see what happens
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;

        }
        // After done debugging remove velocity to the left
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 16;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 52;
            NPC.defense = 30;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 12f;
            AnimationType = NPCID.SkeletonMerchant;
            NPC.dontTakeDamageFromHostiles = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            
            NPC.nameOver = 0f;
           // NPC.ShowNameOnHover = false; // It's such a shame I can't use the fake HP I designed since this prevents trading. It was gonna be so cool :(
            

        }

        string lifeTextString = "0";
        int SwitchLifeTextTimer;
        int ChooseRandomLife;
        int NPCCount;
        int NPCwidth;
        int NPCheight;
        NPC guideNPC;
        bool DespawnBecauseDuplicate = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(DespawnBecauseDuplicate);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            DespawnBecauseDuplicate = reader.ReadBoolean();
        }
        public override void AI()
        {
            
            ChooseRandomLife = Main.rand.Next(1, 6);
            if (ChooseRandomLife == 1)
            {
                NPC.lifeMax = 999999;
                NPC.life = NPC.lifeMax;
            }
            if (ChooseRandomLife == 2)
            {
                NPC.lifeMax = 8008135;
                NPC.life = NPC.lifeMax;
            }
            if (ChooseRandomLife == 3)
            {
                NPC.lifeMax = 1;
                NPC.life = NPC.lifeMax;
            }
            if (ChooseRandomLife == 4)
            {
                //NPC.c
                NPC.lifeMax = 100;
                NPC.life = NPC.lifeMax;
            }
            if (ChooseRandomLife == 5)
            {
                
                NPC.lifeMax = 1000;
                NPC.life = NPC.lifeMax;
            }
            SwitchLifeTextTimer++;
            /*
            if (NPC.Hitbox.Contains(Main.MouseWorld.ToPoint()))
                {
                PopupText.NewText(new AdvancedPopupRequest()
                {
                    Text = lifeTextString,
                    Color = Color.White,
                    DurationInFrames = 10,
                    Velocity = Vector2.Zero

                }, NPC.position + new Vector2(0, -15));
                } */

            if (NPC.CountNPCS(ModContent.NPCType<SpectralWatchman>()) > 1)
            {
                if (Main.rand.NextBool(5))
                {
                    
                    
                     
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        
                        NPC.netUpdate = true;
                        DespawnBecauseDuplicate = true;
                    }
                }
                if (DespawnBecauseDuplicate)
                {
                    Dust dust;
                    for (int i = 0; i < 10; i++)
                    {
                        dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Electric, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 0, new Color(255, 0, 133), 1f);
                        dust.noGravity = true; dust.shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
                        dust.fadeIn = 1.0116279f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }

            //CoolEffectTimer++;
            if (pulledup == true)
            {
                for (int i = 0; i < 15; i++) // why do I have pulledup for Spectral Watchman
                {
                    int randchange = Main.rand.Next(-6, 6);
                    int dust = Dust.NewDust(NPC.Center, 0, 0, DustID.Smoke, NPC.velocity.X + randchange, NPC.velocity.Y + randchange, 10, Color.WhiteSmoke, 1);
                    dust = Dust.NewDust(NPC.Center, 0, 0, DustID.Smoke, NPC.velocity.X - randchange, NPC.velocity.Y - randchange, 10, Color.WhiteSmoke, 1);
                }
               

                pulledup = false;
            }
            NPC.dontTakeDamageFromHostiles = true;
        }
        public override bool CanChat() // gotta add since isn't a real town npc
        {
            return true;
        }
        

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Spectral Mirrorman"
            };
        }
     //   int ChatsHad; // scrapping that line, it sucks
        public override string GetChat()
        {
            //  ChatsHad += 1;
            WeightedRandom<string> chat = new WeightedRandom<string>();
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (guide > 0)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.GuideDia", Main.npc[guide].GivenName));
            }
            if (wizard > 0)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.WizardDia", Main.npc[wizard].GivenName));
            }
            if (Main.moonPhase == 5)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.MoonDia"));
            }
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue1")); // So glad ExampleMod has localization tutorials, would have never figured this out
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue4"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue5"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue6"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.SpectralMirrorman.StandardDialogue7"));

            string chosenChat = chat;

            return chosenChat;
        }

       
        

        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
           // button = Language.GetTextValue("LegacyInterface.28");
            button = "Enhance";
            button2 = "Gaze into mirror...";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                Main.playerInventory = true;
                // remove the chat window...
                Main.npcChatText = "...";
                // and start an instance of our UIState.
                ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface.SetState(new UI.SpectralWatchmanUI()); // idk if Spectral Watchman UI was supposed to be inside VanillaItemSlotWrapper
                                                                                                                                                // But I don't really care
                                                                                                                                                // Note that even though we remove the chat window, Main.LocalPlayer.talkNPC will still be set correctly and we are still technically chatting with the npc.


            }
            else
            {
                Main.LocalPlayer.TeleportationPotion();
                Main.LocalPlayer.Heal(400);
                Main.LocalPlayer.HealEffect(400, true);
            }

           
        
        }
        




        

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 4f;

        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 90;
            randExtraCooldown = 12;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!Main.hardMode)
            {
                projType = ProjectileID.MagicDagger;
                attackDelay = 22;
            }
            else
                projType = ProjectileID.FairyQueenMagicItemShot;
            attackDelay = 20;

        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 2f;
            randomOffset = 2f;
            gravityCorrection = -1f;
        } 
        int CoolEffectTimer;

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {



            if (Main.hardMode && Main.LocalPlayer.ZoneHallow && !NPC.AnyNPCs(ModContent.NPCType<SpectralWatchman>()))
            {
                return SpawnCondition.Cavern.Chance * 0.005f;
            }
            else return SpawnCondition.Underground.Chance * 0f;
            
        }
        bool RandomSpriteCalc = true;
        int SpriteSelected;
        Texture2D texture;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            CoolEffectTimer++;
            if (RandomSpriteCalc)
            {
                SpriteSelected = Main.rand.Next(1, 5);
                RandomSpriteCalc = false;
            }
            if (SpriteSelected <= 3)
            {
                 texture = TextureAssets.Npc[NPC.type].Value;
            }
            else if (SpriteSelected == 4)
            {
                 texture = ModContent.Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchmanAlt").Value;
            }
            else
            {
                 texture = TextureAssets.Npc[NPC.type].Value;
            }
                
                Player target = Main.player[NPC.target];
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

                /*spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
                NPC.frame, drawColor, NPC.rotation,
                new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */

                //  spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchman_glowmask").Value, NPC.Center - screenPos,
                //    NPC.frame, Color.White, NPC.rotation,
                //   new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);


                // ^^^^ Want to add a glowmask cause that would be awesome
                spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, new Color(226, 45, 61, 1) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.51f, TextureAssets.Npc[NPC.type].Value.Height * 0.023f), NPC.scale, spriteEffects, 0f);
                
                if (CoolEffectTimer > 60)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 ExtremeRotateEffect = new Vector2(5, 3) + new Vector2((20 * MathF.Cos(Main.GlobalTimeWrappedHourly * 100) + i), (7 * MathF.Sin(Main.GlobalTimeWrappedHourly * 100) + i)); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
                        spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + ExtremeRotateEffect, NPC.frame, new Color(226, 45, 121, 50) * (0.5f + 0.5f * ((250 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.51f, TextureAssets.Npc[NPC.type].Value.Height * 0.023f), NPC.scale, spriteEffects, 0f);
                   
                    }
                    //Vector2 position = Main.LocalPlayer.Center; damn I always forget how easy it is to spawn shit at the player
                    if (Main.rand.NextBool(2))
                    {
                        Dust dust;
                        dust = Terraria.Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Electric, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 0, new Color(255, 0, 133), 0.8f);
                        dust.noGravity = true; dust.shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
                        dust.fadeIn = 1.0116279f;
                    }

                }
                if (CoolEffectTimer > 70)
                {
                    CoolEffectTimer = 0;
                }
                for (int i = 0; i < 1; i++)
                { // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
                    Vector2 RotateEffect = new Vector2(2, 1).RotatedBy((5 * MathF.Sin(Main.GlobalTimeWrappedHourly)) + i); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
                    spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + RotateEffect, NPC.frame, new Color(226, 45, 61, 1) * (0.5f + 0.5f * ((250 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.51f, TextureAssets.Npc[NPC.type].Value.Height * 0.023f), NPC.scale, spriteEffects, 0f);
                }
                for (int i = 0; i < 1; i++)
                { // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
                    Vector2 RotateEffect = new Vector2(2, 1).RotatedBy((-5 * MathF.Sin(Main.GlobalTimeWrappedHourly)) + i); // piover2 makes it centered in middle of sprite, increasing makes it move forward.
                    spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + RotateEffect, NPC.frame, new Color(226, 45, 61, 1) * (0.5f + 0.5f * ((250 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.51f, TextureAssets.Npc[NPC.type].Value.Height * 0.023f), NPC.scale, spriteEffects, 0f);
                }



                return false;
            
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
                
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundHallow,
                //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                //BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                
				// Sets your NPC's flavor text in the bestiary.
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.Mirrorman")

              //  new FlavorTextBestiaryInfoElement("The enigmatic Mirrorman, now freed from his containment within the world's guardian, traverses the Underground Hallow for a reason unknown... and also provides better services than the Goblin Tinkerer."),

            });
            
        }
        /*
        public class WatchmanProfile : ITownNPCProfile
        {
            public int RollVariation() => 0;
            public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

            public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
            {
                if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchman");

                if (npc.altTexture == 1)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchman");

                return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/SpectralWatchman");
            }

            public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Creaturia/NPCs/Town/SpectralWatchman"); // Need head texture
        } */

    }

}
