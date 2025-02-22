using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Audio;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles;

namespace Creaturia.Items.Weapon
{
	public class ElectricEel : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			// DisplayName.SetDefault("Electric Eel");
			/* Tooltip.SetDefault("Your summons will focus struck enemies\n" +
				"100% chance to electrify enemies for 2.5 seconds minimum, up to 5 seconds when charged \n" +
				"Charging the whip will increase whip length up to 2x"); */


		}

        
        public override void SetDefaults()
		{
			// whip stuff
			Item.DefaultToWhip(ModContent.ProjectileType<EelWhipProjectile>(), 20, 2, 4);
			
			Item.shootSpeed = 4;
			Item.knockBack = 1;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(gold: 6, silver: 50);
			Item.channel = true;
			//Item.shoot = ModContent.ProjectileType<EelWhipProjectile>();
			
		}

		
		public override bool MeleePrefix()
		{
			return true;
		}
		
	}
	public class EelWhipProjectile : ModProjectile // This was one of the first things I added to the mod that's still here. As you can probably tell this code is straight from ExampleMod's example whip.
												   // That's something that, now that I'm actually planning on publishing the mod, I'm not too happy about. So keep in mind this will inevitably be rewritten.
												   // Tbh I'll probably keep the charging mechanic and just buff it since it has a lot of potential to make whips more interesting

	{
		public override void SetStaticDefaults()
		{
			// This makes the projectile use whip collision detection and allows flasks to be applied to it.
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

		public override void SetDefaults()
		{
			// This method quickly sets the whip's properties.
			Projectile.DefaultToWhip();

			// use these to change from the vanilla defaults
			// Projectile.WhipSettings.Segments = 20;
			// Projectile.WhipSettings.RangeMultiplier = 1f;
		}

		private float Timer
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value; 
		}

		private float ChargeTime
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;// so one thing I don't really understand about this - how is Projectile.ai[1] equal to the time the player has left mouse button held?
		}

		// This example uses PreAI to implement a charging mechanic.
		// If you remove this, also remove Item.channel = true from the item's SetDefaults.
		public override bool PreAI()
		{
			Player owner = Main.player[Projectile.owner];

			// Like other whips, this whip updates twice per frame (Projectile.extraUpdates = 1), so 120 is equal to 1 second.
			if (!owner.channel || ChargeTime >= 120) // I don't know what channel is
			{
				return true; // Let the vanilla whip AI run.
			}

			if (++ChargeTime % 12 == 0) // 1 segment per 12 ticks of charge.
				Projectile.WhipSettings.Segments++;

			// Increase range up to 2x for full charge. 
			Projectile.WhipSettings.RangeMultiplier += 1 / 120f;

			// Reset the animation and item timer while charging.
			owner.itemAnimation = owner.itemAnimationMax;
			owner.itemTime = owner.itemTimeMax;

			if (Main.rand.Next(1, 400) < ChargeTime)
            {
				if (owner.direction == 1)
                {
					Dust dust = Dust.NewDustDirect(new Vector2(owner.position.X - 20 + Main.rand.Next(-2, 2), owner.position.Y - 20 + Main.rand.Next(-2, 2)), owner.width, owner.height + 8, DustID.Electric, owner.velocity.X + Main.rand.Next(-1, 1), owner.velocity.Y + Main.rand.Next(-1, 1), 0, new Color(255, 0, 133), 0.4f);
					dust.noGravity = true;
					dust.fadeIn = 1f;
				}
                if (owner.direction == -1)
                {
Dust dust = Dust.NewDustDirect(new Vector2(owner.position.X + 20 + Main.rand.Next(-2, 2), owner.position.Y - 20 + Main.rand.Next(-2, 2)), owner.width, owner.height + 8, DustID.Electric, owner.velocity.X + Main.rand.Next(-1, 1), owner.velocity.Y + Main.rand.Next(-1, 1), 0, new Color(255, 0, 133), 0.4f);
				dust.noGravity = true;
				dust.fadeIn = 1f;
                }
				
				
			}
			if (ChargeTime == 20)
            {
				var ZapSound = SoundID.DD2_LightningAuraZap;
				ZapSound.Pitch = -0.6f;
				SoundEngine.PlaySound(ZapSound, owner.position);
			}
			if (ChargeTime == 80)
            {
				var ZapSound = SoundID.DD2_LightningAuraZap;
				ZapSound.Pitch = -0.4f;
				SoundEngine.PlaySound(ZapSound, owner.position);
			}
			if (ChargeTime == 100)
			{
				var ZapSound = SoundID.DD2_LightningAuraZap;
				ZapSound.Pitch = -0.2f;
				SoundEngine.PlaySound(ZapSound, owner.position);
			}
			if (ChargeTime == 110)
			{
				var ZapSound = SoundID.DD2_LightningAuraZap;
				ZapSound.Pitch = 0f;
				SoundEngine.PlaySound(ZapSound, owner.position);
			}
			if (ChargeTime == 119)
			{
				var ZapSound = SoundID.DD2_LightningAuraZap;
				ZapSound.Pitch = 0.2f;
				SoundEngine.PlaySound(ZapSound, owner.position);
			}

			return false; // Prevent the vanilla whip AI from running.
		}
        public override void AI()
        {
			if (Main.rand.NextBool(5))
			{
				
				Dust dust = Dust.NewDustDirect(Projectile.position + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3)), Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X + Main.rand.Next(-4, 4), Projectile.velocity.Y + Main.rand.Next(-4, 4), 0, new Color(255, 0, 133), 0.4f);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				
			
				
				
			}
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Electrified, 150 + ((int)ChargeTime * 2));
			Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
			Projectile.damage = (int)(Projectile.damage * 0.6f); // 30% multihit penalty 
		}

		// This method draws a line between all points of the whip, in case there's empty space between the sprites.
		private void DrawLine(List<Vector2> list)
		{
			Texture2D texture = TextureAssets.FishingLine.Value;
			Rectangle frame = texture.Frame();
			Vector2 origin = new Vector2(frame.Width / 2, 2);

			Vector2 pos = list[0];
			for (int i = 0; i < list.Count - 1; i++)
			{
				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.Blue);
				Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

				pos += diff;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			List<Vector2> list = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, list);

			DrawLine(list);

			//Main.DrawWhip_WhipBland(Projectile, list);
			// The code below is for custom drawing.
			// If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
			// However, you must adhere to how they draw if you do.

			SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Main.instance.LoadProjectile(Type);
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Vector2 pos = list[0];

			for (int i = 0; i < list.Count - 1; i++)
			{
				// These two values are set to suit this projectile's sprite
				Rectangle frame = new Rectangle(0, 0, 18, 26);
				Vector2 origin = new Vector2(5, 8);				//Rectangle frame = new Rectangle(0, 0, 10, 26);
																//Vector2 origin = new Vector2(5, 8);
				float scale = 1;

				// These statements determine what part of the spritesheet to draw for the current segment.
				// They can also be changed to suit your sprite.
				if (i == list.Count - 2)
				{
					frame.Y = 72;
					frame.Height = 24;//frame.Y = 74;
									  //frame.Height = 18;

					// For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
					Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
					float t = Timer / timeToFlyOut;
					scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
				}
				else if (i > 10)
				{
					frame.Y = 61;
					frame.Height = 10;
				}
				else if (i > 5)
				{
					frame.Y = 51;
					frame.Height = 12;
				}
				else if (i > 0)
				{
					frame.Y = 39;
					frame.Height = 16;
				}

				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
				Color color = Lighting.GetColor(element.ToTileCoordinates());

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

				pos += diff;
			}
			return false;
		}
	}
}