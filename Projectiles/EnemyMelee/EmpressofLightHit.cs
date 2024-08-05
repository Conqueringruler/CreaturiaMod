using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;
using Creaturia.NPCs.Creatures;
using Creaturia.NPCs.Enemies;
using Creaturia.NPCs.Enemies.Boss.FishBosses;

namespace Creaturia.Projectiles.EnemyMelee
{
	public class EmpressofLightHit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Empress of Light");

		}
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;
		public override void SetDefaults()
		{
			Projectile.alpha = 0;

			Projectile.width = 380;
			Projectile.height = 210;
			Projectile.maxPenetrate = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.damage = 50;
			Projectile.tileCollide = false;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 1;
		}
		
        public override bool? CanHitNPC(NPC target)
        {
			
			if (target.type is NPCID.Unicorn or NPCID.Pixie or NPCID.SandsharkHallow or NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.QueenSlimeMinionBlue or NPCID.QueenSlimeMinionPink or NPCID.Gastropod or NPCID.LightMummy or NPCID.RainbowSlime or NPCID.FlyingFish || target.type == ModContent.NPCType<RainbowFish>() || target.boss || target.type is NPCID.GolemHead or NPCID.GolemFistLeft or NPCID.GolemFistRight or NPCID.MoonLordHand or NPCID.MoonLordCore or NPCID.EmpressButterfly)
			{
				return false;
			}
			if (target.type is NPCID.EaterofWorldsHead or NPCID.BrainofCthulhu or NPCID.Plantera)
			{
				return true;
			}
			else return true;
			
		} 
        public override void AI()
		{
			NPC owner = Main.npc[Projectile.owner];
			Projectile.velocity = new Vector2(0, 0);

			Projectile.position = owner.position;


		} 

	}
}