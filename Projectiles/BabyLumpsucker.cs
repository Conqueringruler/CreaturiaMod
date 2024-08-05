using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using Creaturia.Projectiles;
using Terraria.GameContent;
using Creaturia.Common.Players;
using Creaturia.Buffs;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace Creaturia.Projectiles
{
    public class BabyLumpsucker : ModProjectile
    {
        public override string Texture => "Creaturia/NPCs/Enemies/Boss/FishBosses/BabyLumpFish_EyeGlow";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Lumpsucker");
        }
        
        private int shootTimer;
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 26;
            // Projectile.aiStyle = 62;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            //Projectile.timeLeft *= 5;
            Projectile.minion = true;
            //Projectile.friendly = true;
            //Projectile.minionSlots = 1f; // No slots because special minion
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            
        }
        NPC nPC2;

        bool FlipSprite = false;
        int FlipSpriteInt = 0;
        int ExplodeChance = 1;
        int RecoverTimer = 500;
        bool stopRecovering = true;
        int jitter = 0;
        public override void AI()
        {
            

            FlipSpriteInt++; // this is so bad

            Player owner = Main.player[Projectile.owner];
            float num = 0f;
            float num2 = 0f;
            float num3 = 20f;
            float num4 = 40f;
            float num5 = 0.69f;
        
            if (owner.HasBuff(ModContent.BuffType<BabyLumpsuckerBuff>()))
            {
                Projectile.timeLeft = 2;
            }
          
            if (owner.GetModPlayer<CreaturiaPlayer>().LumpsuckerAcc == false)
            {
                Projectile.timeLeft = 0;
            }


            if (shootTimer > 10)
            {
                if (shootTimer > 200)
                {


                    jitter++;
                    if (jitter > 3)
                    {
                        jitter = 0;
                    }
                }
                Projectile.scale = 1f + (float)((float)shootTimer / 1600) + (float)((float)jitter/50);
            }
            else if (RecoverTimer >= 500)
            {
                Projectile.scale = 1;
            }

            if (ExplodeChance == 6)
            {
                
                SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, Projectile.position);
                shootTimer = 0;
                Projectile.scale = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), ProjectileID.GoldenShowerHostile, 25, 0, owner.whoAmI);


                    projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                    projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                    projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                    projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                    projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), ProjectileID.GoldenShowerFriendly, 25, 0  , owner.whoAmI);
                    if (Main.expertMode)
                    {
                        projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-5, 6), Main.rand.Next(-5, 6)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                        projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-5, 6), Main.rand.Next(-5, 6)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                    }
                    if (Main.masterMode)
                    {
                        projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), ProjectileID.GoldenShowerFriendly, 25, 0, owner.whoAmI);
                        projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), ProjectileID.GoldenShowerFriendly, 25, 0  , owner.whoAmI);
                    }
                }
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-15, 16)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-7, 7), Main.rand.Next(-7, 7));

                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));
                Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-5, 6)), Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6));

                
                SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, Projectile.position);
                RecoverTimer = 0;
                ExplodeChance = 1;
                stopRecovering = false;
            }
            
        if (RecoverTimer < 500)
            {
                RecoverTimer++;
                shootTimer = 0;

            }
        if (RecoverTimer >= 500 && stopRecovering == false)
            {
                Projectile.position = owner.Center + new Vector2(-800, -800);
                Projectile.scale = 1;
                stopRecovering = true;
            }
            
                if (owner.dead)
                {
                owner.DelBuff(BuffType<BabyLumpsuckerBuff>());
                }
                if (Main.player[Projectile.owner].hornetMinion)
                {
                    Projectile.timeLeft = 2;
                }
            
          
                if (Projectile.extraUpdates > 1)
                {
                    Projectile.extraUpdates = 0;
                }
                if (Projectile.numUpdates > 1)
                {
                    Projectile.numUpdates = 0;
                }
            
          
            float num10 = 0.05f;
            float num11 = Projectile.width;
            bool flag5 = false;

            for (int m = 0; m < 1000; m++)
            {
                if (m != Projectile.whoAmI && Main.projectile[m].active && Main.projectile[m].owner == Projectile.owner && Main.projectile[m].type == Projectile.type && Math.Abs(Projectile.position.X - Main.projectile[m].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[m].position.Y) < num11)
                {
                    if (Projectile.position.X < Main.projectile[m].position.X)
                    {
                        Projectile.velocity.X -= num10;
                    }
                    else
                    {
                        Projectile.velocity.X += num10;
                    }
                    if (Projectile.position.Y < Main.projectile[m].position.Y)
                    {
                        Projectile.velocity.Y -= num10;
                    }
                    else
                    {
                        Projectile.velocity.Y += num10;
                    }
                    
                }
            }
            Vector2 vector = Projectile.position;
            float num12 = 400f;
           
            bool flag = false;
            int num13 = -1;
           // Projectile.tileCollide = true;
           // keeps despawning on tile collide so I guess I'll just let it stay off
          
            
                NPC ownerMinionAttackTargetNPC2 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(Projectile))
                {
                    float num17 = Vector2.Distance(ownerMinionAttackTargetNPC2.Center, Projectile.Center);
                    float num18 = num12 * 3f;
                    if (num17 < num18 && !flag)
                    {
                        bool flag2 = false;
                        if ((Projectile.type != 963) ? Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height) : Collision.CanHit(Projectile.Center, 1, 1, ownerMinionAttackTargetNPC2.Center, 1, 1))
                        {
                            num12 = num17;
                            vector = ownerMinionAttackTargetNPC2.Center;
                            flag = true;
                            num13 = ownerMinionAttackTargetNPC2.whoAmI;
                        Projectile.rotation = Projectile.AngleTo(vector);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {


                            shootTimer++;
                            if (shootTimer > 300)
                            {
                                Vector2 directionshoot = (ownerMinionAttackTargetNPC2.Center - Projectile.Center + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-95, 95))).SafeNormalize(Vector2.UnitX);
                                SoundEngine.PlaySound(SoundID.Zombie78, Projectile.Center);
                                int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerFriendly, 35, 0, owner.whoAmI);
                                shootTimer = 0;
                                ExplodeChance = Main.rand.Next(1, 7); // can't return 7 btw

                            }
                        }

                        FlipSpriteInt = 0;
                    }
                    }
                }
                if (!flag)
                {
                    for (int num19 = 0; num19 < 200; num19++)
                    {
                        nPC2 = Main.npc[num19];
                        if (!nPC2.CanBeChasedBy(Projectile))
                        {
                            continue;
                        }
                        float num20 = Vector2.Distance(nPC2.Center, Projectile.Center);
                        if (!(num20 >= num12))
                        {
                            bool flag3 = false;
                            if ((Projectile.type != 963) ? Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height) : Collision.CanHit(Projectile.Center, 1, 1, nPC2.Center, 1, 1))
                            {
                                num12 = num20;
                                vector = nPC2.Center;
                                flag = true;
                                num13 = num19;
                            Projectile.rotation = Projectile.AngleTo(vector);

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (ownerMinionAttackTargetNPC2 == null)
                                {

                                    shootTimer++;

                                    if (shootTimer > 300)
                                    {
                                        Vector2 directionshoot = (nPC2.Center - Projectile.Center + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-95, 95))).SafeNormalize(Vector2.UnitX);
                                        SoundEngine.PlaySound(SoundID.Zombie78, Projectile.Center);
                                        int projectile = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, directionshoot * (float)Main.rand.Next(1, 10), ProjectileID.GoldenShowerFriendly, 35, 0, owner.whoAmI);

                                        shootTimer = 0;

                                        ExplodeChance = Main.rand.Next(1, 7); // can't return 7 btw
                                    }
                                }
                            }
                            FlipSpriteInt = 0;
                        }
                        }
                    }
                }
            
            int num21 = 500;
           
            if (flag)
            {
                num21 = 1000;
            }
          
            Player player = Main.player[Projectile.owner];
            if (Vector2.Distance(player.Center, Projectile.Center) > (float)num21)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
                Projectile.rotation = Projectile.velocity.X * 0.05f;
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
            }
            bool flag4 = false;
            
            if (flag4)
            {
                if (Projectile.ai[0] <= 1f && Projectile.localAI[1] <= 0f)
                {
                    Projectile.localAI[1] = -1f;
                }
                else
                {
                    Projectile.localAI[1] = Utils.Clamp(Projectile.localAI[1] + 0.05f, 0f, 1f);
                    if (Projectile.localAI[1] == 1f)
                    {
                        Projectile.localAI[1] = -1f;
                    }
                }
            }
            
            if (Projectile.ai[0] >= 2f)
            {
                
                Projectile.ai[0] += 1f;
                if (flag4)
                {
                    Projectile.localAI[1] = Projectile.ai[0] / num4;
                }
                if (!flag)
                {
                    Projectile.ai[0] += 1f;
                }
                if (Projectile.ai[0] > num4)
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                    if (flag && Projectile.type == 963 && (vector - Projectile.Center).Length() < 50f)
                    {
                        Projectile.ai[0] = 2f;
                    }
                }
                Projectile.velocity *= num5;
            }
            else if (flag && (flag5 || Projectile.ai[0] == 0f))
            {
                Vector2 v = vector - Projectile.Center;
                float num22 = v.Length();
                v = v.SafeNormalize(Vector2.Zero);
                
               
                if (num22 > 200f)
                {
                    float num26 = 6f + num2 * num;
                    v *= num26;
                    float num27 = num3 * 2.2f;
                    float num27fory = num3 * 1.5f;
                    Projectile.velocity.X = (Projectile.velocity.X * num27 + v.X) / (num27 + 1f);
                    Projectile.velocity.Y = (Projectile.velocity.Y * num27fory + v.Y) / (num27fory + 2f);

                 
                }
               
              
                 if (Projectile.velocity.Y > -1f)
                {
                    Projectile.velocity.Y -= 0.06f;
                }
            }
            else
            {
                if (Projectile.type != 963 && !Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
                {
                    Projectile.ai[0] = 1f;
                }
                float num31 = 6f;
                if (Projectile.ai[0] == 1f)
                {
                    num31 = 15f;
                }
               
                Vector2 center2 = Projectile.Center;
                Vector2 vector6 = player.Center - center2 + new Vector2(0f, -60f);
                
                if (Projectile.type == 375)
                {
                    Projectile.ai[1] = 80f;
                    Projectile.netUpdate = true;
                    vector6 = player.Center - center2;
                    int num32 = 1;
                    for (int num33 = 0; num33 < Projectile.whoAmI; num33++)
                    {
                        if (Main.projectile[num33].active && Main.projectile[num33].owner == Projectile.owner && Main.projectile[num33].type == Projectile.type)
                        {
                            num32++;
                        }
                    }
                    vector6.X -= 10 * Main.player[Projectile.owner].direction;
                    vector6.X -= num32 * 40 * Main.player[Projectile.owner].direction;
                    vector6.Y -= 10f;
                }
                float num34 = vector6.Length();
                if (num34 > 200f && num31 < 9f)
                {
                    num31 = 9f;
                }
                if ((Projectile.type == 423 || Projectile.type == 407) && num34 > 300f && num31 < 12f)
                {
                    num31 = 12f;
                }
                if (Projectile.type == 375)
                {
                    num31 = (int)((double)num31 * 0.75);
                }
                if (num34 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num34 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
                }
                if (Projectile.type == 375 || Projectile.type == 963)
                {
                    if (num34 > 10f)
                    {
                        vector6 = vector6.SafeNormalize(Vector2.Zero);
                        if (num34 < 50f)
                        {
                            num31 /= 2f;
                        }
                        vector6 *= num31;
                        Projectile.velocity = (Projectile.velocity * 20f + vector6) / 21f;
                    }
                    else
                    {
                        Projectile.direction = Main.player[Projectile.owner].direction;
                        Projectile.velocity *= 0.91f;
                    }
                }
                
                else if (num34 > 70f)
                {
                    vector6 = vector6.SafeNormalize(Vector2.Zero);
                    vector6 *= num31;
                    Projectile.velocity = (Projectile.velocity * 10f + vector6) / 11f;
                    Projectile.velocity.Y *= 0.96f;
                    Projectile.rotation = Projectile.velocity.X * 0.05f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.18f;
                        Projectile.velocity.Y = -0.04f;
                    }
                    Projectile.velocity *= 1.01f;
                }
               
            }
            
            Projectile.frameCounter++;
            
              
            
           
           
            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = (Projectile.direction = -1);
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = (Projectile.direction = 1);
            }
           
            
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] += Main.rand.Next(1, 4);
                }
                int num44 = 90;
                if (Main.player[Projectile.owner].strongBees)
                {
                    num44 = 70;
                }
                if (Projectile.ai[1] > (float)num44)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            
            if (!flag5 && Projectile.ai[0] != 0f)
            {
                return;
            }
            float num45 = 0f;
            int num46 = 0;
            
                num45 = 10f;
                num46 = 374;
            
           
        }
       
        public override bool PreDraw(ref Color lightColor)
        {
            


            Texture2D texture = Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/BabyLumpFish").Value;
            Texture2D eyetexture = Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/BabyLumpFish_EyeGlow").Value;
            Color drawColor = Color.White;
            SpriteEffects spriteEffects = SpriteEffects.None;
            Rectangle sourceRectangle = texture.Frame(1, 1);

            if (Projectile.velocity.X > 0)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
            }

            if (Projectile.velocity.X < 0)
            {
                if (FlipSpriteInt == 0)
                {
                    spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
                }
                if (FlipSpriteInt > 0)
                {
                    spriteEffects = SpriteEffects.None;
                }
                /*
                if (nPC2 != null)
                {


                    if (nPC2.active)
                    {
                        spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
                    }
                   else if (Projectile.OwnerMinionAttackTargetNPC == null)
                    {
                     //   spriteEffects = SpriteEffects.None;
                    }
                    else if (!Projectile.OwnerMinionAttackTargetNPC.active)
                    {
                     //   spriteEffects = SpriteEffects.None;
                    }
                }
                else if (Projectile.OwnerMinionAttackTargetNPC != null)
                {


                    if (Projectile.OwnerMinionAttackTargetNPC.active)
                    {
                        spriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
                    }
                    else if (nPC2 == null)
                    {
                     //   spriteEffects = SpriteEffects.None;
                    }
                    else if (!nPC2.active)
                    {
                     //   spriteEffects = SpriteEffects.None;
                    }
                } */
               

                //SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            }


            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition,
            sourceRectangle, drawColor, Projectile.rotation,
            new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), Projectile.scale, spriteEffects, 0);

            Main.EntitySpriteDraw(eyetexture, Projectile.Center - Main.screenPosition,
            sourceRectangle, Color.White, Projectile.rotation,
            new Vector2(eyetexture.Width * 0.5f, eyetexture.Height * 0.5f), Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
    

}
