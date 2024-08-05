using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;

namespace Creaturia.Projectiles
{
	public class HoopSnakePoison : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hoop Snake Poison");
			Main.projFrames[Projectile.type] = 3;
		}
		private int DustTimer;
		public override void SetDefaults()
		{
			
			Projectile.width = 10;
			Projectile.height = 10;

			Projectile.damage = 5;
			AIType = ProjectileID.JungleSpike;
			Projectile.CloneDefaults(ProjectileID.JungleSpike);
		}
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			target.AddBuff(BuffID.Poisoned, 150);
		}
       
        public override void AI()
		{
			
			if (Main.masterMode)
            {
				Projectile.hostile = true;
				Projectile.friendly = false;
			}
			if (!Main.masterMode)
			{
				Projectile.friendly = true;
				Projectile.hostile = false;
			}
			DustTimer++;
			if (DustTimer > 4)
			{

				var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.Green, 1.5f);
				DustTimer = 0;
			}
			

		}
		

	}
}