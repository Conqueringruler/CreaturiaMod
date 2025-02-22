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
using Terraria.Graphics;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Creaturia.NPCs.Creatures
{
	internal class DungeonFrog : ModNPC
	{


		//public override string Texture => "Terraria/Images/NPC_" + NPCID.Frog;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ectoad");
			Main.npcCatchable[NPC.type] = true;
			Main.npcFrameCount[NPC.type] = 12;
			NPCID.Sets.CountsAsCritter[Type] = true;

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        { // frog runs now in bestiary!! yay :D
            Velocity = -1f
        };
        public override void SetDefaults()
		{
			NPC.width = 40;
			NPC.height = 30;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.scale = 1f;
			NPC.stepSpeed = 100;
			//NPC.catchItem = (short)ItemType<JackrabbitItem>();
			NPC.lavaImmune = false;
			NPC.aiStyle = 7;
			NPC.dontTakeDamageFromHostiles = true;
			//AnimationType = NPCID.Frog;
			NPC.ShowNameOnHover = false;
			NPC.color = Color.SlateBlue;
		}
		public override void AI()
		{
			base.AI();
			NPC.ShowNameOnHover = false;
			Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 1f);

		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Player target = Main.player[NPC.target];
			SpriteEffects spriteEffects = SpriteEffects.None;





			if (NPC.spriteDirection == 1)
			{
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//spriteEffects = SpriteEffects.FlipHorizontally;
				//NPC.spriteDirection = -1;

				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
			}

			if (NPC.spriteDirection == 0)
			{
				//NPC.spriteDirection = 1;
				spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;

				//SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

			}



			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + new Vector2(0, -18), NPC.frame, Color.White, NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.019f), NPC.scale, spriteEffects, 0f);


			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + new Vector2(0, -18), NPC.frame, new Color(20, 50, 200, 0) * (0.2f + 0.5f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.019f), NPC.scale, spriteEffects, 0f);



		/*	for (int i = 0; i < 4; i++)
			{ // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
				Vector2 circular = new Vector2(2, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
				spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos  + new Vector2(0, NPC.gfxOffY) + circular , NPC.frame, new Color(20, 50, 200, 0) * (0.2f + 0.5f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.019f), NPC.scale, spriteEffects, 0f);
			} */


			
			return false;
		} 

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{



			if (NPC.downedPlantBoss == true)
            {
				return SpawnCondition.Dungeon.Chance * 0.06f;
			}
			else return SpawnCondition.Dungeon.Chance * 0f;
		}
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override bool CanHitNPC(NPC target)/* tModPorter Suggestion: Return true instead of null */
        {
            return false;
        }
        public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
                for (int i = 0; i < 30; i++)
                {
                  var dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.BlueTorch, NPC.velocity.X + Main.rand.Next(-10, 10), NPC.velocity.Y + Main.rand.Next(-10, 10), 30, Color.White, Main.rand.NextFloat(1f, 1.9f));
					//dust.noGravity = true;
					dust.velocity.X *= 0.95f;
                    dust.velocity.Y *= 0.95f;
					dust.noGravity = true;
					
                }
                for (int i = 0; i < 30; i++)
                {
                    var dust = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), NPC.width, NPC.height, DustID.Firework_Blue, NPC.velocity.X + Main.rand.Next(-10, 10), NPC.velocity.Y + Main.rand.Next(-10, 10), 30, Color.White, Main.rand.NextFloat(1f, 1.4f));
                    //dust.noGravity = true;
                    dust.velocity.X *= 0.99f;
                    dust.velocity.Y *= 0.99f;
					

                }
				//Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
				//	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
				SoundEngine.PlaySound(SoundID.NPCDeath39);
            }
		}

        /*public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			if (Main.rand.NextBool(5))
			{
				//	Item.NewItem(NPC.getRect(), ItemID.Leather);
			}
		} */

        /*public override void OnCaughtBy(Player player, Item item, bool failed)
		{
			item.stack = 1;

			try
			{
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0)
				{
					//	Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType = (byte)Main.rand.Next(50, 150);
					WorldGen.SquareTileFrame(NPCCenter.X, NPCCenter.Y, true);
				}
			}
			catch
			{
				return;
			}
		} */
        int num = 1;
        public override void FindFrame(int frameHeight)
        {
            NPC.position += NPC.netOffset;
            
            if (!Main.dedServ)
            {
                if (!TextureAssets.Npc[NPC.type].IsLoaded)
                {
                    return;
                }
                num = TextureAssets.Npc[NPC.type].Height() / Main.npcFrameCount[NPC.type];
            }
            int num2 = 0;
            if (NPC.aiAction == 0)
            {
                num2 = ((NPC.velocity.Y < 0f) ? 2 : ((NPC.velocity.Y > 0f) ? 3 : ((NPC.velocity.X != 0f) ? 1 : 0)));
            }
            else if (NPC.aiAction == 1)
            {
                num2 = 4;
            }
            NPC.spriteDirection = NPC.direction;
			if (!NPC.wet)
			{

				if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
				{
					NPC.frame.Y = 0;
				}
				if (NPC.velocity.X > 0.05f || NPC.velocity.X < -0.05f)
				{
					NPC.frameCounter++;
					if (NPC.frameCounter < 5)
					{
						NPC.frame.Y = 6 * num;
					}
					else if (NPC.frameCounter < 10)
					{
						NPC.frame.Y = 7 * num;
					}
					else if (NPC.frameCounter < 15)
					{
						NPC.frame.Y = 8 * num;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = 9 * num;
					}
					else if (NPC.frameCounter < 25)
					{
						NPC.frame.Y = 10 * num;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = 11 * num;
					}
					else if (NPC.frameCounter < 35)
					{
						NPC.frame.Y = 11 * num;
						NPC.frameCounter = 0;
					}
					if (NPC.frameCounter > 35)
					{
						NPC.frameCounter = 0;
					}

					//NPC.frame.Y = num * 10;

				}
			}
			/*
            if (NPC.wet)
            {
                NPC.frameCounter = 0.0;
                if (NPC.velocity.X > 0.25f || NPC.velocity.X < -0.25f)
                {
					
                    NPC.frame.Y = num * 10;
                }
                else if (NPC.velocity.X > 0.15f || NPC.velocity.X < -0.15f)
                {
                    NPC.frame.Y = num * 11;
                }
                else
                {
                    NPC.frame.Y = num * 12;
                }
				
            }
            if (NPC.velocity.Y == 0f && NPC.velocity.X == 0f)
            {
                if (NPC.velocity.X == 0f)
                {
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 6.0)
                    {
                        NPC.frameCounter = 0.0;
                        NPC.frame.Y += num;
                    }
                    if (NPC.frame.Y > num * 5)
                    {
                        NPC.frame.Y = 0;
                    }
                    
                }
                NPC.frameCounter += 1.0;
                int num216 = 6;
                if (NPC.frameCounter < (double)num216)
                {
                    NPC.frame.Y = 0;
                    
                }
                if (NPC.frameCounter < (double)(num216 * 2))
                {
                    NPC.frame.Y = num * 6;
                    
                }
                if (NPC.frameCounter < (double)(num216 * 3))
                {
                    NPC.frame.Y = num * 8;
                    
                }
                NPC.frame.Y = num * 9;
                if (NPC.frameCounter >= (double)(num216 * 4 - 1))
                {
                    NPC.frameCounter = 0.0;
                }
            }
            else if (NPC.velocity.Y > 0f)
            {
                NPC.frame.Y = num * 9;
            }
            else
            {
                NPC.frame.Y = num * 8;
            }
			*/
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Use AddRange instead of calling Add multiple times
			
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
				
				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.,
				new FlavorTextBestiaryInfoElement("A mysterious inhabitant of the dungeon, subsisting off things that it really shouldn't.")
			});
		}
	}

	/*internal class DungeonFrogItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DungeonFrogItem");
		}

		public override void SetDefaults()
		{
			//item.useStyle = 1;
			//item.autoReuse = true;
			//item.useTurn = true;
			//item.useAnimation = 15;
			//item.useTime = 10;
			//item.maxStack = 999;
			//item.consumable = true;
			Item.width = 28;
			Item.height = 36;
			//item.makeNPC = 360;
			//item.noUseGraphic = true;
			//item.bait = 15;

			Item.CloneDefaults(ItemID.GlowingSnail);
			Item.makeNPC = (short)NPCType<DungeonFrog>();
		}
	} */
}