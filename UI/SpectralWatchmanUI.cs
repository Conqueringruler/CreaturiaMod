using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Creaturia.NPCs;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics;
using Terraria.GameContent.UI;
using Terraria.GameContent;
using Creaturia.NPCs.Town;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using System.IO;
using System.Linq;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Shaders;
using Creaturia;


namespace Creaturia.UI;

// This class represents the UIState for our ExamplePerson Awesomeify chat function. It is similar to the Goblin Tinkerer's Reforge function, except it only gives Awesome and ReallyAwesome prefixes. 

internal class VanillaItemSlotWrapper : UIElement
{
	internal Item Item;
	private readonly int _context;
	private readonly float _scale;
	internal Func<Item, bool> ValidItemFunc;
	

	public VanillaItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f)
	{
		_context = context;
		_scale = scale;
		Item = new Item();
		Item.SetDefaults(0);

		Width.Set(TextureAssets.InventoryBack9.Width() * scale, 0f);
		Height.Set(TextureAssets.InventoryBack9.Height() * scale, 0f);
	}

	protected override void DrawSelf(SpriteBatch spriteBatch)
	{
		float oldScale = Main.inventoryScale;
		Main.inventoryScale = _scale;
		Rectangle rectangle = GetDimensions().ToRectangle();

		if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
		{
			Main.LocalPlayer.mouseInterface = true;
			if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
			{
				// Handle handles all the click and hover actions based on the context.
				ItemSlot.Handle(ref Item, _context);
			}
		}
		// Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
		ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
		Main.inventoryScale = oldScale;
	}
}
public class SpectralWatchmanUI : UIState
	{
	int awesomePrice;
	int PurchasingItem;
	int PurchasingAmount;
	int TotalOfPurchasingItem;


	int StartingStackInInventory;
	int RandomPrefixValue;
	Color RarityColor;




		private VanillaItemSlotWrapper _vanillaItemSlot;
	
	public override void OnInitialize()
		{
		
		_vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 50 },
				Top = { Pixels = 270 },
				ValidItemFunc = item => item.IsAir || !item.IsAir && item.Prefix(-3)
			};
			// Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
			Append(_vanillaItemSlot);
		}

		// OnDeactivate is called when the UserInterface switches to a different state. In this mod, we switch between no state (null) and this state (ExamplePersonUI).
		// Using OnDeactivate is useful for clearing out Item slots and returning them to the player, as we do here.
		public override void OnDeactivate()
		{
			if (!_vanillaItemSlot.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnClonedItem(Player.GetSource_None(), _vanillaItemSlot.Item, _vanillaItemSlot.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_vanillaItemSlot.Item.TurnToAir();
			}
			// Note that in ExamplePerson we call .SetState(new UI.ExamplePersonUI());, thereby creating a new instance of this UIState each time. 
			// You could go with a different design, keeping around the same UIState instance if you wanted. This would preserve the UIState between opening and closing. Up to you.
		}

		// Update is called on a UIState while it is the active state of the UserInterface.
		// We use Update to handle automatically closing our UI when the player is no longer talking to our Example Person NPC.
		public override void Update(GameTime gameTime)
		{
			// Don't delete this or the UIElements attached to this UIState will cease to function.
			base.Update(gameTime);

			// talkNPC is the index of the NPC the player is currently talking to. By checking talkNPC, we can tell when the player switches to another NPC or closes the NPC chat dialog.
			if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<SpectralWatchman>())
			{
			// When that happens, we can set the state of our UserInterface to null, thereby closing this UIState. This will trigger OnDeactivate above.



			ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface.SetState(null);


			
		}
	}

		private bool tickPlayed;
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			// This will hide the crafting menu similar to the reforge menu. For best results this UI is placed before "Vanilla: Inventory" to prevent 1 frame of the craft menu showing.
			Main.hidePlayerCraftingMenu = true;

		// Here we have a lot of code. This code is mainly adapted from the vanilla code for the reforge option.
		// This code draws "Place an item here" when no item is in the slot and draws the reforge cost and a reforge button when an item is in the slot.
		// This code could possibly be better as different UIElements that are added and removed, but that's not the main point of this example.
		// If you are making a UI, add UIElements in OnInitialize that act on your ItemSlot or other inputs rather than the non-UIElement approach you see below.

		PurchasingItem = ItemID.CrystalShard;

		const int slotX = 50;
			const int slotY = 270;
			if (!_vanillaItemSlot.Item.IsAir)
			{
			

			if (_vanillaItemSlot.Item.rare is ItemRarityID.White or ItemRarityID.Blue or ItemRarityID.Green or ItemRarityID.Orange or ItemRarityID.LightRed)
			{
				RarityColor = Colors.RarityRed;
				if (Main.time > 1900)
				{
					PurchasingItem = ItemID.CrystalShard;

					

					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 2500));
					TotalOfPurchasingItem = (Main.LocalPlayer.CountItem(PurchasingItem));
					awesomePrice = Item.buyPrice(0, 1, 0, 0);
				}
				else
				{
					PurchasingItem = ItemID.PixieDust;

					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 2500));
					 // Unless I change the buying system I can't do Pixie Dust because of stack limit
				}

			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Pink or ItemRarityID.LightPurple)
			{
				RarityColor = Colors.RarityPink;
				if (Main.time > 1900)
				{
					PurchasingItem = ItemID.SoulofLight;

					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 12000));
					awesomePrice = Item.buyPrice(1, 1, 0, 0);
				}
				else
				{
					PurchasingItem = ItemID.SoulofNight;

					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 12000));
					awesomePrice = Item.buyPrice(2, 1, 0, 0);
				}
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Lime)
			{
				RarityColor = Colors.RarityLime;
				PurchasingItem = ItemID.BrokenHeroSword;

				PurchasingAmount = (2);
				awesomePrice = Item.buyPrice(10, 1, 0, 0);
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Yellow)
			{
				RarityColor = Colors.RarityYellow;
				PurchasingItem = ItemID.Ectoplasm;

				PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 15000));
				awesomePrice = Item.buyPrice(20, 1, 0, 0);
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Cyan)
			{
				RarityColor = Colors.RarityCyan;
				PurchasingItem = ItemID.Ectoplasm; // but have it be more

				PurchasingAmount = (15 + (int)(_vanillaItemSlot.Item.value / 12000));
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Red)
			{
				RarityColor = Colors.RarityRed;
				PurchasingItem = ItemID.Ectoplasm; // but have it be even more

				PurchasingAmount = (15 + (int)(_vanillaItemSlot.Item.value / 10000));
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Purple)
			{
				RarityColor = Colors.RarityPurple;
				PurchasingItem = ItemID.LunarOre;
				PurchasingAmount = (20);
			}
			
			// ADD EXCEPTION FOR MOD RARITY!

			foreach (Item PurchasingItemItem in Main.LocalPlayer.inventory)
			{
				if (PurchasingItemItem.type == PurchasingItem)
					TotalOfPurchasingItem = PurchasingItemItem.stack;
			}


			string costText = Language.GetTextValue("LegacyInterface.46") + ": ";
				string coinsText = "";
				int[] coins = Utils.CoinsSplit(awesomePrice);
			coinsText = coinsText + "" + PurchasingAmount + $"[i:{PurchasingItem}" + "] ";
			if (coins[3] > 0)
				{
					//coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + " " + $"[i:{PurchasingItem}]" + "] ";
					//coinsText = coinsText + ":" + PurchasingAmount + $"[i:{PurchasingItem}]" + "] ";
				//coinsText = coinsText +"[c/" + Colors.AlphaDarken(Colors.RarityGreen).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
			}
				if (coins[2] > 0)
				{
					//coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";

				}
				if (coins[1] > 0)
				{
					//coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
				}
				if (coins[0] > 0)
				{
					//coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
				}
				ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, costText, new Vector2(slotX + 105, slotY + 10), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, coinsText, new Vector2(slotX + 105 + FontAssets.MouseText.Value.MeasureString(costText).X, (float)slotY + 10), RarityColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				int reforgeX = slotX + 75;
				int reforgeY = slotY + 21;

				int FalsereforgeX = slotX + 67;
				int FalsereforgeY = slotY + 14;

			bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;
				Texture2D falseReforgeTexture = (Texture2D)TextureAssets.Sun3;

			//Texture2D reforgeTexture = (Texture2D)TextureAssets.Reforge[hoveringOverReforgeButton ? 1 : 0];
			Texture2D reforgeTexture = (Texture2D)TextureAssets.Sun;
			Main.spriteBatch.Draw(falseReforgeTexture, new Vector2(FalsereforgeX, FalsereforgeY), null, new Color(254, 85, 200, 0) * (0.5f + 0.7f * ((200 - 100) / 255f)), 0f, falseReforgeTexture.Size() / 3f, 0.8f, SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(falseReforgeTexture, new Vector2(FalsereforgeX, FalsereforgeY), null, new Color(254, 85, 200, 1), 0f, falseReforgeTexture.Size() / 3f, 0.8f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, new Color(254, 85, 200, 0) * (0.5f + 0.7f * ((200 - 100) / 255f)), 0f, reforgeTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
			if (hoveringOverReforgeButton)
				{
				//Main.hoverItemName = Language.GetTextValue("LegacyInterface.19");
				// This above is localized into being the text "Reforge" for different languages, so I want to make localizations for mine as well
			    Main.hoverItemName = "Enhance";
				if (!tickPlayed)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					tickPlayed = true;
					Main.LocalPlayer.mouseInterface = true;
				if (Main.mouseLeftRelease && Main.mouseLeft && Main.LocalPlayer.HasItem(PurchasingItem)
				&& (TotalOfPurchasingItem >= PurchasingAmount) && ItemLoader.PreReforge(_vanillaItemSlot.Item))
					{

					// Make custom cost below 





					//Main.LocalPlayer.BuyItem(awesomePrice, -1);


					
				
					Main.LocalPlayer.inventory[Main.LocalPlayer.FindItem(PurchasingItem)].TurnToAir();
					
					Main.LocalPlayer.QuickSpawnItem(Player.GetSource_None(), PurchasingItem, (TotalOfPurchasingItem - PurchasingAmount));
						bool favorited = _vanillaItemSlot.Item.favorited;
						int stack = _vanillaItemSlot.Item.stack;
						Item reforgeItem = new Item();
						reforgeItem.netDefaults(_vanillaItemSlot.Item.netID);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(_vanillaItemSlot.Item);
					

					if (_vanillaItemSlot.Item.accessory)
					{
							RandomPrefixValue = Main.rand.Next(1, 3); // remember that 3 is not one of the choosable options, since it excludes the last value
							if (RandomPrefixValue == 1)
                        {
							reforgeItem.Prefix(PrefixID.Warding);
						}
						if (RandomPrefixValue == 2)
						{
							reforgeItem.Prefix(PrefixID.Menacing);
						}
					

						

					}
					if (!_vanillaItemSlot.Item.accessory)
					{
						RandomPrefixValue = Main.rand.Next(1, 4); // remember that 4 is not one of the choosable options, since it excludes the last value
						if (RandomPrefixValue == 1)
						{
							if (_vanillaItemSlot.Item.knockBack > 0)
							{
								reforgeItem.Prefix(PrefixID.Godly);
							}
                            else
                            {
								reforgeItem.Prefix(PrefixID.Demonic);
							}
							
						}
						if (RandomPrefixValue == 2)
						{
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Ranged)
							{
								if (_vanillaItemSlot.Item.knockBack > 0) // can't get modifiers that affect knockback
								{
									reforgeItem.Prefix(PrefixID.Unreal);
								}
								else
                                {
									reforgeItem.Prefix(PrefixID.Demonic);
								}
								
							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Magic || _vanillaItemSlot.Item.DamageType == DamageClass.Summon)
							{
								if (_vanillaItemSlot.Item.mana > 3 && _vanillaItemSlot.Item.knockBack != 0)
                                {
									reforgeItem.Prefix(PrefixID.Mythical);
								}
								else if (_vanillaItemSlot.Item.mana <= 3 && _vanillaItemSlot.Item.knockBack == 0)
                                {
									reforgeItem.Prefix(PrefixID.Godly);
                                }
                                else
                                {
									reforgeItem.Prefix(PrefixID.Demonic);
								}
								
							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Melee && _vanillaItemSlot.Item.DamageType != DamageClass.MeleeNoSpeed || _vanillaItemSlot.Item.DamageType == DamageClass.SummonMeleeSpeed)
							{
								reforgeItem.Prefix(PrefixID.Legendary);
							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.SummonMeleeSpeed)
							{
								reforgeItem.Prefix(PrefixID.Legendary);
							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.MeleeNoSpeed)
                            {
								reforgeItem.Prefix(PrefixID.Godly); // MeleeNoSpeed can't get melee prefixes, only universal ones
							}
						}

						if (RandomPrefixValue == 3)
						{
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Ranged)
							{
								if (_vanillaItemSlot.Item.knockBack == 0f) // can't get modifiers that affect knockback
								{
									reforgeItem.Prefix(PrefixID.Deadly2);
								}
								else
								{
									reforgeItem.Prefix(PrefixID.Deadly);
								}

							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Magic || _vanillaItemSlot.Item.DamageType == DamageClass.Summon)
							{
								if (_vanillaItemSlot.Item.mana > 3 && _vanillaItemSlot.Item.knockBack != 0)
								{
									reforgeItem.Prefix(PrefixID.Masterful);
								}
								else if (_vanillaItemSlot.Item.mana <= 3 && _vanillaItemSlot.Item.knockBack == 0)
								{
									reforgeItem.Prefix(PrefixID.Demonic);
								}
								else
								{
									reforgeItem.Prefix(PrefixID.Demonic);
								}

							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.Melee && _vanillaItemSlot.Item.DamageType != DamageClass.MeleeNoSpeed || _vanillaItemSlot.Item.DamageType == DamageClass.SummonMeleeSpeed)
							{
								reforgeItem.Prefix(PrefixID.Savage);
							}
							if (_vanillaItemSlot.Item.DamageType == DamageClass.MeleeNoSpeed)
							{
								reforgeItem.Prefix(PrefixID.Superior); // MeleeNoSpeed can't get melee prefixes, only universal ones
							}
						}



					}
					
						_vanillaItemSlot.Item = reforgeItem.Clone();
						_vanillaItemSlot.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(_vanillaItemSlot.Item.width / 2);
						_vanillaItemSlot.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(_vanillaItemSlot.Item.height / 2);
						_vanillaItemSlot.Item.favorited = favorited;
						_vanillaItemSlot.Item.stack = stack;
						ItemLoader.PostReforge(_vanillaItemSlot.Item);
						PopupText.NewText(PopupTextContext.ItemReforge, _vanillaItemSlot.Item, _vanillaItemSlot.Item.stack, true, false);
						SoundEngine.PlaySound(SoundID.DD2_DarkMageHealImpact);
					}
				}
				else
				{
					tickPlayed = false;
				}
			}
			else
			{
				string message = "Place an item here to enhance";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}

