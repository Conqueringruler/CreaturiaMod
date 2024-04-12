using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Creaturia.Currencies.FishCurrencies
{
	internal class ChaosFishCurrency : CustomCurrencySingleCoin
	{
		public ChaosFishCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(88, 25, 120);
		}

	}
}