using Terraria;
using Terraria.ModLoader;
using Creaturia;
using Creaturia.Items;
using Creaturia.Items.Weapon;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ID;

namespace Creaturia.Buffs
{
    public class DunkleBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dunkle Spirit");
            /* Description.SetDefault("Huge increase in physical strength, but items cannot be used\n" +
                                    "Defense increased by 50, Endurance increased by 5%, Movement Speed increased by 10%"); */
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.cursed = true;
            player.maxMinions = 0;
            player.noItems = true;
            player.endurance += 0.05f;
            player.moveSpeed += 0.1f;
            player.statDefense += 50;
            
            if (Main.rand.NextBool(2))
            {
                var dust = Dust.NewDustDirect(player.position + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, DustID.CursedTorch, player.velocity.X + Main.rand.Next(-5, 5), player.velocity.Y + Main.rand.Next(-5, 5), 100, Color.White, Main.rand.NextFloat(0.5f, 2f));
                //dust.velocity.Y /= 10;
                //dust.color = new Color(180, 180, 180);
                dust.noGravity = true;
                
            }


        }
    }
}