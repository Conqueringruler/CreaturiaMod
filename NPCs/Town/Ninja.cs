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
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria.GameContent.Events;

using Creaturia.Items;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles;
using Microsoft.Xna.Framework;

namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class Ninja : ModNPC
    {
        public override string Texture
        {
            get { return "Creaturia/NPCs/Town/Ninja"; }
        }



        


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 20;
            NPCID.Sets.AttackAverageChance[NPC.type] = 8;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Hate)

                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate);


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                //Direction = -1

            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 12;
            NPC.defense = 17;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 12f;
            AnimationType = NPCID.Guide;
        }
        public override bool CanGoToStatue(bool toKingStatue) => true;
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++) // I wish I knew what the fuck this does
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

               
                    if (NPC.downedSlimeKing)
                    {
                        return true;
                    }
                
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Daisuke",
                "Hayato",
                "Takehiko",
                "Hihona",
                "Miyamoto",
                "Isayama",
                "Iwata",
                "Yamauchi",
                "Kaneda",
                "Yamauchi",
                "Takuya Yamashiro"
            };
        }
        
        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            if (angler >= 0 && Main.rand.NextBool(6))
            {
                return "Keep " + Main.npc[angler].GivenName + " away from me. Where I come from kids like that would have been straightened out. ";
            }
            if (Main.LocalPlayer.HasItem(ItemID.Tabi) && Main.rand.NextBool(3))
            {
                return "I'd be happy to turn that Tabi into a Black Belt. What most people don't know is that they're actually the same thing, just folded differently.";
            }
                if (Main.LocalPlayer.HasItem(ItemID.BlackBelt) && Main.rand.NextBool(3))
                {
                    return "I'd be happy to turn that Black Belt into a Tabi. What most people don't know is that they're actually the same thing, just folded differently.";
                }
                if (BirthdayParty.PartyIsUp && Main.rand.NextBool(3))
            {
                return "What? You expect me to take off my helmet for the party? Not happening.";
            }
               if (player.ZoneHallow && Main.rand.NextBool(4))
            {
                return "How am I supposed to be stealthy in a place where everything glows and has magical powers?";
            }
            if (player.ZoneSnow && Main.rand.NextBool(4))
            {
                return "The snow here reminds me of a prefecture I lived in for a while. It's very soothing. ";
            }
            switch (Main.rand.Next(8))
            {
                case 0:
                    return "Thank you from freeing me from that beast. I'll ignore that you stole some of my extra clothes.";
                case 1:
                    return "I have all the shurikens and- well now all the gel you'll ever need.";
                case 2:
                    return "That angler kid is a little brat, back in my day we'd be beaten with katanas for treating superiors like that.";
                case 3:
                    return "一部のねえ、私はねえ、私はあなたが私たちが金属のたわごとを鉱山20サドルに金属化するのが好きではありません。 You didn't understand a word I said, did you?.";
                case 4:
                    return "It's nice to see another determined warrior out here, even though I've fallen far. Sigh.";
                case 5:
                    return "...Queen Slime... ahaha hey there";
                case 6:
                    return "I hate slimes.";
                case 7:
                    return "You can see I don't sell all the products my homeland produces, so if there's something you need, it may often arrive from a merchant - a traveling merchant.";
                default:
                    return "I hate slimes.";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");

            if (Main.LocalPlayer.HasItem(ItemID.Tabi))
            {
                button2 = ("Exchange " + Lang.GetItemNameValue(ItemID.Tabi) + " For " + Lang.GetItemNameValue(ItemID.BlackBelt));
            }
            else if (Main.LocalPlayer.HasItem(ItemID.BlackBelt))
            {
                button2 = ("Exchange " + Lang.GetItemNameValue(ItemID.BlackBelt) + " For " + Lang.GetItemNameValue(ItemID.Tabi));
            }

            //   button2 = "Custom";

        }
        Item BlackBeltItemItem = new Item(ItemID.BlackBelt, 1, 0);
        Item TabiItemItem = new Item(ItemID.Tabi, 1, 0);
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                if (Main.LocalPlayer.HasItem(ItemID.Tabi))
                {
                    int BlackBeltItem = ItemID.BlackBelt;
                    int TabiItem = Main.LocalPlayer.FindItem(ItemID.Tabi);
                    int TabiItemReforge = Main.item[TabiItem].prefix;
                    
                    SoundEngine.PlaySound(SoundID.Coins);
                    Main.LocalPlayer.inventory[TabiItem].TurnToAir();
                    Main.LocalPlayer.QuickSpawnClonedItem(NPC.GetSource_GiftOrReward(), BlackBeltItemItem, 1);



                }
                else if (Main.LocalPlayer.HasItem(ItemID.BlackBelt))
                {

                    int TabiItem = ItemID.Tabi;
                    int BlackBeltItem = Main.LocalPlayer.FindItem(ItemID.BlackBelt);
                    int BlackBeltItemReforge = Main.item[BlackBeltItem].prefix;

                    SoundEngine.PlaySound(SoundID.Coins);
                    
                    Main.LocalPlayer.inventory[BlackBeltItem].TurnToAir();
                    
                    
                    Main.LocalPlayer.QuickSpawnClonedItem(NPC.GetSource_GiftOrReward(), TabiItemItem, 1);
                   
                    
                    
                }
            }
        }

        

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.RedDynastyShingles);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Sake);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Shuriken);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaHood);
            shop.item[nextSlot].shopCustomPrice = 8000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaShirt);
            shop.item[nextSlot].shopCustomPrice = 9200;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaPants);
            shop.item[nextSlot].shopCustomPrice = 7800;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Katana);
            nextSlot++;
            if (Main.slimeRain)
            {
                shop.item[nextSlot].SetDefaults(ItemID.Gel);
                nextSlot++;
            }    
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.TigerClimbingGear);
                shop.item[nextSlot].shopCustomPrice = 19000;
                nextSlot++;
            }
            


            if (NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleSword>());
                nextSlot++;
                shop.item[nextSlot].shopCustomPrice = 180;
                nextSlot++;
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<ExampleSword>());
                nextSlot++;
                shop.item[nextSlot].shopCustomPrice = 90;
                nextSlot++;
            }
        
            if (Main.hardMode)
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
            }
       
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
            }

            if (NPC.life <= 0)
            {
                Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Top, NPC.velocity * hitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaHeadGore").Type, NPC.scale);
                for (int i = 0; i <= 2; i++)
                {
                    Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Bottom, NPC.velocity * hitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaFootGore").Type, NPC.scale);
                    Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * hitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaArmGore").Type, NPC.scale);
                }
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
                }
            }
        }


        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 4f;

        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 82;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<NinjasShuriken>();
            attackDelay = 2;

        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 9f;
            randomOffset = 2f;
            gravityCorrection = -10f;
        }

        

        //fuck you spawn conditions 
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {

            int score = 0;
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {

                    if (NPC.downedSlimeKing)
                    {
                        score++;
                    }
                }
            }
            return score >= 1;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("The Ninja, now freed from captivity, has opted to join in on ADD COOL DESCRIPTION."),

            });
        }
        public class ExamplePersonProfile : ITownNPCProfile
        {
            public int RollVariation() => 0;
            public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

            public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
            {
                if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/Ninja");

                if (npc.altTexture == 1)
                    return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/NinjaNPC_Party");

                return ModContent.Request<Texture2D>("Creaturia/NPCs/Town/NinjaNPC");
            }

            public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("Creaturia/NPCs/Town/NinjaNPC_Head");
        }
    }

}
