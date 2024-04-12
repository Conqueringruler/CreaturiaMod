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
using Terraria.GameContent.Shaders;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;

namespace Creaturia.Projectiles
{
	public class GravityGloop : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.SandBallFalling;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gravity Gloop");
		}
		private int sparkletimer;
		private int ExplodeTimer;
		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.aiStyle = 1;
			Projectile.damage = 0;
			Projectile.width = 15;
			Projectile.height = 15;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.knockBack = 0f;
			
			
		}

		
       
        public override void AI()
		{
			for (int i = 0; i < 255; i++) // This is the max number of players. 
            {
				Player target = Main.player[i];


				if (target.active && !target.dead && Vector2.Distance(Projectile.Center, target.Center) < 80f)
				{
					target.AddBuff(BuffID.Gravitation, 4000, quiet: false);
				}

			}

		

			Projectile.velocity += new Vector2(0f, -0.36f);
			ExplodeTimer++;
			if (ExplodeTimer > 200)
			{


				ExplodeTimer = 0;
				Projectile.active = false;

			}
			sparkletimer++;
			int dustType = 244;
			var dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-20, 20)), Projectile.width, Projectile.height, dustType, 0, 0, 0, Color.MediumPurple);
			dust.shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
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
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-2, 2)), Projectile.width, Projectile.height, dustType, 0, 0, 60, Color.Purple, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-2, 2)), Projectile.width, Projectile.height, dustType, 0, 0, 60, Color.Purple, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-2, 2)), Projectile.width, Projectile.height, dustType, 0, 0, 60, Color.Purple, 0.5f);
				}
				if (Main.rand.NextBool(4))
				{
					dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-2, 2)), Projectile.width, Projectile.height, dustType, 0, 0, 60, Color.Purple, 0.5f);
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
			return new Color(128, 0, 128, 0) * (0.5f + 0.5f * ((200 - Projectile.alpha) / 255f));
			
		}

	}

}
