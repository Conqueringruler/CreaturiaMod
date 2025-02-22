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
	public class NinjasShuriken : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Ninja's Shuriken");
		}
		private int ExplodeTimer;
		public override void SetDefaults()
		{
			AIType = ProjectileID.Shuriken;
			Projectile.CloneDefaults(ProjectileID.Shuriken);
			Projectile.friendly = true;
            
		}
		
		public override void AI()
		{
			
			ExplodeTimer++;
			if (ExplodeTimer > 1)
			{

				var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 - 10, Projectile.velocity.Y * 0.1f, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 2, Projectile.velocity.Y * 0 + 2, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 2, Projectile.velocity.Y * 0.1f - 2, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0 + 5, Projectile.velocity.Y * 0, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.1f - 2, Projectile.velocity.Y * 0.1f + 8, 60, Color.LightGray, 1f);
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0, Projectile.velocity.Y * 0 - 10, 60, Color.LightGray, 1f);
				int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity, ProjectileID.Shuriken, Projectile.damage, Projectile.knockBack, Projectile.owner);
				projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 1.3f, ProjectileID.Shuriken, Projectile.damage, Projectile.knockBack, Projectile.owner);
				projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 0.8f, ProjectileID.Shuriken, Projectile.damage, Projectile.knockBack, Projectile.owner);
				ExplodeTimer = 0;
				Projectile.active = false;
				
			}


		}

	}
}