using Terraria;
using Terraria.ModLoader;
using Creaturia;
using Creaturia.Items;
using Creaturia.Items.Weapon;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace Creaturia.Buffs
{
    public class SlipperyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slippery");
            Description.SetDefault("50% chance to dodge attacks while slippery!");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }


        // Dodge chance will be done inside ModPlayer
        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.NextBool(2))
            {
                var dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.Water, player.velocity.X, player.velocity.Y, 100, Color.DarkGray, 1);
                dust.velocity.Y /= 10;
                //dust.color = new Color(180, 180, 180);
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(55, Main.LocalPlayer);
            }
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle. Vector2 position = Main.LocalPlayer.Center; dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 196, 0f, 0f, 0, new Color(255,255,255), 1f)];

        }
        
    }
}