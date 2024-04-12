using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Creaturia.Currencies.FishCurrencies
{
	internal class RainbowScaleCurrency : CustomCurrencySingleCoin
	{
		public RainbowScaleCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = Color.Pink;
		}

	}
}