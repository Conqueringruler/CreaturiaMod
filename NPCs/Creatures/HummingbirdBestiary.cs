using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;

namespace Creaturia.NPCs.Creatures
{
	internal class HummingbirdBestiary : ModNPC
	{

		public override string Texture => "Creaturia/NPCs/Creatures/HummingBird1";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plubee");
			Main.npcCatchable[NPC.type] = false;
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlackDragonfly];
            ContentSamples.NpcBestiaryRarityStars[ModContent.NPCType<HummingbirdBestiary>()] = 2;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 1f,
                //Direction = -1
                PortraitScale = 1f,
				PortraitPositionXOverride = 0f,
				PortraitPositionYOverride = 0f,
                Scale = 1f,
				Position = new Vector2(-4, 15)

            };
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

		}
		int spawndust;
      /*  public override bool PreAI() // ENABLE AFTER TESTING
        {
            NPC.Opacity = 0f;
            NPC.alpha = 255;
            return base.PreAI();
        } */
        public override void SetDefaults()
		{

			NPC.width = 30;
			NPC.height = 28;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			//NPC.HitSound = SoundID.NPCHit1;
			//NPC.DeathSound = SoundID.NPCDeath52;
			NPC.stepSpeed = 30;
			NPC.aiStyle = -1;
			NPC.dontTakeDamageFromHostiles = true;
            AnimationType = NPCID.BlackDragonfly;
            //AnimationType = NPCID.Bunny;
            NPC.color = Color.DeepSkyBlue;
			//NPC.ShowNameOnHover = false;
			NPC.stepSpeed = 300;
			//NPC.value = 132232;
			//AIType = NPCID.DesertBeast;
			NPC.alpha = 255;
			NPC.Opacity = 0;
			NPC.ShowNameOnHover = false;
			NPC.color = new Color(0, 0, 0, 255);
           
        }
		public override void AI()
		{
			NPC.Opacity = 0f;
			NPC.alpha = 255;
            //NPC.SimpleStrikeNPC(9999, 0, false, 0f, DamageClass.Default);
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
			NPC.StrikeInstantKill();
			}
        }
      

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
		if (Main.hardMode)
            {
				return SpawnCondition.OverworldNight.Chance * 0.002f;
			}
		else
            {
				return SpawnCondition.OverworldNight.Chance * 0.005f;
			}
			
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
		
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
		
		}

        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(NPC); // Need to specify since StrikeNPC I don't think would count as a player kill
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Player target = Main.player[NPC.target];
            SpriteEffects spriteEffects = SpriteEffects.None;











            if (NPC.position.X > target.position.X)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.None;
            }

            if (NPC.position.X < target.position.X)
            {
                //NPC.spriteDirection = 1;
                spriteEffects = SpriteEffects.None | SpriteEffects.None;

                //SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            }

           /* for (int i = 0; i < 4; i++)
            { // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
                Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(150, 100, 70, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
            }*/

			if (NPC.IsABestiaryIconDummy)
			{

			
            spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Creatures/HummingBird2").Value, (NPC.Center + new Vector2(-30, -24)) - screenPos,
            NPC.frame, drawColor, NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

            spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Creatures/LeafyHummingbird").Value, (NPC.Center + new Vector2(40, 12)) - screenPos,
            NPC.frame, drawColor, NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

            spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Creatures/Butter1Hummingbird").Value, (NPC.Center + new Vector2(35, -32)) - screenPos,
           NPC.frame, drawColor, NPC.rotation,
           new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
            spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Creatures/PurpleHeadedHummingbird").Value, (NPC.Center + new Vector2(-35, 22)) - screenPos,
         NPC.frame, drawColor, NPC.rotation,
         new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

            spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Creatures/HummingBird1").Value, NPC.Center - screenPos,
            NPC.frame, Color.White, NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);
            }
            return false;
        }
        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
		{
           // var bird1entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<HummingBird1>());
			//Main.BestiaryDB.Register(bestiaryEntry);
			
            /*var bird2entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<HummingBird2>());
            var bird3entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<Butter1Hummingbird>());
            var bird4entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<LeafyHummingbird>());
			var bird5entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<PurpleHeadedHummingbird>());
            var bird6entry = Main.BestiaryDB.FindEntryByNPCID(ModContent.NPCType<TorchwoodHummingbird>());
			if (bird1entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
	            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<HummingBird1>()], quickUnlock: true);
			}

			if (bird2entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<HummingBird2>()], quickUnlock: true);
			}
			if (bird3entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<Butter1Hummingbird>()], quickUnlock: true);
			}
			if (bird4entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<LeafyHummingbird>()], quickUnlock: true);
			}
			if (bird5entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<PurpleHeadedHummingbird>()], quickUnlock: true);
			}
			if (bird6entry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<TorchwoodHummingbird>()], quickUnlock: true);
			} */
            // Use AddRange instead of calling Add multiple times
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.Hummingbirds")
            });
		}
	}

	
	}
