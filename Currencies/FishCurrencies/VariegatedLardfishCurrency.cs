using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Creaturia.Currencies.FishCurrencies
{
	internal class VariegatedLardfishCurrency : CustomCurrencySingleCoin
	{
		public VariegatedLardfishCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(49, 104, 81);
		}

	}
}