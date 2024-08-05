using Terraria;
using Terraria.ModLoader;
using Creaturia;
using Creaturia.Items;
using Creaturia.Items.Weapon;
using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ID;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Creaturia.Projectiles;
using Creaturia.Common.Players;

namespace Creaturia.Buffs
{
    public class BabyLumpsuckerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baby Lumpsucker");
            // Description.SetDefault("A Baby Lumpsucker fights for you!");
           Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
           
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BabyLumpsucker>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}