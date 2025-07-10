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
using Creaturia.NPCs.Enemies;

namespace Creaturia.NPCs.Creatures
{
	internal class CorruptHamster : ModNPC
	{

		

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plubee");
		

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 1f,
				//Direction = -1

			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
        }
		int spawndust;
		public override void SetDefaults()
		{

			NPC.width = 22;
			NPC.height = 14;
			NPC.damage = 10;
			NPC.defense = 0;
			NPC.lifeMax = 30;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 50;
			NPC.aiStyle = 3;
			AIType = NPCID.CorruptBunny;
			//NPC.ShowNameOnHover = false;


			//AIType = NPCID.DesertBeast;
			
		}
		public override void AI()
		{
			if (MathF.Abs(NPC.velocity.Y) > 4)
			{
				NPC.velocity.Y *= 0.95f;
			}
            if (MathF.Abs(NPC.velocity.X) > 3)
            {
                NPC.velocity.X *= 0.95f;
            }
        }

       

       

		public override void HitEffect(NPC.HitInfo hit)
		{
            for (int i = 0; i < 8; i++)
            {
                Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Water_BloodMoon, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                bloodDust.velocity *= 1.8f;
                bloodDust.velocity.Y *= 0.4f;

                Dust.NewDustPerfect(NPC.Center, DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)), default, Color.Purple, 1.5f);
            }
            if (NPC.life <= 0)
			{
                for (int i = 0; i < 14; i++)
                {
                    Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                    bloodDust.velocity *= 1.8f;
                    bloodDust.velocity.Y *= 0.4f;
                }
               


                    for (int i = 0; i < 8; i++)
                    {
                        Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Water_BloodMoon, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                        bloodDust.velocity *= 1.8f;
                        bloodDust.velocity.Y *= 0.4f;

                        Dust.NewDustPerfect(NPC.Center, DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)), default, Color.DarkRed, 1.5f);
                    }
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.NextFloat(-2, 2)), new Vector2(Main.rand.NextFloat(-1, 1), -2), 88, 1f); 
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.NextFloat(-2, 2)), new Vector2(Main.rand.NextFloat(-1, 1), -2), 88, 1f);
            }
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//if (Main.rand.NextBool(5))
			//{
			//	npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 80, 1, 2));
			//	npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 2, 9));
			//	npcLoot.Add(ItemDropRule.Common(ItemID.SilverCoin, 1, 22, 91));
			//}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;

            SpriteEffects spriteEffects = SpriteEffects.None;


            if (NPC.direction == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally | SpriteEffects.None;
            }

            if (NPC.direction == -1)
            {
                //NPC.spriteDirection = 1;
                spriteEffects = SpriteEffects.None | SpriteEffects.None;
            }

            //  spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, new Color(3, 20, 227, 1) * (0.5f + 0.5f * ((200 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.50f, TextureAssets.Npc[NPC.type].Value.Height * 0.07f), NPC.scale, spriteEffects, 0f);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
             NPC.frame, drawColor, NPC.rotation,
             new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);


            return false;

        }


        public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.CorruptHamster")
            });
        }
	}

	
	}
