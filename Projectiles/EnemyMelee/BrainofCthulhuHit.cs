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
	public class BrainofCthulhuHit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brain of Cthulhu");
		}
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;
		public override void SetDefaults()
		{
			Projectile.alpha = 255;

			Projectile.width = 190;
			Projectile.height = 178;
			Projectile.maxPenetrate = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Default;
			Projectile.damage = 15;
			Projectile.tileCollide = false;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 1;
			
		}
		
        public override bool? CanHitNPC(NPC target)
        {
			if (target.type is NPCID.QueenSlimeBoss or NPCID.HallowBoss)
			{
				return true;
			}
			if (target.type is NPCID.CorruptBunny or NPCID.Corruptor or NPCID.EaterofSouls or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.DevourerBody or NPCID.CorruptSlime or NPCID.Slimer or NPCID.Slimeling or NPCID.DarkMummy or NPCID.CorruptGoldfish or NPCID.Crimslime or NPCID.CrimsonBunny or NPCID.CrimsonGoldfish or NPCID.Crimera or NPCID.BigCrimera or NPCID.CrimsonAxe or NPCID.BigCrimslime or NPCID.Herpling or NPCID.Creeper or NPCID.BrainofCthulhu or NPCID.BloodMummy or NPCID.BloodJelly or NPCID.BloodFeeder or NPCID.BloodCrawler or NPCID.BloodCrawlerWall or NPCID.FaceMonster or NPCID.FloatyGross or NPCID.IchorSticker or NPCID.BloodCrawlerWall or NPCID.DesertGhoulCrimson or NPCID.DesertGhoulCorruption or NPCID.CursedHammer or NPCID.SeekerBody or NPCID.Clinger or NPCID.BigMimicCorruption or NPCID.BigMimicCrimson || target.boss || target.type is NPCID.ServantofCthulhu or NPCID.CultistBoss or NPCID.SkeletronHand or NPCID.VileSpitEaterOfWorlds or NPCID.VileSpit)
			{
				return false;
			}
			if (target.type is NPCID.QueenSlimeBoss or NPCID.HallowBoss or NPCID.Plantera)
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
			owner.ai[0] = Projectile.whoAmI;

		} 

	}
}