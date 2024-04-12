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
	public class RainbowBall : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.WaterStream;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rainbow Dust");
		}
		private int sparkletimer;
		private int ExplodeTimer;
		public override void SetDefaults()
		{
			Projectile.friendly = false;
			Projectile.aiStyle = 1;
			Projectile.damage = 20;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
		}

		int RainbowDust;
		public override void AI()
		{
			
			RainbowDust++;
			if (RainbowDust > 4)
			{
				int projectile = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, Projectile.velocity * 0, ModContent.ProjectileType<SmallRainbowDust>(), 10, 0);
				RainbowDust = 0;
			}

			ExplodeTimer++;

			Projectile.velocity.X /= (1f + (float)ExplodeTimer / 8000);
			Projectile.velocity.Y /= (1f + (float)ExplodeTimer / 8000);
			Projectile.scale /= 1.01f;
			Projectile.Opacity /= 1.01f;

			if (ExplodeTimer > 400 || Projectile.scale < 0.2f || Projectile.Opacity < 0.2f)
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
			public override Color? GetAlpha(Color lightColor)
		{
			Color fairyQueenWeaponsColor = Projectile.GetFairyQueenWeaponsColor();
			return fairyQueenWeaponsColor;
		}

	}

	}
