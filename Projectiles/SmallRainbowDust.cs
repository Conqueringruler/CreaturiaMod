using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using Creaturia.Projectiles;

namespace Creaturia.Projectiles
{
	public class SmallRainbowDust : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MudBall;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rainbow Dust");
		}
		private int sparkletimer;
		private int ExplodeTimer;
		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.Opacity = 0;
			Projectile.aiStyle = 44;
			Projectile.damage = 20;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.hostile = true;
		}

		public override void AI()
		{

			ExplodeTimer++;
			if (ExplodeTimer > 150)
			{

				
				ExplodeTimer = 0;
				Projectile.active = false;

			}
			sparkletimer++;
			int dustType = Main.rand.Next(59, 65);
			var dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-20, 20)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Main.DiscoColor);
			dust.noGravity = true;
			if (sparkletimer > 35)
            {
				/*	//int dustsize = Main.rand.Next((int)0.3, (int)1.5);
					int dust = Dust.NewDust(Projectile.position + new Vector2(Main.rand.Next(-90, 90), Main.rand.Next(-90, 90)), Projectile.width, Projectile.height, DustID.RainbowRod, 0, 0, 40);
					dust = Dust.NewDust(Projectile.position + new Vector2(Main.rand.Next(-90, 90), Main.rand.Next(-90, 90)), Projectile.width, Projectile.height, DustID.RainbowRod, 0, 0, 40);
					dust = Dust.NewDust(Projectile.position + new Vector2(Main.rand.Next(-90, 90), Main.rand.Next(-90, 90)), Projectile.width, Projectile.height, DustID.RainbowRod, 0, 0, 40);
					dust = Dust.NewDust(Projectile.position + new Vector2(Main.rand.Next(-90, 90), Main.rand.Next(-90, 90)), Projectile.width, Projectile.height, DustID.RainbowRod, 0, 0, 40);

					sparkletimer = Main.rand.Next(-10, 10); */
				
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-7, 7)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Main.DiscoColor, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-7, 7)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Main.DiscoColor, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-7, 7)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Main.DiscoColor, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-7, 7)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Main.DiscoColor, 0.5f);
				}

				dust.velocity.X = Main.rand.NextFloat(-0.01f, 0.01f);
				dust.velocity.Y = Main.rand.NextFloat(-0.01f, 0.01f);
				dust.noGravity = true;
				dust.scale *= 1f + Main.rand.NextFloat(-0.06f);
				sparkletimer = 0;
			}
			dust.alpha -= ExplodeTimer / 40;
		}

	}
}