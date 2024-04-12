using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;

namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{

	internal class RainbowFishSpawner : ModNPC
	{
		public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeballFlyingFish;

		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
			Main.npcFrameCount[NPC.type] = 4;


		}
		private int FishSpawning;
		private bool spawnspot = true;
		public override void SetDefaults()
		{
			NPC.Opacity = 0;
			NPC.width = 16;
			NPC.height = 16;
			NPC.damage = 35;
			NPC.defense = 2;
			NPC.immortal = true;
			NPC.lifeMax = 1;
			NPC.knockBackResist = 0;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.lavaImmune = false;
			NPC.friendly = false;
			NPC.aiStyle = 44;
			AnimationType = NPCID.EyeballFlyingFish;
		}

		public override void AI()
		{
			if (spawnspot == true)
            {
				NPC.position.Y += 45;
				spawnspot = false;
            }
		NPC.velocity.X = 0;
		NPC.velocity.Y = 0;
			FishSpawning++;
			if (FishSpawning <= 100)
			{
				Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
				if (FishSpawning == 20)
                {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<RainbowFish>(), 0, NPC.whoAmI);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					SoundEngine.PlaySound(SoundID.Splash, NPC.position);
				}
				if (FishSpawning == 40)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<RainbowFish>(), 0, NPC.whoAmI);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					SoundEngine.PlaySound(SoundID.Splash, NPC.position);

				}
				if (FishSpawning == 60)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<RainbowFish>(), 0, NPC.whoAmI);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					SoundEngine.PlaySound(SoundID.Splash, NPC.position);

				}
				if (FishSpawning == 80)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<RainbowFish>(), 0, NPC.whoAmI);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					SoundEngine.PlaySound(SoundID.Splash, NPC.position);
				}
				if (FishSpawning == 100)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<RainbowFish>(), 0, NPC.whoAmI);
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.Water, NPC.velocity.X + Main.rand.Next(-15, 15), NPC.velocity.Y + Main.rand.Next(-15, 15));
					SoundEngine.PlaySound(SoundID.Splash, NPC.position);
					FishSpawning = 0;
					NPC.active = false;
				}
					
				
			}
		
			
		}
		



		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return false;
		}



		




	}


}