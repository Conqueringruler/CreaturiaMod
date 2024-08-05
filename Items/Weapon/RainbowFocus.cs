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
    public class RainbowFocus : ModItem // PROJECTILE IS IN HERE TOO
    {
        public override string Texture => "Terraria/Images/Gore_" + 1266;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rainbow Focus"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("'Only blesses those of great strength' \n" +
                               "Converts Mana to HP based on strength of magical items equipped\n" + 
                               "Right Click to use 400 Mana for a great heal, recharges after 6 normal heals"); */
            //ItemID.Sets.Spears[Item.type] = true;

        }

        public override void SetDefaults()
        {

            Item.shootSpeed = 0;
            Item.knockBack = 0;
            
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 32; //time in ticks (60 ticks == 1 second.)
            Item.useTime = 32;
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
            Item.color = Main.DiscoColor;                //Item.channel = true;
            Item.shoot = ProjectileID.PurificationPowder;

        }

        int magicCuffsBuff = 0;
        int ManaFlowerBuff = 0;
        int ManaMaxBuff = 0;
        int ManaMagnetBuff = 0;
        int RecoverAlt = 0;
        public override bool AltFunctionUse(Player player)
        {
            if (player.statMana >= 400 && RecoverAlt > 6)
            {
                SoundEngine.PlaySound(new SoundStyle("Creaturia/Assets/Sounds/RainbowFocus"), player.Center);
                player.statMana -= 400;

                player.statLife += ((2 + ManaMaxBuff + magicCuffsBuff + ManaFlowerBuff + ManaMagnetBuff) * 11);
                player.HealEffect((2 + ManaMaxBuff + magicCuffsBuff + ManaFlowerBuff + ManaMagnetBuff) * 11);

                RecoverAlt = 0;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public override void OnConsumeMana(Player player, int manaConsumed)
        {
            RecoverAlt += 1;
            Item.color = Main.DiscoColor;
              if (player.statManaMax2 <= 200)
              {
                  ManaMaxBuff = 0;
              }
               else if (player.statManaMax2 > 200 && player.statManaMax2 <= 250)
              {
                  ManaMaxBuff = 1;
              }
            else if (player.statManaMax2 > 250 && player.statManaMax2 <= 300)
            {
                ManaMaxBuff = 2;
            }
            else if (player.statManaMax2 > 300 && player.statManaMax2 <= 350)
            {
                ManaMaxBuff = 3;
            }
            else  if (player.statManaMax2 > 350 && player.statManaMax2 <= 400)
              {
                  ManaMaxBuff = 4;
              } 
            
            if (player.magicCuffs)
            {
                magicCuffsBuff = 2;
            }
            else
            {
                magicCuffsBuff = 0;
            }
            if (player.manaFlower)
            {
                ManaFlowerBuff = 2;
            }
            else
            {
                ManaFlowerBuff = 0;
            }
            if (player.manaMagnet)
            {
                ManaMagnetBuff = 2;
            }
            else
            {
                ManaMagnetBuff = 0;
            }
           
            player.statLife += 2 + ManaMaxBuff + magicCuffsBuff + ManaFlowerBuff + ManaMagnetBuff;
            player.HealEffect(2 + ManaMaxBuff + magicCuffsBuff + ManaFlowerBuff + ManaMagnetBuff);
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FairyCritterBlue)
                .AddIngredient(ItemID.FairyCritterGreen)
                .AddIngredient(ItemID.FairyCritterPink)
            .AddIngredient(ItemID.SpellTome)
            .AddIngredient(ModContent.ItemType<RainbowScale2>(), 15)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

    }
}