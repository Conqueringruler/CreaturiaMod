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

namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class DuckMaiden : ModNPC
    {
        public override string Texture
        {
            
            get { return "Creaturia/NPCs/Town/Fishman"; }
        } 
        private bool pulledup = true;




        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[NPC.type] = 15;
            NPCID.Sets.AttackAverageChance[NPC.type] = 8;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPCID.Sets.SpawnsWithCustomName[Type] = true; // So it chooses a name like a townnpc since it isnt actually one
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            database.Entries.Remove(bestiaryEntry);
        }
        public override bool CanGoToStatue(bool toQueenStatue) => true;
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 22;
            NPC.damage = 22;
            NPC.defense = 17;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 12f;
            AnimationType = NPCID.Guide;

        }
        
        
        public override void AI()
        {

            if (pulledup == true)
            {
                for (int i = 0; i < 15; i++)
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
                "Duck Maiden",
            };
        }

        public override string GetChat()
        {
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            int pirate = NPC.FindFirstNPC(NPCID.Pirate);
            if (angler >= 0 && Main.rand.NextBool(7))
            {
                return ".";
            }
            if (pirate >= 0 && Main.rand.NextBool(7))
            {
                return "n.";
            }
            if (Main.moonPhase == 5 && Main.rand.NextBool(4))
            {
                return ".";
            }
            switch (Main.rand.Next(6))
            {
                case 0:
                    return ". ";
                case 1:
                    return ".";
                case 2:
                    return ".";
                case 3:
                    return ".";
                case 4:
                    return ".";
                case 5:
                    return ".";
                case 6:
                    return ".";
                default:
                    return ".";
            }
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
                shop = true;
            }
            else
            {
                //   Main.npcChatText = "oppa gangam style";
            }
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {

            shop.item[nextSlot].SetDefaults(ItemID.Duck);
            shop.item[nextSlot].shopCustomPrice = 1;
            shop.item[nextSlot].shopSpecialCurrency = Creaturia.BassId;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Duck);
            shop.item[nextSlot].shopCustomPrice = 2;
            shop.item[nextSlot].shopSpecialCurrency = Creaturia.PrismiteId;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Duck);
            shop.item[nextSlot].shopCustomPrice = 10;
            shop.item[nextSlot].shopSpecialCurrency = Creaturia.FrostMinnowId;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Duck);
            shop.item[nextSlot].shopCustomPrice = 10;
            shop.item[nextSlot].shopSpecialCurrency = Creaturia.VariegatedLardfishId;
            nextSlot++;
            if (Main.moonPhase == 5)
            {

                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                shop.item[nextSlot].shopCustomPrice = 5;
                shop.item[nextSlot].shopSpecialCurrency = Creaturia.GoldenCarpId;
                nextSlot++;
            }
            if (Main.moonPhase > 6)
            {

                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                shop.item[nextSlot].shopCustomPrice = 20;
                shop.item[nextSlot].shopSpecialCurrency = Creaturia.FlarefinKoiId;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                shop.item[nextSlot].shopCustomPrice = 40;
                shop.item[nextSlot].shopSpecialCurrency = Creaturia.VariegatedLardfishId;
                nextSlot++;
            }
            if (Main.hardMode)
            {

                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                shop.item[nextSlot].shopCustomPrice = 1;
                shop.item[nextSlot].shopSpecialCurrency = Creaturia.FlarefinKoiId;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Duck);
                shop.item[nextSlot].shopCustomPrice = 40;
                shop.item[nextSlot].shopSpecialCurrency = Creaturia.VariegatedLardfishId;
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
            cooldown = 90;
            randExtraCooldown = 12;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!Main.hardMode)
            {
                projType = ProjectileID.Swordfish;
                attackDelay = 22;
            }
            else
                projType = ProjectileID.TitaniumTrident;
            attackDelay = 20;

        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 2f;
            randomOffset = 2f;
            gravityCorrection = -1f;
        }
        public class ExamplePersonProfile : ITownNPCProfile
        {
            public int RollVariation() => 0;
            public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

            public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
            { // public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;
                
                if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn && npc.lifeMax == 340)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman");
                
                if (npc.altTexture == 1)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman_Party");

                return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Fishman");
            }

            public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Creaturia/NPCs/Town/Fishman_Head");
        }
    }

}
