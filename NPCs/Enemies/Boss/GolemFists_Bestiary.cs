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

namespace Creaturia.NPCs.Enemies.Boss
{
	internal class GolemFists_Bestiary : ModNPC
	{

		//public override string Texture => "Creaturia/NPCs/Enemies/Boss/GolemFists_Bestiary";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plubee");
		
           

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 1f,
                //Direction = -1e
                PortraitScale = 0.3f,
				PortraitPositionXOverride = 5f,
				PortraitPositionYOverride = 70f,
                Scale = 0.35f,
				Position = new Vector2(-65, 110)

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

			NPC.width = 600;
			NPC.height = 600;
			NPC.damage = 80;
			NPC.defense = 0;
			NPC.lifeMax = 1;
			//NPC.HitSound = SoundID.NPCHit1;
			//NPC.DeathSound = SoundID.NPCDeath52;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
            //AnimationType = NPCID.Bunny;
           
			//NPC.value = 132232;
			//AIType = NPCID.DesertBeast;
			NPC.ShowNameOnHover = false;
           
        }
		public override void AI()
		{
            //NPC.SimpleStrikeNPC(9999, 0, false, 0f, DamageClass.Default);
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
			NPC.StrikeInstantKill();
			}
        }
      

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
		
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

			if (NPC.IsABestiaryIconDummy)
			{
				if (NPC.scale == 0.3f) // So it's only visible in the portrait
				{

				
				spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/GolemFists_Bestiary_Alt3").Value, (NPC.Center + new Vector2(0, 180)) - screenPos,
	   NPC.frame, new Color(255, 255, 255, 50), NPC.rotation,
	   new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, SpriteEffects.None, 0f);
			}
            }

            /*Main.instance.LoadNPC(NPCID.Pumpking);
			Main.instance.LoadNPC(NPCID.IceQueen);
			var pumTexture = TextureAssets.Npc[NPCID.Pumpking].Value;
			var iceTexture = TextureAssets.Npc[NPCID.IceQueen].Value;

            spriteBatch.Draw(pumTexture, (NPC.Center + new Vector2(-3, 0)) - screenPos,
       NPC.frame, new Color(255, 255, 255, 60), NPC.rotation,
       new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, SpriteEffects.None, 0f);

            spriteBatch.Draw(iceTexture, (NPC.Center + new Vector2(4, 0)) - screenPos,
            NPC.frame, new Color(255, 255, 255, 60), NPC.rotation,
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, SpriteEffects.None, 0f);
			*/
            return true;
        }
        public override void OnKill()
        {
            Main.BestiaryTracker.Kills.RegisterKill(NPC); // Need to specify since StrikeNPC I don't think would count as a player kill
        }

      
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            ContentSamples.NpcBestiaryRarityStars[ModContent.NPCType<GolemFists_Bestiary>()] = 4;
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple,
				
				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.GolemFists")
			//	new FlavorTextBestiaryInfoElement("Once only parts of Golem, these fists have been possessed and empowered, one by the King of Fright and one by the Queen of Frost. Defeating either of those two will surely break the fist's spell and return said fist to normal...")
            });
        }
    }

	
	}
