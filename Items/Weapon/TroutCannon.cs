using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.DataStructures;
using Creaturia.Buffs;
using Creaturia.Items;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creaturia.NPCs.Town;
using Terraria.Utilities;
using Terraria.IO;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace Creaturia.Items.Weapon
{
    public class TroutCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Dart Rifle");
            /* Tooltip.SetDefault("'Antique' \n"
							 + "Highly accurate, but slow"); */
            //ItemID.Sets.Spears[Item.type] = true;

        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.knockBack = 5.25f;
            Item.width = 40;
            Item.height = 18;
            Item.damage = 40;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item11;
            //	Item.shootSpeed = 4f; // The speed of the projectile measured in pixels per frame
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 5, silver: 50);
            Item.DamageType = DamageClass.Ranged;
            //Item.ammo = AmmoID.Dart;
            Item.shoot = ModContent.ProjectileType<TroutProj>();
            Item.useAmmo = ItemID.Trout;
            //Item.channel = true;


        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 1f);
        }



    }
    public class TroutProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Tuna;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.Explosive[Type] = true;
            ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            //Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.timeLeft = 180;
            //     DrawOffsetX = -2;
            //   DrawOriginOffsetY = -5;
        }
        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.PrepareBombToBlow();
            }

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 10f)
            {
                Projectile.ai[0] = 10f;
                // Roll speed dampening. 
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;

                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                // Delayed gravity
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }
            // Rotation increased by velocity.X 
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0f;
            Projectile.timeLeft = 3;
            return false;
        }
        public override void PrepareBombToBlow()
        {
            Projectile.tileCollide = false; // This is important or the explosion will be in the wrong place if the bomb explodes on slopes.
            Projectile.alpha = 255;

            // Resize the hitbox of the projectile for the blast "radius".
            // Rocket I: 128, Rocket III: 200, Mini Nuke Rocket: 250
            //128 = 8 tiles
            Projectile.Resize(96, 96);

            Projectile.damage = 30; // Bomb: 100, Dynamite: 250
            Projectile.knockBack = 4f; // Bomb: 8f, Dynamite: 10f
        }
        
        public override void OnKill(int timeLeft)
        {
           SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with { Volume = 0.65f } with { PitchVariance = 0.2f }, Projectile.position); // had no idea the "with" part was possible until recently, use for other sounds
            SoundEngine.PlaySound(SoundID.NPCDeath1 with { Volume = 0.8f} with { PitchVariance = 0.1f}, Projectile.position);

            Projectile.Resize(22, 22);


            for (int i = 0; i < 30; i++)
            {
                Dust bloodDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), default, default, 1f);
                bloodDust.velocity *= 1.8f;
                bloodDust.velocity.Y *= 0.4f;
            }
            for (int i = 0; i < 15; i++)
            {
                Dust bloodDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water_BloodMoon, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), default, default, 1f);
                bloodDust.velocity *= 1.8f;
                bloodDust.velocity.Y *= 0.4f;

                Dust.NewDustPerfect(Projectile.Center, DustID.FoodPiece, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f)), default, Color.DarkRed, 1.5f);
            }


            // Spawn a bunch of smoke gores.
            for (int k = 0; k < 2; k++)
            {
                float speedMulti = 2.4f;
                if (k == 1)
                {
                    speedMulti = 2.8f;
                    Gore tailGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, GoreID.CrimsonGoldfishTail);
                    tailGore.velocity *= speedMulti;
                    tailGore.velocity += Vector2.One;
                }

                Gore ChunkGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, GoreID.BloodZombieChunk2, Main.rand.NextFloat(0.6f, 1f));
                ChunkGore.velocity *= speedMulti;
                ChunkGore.velocity += Vector2.One;
                if (Main.rand.NextBool(2))
                {
                ChunkGore = Gore.NewGoreDirect(Projectile.GetSource_Death(), Projectile.position, default, GoreID.BloodZombieChunk2, Main.rand.NextFloat(0.6f, 1f));
                ChunkGore.velocity *= speedMulti;
                ChunkGore.velocity += Vector2.One;

            }
        }

        }
    }
}