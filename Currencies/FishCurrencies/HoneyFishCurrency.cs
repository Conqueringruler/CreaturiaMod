using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Creaturia.Currencies.FishCurrencies
{
	internal class HoneyFishCurrency : CustomCurrencySingleCoin
	{
		public HoneyFishCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(126, 58, 2);
		}

	}
}