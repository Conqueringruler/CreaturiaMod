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
using Creaturia.Projectiles;

namespace Creaturia.Items.Weapon
{
    public class ScaledLamp : ModItem // PROJECTILE IS IN HERE TOO
    {
        //public override string Texture => "Terraria/Images/Gore_" + 344;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vertebral Lamp"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("'The bone makes the lamp nearly unbreakable' \n" +
                               "Hold to summon the ghost and power of the Dunkle\n" + 
                               "Items and summons cannot be used during this"); */
            //ItemID.Sets.Spears[Item.type] = true;

        }

        public override void SetDefaults()
        {

            Item.shootSpeed = 0;
            Item.knockBack = 0;
            
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 64; //time in ticks (60 ticks == 1 second.)
            Item.useTime = 64;
            Item.knockBack = 2.5f;
            Item.width = 32;
            Item.autoReuse = true;
            Item.height = 32;
            Item.damage = 0;
            Item.mana = 40;
            
            //	Item.shoot = ModContent.ProjectileType<TridentProjectile>();
            //	Item.shootSpeed = 4f; // The speed of the projectile measured in pixels per frame.
            Item.UseSound = SoundID.MaxMana; // The sound that this item makes when used
            Item.rare = ItemRarityID.Pink; // The color of the name of your item
            Item.value = Item.sellPrice(gold: 2, silver: 50);
            Item.DamageType = DamageClass.Magic; // Deals melee damageo
            //Item.color = Color.Silver;                //Item.channel = true;
            Item.shoot = ModContent.ProjectileType<DunkleGhost>();

        }

        int magicCuffsBuff = 0;
        int ManaFlowerBuff = 0;
        int ManaMaxBuff = 0;
        int ManaMagnetBuff = 0;
        int RecoverAlt = 0;
        int rightclickbufftimer = 2000;
        private bool rightclickplayedsound = false;
        private bool rightclickready = false;
        public override bool AltFunctionUse(Player player)
        {
            if (rightclickbufftimer > 2000)
            {


                return true;

            }
            else
            {
                return false;
            }
            
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && rightclickbufftimer > 2000)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override void UpdateInventory(Player player)
        {
            if (rightclickbufftimer < 2499) // Confusing myself with this convoluted system lol
            {
                //rightclickplayedsound = false;
            }
            if (rightclickbufftimer < 2502)
            {
                rightclickbufftimer++;
            }
            if (rightclickbufftimer > 2500)
            {

                rightclickready = true;


            }

            if (rightclickready == true && rightclickplayedsound == false)
            {
                SoundEngine.PlaySound(SoundID.Item9, player.position);
                CombatText.NewText(player.Hitbox, Color.MediumPurple, "Vertebral Lamp has recharged!");
                var dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.CursedTorch, player.velocity.X * Main.rand.Next(-2, 2), player.velocity.Y * Main.rand.Next(-2, 2), 60, Color.White, Main.rand.NextFloat(0.6f, 1.4f));

                for (int i = 0; i < 10; i++)
                {
                    dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.CursedTorch, player.velocity.X * Main.rand.Next(-2, 2), player.velocity.Y * Main.rand.Next(-2, 2), 60, Color.White, Main.rand.NextFloat(0.6f, 1.4f));
                }


                rightclickplayedsound = true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (player.altFunctionUse == 2 && rightclickbufftimer > 2500)
            {
                player.AddBuff(ModContent.BuffType<DunkleBuff>(), 750);
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                rightclickbufftimer = 0;
                var PukeSound = SoundID.DD2_OgreSpit;
                PukeSound.Pitch = 1.6f;
                SoundEngine.PlaySound(PukeSound, player.position);
                PukeSound.Volume = 0.6f;
                PukeSound.PitchVariance = 0.2f;
                rightclickready = false;


                return true;
            }
            else
            {
                return false;
                //Projectile.NewProjectile(source, position, velocity, type, (int)(damage), (int)(knockback), player.whoAmI, player.altFunctionUse == 2 ? 1 : 0);
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DjinnLamp)
            .AddIngredient(ModContent.ItemType<DunkleVertebrae>(), 15)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

    }
}