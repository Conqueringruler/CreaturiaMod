using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Creaturia.Currencies.FishCurrencies
{
	internal class TunaCurrency : CustomCurrencySingleCoin
	{
		public TunaCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(46, 49, 47);
		}

	}
}