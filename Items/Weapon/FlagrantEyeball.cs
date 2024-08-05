using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creaturia.Items.Weapon
{
	public class FlagrantEyeball : ModItem
	{
		public override void SetStaticDefaults()
		{
			 // DisplayName.SetDefault("Flagrant Eye"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			// Tooltip.SetDefault("'It's the Eye of the luuumpfiish'");
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot; 
			Item.useAnimation = 45; //time in ticks (60 ticks == 1 second.)
			Item.useTime = 45; 
			Item.knockBack = 5.5f; 
			Item.width = 32;
			Item.height = 32; 
			Item.damage = 25; 
			Item.noUseGraphic = true; 
			Item.shoot = ModContent.ProjectileType<LumpEye>(); 
			Item.shootSpeed = 12f; // The speed of the projectile measured in pixels per frame.
			Item.UseSound = SoundID.Item1; // The sound that this item makes when used
			Item.rare = ItemRarityID.Green; // The color of the name of your item
			Item.value = Item.sellPrice(gold: 1, silver: 50); 
			Item.DamageType = DamageClass.MeleeNoSpeed; // Deals melee damage
			Item.channel = true;
			Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
		}

		
	}
}