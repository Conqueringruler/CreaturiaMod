using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Creaturia.Common.Players;
using Creaturia.NPCs.Creatures;
using Creaturia.Buffs;
using Creaturia.Projectiles;

namespace Creaturia.Items.Accessories
{
	[AutoloadEquip(EquipType.Neck)]
	public class SpinyNecklace : ModItem
	{
        //public override string Texture => "Terraria/Images/Item_" + ItemID.PygmyNecklace;
        
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			// DisplayName.SetDefault("Spiney Necklace");
			/* Tooltip.SetDefault("Increases your max number of minions by 1\n" + 
							   "A Baby Lumpsucker has mistaken you for its mother"); */
				
                           //    "When transformed can penetrate once");
		}

		public override void SetDefaults()
		{
			//Item.damage = 5;
			//Item.DamageType = DamageClass.Ranged;
			Item.width = 24;
			Item.height = 34;
			Item.maxStack = 1;
			Item.accessory = true;
			Item.consumable = true;
			//Item.knockBack = 0.2f;
			Item.value = 2000;
			Item.rare = ItemRarityID.Green;
			//
			//Item.shoot = ModContent.ProjectileType<FishBulletProj>();
			//Item.shootSpeed = 7f;
			//Item.ammo = AmmoID.Bullet;
		}
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<CreaturiaPlayer>().LumpsuckerAcc = true;
            // Dodge chance is done inside ModPlayer!!
            player.slotsMinions += 1;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<BabyLumpsucker>()] < 1)
            {
                int num14 = 10;
                int num15 = 30;
                int num16 = Projectile.NewProjectile(player.GetSource_FromAI(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<BabyLumpsucker>(), num15, num14, Main.myPlayer);
                Main.projectile[num16].originalDamage = num15;
				if (player.FindBuffIndex(ModContent.BuffType<BabyLumpsuckerBuff>()) == -1)
				{
					player.AddBuff(ModContent.BuffType<BabyLumpsuckerBuff>(), 3);
				}
            }
        }
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<LumpsuckerHeart>(15)
				.AddIngredient(ItemID.PygmyNecklace)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}


	}

	

}

