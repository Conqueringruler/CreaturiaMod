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
    public class FishmanBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blessing of the Fishman");
            Description.SetDefault("Huge increase in movement and melee speed, and amplifies your senses!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.35f;
            player.dangerSense = true;
            player.detectCreature = true;
            player.nightVision = true;
            player.gills = true;
            player.moveSpeed += 0.5f;
            player.frogLegJumpBoost = true;
            player.trident = true;
            if (player.HeldItem.type != ModContent.ItemType<TridentoftheFishman>())
            {
                player.ClearBuff(ModContent.BuffType<FishmanBuff>());
            }
            
        }
    }
}