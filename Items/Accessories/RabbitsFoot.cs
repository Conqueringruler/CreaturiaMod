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

namespace Creaturia.Items.Accessories
{
	public class RabbitsFoot : ModItem
	{

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			DisplayName.SetDefault("Rabbit's Foot");
			Tooltip.SetDefault("'They say this is lucky... I say it's creepy.' \n" +
				"Increases luck by a small amount.\n" + 
				"1% chance to dodge attacks");
				
                           //    "When transformed can penetrate once");
		}

		public override void SetDefaults()
		{
			//Item.damage = 5;
			//Item.DamageType = DamageClass.Ranged;
			Item.width = 24;
			Item.height = 38;
			Item.maxStack = 1;
			Item.accessory = true;
			Item.consumable = true;
			//Item.knockBack = 0.2f;
			Item.value = 2000;
			Item.rare = ItemRarityID.Green;
			//Item.shoot = ModContent.ProjectileType<FishBulletProj>();
			//Item.shootSpeed = 7f;
			//Item.ammo = AmmoID.Bullet;
		}
        public override void UpdateEquip(Player player)
        {
			// Dodge chance is done inside ModPlayer!!
			player.luck += 0.15f;
			player.GetModPlayer<CreaturiaPlayer>().RabbitFootAcc = true;
        }



    }

	

}

