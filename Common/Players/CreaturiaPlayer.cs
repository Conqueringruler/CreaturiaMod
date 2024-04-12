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
using Creaturia.NPCs.Town;
using Creaturia.Items;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework.Graphics;
using Creaturia.Buffs;

namespace Creaturia.Common.Players
{
    public class CreaturiaPlayer : ModPlayer
    {
        static public bool playeatinganimation = false;
        //public static CreaturiaPlayer CreaturiaPlayerInstance => ModContent.GetInstance<CreaturiaPlayer>();
        public override void PreUpdate()
        {
            Main.runningCollectorsEdition = true;

        }
        // public override void UpdateEquips()
        // {
        //     base.UpdateEquips();
        // }
        public bool RabbitFootAcc;
        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            if (vendor.type == ModContent.NPCType<Fishman>())
            {
                //
                SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("Creaturia/Assets/Sounds/FishmanGulp").WithVolumeScale(0.8f), vendor.Center);
                playeatinganimation = true;
            }
            // base.PostBuyItem(vendor, shopInventory, item);
        }
        public override void ResetEffects()
        {
            RabbitFootAcc = false; // Always need to do apparently
        }


        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == NPCType<TheHellborneSkull>())
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was incinerated");
            }
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == NPCType<SkinWalker>())
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was torn to shreds");
            }
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == NPCID.GreenSlime && Main.hardMode)
            {
                if (Main.LocalPlayer.name == "Ripple" || Player.name == "Ripplio" || Player.name == "Jesus" || Player.name == "Rio")
                {
                    damageSource = PlayerDeathReason.ByCustomReason("Rio just died to a green slime in hardmode. Embarrassing. ");
                }
                //damageSource = PlayerDeathReason.ByCustomReason("Rio just died to a green slime in hardmode. Embarrassing. ");
            }
            if (damageSource.SourceNPCIndex >= 0 && (Main.npc[damageSource.SourceNPCIndex].type == NPCID.GreenSlime) && Main.hardMode && (Main.LocalPlayer.name is "Marlilo" or "Merlm" or "Marlon" or "1.4 Alpha Tmodloader"))
            {
                damageSource = PlayerDeathReason.ByCustomReason("Marlon just died to a green slime in hardmode. Embarrassing. ");
            }
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == NPCID.GreenSlime && Main.hardMode && (Main.LocalPlayer.name is "Kadoons" or "Kirk" or "Kirg"))
            {
                damageSource = PlayerDeathReason.ByCustomReason("Kirk just died to a green slime in hardmode. Embarrassing. ");
            }
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == NPCID.GreenSlime && Main.hardMode && (Main.LocalPlayer.name is "Oceanosity" or "Ron Weasel" or "Andrew"))
            {
                damageSource = PlayerDeathReason.ByCustomReason("Andrew just died to a green slime in hardmode. Embarrassing.");
            }
            if (Main.rand.NextBool(2))
            {
                if (Main.player[Main.myPlayer].HasBuff<SlipperyBuff>()) // Might want to use this instead of localPlayer, still need to see if it works in multiplayer though
                {
                    //if (Main.rand.NextBool(2))
                    //{
                    // damage = 0;
                    // crit = false;
                    // cooldownCounter = 600;
                    // playSound = false;
                    // customDamage = false;
                    // Player.shadowDodge = true;
                    if (Player.whoAmI == Main.myPlayer)
                    {

                        RabbitDodge();
                        SoundEngine.PlaySound(SoundID.GlommerBounce, Player.position);
                        return false;

                    }
                }
            }
            if (Main.rand.NextBool(100))
            {
                if (RabbitFootAcc) // Might want to use this instead of localPlayer, still need to see if it works in multiplayer though
                {
                    if (Player.whoAmI == Main.myPlayer)
                    {

                        SlipperyDodge();
                        SoundEngine.PlaySound(SoundID.Shatter, Player.position);
                        return false;

                    }
                    }
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }
        
        void SlipperyDodge()
        {
            
            if (Player.whoAmI == Main.myPlayer)
            {
                NetMessage.SendData(MessageID.Dodge, -1, -1, null, Player.whoAmI, 1f); // I'm gonna be honest, I have no idea what this does. It's what the vanilla dodges do though so I'll go with it
                                                                                       // Edit: changed "62" to MessageID.Dodge so now I know what's going on
                
              
            }

            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80); // incase I take a break and forget, the ? value : value returns the first or second value depending on if it's true or false.
                                                                            // Main.player[Main.myPlayer].AddBuff(BuffID.ShadowDodge, 100);
            for (int i = 0; i < 100; i++)
            {
                // All this below is the dust and gore from the Ninja dodge.

                int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num].position.X += Main.rand.Next(-20, 21);
                Main.dust[num].position.Y += Main.rand.Next(-20, 21);
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].noGravity = true;
                }
            }


            // }
        }
        void RabbitDodge()
        {

            if (Player.whoAmI == Main.myPlayer)
            {
                NetMessage.SendData(62, -1, -1, null, Player.whoAmI, 1f); // I'm gonna be honest, I have no idea what this does. It's what the vanilla dodges do though so I'll go with it



            }

            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80); // incase I take a break and forget, the ? value : value returns the first or second value depending on if it's true or false.
                                                                            // Main.player[Main.myPlayer].AddBuff(BuffID.ShadowDodge, 100);
            for (int i = 0; i < 100; i++)
            {
                // All this below is the dust and gore from the Ninja dodge.

                int num = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num].position.X += Main.rand.Next(-20, 21);
                Main.dust[num].position.Y += Main.rand.Next(-20, 21);
                Main.dust[num].velocity *= 0.4f;
                Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(Player.cWaist, Player);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale *= 1f + (float)Main.rand.Next(40) * 0.01f;
                    Main.dust[num].noGravity = true;
                }
            }


            // }
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (Main.player[Main.myPlayer].HasBuff<SlipperyBuff>()) // Might want to use this instead of localPlayer, still need to see if it works in multiplayer though
            {
                //if (Main.rand.NextBool(2))
                //{
                // damage = 0;
                // crit = false;
                // cooldownCounter = 600;
                // playSound = false;
                // customDamage = false;
                // Player.shadowDodge = true;
               
            }
        }

        string npcLifeText = "Spectral Watchman: 0/0";
        int genRandomNumber;
        
        public override void PostUpdate()
        {
            genRandomNumber = Main.rand.Next(1, 10);
            if (genRandomNumber == 1)
            {
                npcLifeText = "Spectral Watchman: 0/0";
            }
            if (genRandomNumber == 2)
            {
                npcLifeText = "Spectral Watchman: 99999/99999";
            }
            if (genRandomNumber == 3)
            {
                npcLifeText = "Spectral Watchman: ???";
            }
            if (genRandomNumber == 4)
            {
                npcLifeText = "Spectral Watchman: " + $"[i:{ItemID.FirstFractal}" + "] ";
            }
            if (genRandomNumber == 5)
            {
                npcLifeText = "Spectral Watchman: " + $"[i:{ModContent.NPCType<SpectralWatchman>()}" + "] "; // if this doesn't work or is giant then I'll change to an item
            }
            if (genRandomNumber == 6)
            {
                npcLifeText = "Spectral Watchman: -1/-1";
            }
            if (genRandomNumber == 7)
            {
                npcLifeText = "Spectral Watchman: ∞";
            }
            if (genRandomNumber == 8)
            {
                npcLifeText = "Spectral Watchman: X/X";
            }
            if (genRandomNumber == 9)
            {
                npcLifeText = "Spectral Watchman: 8008135/8008135";
            }
            // Dungeon frog
            for (int i = 0; i < Main.npc.Length; i++) // I cannot remember what Main.npc.Length does lol
            {
               NPC npc = Main.npc[i];
               if (npc.active && npc.type == ModContent.NPCType<DungeonFrog>() && npc.Hitbox.Contains(Main.MouseWorld.ToPoint())/* && !Player.dead*/)
               {
                   Player.cursorItemIconEnabled = true;
                   Player.cursorItemIconID = ItemID.Worm;
                   Player.cursorItemIconText = "";
                
                if (Main.mouseRight && Main.npcChatRelease)
                {
                    if (Player.HasItem(ItemID.Worm))
                    {

                        Main.npcChatRelease = false;
                        if (PlayerInput.UsingGamepad)
                        {
                                
                            Player.releaseInventory = false;
                        }
                        if (Player.talkNPC != i && !Player.tileInteractionHappened)
                        {
                            Player.QuickSpawnItem(npc.GetSource_Loot(), ItemID.Ectoplasm, Main.rand.Next(2, 5));
                            // Item.NewItem(npc.GetSource_Loot(), new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), ItemID.Ectoplasm, Main.rand.Next(2, 5));
                            SoundEngine.PlaySound(SoundID.Item104);
                                string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<DungeonFrog>()];
                                Main.BestiaryTracker.Kills.SetKillCountDirectly(persistentId, 50); // I wonder if kills will work for a critter?
                                npc.Transform(NPCType<DungeonFrogEmpty>());
                            Player.ConsumeItem(ItemID.Worm);

                            for (int j = 0; j < 12; j++)
                            {
                                Dust.NewDustDirect(npc.position + new Vector2(Main.rand.Next(-15, 15)), npc.width, npc.height, DustID.Clentaminator_Blue, npc.velocity.X + Main.rand.Next(-6, 6), npc.velocity.Y + Main.rand.Next(-6, 6));
                                    Dust.NewDustDirect(npc.position + new Vector2(Main.rand.Next(-15, 15)), npc.width, npc.height, DustID.TintableDustLighted, npc.velocity.X + Main.rand.Next(-6, 6), npc.velocity.Y + Main.rand.Next(-6, 6));
                                }
                        }
                    }
                }
            }
                if (npc.active && npc.type == ModContent.NPCType<SpectralWatchman>() && npc.Hitbox.Contains(Main.MouseWorld.ToPoint())/* && !Player.dead*/)
                {
                    
                        
                        Player.cursorItemIconEnabled = true;
                        Player.cursorItemIconID = ModContent.ItemType<BlankItem>();
                        Player.cursorItemIconText = npcLifeText;

                }
            }
        }
    }
}






