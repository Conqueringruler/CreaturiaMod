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
	public class AncientDartRifle : ModItem // PROJECTILE IS IN HERE TOO
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Dart Rifle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			/* Tooltip.SetDefault("'Antique' \n"
							 + "Highly accurate, but slow"); */
			//ItemID.Sets.Spears[Item.type] = true;
			
		}
		
		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.shootSpeed = 12f;
			Item.crit = 1;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 32;
			Item.useTime = 32;
			Item.knockBack = 5.25f;
			Item.width = 40;
			Item.height = 18;
			Item.damage = 36;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item11;
		//	Item.shootSpeed = 4f; // The speed of the projectile measured in pixels per frame
			Item.rare = ItemRarityID.Orange; 
			Item.value = Item.sellPrice(gold: 5, silver: 50);
			Item.DamageType = DamageClass.Ranged;
			//Item.ammo = AmmoID.Dart;
			Item.shoot = ProjectileID.PurificationPowder; // wtf whhy
			Item.useAmmo = AmmoID.Dart;
			//Item.channel = true;
			
			
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8f, 1f);
		}
		public override void AddRecipes()
        {
			CreateRecipe()
			.AddIngredient(ItemID.Musket)
			.AddIngredient(ModContent.ItemType<AncientDartGunPieces>())
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
		}


    }
}