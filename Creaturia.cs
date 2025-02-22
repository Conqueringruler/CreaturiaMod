using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using Creaturia;
using Creaturia.Currencies.FishCurrencies;

using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using Creaturia.NPCs.Creatures;
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Creaturia.NPCs.Enemies;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Creaturia.Items.Consumables.Fishing;
using Creaturia.Items;
using Creaturia.Projectiles;
using System.Timers;
using Terraria.Localization;
using Terraria.Chat;




using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;


using Terraria.GameContent.Dyes;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Creaturia.NPCs;
using Creaturia.Common;
using Creaturia.NPCs.Town;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;

namespace Creaturia
{
    public class Creaturia : Mod
    {
        internal static bool ConsolariaLoaded;

        internal static Creaturia instance;
        public static int PrismiteId;
        public static int GoldenCarpId;
        public static int ChaosFishId;
        public static int FlarefinKoiId;
        public static int VariegatedLardfishId;
        public static int HoneyFishId;
        public static int FrostMinnowId;
        public static int BassId;
        public static int TunaId;
        public static int RainbowScaleId;
        internal UserInterface SpectralWatchmanUserInterface;




        public override void Load()
        {




            instance = this;
            ConsolariaLoaded = ModLoader.HasMod("Consolaria");
            PrismiteId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.PrismiteCurrency(ItemID.Prismite, 999L, "Prismite"));
            FlarefinKoiId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.FlarefinKoiCurrency(ItemID.FlarefinKoi, 999L, "Flarefin Koi"));
            HoneyFishId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.HoneyFishCurrency(ItemID.Honeyfin, 999L, "Honey Fin"));
            VariegatedLardfishId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.VariegatedLardfishCurrency(ItemID.VariegatedLardfish, 999L, "Variegated Lardfish"));
            FrostMinnowId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.FrostMinnowCurrency(ItemID.FrostMinnow, 999L, "Frost Minnow"));
            ChaosFishId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.ChaosFishCurrency(ItemID.ChaosFish, 999L, "Chaos Fish"));
            GoldenCarpId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.GoldenCarpCurrency(ItemID.GoldenCarp, 999L, "Golden Carp"));
            BassId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.BassCurrency(ItemID.Bass, 999L, "Bass"));
            TunaId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.TunaCurrency(ItemID.Tuna, 999L, "Tuna"));
            RainbowScaleId = CustomCurrencyManager.RegisterCurrency(new Currencies.FishCurrencies.RainbowScaleCurrency(ModContent.ItemType<RainbowScale2>(), 999L, "Rainbow Scale"));



            SpectralWatchmanUserInterface = new UserInterface();
        }
        public override void Unload()
        {

        }

        //public override void UpdateUI(GameTime gameTime)
        //  {
        // UpdateUI is done in ModSystem, NOT Mod!!!
        // }

        public override void PostSetupContent()
        {

            /*  if (ModLoader.HasMod("BossChecklist"))
			  {
				  ModLoader.TryGetMod(("BossChecklist"), out Mod bossChecklist);
				  if (bossChecklist != null)
				  {																												// Placeholder for corrupt fish
					  bossChecklist.Call("AddEvent", 5.5f, new List<int> { NPCType<CrimsonFishMiniBoss>(), NPCType<RainbowFish>(), NPCType<ExampleFlutterSlime>(),  NPCType<TheGreatTyrannosaurus>() },
						  this, "", (Func<bool>)(() => DownedBossSystem.downedAncient), ItemID.SoulofNight,
						  new List<int> { ItemType<PutridScale>(), ItemType<>(), ItemType<>(), ItemType<>(), ItemType<>(), ItemID. },
						  "Use [i:" + ItemType<HallowBait>() + "]", null, "Creaturia/NPCs/Enemies/Boss/FishBosses/Lumpsucker_Bestiary");
				  }

			  } */
        }

        //	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)      <--- is done in ModSystem
        internal enum MessageType : byte // I wanted to just use numbers like 0, 1, 2, etc, but this is being a bitch so I guess not
        {
            GoldenFishMsg,
            GoldenFishActivateMsg,
            FallenPixieMsg,
            IdkWhyMsg,
            DungeonFrogMsg,
            WishMsg,
            SlugMsg
        }
        bool DoneYet = false;

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            switch (msgType)
            {
                case MessageType.GoldenFishMsg: // GoldenFish Sync
                    if (Main.npc[reader.ReadInt32()].ModNPC is GoldenFish wishingfish)
                    {
                        wishingfish.ChoosedWishes = reader.ReadBoolean();
                        wishingfish.WishGranted = reader.ReadBoolean();
                        wishingfish.EvilCalculator = reader.ReadInt32(); // I hope this isn't too much data
                        wishingfish.PunishmentChooser = reader.ReadInt32();
                        wishingfish.RichesWish = reader.ReadInt32();
                        wishingfish.WishesWish = reader.ReadInt32(); // CHANGE ALL THIS TO MATCH PACKET!!!

                        wishingfish.FishesWish = reader.ReadInt32();

                        wishingfish.OresWish = reader.ReadInt32();

                        wishingfish.SoulsWish = reader.ReadInt32();

                        wishingfish.WarWish = reader.ReadInt32();

                        wishingfish.TryDoWish = reader.ReadBoolean();
                        Main.NewText("Packet Worked!", Color.BlueViolet);



                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket(); // use this instead of other
                            packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
                            packet.Write((Int32)wishingfish.NPC.whoAmI); // NPC identity
                            packet.Write((bool)wishingfish.ChoosedWishes); // WishesChosen
                            packet.Write((bool)wishingfish.WishGranted); // Wish Granted
                            packet.Write((Int32)wishingfish.EvilCalculator); // Evil Calc
                            packet.Write((Int32)wishingfish.PunishmentChooser); // Punshment Chosen
                            packet.Write((Int32)wishingfish.RichesWish);
                            packet.Write((Int32)wishingfish.WishesWish);
                            packet.Write((Int32)wishingfish.FishesWish);                                                 // CHANGE ALL THIS TO MATCH PACKET!
                            packet.Write((Int32)wishingfish.OresWish);
                            packet.Write((Int32)wishingfish.SoulsWish);
                            packet.Write((Int32)wishingfish.WarWish); // this is the worst code I have ever written
                            packet.Write((bool)wishingfish.TryDoWish);
                            packet.Send();

                            /* 
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
                             */


                            wishingfish.NPC.netUpdate = true;

                        }

                    }
                    break;
                case MessageType.GoldenFishActivateMsg: // GoldenFish Sync
                    if (Main.npc[reader.ReadInt32()].ModNPC is GoldenFish wishingfish2)
                    {
                        wishingfish2.ChoosedWishes = reader.ReadBoolean();
                        wishingfish2.WishGranted = reader.ReadBoolean();
                        wishingfish2.TryDoWish = reader.ReadBoolean();
                        bool firstbutton = reader.ReadBoolean();
                        bool secondbutton = reader.ReadBoolean();
                        Main.NewText("Packet Worked!", Color.BlueViolet);



                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket(); // use this instead of other
                            packet.Write((byte)Creaturia.MessageType.GoldenFishMsg); // id
                            packet.Write((Int32)wishingfish2.NPC.whoAmI); // NPC identity
                            packet.Write((bool)wishingfish2.ChoosedWishes); // WishesChosen
                            packet.Write((bool)wishingfish2.WishGranted); // Wish Granted
                            packet.Write((Int32)wishingfish2.EvilCalculator); // Evil Calc
                            packet.Write((Int32)wishingfish2.PunishmentChooser); // Punshment Chosen
                            packet.Write((Int32)wishingfish2.RichesWish);
                            packet.Write((Int32)wishingfish2.WishesWish);
                            packet.Write((Int32)wishingfish2.FishesWish);                                                 // CHANGE ALL THIS TO MATCH PACKET!
                            packet.Write((Int32)wishingfish2.OresWish);
                            packet.Write((Int32)wishingfish2.SoulsWish);
                            packet.Write((Int32)wishingfish2.WarWish); // this is the worst code I have ever written
                            packet.Write((bool)wishingfish2.TryDoWish);
                            packet.Send();

                            /* 
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
                             */


                            wishingfish2.NPC.netUpdate = true;

                        }

                    }
                    break;
                case MessageType.FallenPixieMsg: // Fallen Pixie info from projectile
                    if (Main.npc[reader.ReadInt32()].ModNPC is FallenPixie fallenpixie)
                    {

                        fallenpixie.FairyIsActivatedByPacket = reader.ReadBoolean();

                        //fallenpixie.NewLife = reader.ReadByte();
                        //fallenpixie.SyncTheShit = reader.ReadBoolean();
                        Main.NewText("Packet Worked!", Color.DeepPink);
                        Logger.WarnFormat("Fallen Pixie Packet Worked!", msgType);



                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket(); // use this instead of other 
                            packet.Write((byte)MessageType.FallenPixieMsg); // id 

                            packet.Write(fallenpixie.NPC.whoAmI); // NPC identity 
                                                                  // packet.Write((byte)15);
                            packet.Write((bool)true);
                            //packet.Write((bool)true);
                            packet.Send();
                            fallenpixie.NPC.netUpdate = true;

                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, fallenpixie.NPC.whoAmI);
                        }
                        fallenpixie.Activate();
                    }
                    break;
                case MessageType.IdkWhyMsg: // Fallen Pixie sync
                    if (Main.npc[reader.ReadByte()].ModNPC is FallenPixie fallenpixie2 && fallenpixie2.NPC.active)
                    {
                        // fallenpixie2.
                        // Wtf did I have this for I have no memory of why I would make this
                    }
                    break;
                case MessageType.DungeonFrogMsg: // Dongeon Froge sync
                    if (Main.npc[reader.ReadInt32()].ModNPC is DungeonFrog dungeonfrog)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            Dust.NewDustDirect(dungeonfrog.NPC.position + new Vector2(Main.rand.Next(-15, 15)), dungeonfrog.NPC.width, dungeonfrog.NPC.height, DustID.Clentaminator_Blue,
                                dungeonfrog.NPC.velocity.X + Main.rand.Next(-6, 6), dungeonfrog.NPC.velocity.Y + Main.rand.Next(-6, 6));
                            Dust.NewDustDirect(dungeonfrog.NPC.position + new Vector2(Main.rand.Next(-15, 15)), dungeonfrog.NPC.width, dungeonfrog.NPC.height, DustID.TintableDustLighted,
                                dungeonfrog.NPC.velocity.X + Main.rand.Next(-6, 6), dungeonfrog.NPC.velocity.Y + Main.rand.Next(-6, 6));
                        }

                        dungeonfrog.NPC.Transform(NPCType<DungeonFrogEmpty>());


                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket(); // use this instead of other 
                            packet.Write((byte)MessageType.DungeonFrogMsg); // id 

                            packet.Write((Int32)dungeonfrog.NPC.whoAmI); // NPC identity 
                                                                         // packet.Write((byte)15);

                            //packet.Write((bool)true);
                            packet.Send();


                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, dungeonfrog.NPC.whoAmI);
                        }
                    }
                    break;
                case MessageType.WishMsg: // GoldenFish Sync
                    if (Main.npc[reader.ReadInt32()].ModNPC is GoldenFish wisherfish)
                    {
                        wisherfish.ChoosedWishes = reader.ReadBoolean();
                        wisherfish.WishGranted = reader.ReadBoolean();
                        wisherfish.EvilCalculator = reader.ReadInt32(); // I really hope this isn't too much data
                        wisherfish.PunishmentChooser = reader.ReadInt32();
                        wisherfish.Button1IsRiches = reader.ReadBoolean();
                        wisherfish.Button1IsWishes = reader.ReadBoolean(); // CHANGE ALL THIS TO MATCH PACKET!!!

                        wisherfish.Button1IsFishes = reader.ReadBoolean();

                        wisherfish.Button2IsOres = reader.ReadBoolean();

                        wisherfish.Button2IsSouls = reader.ReadBoolean();

                        wisherfish.Button2IsWar = reader.ReadBoolean();

                        wisherfish.TryDoWish = reader.ReadBoolean();
                        bool firstbutton = reader.ReadBoolean();
                        bool secondbutton = reader.ReadBoolean();
                        //Main.NewText("Wish Packet Worked!", Color.Gold);

                        if (DoneYet == false)
                        {


                            if (firstbutton)
                            {


                                if (wisherfish.EvilCalculator != 1)
                                {





                                    if (wisherfish.Button1IsWishes != true)
                                    {
                                        Main.npcChatText = "Your wish is my command!";
                                    }


                                    if (wisherfish.Button1IsDishes != true)
                                    {

                                        if (wisherfish.Button1IsFishes == true)
                                        {
                                            if (Main.rand.NextBool(3) && Main.hardMode)
                                            {

                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.ChaosFish, Main.rand.Next(0, 3));

                                            }
                                            if (Main.rand.NextBool(3))
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Ebonkoi, Main.rand.Next(0, 3));
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Honeyfin, Main.rand.Next(0, 3));
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CrimsonTigerfish, Main.rand.Next(0, 5));
                                            }
                                            if (Main.rand.NextBool(5))
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Fish, 1);
                                            }
                                            if (Main.rand.NextBool(4))
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.BalloonPufferfish, 1);
                                            }
                                            if (Main.rand.NextBool(3) && Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CrystalSerpent, 1);
                                            }
                                            if (Main.rand.NextBool(3) && Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.ObsidianSwordfish, 1);
                                            }
                                            if (Main.rand.NextBool(5) && !Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PurpleClubberfish, 1);
                                            }
                                            if (Main.rand.NextBool(5) && !Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Swordfish, 1);
                                            }
                                            if (Main.rand.NextBool(3) && !Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.ReaverShark, 1);
                                            }
                                            if (Main.rand.NextBool(1))
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.GoldenCarp, 1);
                                            }
                                            //  Main.LocalPlayer.QuickSpawnItem(EntitySource_Loot, ItemID.AtlanticCod); 
                                            if (Main.rand.NextBool(2) && Main.hardMode)
                                            {
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PrincessFish, Main.rand.Next(0, 4));
                                                Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Prismite, Main.rand.Next(0, 3));
                                            }
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Tuna, Main.rand.Next(0, 7));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.FrostMinnow, Main.rand.Next(0, 3));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.DoubleCod, Main.rand.Next(0, 6));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.RedSnapper, Main.rand.Next(0, 6));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.BombFish, Main.rand.Next(2, 8));

                                        }

                                        // holy carp louis i frickin love nestled if statements and repeating rand.NextBool!
                                        // thats great petah cause we have plenty!





                                        if (wisherfish.Button1IsRiches == true)
                                        {
                                            int projectile = Projectile.NewProjectile(wisherfish.NPC.GetSource_FromAI(), wisherfish.NPC.Center + new Vector2(0f, -42f), wisherfish.NPC.velocity, ProjectileID.CoinPortal, 0, 0);
                                            projectile = Projectile.NewProjectile(wisherfish.NPC.GetSource_FromAI(), wisherfish.NPC.Center + new Vector2(0f, -42f), wisherfish.NPC.velocity, ProjectileID.CoinPortal, 0, 0);
                                        }
                                        //    else
                                        //   {

                                        //     }
                                    }
                                    if (wisherfish.Button1IsDishes == true)
                                    {
                                        Main.npcChatText = "Your wish is my command!";
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CookedFish, Main.rand.Next(1, 4));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Escargot, Main.rand.Next(0, 3));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.FroggleBunwich, Main.rand.Next(0, 3));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.FruitSalad, Main.rand.Next(0, 3));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.GrubSoup, Main.rand.Next(0, 3));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SeafoodDinner, Main.rand.Next(1, 3));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PeachSangria, Main.rand.Next(0, 2));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PinaColada, Main.rand.Next(0, 2));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PrismaticPunch, Main.rand.Next(0, 2));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.BananaSplit, Main.rand.Next(0, 2));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.FlaskofGold, 1);
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.RedPotion, Main.rand.Next(0, 2));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.ManaPotion, Main.rand.Next(0, 4));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.HealingPotion, Main.rand.Next(0, 4));
                                    }
                                }


                                if (wisherfish.Button1IsWishes == true || wisherfish.EvilCalculator == 1)
                                {
                                    if (wisherfish.EvilCalculator == 1)
                                    {
                                        Main.npcChatText = "'You really thought I would grant you a wish? Muahahaha!'";
                                    }
                                    if (wisherfish.Button1IsWishes == true)
                                    {
                                        Main.npcChatText = "'How dare you try to cheat the system like that!'";
                                        if (Main.netMode != NetmodeID.MultiplayerClient)
                                        {
                                            wisherfish.EvilCalculator = 1;
                                        }
                                    }
                                    //  Main.PlaySound(SoundID.Item8);

                                    wisherfish.NPC.color = Color.Red;
                                }
                                /* if (EvilCalculator == 1)
                                 {
                                    // Main.PlaySound(SoundID.Item8);

                                     Main.wisherfish.NPCChatText = "'You really thought I would grant you a wish? Muahahaha!'";
                                     wisherfish.NPC.Newwisherfish.NPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X, (int)wisherfish.NPC.Right.Y, wisherfish.NPCID.BloodNautilus, 0, wisherfish.NPC.whoAmI);
                                     wisherfish.NPC.color = Color.Red;

                                 } */


                                wisherfish.WishGranted = true;
                            }

                            if (secondbutton)
                            {






                                if (wisherfish.EvilCalculator == 1)
                                {
                                    if (wisherfish.EvilCalculator == 1)
                                    {
                                        Main.npcChatText = "'You really thought I would grant you a wish? Muahahaha!'";
                                        wisherfish.NPC.color = Color.Red;

                                    }

                                    //  Main.PlaySound(SoundID.Item8);

                                }

                                if (wisherfish.EvilCalculator != 1)
                                {
                                    Main.npcChatText = "Your wish is my command, sire!";
                                    wisherfish.Button1IsWishes = false;

                                    if (wisherfish.Button2IsSouls == true)
                                    {
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofFlight, Main.rand.Next(0, 13));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofNight, Main.rand.Next(0, 13));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofLight, Main.rand.Next(0, 13));
                                        if (NPC.downedMechBoss3)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofFright, Main.rand.Next(0, 9));
                                        }
                                        if (NPC.downedMechBoss2)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofSight, Main.rand.Next(0, 9));
                                        }
                                        if (NPC.downedMechBoss1)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SoulofMight, Main.rand.Next(0, 9));
                                        }

                                    }


                                    if (wisherfish.Button2IsWar == true)
                                    {
                                        if (!Main.hardMode)
                                        {
                                            Main.StartInvasion(InvasionID.GoblinArmy);
                                            NetMessage.SendData(MessageID.WorldData);

                                        }

                                        if (Main.hardMode && Main.LocalPlayer.statLifeMax > 200 && !NPC.downedPlantBoss)
                                        {
                                            Main.StartInvasion(InvasionID.PirateInvasion);
                                            NetMessage.SendData(MessageID.WorldData);
                                        }
                                        if (NPC.downedPlantBoss && Main.hardMode && Main.LocalPlayer.statLifeMax > 200)
                                        {
                                            Main.StartInvasion(InvasionID.MartianMadness);
                                            NetMessage.SendData(MessageID.WorldData);
                                        }
                                    }
                                    if (wisherfish.Button2IsOres == true)
                                    {
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CopperOre, Main.rand.Next(0, 6));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.TinOre, Main.rand.Next(0, 6));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SilverOre, Main.rand.Next(0, 6));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.TungstenOre, Main.rand.Next(0, 6));
                                        if (Main.rand.NextBool(2) && !Main.hardMode)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CopperOre, Main.rand.Next(0, 13));
                                        }
                                        if (Main.rand.NextBool(2) && !Main.hardMode)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.TinOre, Main.rand.Next(0, 13));
                                        }
                                        if (Main.rand.NextBool(2) && !Main.hardMode)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.SilverOre, Main.rand.Next(0, 11));
                                        }
                                        if (Main.rand.NextBool(2) && !Main.hardMode)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.TungstenOre, Main.rand.Next(0, 11));
                                        }

                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.IronOre, Main.rand.Next(0, 11));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.LeadOre, Main.rand.Next(0, 11));

                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.GoldOre, Main.rand.Next(0, 11));
                                        Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PlatinumOre, Main.rand.Next(0, 11));
                                        if (NPC.downedBoss1)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CrimtaneOre, Main.rand.Next(0, 13));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.DemoniteOre, Main.rand.Next(0, 13));
                                        }

                                        if (NPC.downedBoss2)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Meteorite, Main.rand.Next(0, 17));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PlatinumOre, Main.rand.Next(0, 11)); // Want it to give more of lesser tier ores as you progress
                                        }
                                        if (NPC.downedBoss3 && NPC.downedBoss2)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.Hellstone, Main.rand.Next(6, 21));
                                        }
                                        if (Main.hardMode)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.CobaltOre, Main.rand.Next(5, 13));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.PalladiumOre, Main.rand.Next(5, 13));
                                        }

                                        if (NPC.downedMechBossAny || NPC.downedQueenSlime)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.MythrilOre, Main.rand.Next(5, 13));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.AdamantiteOre, Main.rand.Next(5, 13));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.OrichalcumOre, Main.rand.Next(5, 13));
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.TitaniumOre, Main.rand.Next(5, 13));
                                        }
                                        if (NPC.downedPlantBoss)
                                        {
                                            Item.NewItem(wisherfish.NPC.GetSource_Loot(), wisherfish.NPC.Center, ItemID.ChlorophyteOre, Main.rand.Next(5, 21));
                                        }
                                        //  if (ModLoader.GetMod("Consolaria") != null && wisherfish.NPC.downedMechBossAny && )
                                        //  {

                                        //   }


                                    }

                                }
                            }
                            if (wisherfish.EvilCalculator == 1 || wisherfish.Button1IsWishes && firstbutton)
                            {
                                if (Main.GraveyardVisualIntensity < 0.8f)
                                {
                                    Main.GraveyardVisualIntensity += 0.2f;
                                }


                                for (int i = 0; i < 16; i++)
                                {
                                    Dust.NewDustDirect(wisherfish.NPC.position + new Vector2(Main.rand.Next(-3, 3)), wisherfish.NPC.width, wisherfish.NPC.height, DustID.Cloud, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), 0, Color.Red);
                                }

                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {

                                    if (Main.bloodMoon)
                                    {
                                        NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X, (int)wisherfish.NPC.Right.Y, NPCID.BloodNautilus, 0, wisherfish.NPC.whoAmI);
                                    }
                                    if (wisherfish.PunishmentChooser == 1)
                                    {
                                        int projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0, -140), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);

                                        for (int i = 0; i < 5; i++)
                                        {
                                            projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0 + (i * 5), -140 + (i * -5)), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        }
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(10, -180), wisherfish.NPC.velocity, ProjectileID.BeeHive, 130, 0);
                                        projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-10, -180), wisherfish.NPC.velocity, ProjectileID.BeeHive, 130, 0);

                                        // High up boulders
                                        for (int i = 0; i < 5; i++)
                                        {
                                            projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(20 + (i * -40), -540), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                        }
                                    }
                                    if (wisherfish.PunishmentChooser == 2)
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
                                            NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X + Main.rand.Next(-100, 100), (int)wisherfish.NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.CaveBat, 0, wisherfish.NPC.whoAmI);
                                        }
                                        for (int i = 0; i < 3; i++)
                                        {

                                            NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X + Main.rand.Next(-100, 100), (int)wisherfish.NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.GiantBat, 0, wisherfish.NPC.whoAmI);
                                            NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X + Main.rand.Next(-100, 100), (int)wisherfish.NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.JungleBat, 0, wisherfish.NPC.whoAmI);
                                            NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X + Main.rand.Next(-100, 100), (int)wisherfish.NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.IceBat, 0, wisherfish.NPC.whoAmI);
                                            NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)wisherfish.NPC.Right.X + Main.rand.Next(-100, 100), (int)wisherfish.NPC.Right.Y + Main.rand.Next(-50, 50), NPCID.SporeBat, 0, wisherfish.NPC.whoAmI);
                                        }



                                    }
                                    if (wisherfish.PunishmentChooser == 3)
                                    {
                                        if (Main.bloodMoon == false && Main.dayTime == false)
                                        {
                                            Main.bloodMoon = true;
                                        }
                                        else
                                        {
                                            if (Main.netMode != NetmodeID.MultiplayerClient)
                                            {


                                                int projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0, -140), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);

                                                for (int i = 0; i < 5; i++)
                                                {
                                                    projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(0 + (i * 5), -140 + (i * -5)), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                                }
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(10, -180), wisherfish.NPC.velocity, ProjectileID.BeeHive, 130, 0);
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-10, -180), wisherfish.NPC.velocity, ProjectileID.BeeHive, 130, 0);

                                                // High up boulders
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(20, -540), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-20, -540), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(60, -540), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                                projectile = Projectile.NewProjectile(NPC.GetSource_NaturalSpawn(), Main.LocalPlayer.position + new Vector2(-60, -540), wisherfish.NPC.velocity, ProjectileID.Boulder, 130, 0);
                                            }
                                        }
                                    }
                                    if (wisherfish.PunishmentChooser == 4)
                                    {
                                        Main.LocalPlayer.statLife = 1;

                                        NPC.NewNPC(wisherfish.NPC.GetSource_FromAI(), (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y - 100, NPCID.KingSlime, 0, wisherfish.NPC.whoAmI);

                                        Main.LocalPlayer.AddBuff(BuffID.Cursed, 800);

                                    }



                                }

                            }
                            wisherfish.NPC.active = false;
                            wisherfish.NPC.netUpdate = true;
                            for (int i = 0; i < 20; i++)
                            {
                                Dust dust = Dust.NewDustDirect(wisherfish.NPC.position + new Vector2(Main.rand.Next(-15, 15)), wisherfish.NPC.width, wisherfish.NPC.height,
                                    DustID.GoldFlame, wisherfish.NPC.velocity.X + Main.rand.Next(-5, 5), wisherfish.NPC.velocity.Y + Main.rand.Next(-5, 5));
                                dust.noGravity = true;
                            }
                        }
                    }
                    break;
                case MessageType.SlugMsg:
                    {
                        if (Main.netMode == NetmodeID.Server)
                        {


                            int playerID = reader.ReadInt32();
                            Player thisPlayer = Main.player[playerID];
                            int slugNPC;
                            slugNPC = NPC.NewNPC(thisPlayer.GetSource_FromAI(), (int)thisPlayer.Center.X + Main.rand.Next(-3, 3), (int)thisPlayer.Center.Y + Main.rand.Next(-3, 3), ModContent.NPCType<SlipperySlug>());
                            Main.npc[slugNPC].netUpdate = true;
                        }
                    }
                     break;
                default:
                    Logger.WarnFormat("Creaturia: Unknown Message type: {0}", msgType);
                    break;

            }

        }
    }
}