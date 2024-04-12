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
		

		
	}
}