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

internal class VanillaItemSlotWrapper2 : UIElement
{
	internal Item Item;
	private readonly int _context;
	private readonly float _scale;
	internal Func<Item, bool> ValidItemFunc;
	

	public VanillaItemSlotWrapper2(int context = ItemSlot.Context.BankItem, float scale = 1f)
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
public class SpectralWatchmanUIwithMoney : UIState
	{
	int awesomePrice;
	int PurchasingItem;
	int PurchasingAmount;

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


			awesomePrice = Item.buyPrice(0, 1, 0, 0);

			ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface.SetState(null);


			if (_vanillaItemSlot.Item.rare is ItemRarityID.Blue or ItemRarityID.Green or ItemRarityID.Orange or ItemRarityID.LightRed)
            {
				if (Main.time > 900)
                {
					PurchasingItem = ItemID.CrystalShard;
					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 1000));
				}
                else
                {
					PurchasingItem = ItemID.PixieDust;
					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 1000));
				}
				
            }
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Pink or ItemRarityID.LightPurple)
			{
				if (Main.time > 900)
				{
					PurchasingItem = ItemID.SoulofLight;
					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 1000));
				}
				else
				{
					PurchasingItem = ItemID.SoulofNight;
					PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 1000));
				}
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Lime)
			{
				PurchasingItem = ItemID.BrokenHeroSword;
				PurchasingAmount = (2);
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Yellow)
			{
				PurchasingItem = ItemID.Ectoplasm;
				PurchasingAmount = (10 + (int)(_vanillaItemSlot.Item.value / 1000));
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Cyan)
			{
				PurchasingItem = ItemID.Ectoplasm; // but have it be more
				PurchasingAmount = (15 + (int)(_vanillaItemSlot.Item.value / 900));
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Red)
			{
				PurchasingItem = ItemID.Ectoplasm; // but have it be even more
				PurchasingAmount = (15 + (int)(_vanillaItemSlot.Item.value / 800));
			}
			if (_vanillaItemSlot.Item.rare is ItemRarityID.Purple)
			{
				PurchasingItem = ItemID.LunarOre; 
			}
			// ^^^^^ This is my last error. The last thing I need to get working. Then I can finally start working on the NPC






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

			const int slotX = 50;
			const int slotY = 270;
			if (!_vanillaItemSlot.Item.IsAir)
			{
				



				string costText = Language.GetTextValue("LegacyInterface.46") + ": ";
				string coinsText = "";
				int[] coins = Utils.CoinsSplit(awesomePrice);
				if (coins[3] > 0)
				{
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
				}
				if (coins[2] > 0)
				{
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
				}
				if (coins[1] > 0)
				{
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
				}
				if (coins[0] > 0)
				{
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
				}
				ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, costText, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, coinsText, new Vector2(slotX + 50 + FontAssets.MouseText.Value.MeasureString(costText).X, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				int reforgeX = slotX + 70;
				int reforgeY = slotY + 40;
				bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;
				Texture2D reforgeTexture = (Texture2D)TextureAssets.Reforge[hoveringOverReforgeButton ? 1 : 0];
				Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
				if (hoveringOverReforgeButton)
				{
					Main.hoverItemName = Language.GetTextValue("LegacyInterface.19");
					if (!tickPlayed)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					tickPlayed = true;
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseLeft && Main.LocalPlayer.CanBuyItem(awesomePrice, -1) && ItemLoader.PreReforge(_vanillaItemSlot.Item))
					{

					// Make custom cost below 
					



						Main.LocalPlayer.BuyItem(awesomePrice, -1);
						bool favorited = _vanillaItemSlot.Item.favorited;
						int stack = _vanillaItemSlot.Item.stack;
						Item reforgeItem = new Item();
						reforgeItem.netDefaults(_vanillaItemSlot.Item.netID);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(_vanillaItemSlot.Item);
						// This is the main effect of this slot. Giving the Awesome prefix 90% of the time and the ReallyAwesome prefix the other 10% of the time. All for a constant 1 gold. Useless, but informative.
						if (Main.rand.NextBool(10))
						{
							reforgeItem.Prefix(PrefixID.Godly);
						}
						else
						{
							reforgeItem.Prefix(PrefixID.Massive);
						}
						_vanillaItemSlot.Item = reforgeItem.Clone();
						_vanillaItemSlot.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(_vanillaItemSlot.Item.width / 2);
						_vanillaItemSlot.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(_vanillaItemSlot.Item.height / 2);
						_vanillaItemSlot.Item.favorited = favorited;
						_vanillaItemSlot.Item.stack = stack;
						ItemLoader.PostReforge(_vanillaItemSlot.Item);
						PopupText.NewText(PopupTextContext.ItemReforge, _vanillaItemSlot.Item, _vanillaItemSlot.Item.stack, true, false);
						SoundEngine.PlaySound(SoundID.Item37);
					}
				}
				else
				{
					tickPlayed = false;
				}
			}
			else
			{
				string message = "Place an item here to Enhance";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}

