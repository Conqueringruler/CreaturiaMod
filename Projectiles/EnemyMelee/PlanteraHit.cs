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

namespace Creaturia.Projectiles.EnemyMelee
{
	public class PlanteraHit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plantera");

		}
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;
		public override void SetDefaults()
		{
			Projectile.alpha = 0;
			Projectile.width = 110;
			Projectile.height = 150;
			Projectile.maxPenetrate = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.damage = 80;
			Projectile.tileCollide = false;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 1;
		}
		
        public override bool? CanHitNPC(NPC target)
        {
			if (target.type is NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Retinazer or NPCID.Spazmatism or NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.SkeletronPrime)
			{
				return true;
			}
			if (target.type is NPCID.JungleSlime or NPCID.SpikedJungleSlime or NPCID.PlanterasTentacle or NPCID.PlanterasHook or NPCID.Hornet or NPCID.GiantTortoise or NPCID.JungleBat or NPCID.Snatcher or NPCID.AngryTrapper or NPCID.Arapaima or NPCID.JungleCreeper or NPCID.Derpling or NPCID.JungleCreeperWall or NPCID.TurtleJungle or NPCID.Frog or NPCID.DoctorBones or NPCID.BigMimicJungle or NPCID.Grubby or NPCID.Sluggy or NPCID.Buggy or NPCID.Moth or NPCID.HornetFatty or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.GiantMossHornet or NPCID.LittleMossHornet or NPCID.Piranha or NPCID.LittleHornetFatty or NPCID.HornetStingy or NPCID.GiantMossHornet or NPCID.LittleHornetLeafy or NPCID.LittleHornetStingy or NPCID.LittleHornetSpikey || target.boss || target.type is NPCID.GolemFistLeft or NPCID.GolemFistRight or NPCID.GolemHead or NPCID.GolemHeadFree or NPCID.FlyingSnake or NPCID.Lihzahrd or NPCID.LihzahrdCrawler)
			{
				return false;
			}
			if (target.type is NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Retinazer or NPCID.Spazmatism or NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.SkeletronPrime)
			{
				return true;
			}
			else
				return true;

		} 
        public override void AI()
		{
			NPC owner = Main.npc[Projectile.owner];
			Projectile.velocity = new Vector2(0, 0);

			Projectile.position = owner.position;

		} 

	}
}