using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.RGB;

namespace Creaturia.NPCs.Creatures
{

    internal class VioletSapphireHummingbird : ModNPC
    {
        


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruby-Throated Hummingbird");

            Main.npcCatchable[NPC.type] = true;
            NPC.catchItem = (short)ItemType<VioletSapphireHummingbirdItem>();
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlackDragonfly];
            ContentSamples.NpcBestiaryRarityStars[ModContent.NPCType<VioletSapphireHummingbird>()] = 5;
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Position = new Vector2(55f, 40f),
                PortraitPositionXOverride = 140f,
                PortraitPositionYOverride = 40f
            };  
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

        }
       

        public override void SetDefaults()
        {
           // AIType = ModContent.NPCType<HummingBird1>();
            //NPC.CloneDefaults(NPCID.BlackDragonfly);
            NPC.width = 30;
            NPC.height = 28;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.catchItem = (short)ItemType<VioletSapphireHummingbirdItem>();
            NPC.lavaImmune = false;
            //NPC.aiStyle = -1;
            AnimationType = NPCID.BlackDragonfly;
            NPC.friendly = true;
           // NPC.color = Color.Gray;

        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {

            if (player.dontHurtCritters)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            Player owner = Main.player[projectile.owner];

            if (owner != null)
            {
                if (owner.dontHurtCritters)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /* public override void FindFrame(int frameHeight)
		{


			NPC.frameCounter++;

			if (NPC.frameCounter < 1)
			{
				NPC.frame.Y = 0 * frameHeight;
			}
			else if (NPC.frameCounter < 3)
			{
				NPC.frame.Y = 1 * frameHeight;
			}
			else if (NPC.frameCounter < 4)
			{
				NPC.frame.Y = 2 * frameHeight;
			}
			else if (NPC.frameCounter < 5)
			{
				NPC.frame.Y = 3 * frameHeight;
			}
			else
			{
				NPC.frameCounter = 0;
			}







		} */
        bool FlutterMode = true;

       /* public bool FindFlowersTop(int landX, int landY, out int flowerX, out int flowerY)
        {
            flowerX = landX;
            flowerY = landY;
            if (!WorldGen.InWorld(landX, landY, 31))
                return false;

            int num = 1;
            for (int i = landX - 30; i <= landX + 30; i++)
            {
                for (int j = landY - 20; j <= landY + 20; j++)
                {
                    Tile tile = Main.tile[i, j]; // CHANGE TILE TYPE TO GRASSESFLOWERS
                    if (tile != null && tile.HasTile &&
                        (tile.TileType == 3 && tile.TileFrameX > 107 && tile.TileFrameX < 142 || tile.TileType == 3 && tile.TileFrameX >= 162 || tile.TileType == 27 || tile.TileType == 84
                        || tile.TileType == 227 && tile.TileFrameX > 70 && tile.TileFrameX < 130 || tile.TileType == 227 && tile.TileFrameX > 270 && tile.TileFrameX < 410)
                        && Main.rand.NextBool(num))
                    {
                        flowerX = i;
                        flowerY = j;
                        num++;
                    }
                }
            }

            if (flowerX != landX || flowerY != landY)
                return true;

            return false;
        } */
        public override void AI()
        {
			HummingBirdBase.HummingBirdAI(NPC, Mod);
        }

    

		
		

		

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if (Main.time > 20000 && Main.time < 35000 && Main.dayTime)
            {
                return SpawnCondition.OverworldDayBirdCritter.Chance * 0.005f;
            }
            else
            {
                return 0;
            }
        }

		public override void HitEffect(NPC.HitInfo hit)
		{

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, GoreID.ChumBucketFloatingChunks, 0.8f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(-2, 2), NPC.velocity, Mod.Find<ModGore>("HummingbirdGore").Type, 1f);

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-2, 2)), NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2));
                }
            }


        }
        public override void OnKill()
        {
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<HummingbirdBestiary>(), 0, NPC.whoAmI);
        }
        public override void OnCaughtBy(Player player, Item item, bool failed)
        {
			item.stack = 1;

			try
			{
				// I made the hummingbird so long ago that I don't remember where this came from, whether it's ExampleMod or ported from source code.
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0)
				{

					WorldGen.SquareTileFrame(NPCCenter.X, NPCCenter.Y, true);
				}
			}
			catch
			{
				return;
			}
		}
        
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

               BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
               BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
               BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Sun,

               new FlavorTextBestiaryInfoElement("This rare bird is visually striking and highly valuable!")


           }) ; 
        }

      




        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            spriteBatch.End();
            /*  spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, 
                 DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix); */
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, 
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);


            // GOOD DYES SO FAR: PURPLE OOZE DYE, SHIFTING PEARLSANDS DYE, 

            var shader = GameShaders.Armor.GetSecondaryShader(GameShaders.Armor.GetShaderIdFromItemId(ItemID.TwilightDye), Main.LocalPlayer);
            // var shader2 = GameShaders.Armor.GetShaderIdFromItemId(ItemID.PhaseDye);
            //shader.Shader.Parameters["uSaturation"].SetValue(100f);
            //shader.Shader.Parameters["uOpacity"].SetValue(0f);
            //shader.Shader.Parameters["uOpacity"].SetValue(100f);
            shader.Shader.Parameters["uImageSize0"].SetValue(100f);
            shader.Shader.Parameters["uImageSize1"].SetValue(100f);
            //shader.Shader.Parameters["uTime"].SetValue(0f);
            // shader.Shader.Parameters["uSourceRect"].SetValue();
            shader.Shader.Parameters["uDirection"].SetValue(-1f);

            //shader.UseOpacity(1f);
            shader.Apply(null); 
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Player target = Main.player[NPC.target];
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (NPC.direction == 1)
            {
                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
            }
            if (NPC.direction == -1)
            {
                spriteEffects = SpriteEffects.None | SpriteEffects.None;
            }
           


            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos,
            NPC.frame, drawColor, NPC.rotation, // adjusted this shit to be EXACT!
            new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, -2), NPC.scale, spriteEffects, 0f);
            
          /*  for (int i = 0; i < 3; i++)
            { // This is the glowy effect, reminder that gfxOffY is to offset the y posittion for the little outline
                Vector2 circular = new Vector2(0, 0).RotatedBy(NPC.rotation + i * MathHelper.PiOver2);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY) + circular, NPC.frame, new Color(20, 63, 207, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation, new Vector2(TextureAssets.Npc[NPC.type].Value.Width / 2, -2), NPC.scale * 1.03f * (1 + (MathF.Abs(NPC.velocity.X) / 150 + MathF.Abs(NPC.velocity.Y) / 150)), spriteEffects, 0f);
            } */
            /*	spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/DunklerFish_Tail").Value, (NPC.Center + new Vector2(0f, 0f).RotatedBy(NPC.rotation)) - screenPos,
                NPC.frame, new Color(60, 53, 157, 0) * (0.7f + 0.4f * ((255 - NPC.alpha) / 255f)), NPC.rotation,
                new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0f + 144f, TextureAssets.Npc[NPC.type].Value.Height * 0f + 48f), NPC.scale, spriteEffects, 0f);
            */

            /*spriteBatch.Draw(Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/CrimsonFishMiniBoss_EyeGlow").Value, NPC.Center - screenPos,
			NPC.frame, Color.White, NPC.rotation,
			new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f); */
            // ^ Replace with Dunkle glow
            
           
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }

	internal class VioletSapphireHummingbirdItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ruby-Throated Hummingbird");
			// Tooltip.SetDefault("'Pretty!'");
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.Bunny);
            //item.useStyle = 1;
            //item.autoReuse = true;
            //item.useTurn = true;
            //item.useAnimation = 15;
            //item.useTime = 10;
            //item.maxStack = 999;
            //item.consumable = true;
            //item.width = 12;
            //item.height = 12;
            //item.makeNPC = 360;
            //item.noUseGraphic = true;
            //item.bait = 15;

            // Item.bait = 40;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            
			Item.makeNPC = (short)NPCType<VioletSapphireHummingbird>();
		}
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
        
        
            spriteBatch.End();
            /*  spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, 
                 DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix); */
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap,
                DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var shader = GameShaders.Armor.GetSecondaryShader(GameShaders.Armor.GetShaderIdFromItemId(ItemID.TwilightDye), Main.LocalPlayer);
            // var shader2 = GameShaders.Armor.GetShaderIdFromItemId(ItemID.PhaseDye);
            //shader.Shader.Parameters["uSaturation"].SetValue(100f);
            //shader.Shader.Parameters["uOpacity"].SetValue(0f);
            //shader.Shader.Parameters["uOpacity"].SetValue(100f);
            shader.Shader.Parameters["uImageSize0"].SetValue(100f);
            shader.Shader.Parameters["uImageSize1"].SetValue(100f);
            //shader.Shader.Parameters["uTime"].SetValue(0f);
            // shader.Shader.Parameters["uSourceRect"].SetValue();
            shader.Shader.Parameters["uDirection"].SetValue(-1f);

            //shader.UseOpacity(1f);
            shader.Apply(null);
            return true;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {

        spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
        
    }
}