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
	public class AncientStoneShortsword : ModItem // PROJECTILE IS IN HERE TOO
	{
		public override string Texture => "Terraria/Images/Gore_" + 265;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Stone Shortsword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'Looks like it will crumble in a single hit'");
			//ItemID.Sets.Spears[Item.type] = true;
			
		}
		
		public override void SetDefaults()
		{
			
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 6; //time in ticks (60 ticks == 1 second.)
			Item.useTime = 8;
			Item.knockBack = 2.5f;
			Item.width = 32;
			Item.height = 32;
			Item.damage = 16;
		//	Item.shoot = ModContent.ProjectileType<TridentProjectile>();
		//	Item.shootSpeed = 4f; // The speed of the projectile measured in pixels per frame.
			Item.UseSound = SoundID.Item1; // The sound that this item makes when used
			Item.rare = ItemRarityID.Green; // The color of the name of your item
			Item.value = Item.sellPrice(gold: 0, silver: 50);
            Item.DamageType = DamageClass.Melee; // Deals melee damage
			//Item.channel = true;
			
			
		}
		
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			
            SoundEngine.PlaySound(SoundID.Shatter);
			Item.NewItem(Item.GetSource_None(), player.Center, ItemID.StoneBlock, Main.rand.Next(1, 6));
			Item.NewItem(Item.GetSource_None(), player.Center, ItemID.StoneBlock, Main.rand.Next(1, 6));
			for (int i = 0; i < 20; i++)
            {
				Dust.NewDust(player.position, Item.width, Item.height, DustID.Stone, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), default, Color.White, Main.rand.NextFloat(0.9f, 1.3f)) ;
		}
			Item.useAnimation = 0;
			Item.alpha = 255;
			Item.TurnToAir();

		}
        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            base.OnHitPvp(player, target, damage, crit);
        }
    }
}