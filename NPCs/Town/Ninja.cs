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
using Creaturia.Items.Weapon;
using Newtonsoft.Json.Linq;
using Creaturia.Configs;


namespace Creaturia.NPCs.Town
{
    [AutoloadHead]
    public class Ninja : ModNPC
    {


        public override string Texture
        {
            get { return "Creaturia/NPCs/Town/Ninja"; }
        }





        public bool NinjaEnabled = ModContent.GetInstance<CreaturiaSettings>().Ninja.Contains("Enabled");
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 5;
            NPCID.Sets.DangerDetectRange[NPC.type] = 450;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 20;
            NPCID.Sets.AttackAverageChance[NPC.type] = 7;
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


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
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
            //NPC.damage = 12;
            NPC.defense = 17;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.stepSpeed = 12f;
            AnimationType = NPCID.Guide;
        }
        public override bool CanGoToStatue(bool toKingStatue) => true;
      /*  public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
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
        } */

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Daisuke",
                "Hayato",
                "Takehiko",
                "Hihona",
                "Miyamoto",
                "Isayama",
                "Hiroyuki",
                "Iwata",
                "Yamauchi",
                "Kaneda",
                "Yamauchi",
                "Takuya Yamashiro",
                "Hideki"
            };
        }

        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            int angler = NPC.FindFirstNPC(NPCID.Angler);
            /*   int angler = NPC.FindFirstNPC(NPCID.Angler);
               int merchant = NPC.FindFirstNPC(NPCID.Merchant);
               if (angler > 0 && Main.rand.NextBool(8))
               {
                   return "" + Main.npc[angler].GivenName + " is a disappointment to his lineage. Where I come from kids like that would have been straightened out. ";
               }
               if (merchant > 0 && Main.rand.NextBool(8))
               {
                   return "The greed of " + Main.npc[merchant].GivenName + " disgusts me.";
               } */

            WeightedRandom<string> chat = new WeightedRandom<string>();

            if (Main.LocalPlayer.HasItem(ItemID.Tabi) && Main.rand.NextBool(3))
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.TabiDia"));
            }
            if (Main.LocalPlayer.HasItem(ItemID.BlackBelt) && Main.rand.NextBool(3))
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.BlackBeltDia"));
            }
            if (BirthdayParty.PartyIsUp && Main.rand.NextBool(3))
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.PartyDia"));
            }
            if (Main.rand.NextBool(7) && (angler >= 0))
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.AnglerDia"));
            }
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue1")); // So glad ExampleMod has localization tutorials, would have never figured this out
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue4"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue5"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue6"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue7"));
            chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.StandardDialogue8"));
            if (player.ZoneHallow && Main.rand.NextBool(4))
            {
                if (Main.rand.NextBool(2))
                {
                    chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.HallowDia1"));
                }
                else
                {
                    chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.HallowDia2"));
                }

            }
            if (player.ZoneSnow)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.SnowDia"));
            }
            if (player.active)
            {
                chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.QueenSlimeDia", player.name));
            }
            Point point = NPC.Center.ToTileCoordinates();
            Rectangle value = new Rectangle(point.X, point.Y, 1, 1);
            value.Inflate(25, 25);
            int num = 40;
            Rectangle value2 = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
            value2.Inflate(-num, -num);
            value = Rectangle.Intersect(value, value2);
            int num2 = -1;
            float num3 = -1f;
            for (int i = value.Left; i <= value.Right; i++)
            {
                for (int j = value.Top; j <= value.Bottom; j++)
                {

                    Tile tile = Main.tile[i, j];
                    //if (tile == null || !tile.HasTile || TileID.BlueDynastyShingles != tile.TileType || TileID.RedDynastyShingles != tile.TileType)
                    // {

                    // }

                    if (tile.TileType == TileID.BlueDynastyShingles || tile.TileType == TileID.RedDynastyShingles)
                    {

                        chat.Add(Language.GetTextValue("Mods.Creaturia.Dialogue.Ninja.ShinglesDia"));

                    }
                    // return "The shingles here are truly beautiful. Reminds me of my home. ";
                }

            }

            string chosenChat = chat;

            return chosenChat;

            if (Main.LocalPlayer.HasItem(ItemID.Tabi) && Main.rand.NextBool(3))
            {
                return "I'd be happy to turn that Tabi into a Black Belt. What most people don't know is that they're actually the same thing, just folded differently. The technique to fold them is secret though.";
            }
            if (Main.LocalPlayer.HasItem(ItemID.BlackBelt) && Main.rand.NextBool(3))
            {
                return "I'd be happy to turn that Black Belt into a Tabi. What most people don't know is that they're actually the same thing, just folded differently. The technique to fold them is secret though.";
            }
            if (BirthdayParty.PartyIsUp && Main.rand.NextBool(3))
            {
                return "What? You expect me to take off my helmet for the party? Not happening.";
            }
            if (player.ZoneHallow && Main.rand.NextBool(4))
            {
                if (Main.rand.NextBool(2))
                {
                    return "How am I supposed to be stealthy in a place where everything glows?";
                }
                else
                {
                    return "Please take me out of this place. ";
                }

            }
            if (player.ZoneSnow && Main.rand.NextBool(8))
            {
                return "The snow here reminds me of a prefecture I lived in for a while. It's very soothing. ";
            }
            if (Main.rand.NextBool(6))
            {



            
        /*    Point point = NPC.Center.ToTileCoordinates();
            Rectangle value = new Rectangle(point.X, point.Y, 1, 1);
            value.Inflate(25, 25);
            int num = 40;
            Rectangle value2 = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
            value2.Inflate(-num, -num);
            value = Rectangle.Intersect(value, value2);
            int num2 = -1;
            float num3 = -1f;
            for (int i = value.Left; i <= value.Right; i++) 
            {
                for (int j = value.Top; j <= value.Bottom; j++)
                {

                    Tile tile = Main.tile[i, j];
                    //if (tile == null || !tile.HasTile || TileID.BlueDynastyShingles != tile.TileType || TileID.RedDynastyShingles != tile.TileType)
                    // {

                    // }

                    if (tile.TileType == TileID.BlueDynastyShingles || tile.TileType == TileID.RedDynastyShingles)
                    {

                        return "The shingles here are truly beautiful. Reminds me of my old home. ";

                    }
                    // return "The shingles here are truly beautiful. Reminds me of my home. ";
                }

            } */
        }
            if (Main.rand.NextBool(7) && (angler >= 0))
            {
                return "That angler kid is a little brat, back in my day we'd be beaten with katanas for treating superiors like that.";
            }
            switch (Main.rand.Next(10))
            {
                case 0:
                    return "Thank you from freeing me from that beast. I'll ignore that you stole some of my extra clothes.";
                case 1:
                    return "I have all the shurikens and- well now all the gel you'll ever need.";
                case 2:
                    return "That angler kid is a little brat, back in my day we'd be beaten with katanas for treating superiors like that."; // NOT INCLUDING THIS ONE
                case 3:
                   //  return "一部のねえ、私はねえ、私はあなたが私たちが金属のたわごとを鉱山20サドルに金属化するのが好きではありません。 You didn't understand a word I said, did you?.";
                 return "正表示, 願部私, 金属鉱山。You didn't understand a word I said, did you?";
                case 4:
                    return "It's nice to see another determined warrior out here, even though I've fallen far. Sigh.";
                case 5:
                    return "...Queen Slime... oh hey " + Main.LocalPlayer.name + ", I didn't see you there, haha... ha...";
                case 6:
                    return "I hate slimes.";
                case 7:
                    return "You can see I don't sell all the products my homeland produces, so if there's something you need, it may often arrive from a merchant - perhaps a traveling merchant!";
                case 8:
                    return "Do you know a man named Kuai Liang? Just curious...";
                case 9:
                    return "I used to be big in the 90's... just saying...";
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
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
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
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_GiftOrReward(), BlackBeltItemItem, 1);

                    

                }
                else if (Main.LocalPlayer.HasItem(ItemID.BlackBelt))
                {

                    int TabiItem = ItemID.Tabi;
                    int BlackBeltItem = Main.LocalPlayer.FindItem(ItemID.BlackBelt);
                    int BlackBeltItemReforge = Main.item[BlackBeltItem].prefix;

                    SoundEngine.PlaySound(SoundID.Coins);
                    
                    Main.LocalPlayer.inventory[BlackBeltItem].TurnToAir();
                    
                    
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_GiftOrReward(), TabiItemItem, 1);
                   
                    
                    
                }
            }
        }


        public override void AddShops()
        {
            NPCShop npcShop = new NPCShop(Type)
                    .Add(new Item(ItemID.RedDynastyShingles))
                    .Add(new Item(ItemID.Sake))
                    .Add(new Item(ItemID.Shuriken))
                    .Add(new Item(ItemID.Katana))

                        .Add(new Item(ItemID.NinjaHood)
                        {
                            shopCustomPrice = 80000,
                        })
             .Add(new Item(ItemID.NinjaShirt)
             {
                 shopCustomPrice = 92000,

             })
             .Add(new Item(ItemID.NinjaPants)
             {
                 shopCustomPrice = 78000,
             })
           .Add(new Item(ItemID.TigerClimbingGear)
           {
               shopCustomPrice = 100000,
           }, Condition.Hardmode)

           .Add(new Item(ItemID.MoonCharm), Condition.Hardmode, Condition.DownedMechBossAny, Condition.MoonPhaseFull);

          
            npcShop.Register();
        }
      
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, (NPC.velocity.X + Main.rand.NextFloat(-2f, 2f)), (NPC.velocity.Y + Main.rand.Next(-2, 2)), 0, Color.White, Main.rand.NextFloat(0.8f, 1.1f));
            }

            if (NPC.life <= 0)
            {
                Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Top, NPC.velocity * hit.HitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaHeadGore").Type, NPC.scale);
                for (int i = 0; i <= 2; i++)
                {
                    Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Bottom, NPC.velocity * hit.HitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaFootGore").Type, NPC.scale);
                    Gore.NewGoreDirect(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * hit.HitDirection + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Mod.Find<ModGore>("NinjaArmGore").Type, NPC.scale);
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


        

        //f you spawn conditions 
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {

            int score = 0;
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    if (NinjaEnabled)
                    {
                    if (NPC.downedSlimeKing)
                    {
                        score++;
                    }
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
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.Ninja")

               

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
