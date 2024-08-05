using Microsoft.Xna.Framework;
using System;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
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
using Terraria.GameContent;
using Creaturia.Items;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ReLogic.Content;
using Terraria.GameContent.ItemDropRules;

namespace Creaturia.NPCs.Enemies.Boss.FishBosses
{
	
	public class RainbowFish : ModNPC
	{
		

		public int RainbowDust;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rainbow Fish");
			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.TrailCacheLength[NPC.type] = 5; // 
			NPCID.Sets.TrailingMode[NPC.type] = 0; // The recording mode which idk what that means
			NPCID.Sets.HurtingBees[NPC.type] = true;


			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Bleeding,
					BuffID.Confused
				}
			};

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				CustomTexturePath = "Creaturia/NPCs/Enemies/Boss/FishBosses/RainbowFish_Bestiary",
				Position = new Vector2(0f, -8f),
				//PortraitPositionXOverride = 30f,
				PortraitPositionYOverride = -32f,
				PortraitScale = 0.50f,
				Scale = 0.25f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		private int difficultyhealth = 1500;
		public override void SetDefaults()
		{
			NPC.width = 46;
			NPC.height = 26;
			NPC.damage = 40;
			NPC.defense = 33;
			NPC.lifeMax = difficultyhealth;
			NPC.knockBackResist = 0.1f;
			NPC.HitSound = SoundID.NPCHit44;

			NPC.friendly = false;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.lavaImmune = false;
			NPC.dontTakeDamageFromHostiles = false;
			NPC.aiStyle = 44;
			AnimationType = NPCID.EyeballFlyingFish;
			NPC.value = Item.buyPrice(silver: 85);

			NPC.boss = true;
			if (!Main.dedServ)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/underwater_fishy_terrariaminiboss");
			}
		}
		int aimandshootstream;
		int shoot;
		int shoottime;

		public override bool PreKill()
		{
			if (NPC.FindFirstNPC(ModContent.NPCType<RainbowFish>()) > 1)
            {

			NPC.boss = false;

		}
            else
            {
				NPC.GivenName = "The Rainbow Fish Swarm";
				NPC.boss = true;
			}
			return true;
		}
		bool DidSoundYet = false;
		public override void AI()
		{
           
            float red = (float)Main.DiscoR / 150f;
			float green = (float)Main.DiscoG / 150f;
			float blue = (float)Main.DiscoB / 150f;
			Color Rainbow = new Color(red, green, blue);
            
            red *= 1f;
			green *= 1f;
			blue *= 1f;


            Color ReducedRainbow = Color.Lerp(new Color(red, green, blue), Color.White, 0.5f);
            NPC.color = ReducedRainbow;

            Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), red, green, blue);
			Player target = Main.player[NPC.target];
			Vector2 directiony = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitY);
			Vector2 directionshoot = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
			if (!Main.expertMode && !Main.masterMode)
			{
				difficultyhealth = 1800;
				shoottime = 85;
				
			}
			if (Main.expertMode)
            {
				difficultyhealth = 4000;
				shoottime = 60;
			}
			if (Main.masterMode)
            {
				difficultyhealth = 7000;
				shoottime = 45;
            }
			if (NPC.life <= (difficultyhealth / 6) && (Math.Abs(NPC.velocity.X) < 10)) 
				{
					NPC.velocity += new Vector2(NPC.direction * -1.05f, 0);
					NPC.velocity.Y += (NPC.velocity.Y * 6.95f);
                // Make their hp slowly recover when running
				if (DidSoundYet == false)
				{
					SoundEngine.PlaySound(new SoundStyle("Creaturia/Assets/Sounds/RainbowFocus"), NPC.Center);
					DidSoundYet = true;
				}
				NPC.EncourageDespawn(1000);
				NPC.netUpdate = true;
			}



			if (Math.Abs(NPC.velocity.X) < 7 && (NPC.life >= difficultyhealth / 5))
			{
				NPC.velocity += new Vector2(NPC.direction * 1.06f, NPC.directionY * 2.5f);
				NPC.netUpdate = true;
			}
			
			if (aimandshootstream < 850)
            {
				RainbowDust++;
			}
			
			if (RainbowDust > 5 && aimandshootstream < 800)
			{
				if (shoot < shoottime)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, NPC.velocity * 0, ModContent.ProjectileType<RainbowDust>(), 30, 0);
					}
				}
				RainbowDust = 0;
			}
			if (aimandshootstream < 850)
            {
				NPC.rotation += NPC.velocity.X;
				NPC.knockBackResist = 0.1f;
			}
			aimandshootstream++;
			if (aimandshootstream > 850)
            {
				NPC.knockBackResist = 0f;
				NPC.velocity /= 2;
				NPC.rotation = NPC.AngleTo(target.position);
				shoot++;
				NPC.netUpdate = true;
			}
			if (shoot > shoottime)
            {
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, directionshoot * 11f, ModContent.ProjectileType<RainbowBall>(), 30, 0);
				}
				shoot = 0;
				SoundEngine.PlaySound(SoundID.Item166, NPC.Center);
			}
			if (aimandshootstream >= 1200)
            {
				aimandshootstream = 0;
				shoot = 0;
            }
		}
		
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
				new FlavorTextBestiaryInfoElement("The Rainbow Fish survives the chaos of the Hallow by staying in groups, releasing fragments of its sharp scales to cut into any potential threats - or prey.")
			});
		}



		

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			//npcLoot.Add(ItemDropRule.Common(ItemID.HallowedKey, 15, 1, 1));

			//npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 0, 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowScale2>(), 1, 2, 5));
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowScale2>(), 50 , 0, 7)); //\This new method is cock and balls, don't forget to use terraria.lootshit so stuff can drop and also 1 = 100% chance of dropping, 100 = 1% chance of dropping for some stupid reason
			//npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 50, 1, 2));
		}
        public override void OnKill()
        {
				if (NPC.FindFirstNPC(ModContent.NPCType<RainbowFish>()) <= 1)
				{
					
				}

			Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.CandyApple, Main.rand.Next(0, 2));
		}

        public override void HitEffect(NPC.HitInfo hit)
		{
			Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-15, 15)), NPC.width, NPC.height, DustID.RainbowMk2, NPC.velocity.X + Main.rand.Next(-10, 10), NPC.velocity.Y + Main.rand.Next(-10, 10));

			if (NPC.life <= 0)
			{

			}
			

		}


	

	}


}