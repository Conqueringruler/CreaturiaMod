using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Creaturia.NPCs.Town;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Items.Consumables.Fishing;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Terraria.Localization;

namespace Creaturia.Common.Players
{
    public class GlobalFishingPlayer : ModPlayer
    {
       
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{

			if (Player.ZoneHallow && attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<HallowBait>()) 
            {
				
				
				int rainbowfishnpc = ModContent.NPCType<RainbowFishSpawner>();
				{
					if (Main.hardMode)
                    {
						// itemDrop = -1 so terraria won't spawn the item
						npcSpawn = rainbowfishnpc;
						itemDrop = -1;
						
						sonar.Text = "Chromatic scales shimmer beneath the translucent water...";
						sonar.Color = Color.Pink;
						sonar.Velocity = Vector2.UnitY;
						sonar.DurationInFrames = 300;
						sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);
						
					}
					
				}
			}

			if (Main.hardMode && !attempt.inLava && !attempt.inHoney && Player.ZoneHallow && Main.rand.NextBool(300))
			{

				int rainbowfishnpc = ModContent.NPCType<RainbowFishSpawner>();
				if (Main.hardMode && !NPC.AnyNPCs(ModContent.NPCType<RainbowFish>()))
				{

					npcSpawn = rainbowfishnpc;
					itemDrop = -1;

					sonar.Text = "Chromatic scales shimmer beneath the translucent water...";
					sonar.Color = Color.Pink;
					sonar.Velocity = Vector2.UnitY;
					sonar.DurationInFrames = 300;
					sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);
				}
			}
				if (Player.ZoneCrimson && !attempt.inLava && attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<CrimsonBait>())
			{


				int crimsonfishnpc = ModContent.NPCType<CrimsonFishMiniBoss>();
				{
					if (Main.hardMode)
					{
						// itemDrop = -1 so terraria won't spawn the item
						npcSpawn = crimsonfishnpc;
						itemDrop = -1;

						sonar.Text = "Spikes and flesh pulsate below...";
						sonar.Color = Color.DarkRed;
						sonar.Velocity = Vector2.UnitY;
						sonar.DurationInFrames = 300;
						sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

					}

				}
			}

			if (Player.ZoneCorrupt && !attempt.inLava && attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<CorruptBait>())
			{


				int corruptfishnpc = ModContent.NPCType<DunklerFish>();

				{
					if (Main.hardMode)
					{
						 //itemDrop = -1 so terraria won't spawn the item
						npcSpawn = corruptfishnpc;
					//	Main.NewText("The creature that lies below hides until the next content update to Creaturia...", Color.DarkViolet);
						itemDrop = -1;

						sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.DunklePoleTxt");
						sonar.Color = Color.DarkViolet;
						sonar.Velocity = Vector2.UnitY;
						sonar.DurationInFrames = 300;
						sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

					}

				}
			}
            if (Player.ZoneCorrupt && Main.hardMode && !attempt.inLava && Main.rand.NextBool(300) && !NPC.AnyNPCs(ModContent.NPCType<DunklerFish>()))
            {


                int corruptfishnpc = ModContent.NPCType<DunklerFish>();

                {
                    if (Main.hardMode)
                    {
                        //itemDrop = -1 so terraria won't spawn the item
                        npcSpawn = corruptfishnpc;
                        //	Main.NewText("The creature that lies below hides until the next content update to Creaturia...", Color.DarkViolet);
                        itemDrop = -1;

                        sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.DunklePoleTxt");
                        sonar.Color = Color.DarkViolet;
                        sonar.Velocity = Vector2.UnitY;
                        sonar.DurationInFrames = 300;
                        sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

                    }

                }
            }

            if (Main.hardMode && !attempt.inLava && !attempt.inHoney && Player.ZoneCrimson && Main.rand.NextBool(300) && !NPC.AnyNPCs(ModContent.NPCType<CrimsonFishMiniBoss>()))
			{

				int crimsonfishnpc = ModContent.NPCType<CrimsonFishMiniBoss>();
				{
					if (Main.hardMode && !NPC.AnyNPCs(ModContent.NPCType<RainbowFish>()))
					{
						// itemDrop = -1 so terraria won't spawn the item
						npcSpawn = crimsonfishnpc;
						itemDrop = -1;

						sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.LumpPoleTxt");
						sonar.Color = Color.DarkRed;
						sonar.Velocity = Vector2.UnitY;
						sonar.DurationInFrames = 300;
						sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

					}

				}
			}
			if (!attempt.inLava && !attempt.inHoney && Player.ZoneBeach && Main.rand.NextBool(60) && !NPC.AnyNPCs(ModContent.NPCType<Fishman>()))
			{

				int fishmannpc = ModContent.NPCType<Fishman>();
				if (!NPC.AnyNPCs(fishmannpc))
				{
					
					npcSpawn = fishmannpc;
					itemDrop = -1;

					sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.FishPoleTxt");
					sonar.Color = Color.DarkSeaGreen;
					sonar.Velocity = Vector2.Zero;
					sonar.DurationInFrames = 300;

					
					sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);
				}
			}
            if (Player.ZoneBeach && attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<FishmanDevbait>())
            {


                int fishmannpc = ModContent.NPCType<Fishman>();
                {
                    //if (Main.hardMode)
                    //{
                    // itemDrop = -1 so terraria won't spawn the item
                    npcSpawn = fishmannpc;
                    itemDrop = -1;

                    sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.FishPoleTxt");
                    sonar.Color = Color.DarkSeaGreen;
                    sonar.Velocity = Vector2.UnitY;
                    sonar.DurationInFrames = 300;
                    sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

                    //}

                }
            }
            if (Player.ZoneBeach && attempt.playerFishingConditions.BaitItemType == ModContent.ItemType<GoldenFishDevBait>())
			{


				int goldenfishnpc = ModContent.NPCType<GoldenFish>();
				{
					//if (Main.hardMode)
					//{
					// itemDrop = -1 so terraria won't spawn the item
					npcSpawn = goldenfishnpc;
					itemDrop = -1;

					sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.WishingFishPoleTxt");
                    sonar.Color = Color.DarkGoldenrod;
					sonar.Velocity = Vector2.UnitY;
					sonar.DurationInFrames = 300;
					sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

					//}

				}
			}
           
            if (Main.hardMode && !attempt.inLava && Player.ZoneBeach && Main.rand.NextBool(250) && !NPC.AnyNPCs(ModContent.NPCType<GoldenFish>()))
			{


				int goldenfishnpc = ModContent.NPCType<GoldenFish>();
				{
					//if (Main.hardMode)
					//{
					// itemDrop = -1 so terraria won't spawn the item
					npcSpawn = goldenfishnpc;
					itemDrop = -1;

					sonar.Text = "" + Language.GetOrRegister("Mods.Creaturia.Common.WishingFishPoleTxt");
                    sonar.Color = Color.DarkGoldenrod;
					sonar.Velocity = Vector2.UnitY;
					sonar.DurationInFrames = 300;
					sonarPosition = new Vector2(Player.position.X, Player.position.Y - 64);

					//}

				}
			}
		}
		


    }
}
