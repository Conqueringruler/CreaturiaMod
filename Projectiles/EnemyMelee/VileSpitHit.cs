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
	public class VileSpitHit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vile Spit");
		}
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;
		public override void SetDefaults()
		{
			Projectile.alpha = 0;

			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.maxPenetrate = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.damage = 8;
			Projectile.tileCollide = false;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 1;
		}
		
        public override bool? CanHitNPC(NPC target)
        {
			if (target.type == NPCID.QueenSlimeBoss || target.type == NPCID.HallowBoss)
			{
				return true;
			}
			if (target.type == NPCID.CorruptBunny || target.type == NPCID.Corruptor || target.type == NPCID.EaterofSouls || target.type == NPCID.EaterofWorldsBody || target.type == NPCID.EaterofWorldsHead || target.type == NPCID.EaterofWorldsTail || target.type == NPCID.DevourerBody || target.type == NPCID.CorruptSlime || target.type == NPCID.Slimer || target.type == NPCID.Slimeling || target.type == NPCID.DarkMummy || target.type == NPCID.CorruptGoldfish || target.type == NPCID.Crimslime || target.type == NPCID.CrimsonBunny || target.type == NPCID.CrimsonGoldfish || target.type == NPCID.Crimera || target.type == NPCID.BigCrimera || target.type == NPCID.CrimsonAxe || target.type == NPCID.BigCrimslime || target.type == NPCID.Herpling || target.type == NPCID.Creeper || target.type == NPCID.BrainofCthulhu || target.type == NPCID.BloodMummy || target.type == NPCID.BloodJelly || target.type == NPCID.BloodFeeder || target.type == NPCID.BloodCrawler || target.type == NPCID.BloodCrawlerWall || target.type == NPCID.FaceMonster || target.type == NPCID.FloatyGross || target.type == NPCID.IchorSticker || target.type == NPCID.BloodCrawlerWall || target.type == NPCID.DesertGhoulCrimson || target.type == NPCID.DesertGhoulCorruption || target.type == NPCID.CursedHammer || target.type == NPCID.SeekerBody || target.type == NPCID.Clinger || target.type == NPCID.BigMimicCorruption || target.type == NPCID.BigMimicCrimson || target.boss || target.type == NPCID.ServantofCthulhu || target.type == NPCID.CultistBoss || target.type == NPCID.SkeletronHand || target.type == NPCID.VileSpitEaterOfWorlds || target.type == NPCID.VileSpit)
			{
				return false;
			}
			if (target.type == NPCID.QueenSlimeBoss || target.type == NPCID.HallowBoss || target.type == NPCID.Plantera)
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