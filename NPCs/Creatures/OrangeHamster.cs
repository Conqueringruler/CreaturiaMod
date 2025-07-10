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
using rail;
using Terraria.GameContent.UI.Elements;

namespace Creaturia.NPCs.Creatures
{
    internal class OrangeHamster : ModNPC
    {


        //public override string Texture => "Terraria/Images/NPC_" + NPCID.Frog;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ectoad");
            Main.npcCatchable[NPC.type] = true;
           
            NPCID.Sets.CountsAsCritter[Type] = true;
            NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[Type] = true;
            NPCID.Sets.TownCritter[Type] = true;

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Shimmerfly;
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        { // frog runs now in bestiary!! yay :D
            Velocity = -1f
        };
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 14;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.scale = 1f;
          //  NPC.stepSpeed = 100;
            //NPC.catchItem = (short)ItemType<JackrabbitItem>();
            NPC.aiStyle = 7;
            NPC.CloneDefaults(NPCID.Bunny);
            //AnimationType = NPCID.Frog;
        }
        int num = 1;
        int checkchat;
        int FallTimeTracker = 0;
        int HeatTracker = 0;
        int WaterTracker = 0;
        bool DiedFromFalling = false;
        bool DiedFromCooking = false;
        bool DiedFromWater = false;
        int LastSavedDirection = 1;

        int explodesoontimer = 0;
        int jitter = 0;
        int exploding = 0;

        int SlowlyReduceTimer;

        int Corrupting;

        float yAdjustment;
        public override void AI()
        {



            // FOR BUNNY AI: 
            // NPC.AI[0] IS IF MOVING, 1 IF MOVING, 0 IF NOT MOVING
            // NPC.AI[1] IS THE TIMER TO CHANGE DIRECTIONS/STOP






            /*
            checkchat++;
            if (checkchat > 4)
            {

            
            Main.NewText("NPC.AI[0] = " + NPC.ai[0]);
            Main.NewText("NPC.AI[1] = " + NPC.ai[1]);
            Main.NewText("NPC.AI[2] = " + NPC.ai[2]);
                checkchat = 0;
        } */
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {


                if (NPC.ai[1] > 100)
                {
                    NPC.ai[1] = Main.rand.Next(50, 100);
                }
                if (NPC.ai[0] != 0 && MathF.Abs(NPC.velocity.Y) < 0.05f)
                {


                    if (MathF.Abs(NPC.velocity.X) > 1.5f)
                    {

                    }
                    else
                    {
                        if (NPC.direction == 1)
                        {
                            NPC.velocity.X += 0.45f;
                        }
                        if (NPC.direction == -1)
                        {
                            NPC.velocity.X += -0.45f;
                        }
                    }
                }
                if (NPC.ai[0] == 0)
                {
                    NPC.velocity.X /= 2;
                }

               
                //    float xAdjustment = ((24 * MathF.Sin(Main.GlobalTimeWrappedHourly * 93)));

                    
                        yAdjustment = -20 + (35 * MathF.Sin(Main.GlobalTimeWrappedHourly * 40)); // Keeping y-adjustment so things like fireplaces/campfires still work



             




              
            }
            SlowlyReduceTimer++;

            int num173 = (int)((NPC.position.X + (float)((NPC.width / 2) /*+ xAdjustment */)) / 16f);
                int num174 = (int)((NPC.position.Y + (float)NPC.height + yAdjustment) / 16f);



            // CORRUPTING SECTION -----------------

            if (Main.tile[num173, num174].TileType is TileID.CorruptGrass or TileID.CorruptSandstone or TileID.Ebonsand or TileID.CorruptIce or TileID.CorruptJungleGrass or TileID.CorruptPlants or TileID.CorruptThorns or TileID.CorruptVines
                or TileID.Ebonstone or TileID.CrimsonGrass or TileID.CrimsonPlants or TileID.CrimsonVines or TileID.Crimstone or TileID.Crimsand or TileID.CrimsonVines or TileID.CrimsonHardenedSand or TileID.FleshIce)
            {
                Corrupting += 2;
                explodesoontimer += 2;
            }
            else
            {
                if (SlowlyReduceTimer > 5)
                {
                 Corrupting--;
                    // In Heat it gets set to 0. If set to 0 here then reduce heat never happens
                }
            }

        //    Dust.NewDustDirect(new Vector2(num173, num174), NPC.width, NPC.height, DustID.FireworkFountain_Blue, Main.rand.NextFloat(-0.1f, 0.1f), -0.9f, 110, Color.White, 10f);


           
            if (Corrupting < 0)
            {
                Corrupting = 0;
            }
            if (Corrupting > 0)
            {
                var corrptsmoke = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-2, 2)), NPC.width, NPC.height, DustID.Corruption, Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, 0.3f), 160, Color.White, Main.rand.NextFloat(0.7f, 1f));

                corrptsmoke.alpha += 1;

                corrptsmoke.noGravity = true;
            }
            if (Corrupting >= 50)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {  
                    
                    SoundStyle screech = SoundID.DD2_DarkMageDeath;
                    screech.Variants = new int[] { 0 };
                    screech.Volume = 0.55f;
                    screech.Pitch = 1f;
                        SoundEngine.PlaySound(screech, NPC.position);
                    for (int i = 0; i < 18; i++)
                    {
                        Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), NPC.width, NPC.height, DustID.Corruption, Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(-0.4f, 0.4f), 140, Color.White, Main.rand.NextFloat(0.9f, 1.2f));
                    }

                    NPC.Transform(ModContent.NPCType<CorruptHamster>());
                    

                  
                        
                    NPC.netUpdate = true;
                }
            }

            

            // HEAT SECTION -------------------

            

                if (Main.tile[num173, num174].TileType is TileID.Hellstone or TileID.HellstoneBrick or TileID.AncientHellstoneBrick or TileID.Meteorite or TileID.MeteoriteBrick or TileID.LivingFire
                    or TileID.LivingDemonFire or TileID.Fireplace or TileID.Campfire or TileID.Hellforge or TileID.Furnaces)

                {
                explodesoontimer += 2;
                HeatTracker += 2; // two since it naturally goes down
                    
                }
                else
            {
                if (SlowlyReduceTimer > 5)
                {
                    HeatTracker--;
                    SlowlyReduceTimer = 0;

                    
                }
            }

             

            if (HeatTracker < 0)
            {
                HeatTracker = 0;
            }
            if (HeatTracker > 0)
            {
                if (Main.rand.NextBool(2))
                {
                  var hamtersmoke =  Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-2, 2)), NPC.width, NPC.height, DustID.Smoke, Main.rand.NextFloat(-0.1f, 0.1f), -0.9f, 110, Color.White, Main.rand.NextFloat(0.8f, 1.5f));

                    hamtersmoke.velocity.X = 0;
                    hamtersmoke.fadeIn = 0f;
                    if (hamtersmoke.velocity.Y > -0.3f)
                    {
                        hamtersmoke.velocity.Y = -0.3f;
                    }
                    hamtersmoke.velocity.Y /= 1.05f;
                    hamtersmoke.alpha += 1;
                    
                    hamtersmoke.noGravity = true;
                }
            }
            if (HeatTracker >= 50)
            {
                DiedFromCooking = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SimpleStrikeNPC(5, 0, false, 0f, DamageClass.Default);
                    NPC.netUpdate = true;
                }
            }
           
            explodesoontimer--;
            if (explodesoontimer < 0)
            {
                explodesoontimer = 0;
                exploding = 0;
                jitter = 0;
            }
            if (explodesoontimer > 25)
            {
                exploding++;
                jitter++;

            }

            if (jitter > 3)
            {
                jitter = 0;
            }

            
                  NPC.scale = 1f + (((float)exploding / 120)  - (float)jitter / 15);
              //  NPC.scale = 1.1f;
                
               

            



            if (MathF.Abs(NPC.velocity.Y) > 5f)
                {
                    FallTimeTracker++;
                }
            if (NPC.wet)
            {
                FallTimeTracker = 0;
                WaterTracker++;
            }
            else
            {
                WaterTracker = 0;
            }
            if (WaterTracker > 20)
            {
                explodesoontimer += 2;
            }
            if (WaterTracker > 120)
            {
                NPC.color = new Color(0, WaterTracker, WaterTracker * 2);
                DiedFromWater = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SimpleStrikeNPC(5, 0, false, 0f, DamageClass.Default);
                    NPC.netUpdate = true;
                }
               

            }
                if (NPC.velocity.Y == 0)
                {
                    if (FallTimeTracker < 20)
                    {
                        FallTimeTracker = 0;
                    }
                    else
                    {
                        DiedFromFalling = true;
                        LastSavedDirection = NPC.direction;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SimpleStrikeNPC(5, 0, false, 0f, DamageClass.Default);
                        NPC.netUpdate = true;
                    }
            }
                }
        }
       
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (DiedFromFalling)
                {
                   var flathjamster = Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, new Vector2(0, 5), Mod.Find<ModGore>("FlatHamster").Type, 1f);
                    
                    //
                    // Main.gore[flathjamster].sticky = true;

                    Main.gore[flathjamster].velocity.X = 0;
                    //Main.gore[flathjamster]. = 1;
                }
                else if (DiedFromCooking)
                {
                    

                    //
                    // Main.gore[flathjamster].sticky = true;

                    
                    for (int i = 0; i < 10; i++)
                    {
                        Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                        bloodDust.velocity *= 1.8f;
                        bloodDust.velocity.Y *= 0.4f;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.SpookyWood, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, Color.Black, 1f);
                        bloodDust.velocity *= 1.8f;
                        bloodDust.velocity.Y *= 0.4f;

                        Dust.NewDustPerfect(NPC.Center, DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)), default, Color.Black, 1.5f);
                    }
                    
                    
                        Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.NextFloat(-2, 2)), new Vector2(Main.rand.NextFloat(-1, 1), -2), GoreID.ChimneySmoke1, 1f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.NextFloat(-2, 2)), new Vector2(Main.rand.NextFloat(-1, 1), -2), GoreID.ChimneySmoke2, 1f);
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.position + new Vector2(Main.rand.Next(-4, 4), Main.rand.NextFloat(-2, 2)), new Vector2(Main.rand.NextFloat(-1, 1), -2), GoreID.ChimneySmoke3, 1f);

                }
                else
                {
                    Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, GoreID.BloodZombieChunk2, 1f);
                }
                if (DiedFromCooking)
                {
                    SoundEngine.PlaySound(SoundID.Item45, NPC.position);
                }
                if (DiedFromWater)
                {
                    SoundStyle WaterPop = SoundID.Item85;
                    SoundEngine.PlaySound(WaterPop);
                    WaterPop.Volume = 0.6f;

                    for (int i = 0; i < 6; i++)
                    {
                        Dust bubble = Dust.NewDustDirect(NPC.position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), NPC.width, NPC.height, DustID.BreatheBubble, Main.rand.NextFloat(-2f, 2f), 4f, default, default, 1.8f);
                        
                        bubble.noGravity = true;
                        
                    }
                }

            for (int i = 0; i < 14; i++)
                {
                    Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Blood, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                    bloodDust.velocity *= 1.8f;
                    bloodDust.velocity.Y *= 0.4f;
                }
                if (!DiedFromCooking)
                {


                    for (int i = 0; i < 8; i++)
                    {
                        Dust bloodDust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Water_BloodMoon, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), default, default, 1f);
                        bloodDust.velocity *= 1.8f;
                        bloodDust.velocity.Y *= 0.4f;

                        Dust.NewDustPerfect(NPC.Center, DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)), default, Color.DarkRed, 1.5f);
                    }
                }
                //NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 2f);
                //	Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/"), 1f);
                //  SoundEngine.PlaySound(SoundID.NPCDeath39);
            }
        }

        /*public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			if (Main.rand.NextBool(5))
			{
				//	Item.NewItem(NPC.getRect(), ItemID.Leather);
			}
		} */

        public override void OnCaughtBy(Player player, Item item, bool failed)
		{
            if (failed) // It can fail? huh
            {
                return;
            }
            item.stack = 1;

			try
			{
				var NPCCenter = NPC.Center.ToTileCoordinates();
				if (!WorldGen.SolidTile(NPCCenter.X, NPCCenter.Y) && Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType == 0) // what is this even for
				{
					//	Main.tile[NPCCenter.X, NPCCenter.Y].LiquidType = (byte)Main.rand.Next(50, 150);
					WorldGen.SquareTileFrame(NPCCenter.X, NPCCenter.Y, true);
				}
			}
			catch
			{
				return;
			}
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
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, 4),
          NPC.frame, drawColor, NPC.rotation,
          new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f), NPC.scale, spriteEffects, 0f);

            return false;
            
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Use AddRange instead of calling Add multiple times

            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				//BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.,
				//new FlavorTextBestiaryInfoElement("A mysterious inhabitant of the dungeon, subsisting off things that it really shouldn't."),

                new FlavorTextBestiaryInfoElement("Mods.Creaturia.Bestiary.Hamster")
            });
        }
    }

    internal class OrangeHamsterItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hoop Snake");
            // Tooltip.SetDefault("'Terrarians have long lived in fear of the dreaded Hoop Snake.'");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Rat);
            //item.useStyle = 1;
            //item.autoReuse = true;
            //item.useTurn = true;
            //item.useAnimation = 15;
            //item.useTime = 10;
            //item.maxStack = 999;
            //item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            //item.makeNPC = 360;
            //item.noUseGraphic = true;
            //item.bait = 15;


            Item.makeNPC = (short)NPCType<OrangeHamster>();
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