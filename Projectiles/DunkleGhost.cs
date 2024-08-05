using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using System;
using Creaturia.Projectiles;
using Terraria.GameContent;
using Creaturia.Common.Players;
using Creaturia.Buffs;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace Creaturia.Projectiles
{
    public class DunkleGhost : ModProjectile
    {
        public override string Texture => "Creaturia/NPCs/Enemies/Boss/FishBosses/DunklerFish";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dunkle Ghost");
            Main.projFrames[Projectile.type] = 6;
        }
        
        private int shootTimer;
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 290;
            Projectile.height = 108;
            // Projectile.aiStyle = 62;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            //Projectile.timeLeft *= 5;
            //Projectile.minion = true;
            //Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            
        }

        float speedBoost = 1f;
        public override void AI()
        {
            

           

            Player owner = Main.player[Projectile.owner];
            Vector2 value = Projectile.DirectionTo(owner.position);
            //Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.6f);
            // if (MathF.Abs((MathF.Abs(owner.position.X) + MathF.Abs(owner.position.Y)) - (MathF.Abs(Projectile.position.X) + MathF.Abs(Projectile.position.Y))) > 200f)
            if (Projectile.Distance(owner.position) > 400f)
            {
               // Projectile.velocity = Projectile.DirectionTo(owner.position) * 20f;
                
            }
            speedBoost = Projectile.Distance(owner.position) / 150;

             if (Projectile.Distance(owner.position) > 150f)
            {
                Projectile.velocity = Projectile.DirectionTo(owner.position) * 10f * speedBoost;
            }
            else
            {
                Projectile.velocity *= 0.95f;
            }
            
            if (owner.HasBuff(ModContent.BuffType<DunkleBuff>()))
            {
                Projectile.timeLeft = 2;
            }
          else
            {
                for (int i = 0; i < 25; i++)
                {
                    var dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-80, 80)), Projectile.width, Projectile.height, DustID.Ghost, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), 120, Color.White, Main.rand.NextFloat(0.6f, 1.4f));
                    dust.noGravity = true;
                }
                Projectile.timeLeft = 0;
            }
           
                
            
            

           
           
        }
        SpriteEffects spriteEffects = SpriteEffects.None;
        public override bool PreDraw(ref Color lightColor)
        {

            
            Texture2D texture = Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/DunklerFish").Value;
            //Texture2D eyetexture = Request<Texture2D>("Creaturia/NPCs/Enemies/Boss/FishBosses/BabyLumpFish_EyeGlow").Value;
            Color drawColor = new Color(0, 0, 0, 0.3f + ((0.6f * (MathF.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.2f))));
            //SpriteEffects spriteEffects = SpriteEffects.None;

            if (Projectile.velocity.X > 1.01f)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //spriteEffects = SpriteEffects.FlipHorizontally;
                //NPC.spriteDirection = -1;

                spriteEffects = SpriteEffects.None | SpriteEffects.FlipHorizontally;
            }

            if (Projectile.velocity.X < -1.01f)
            {
               // if (FlipSpriteInt == 0)
               // {
                    spriteEffects = SpriteEffects.None | SpriteEffects.None;
              //  }
               // if (FlipSpriteInt > 0)
              //  {
              //      spriteEffects = SpriteEffects.None;
              //  }
              

            }



            int frameHeight = texture.Height / Main.projFrames[Type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            Projectile.frameCounter++;
            if (Projectile.frameCounter < 5)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.frameCounter < 10)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.frameCounter < 15)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.frameCounter < 20)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.frameCounter < 25)
            {
                Projectile.frame = 4;
            }
            else if (Projectile.frameCounter < 30)
            {
                Projectile.frame = 5;
            }

            else
            {
                Projectile.frameCounter = 0;
            }


            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition,
            sourceRectangle, drawColor, Projectile.rotation,
            new Vector2(texture.Width * 0f + 144f, texture.Height * 0f + 48f), Projectile.scale, spriteEffects, 0);

            /*Main.EntitySpriteDraw(eyetexture, Projectile.Center - Main.screenPosition,
            sourceRectangle, Color.White, Projectile.rotation,
            new Vector2(eyetexture.Width * 0.5f, eyetexture.Height * 0.5f), Projectile.scale, spriteEffects, 0); */
            return false;
        }
    }
    

}
