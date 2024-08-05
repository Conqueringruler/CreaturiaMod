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
using Terraria.GameContent.Drawing;


namespace Creaturia.Projectiles
{
    public class TestingProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Testing");
        }
        private int ExplodeTimer;
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RainbowRodBullet;
        public override void SetDefaults()
        {
            //AIType = ProjectileID.Shuriken;
            //Projectile.CloneDefaults(ProjectileID.Shuriken);
            //Projectile.friendly = true;
            Projectile.timeLeft = 10;
           
        }
        int DestroyTimer;
        public override void AI()
        {
            DestroyTimer++;
            if (DestroyTimer > 200)
            {
                Projectile.active = false;
            }


            int num = 10;
            Player player = Main.player[Projectile.owner];
            int num2 = Main.maxTilesY * 16;
            int num3 = 0;
            if (Projectile.ai[0] >= 0f)
            {
                num3 = (int)(Projectile.ai[1] / (float)num2);
            }
            bool flag = Projectile.ai[0] == -1f || Projectile.ai[0] == -2f;
            if (Projectile.type == 34)
            {
                if (Projectile.frameCounter++ >= 4)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 0;
                    }
                }
                if (Projectile.penetrate == 1 && Projectile.ai[0] >= 0f && num3 == 0)
                {
                    Projectile.ai[1] += num2;
                    num3 = 1;
                    Projectile.netUpdate = true;
                }
                if (Projectile.penetrate == 1 && Projectile.ai[0] == -1f)
                {
                    Projectile.ai[0] = -2f;
                    Projectile.netUpdate = true;
                }
                if (num3 > 0 || Projectile.ai[0] == -2f)
                {
                    Projectile.localAI[0] += 1f;
                }
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[0] >= 0f)
                {
                    if (player.channel && player.HeldItem.shoot == Projectile.type)
                    {
                        Vector2 pointPoisition = Main.MouseWorld;
                        player.LimitPointToPlayerReachableArea(ref pointPoisition);
                        if (Projectile.ai[0] != pointPoisition.X || Projectile.ai[1] != pointPoisition.Y)
                        {
                            Projectile.netUpdate = true;
                            Projectile.ai[0] = pointPoisition.X;
                            Projectile.ai[1] = pointPoisition.Y + (float)(num2 * num3);
                        }
                    }
                    else
                    {
                        Projectile.netUpdate = true;
                        Projectile.ai[0] = -1f;
                        Projectile.ai[1] = -1f;
                        int num4 = Projectile.FindTargetWithLineOfSight();
                        if (num4 != -1)
                        {
                            Projectile.ai[1] = num4;
                        }
                        else if (Projectile.velocity.Length() < 2f)
                        {
                            Projectile.velocity = Projectile.DirectionFrom(player.Center) * num;
                        }
                        else
                        {
                            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero) * num;
                        }
                    }
                }
                if (flag && Projectile.ai[1] == -1f)
                {
                    int num5 = Projectile.FindTargetWithLineOfSight();
                    if (num5 != -1)
                    {
                        Projectile.ai[1] = num5;
                        Projectile.netUpdate = true;
                    }
                }
            }
            Vector2? vector = null;
            float amount = 1f;
            if (Projectile.ai[0] > 0f && Projectile.ai[1] > 0f)
            {
                vector = new Vector2(Projectile.ai[0], Projectile.ai[1] % (float)num2);
            }
            if (flag && Projectile.ai[1] >= 0f)
            {
                int num6 = (int)Projectile.ai[1];
                if (Main.npc.IndexInRange(num6))
                {
                    NPC nPC = Main.npc[num6];
                    if (nPC.CanBeChasedBy(Projectile))
                    {
                        vector = nPC.Center;
                        float t = Projectile.Distance(vector.Value);
                        float num7 = Utils.GetLerpValue(0f, 100f, t, clamped: true) * Utils.GetLerpValue(600f, 400f, t, clamped: true);
                        amount = MathHelper.Lerp(0f, 0.2f, Utils.GetLerpValue(200f, 20f, 1f - num7, clamped: true));
                    }
                    else
                    {
                        Projectile.ai[1] = -1f;
                        Projectile.netUpdate = true;
                    }
                }
            }
            bool flag2 = false;
            if (flag)
            {
                flag2 = true;
            }
            if (vector.HasValue)
            {
                Vector2 value = vector.Value;
                if (Projectile.Distance(value) >= 64f)
                {
                    flag2 = true;
                    Vector2 v = value - Projectile.Center;
                    Vector2 vector2 = v.SafeNormalize(Vector2.Zero);
                    float num8 = Math.Min(num, v.Length());
                    Vector2 value2 = vector2 * num8;
                    if (Projectile.velocity.Length() < 4f)
                    {
                        Projectile.velocity += Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(0.78539818525314331).SafeNormalize(Vector2.Zero) * 4f;
                    }
                    if (Projectile.velocity.HasNaNs())
                    {
                        Projectile.Kill();
                    }
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, value2, amount);
                }
                else
                {
                    Projectile.velocity *= 0.3f;
                    Projectile.velocity += (value - Projectile.Center) * 0.3f;
                    flag2 = Projectile.velocity.Length() >= 2f;
                }
                if (Projectile.timeLeft < 60)
                {
                    Projectile.timeLeft = 60;
                }
            }
            if (flag && Projectile.ai[1] < 0f)
            {
                if (Projectile.velocity.Length() != (float)num)
                {
                    Projectile.velocity = Projectile.velocity.MoveTowards(Projectile.velocity.SafeNormalize(Vector2.UnitY) * num, 4f);
                }
                if (Projectile.timeLeft > 300)
                {
                    Projectile.timeLeft = 300;
                }
            }
            if (flag2 && Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation = Projectile.rotation.AngleTowards(Projectile.velocity.ToRotation(), (float)Math.PI / 4f);
            }
            else
            {
                Projectile.rotation = Projectile.rotation.AngleLerp(0f, 0.2f);
            }
            bool flag3 = Projectile.velocity.Length() > 0.1f && Vector2.Dot(Projectile.oldVelocity.SafeNormalize(Vector2.Zero), Projectile.velocity.SafeNormalize(Vector2.Zero)) < 0.2f;
            
                if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
                {
                    Projectile.soundDelay = 10;
                    SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
                }
                if (Main.rand.Next(9) == 0)
                {
                    int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num9].velocity *= 0.3f;
                    Main.dust[num9].position.X = Projectile.position.X + (float)(Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
                    Main.dust[num9].position.Y = Projectile.position.Y + (float)(Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].velocity += Main.rand.NextVector2Circular(2f, 2f);
                }
                if (flag3)
                {
                    int num10 = Main.rand.Next(2, 5);
                    for (int i = 0; i < num10; i++)
                    {
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 1.5f);
                        dust.velocity *= 0.3f;
                        dust.position = Projectile.Center;
                        dust.noGravity = true;
                        dust.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
                        dust.fadeIn = 2.2f;
                    }
                }
            
            if (Projectile.type != 34)
            {
                return;
            }
            float lerpValue = Utils.GetLerpValue(0f, 10f, Projectile.localAI[0], clamped: true);
            Color newColor = Color.Lerp(Color.Transparent, Color.Crimson, lerpValue);
            if (Main.rand.Next(6) == 0)
            {
                Dust dust2 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 3.5f);
                dust2.noGravity = true;
                dust2.velocity *= 1.4f;
                dust2.velocity += Main.rand.NextVector2Circular(1f, 1f);
                dust2.velocity += Projectile.velocity * 0.15f;
            }
            if (Main.rand.Next(12) == 0)
            {
                Dust dust3 = Dust.NewDustDirect(Projectile.Center, 0, 0, 6, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, newColor, 1.5f);
                dust3.velocity += Main.rand.NextVector2Circular(1f, 1f);
                dust3.velocity += Projectile.velocity * 0.15f;
            }
            if (flag3)
            {
                int num11 = Main.rand.Next(2, 5 + (int)(lerpValue * 4f));
                for (int j = 0; j < num11; j++)
                {
                    Dust dust4 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, newColor, 1.5f);
                    dust4.velocity *= 0.3f;
                    dust4.position = Projectile.Center;
                    dust4.noGravity = true;
                    dust4.velocity += Main.rand.NextVector2Circular(0.5f, 0.5f);
                    dust4.fadeIn = 2.2f;
                    dust4.position += (dust4.position - Projectile.Center) * lerpValue * 10f;
                }

            } 
        }
           
    }
}
