using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Audio;


namespace Creaturia.Items.GlobalItems
{
    public class ItemChanges : GlobalItem
    {

        /*   public override void AddRecipes()
           {
               Mod.CreateRecipe(ItemID.FirstFractal, 1)
               .AddIngredient(ItemID.EmpressBlade, 1)
               .AddIngredient(ItemID.TerraBlade, 1)
               .AddTile(TileID.LunarCraftingStation)
               .Register();
           } */



        //  if (Item. == ItemID.EmpressBlade)
        //{
        //  item.material = true;
        //  }
        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ItemID.VikingHelmet)
            {
                player.npcTypeNoAggro[NPCID.UndeadViking] = true; // in globalNPCs I add the check for them being at full health, if not they attack again
                player.npcTypeNoAggro[NPCID.ArmoredViking] = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //TooltipLine line;
            base.ModifyTooltips(item, tooltips);
            //  if (item.type == ItemID.Seaweed || item.type == ItemID.SiltBlock || item.type == ItemID.SiltBlock)
            //  {
            //       tooltips.Add(new TooltipLine(Mod, "fdfdf", "Can be extractinated"));
            //    }
            if (item.type == ItemID.ThornHook)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#0", "Enemies hit while the player is hooked will be poisoned for 5 seconds"));
            }

            if (item.type == ItemID.VikingHelmet)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#0", "When worn Undead Vikings and Armored Vikings no longer target you"));
            }

            if (item.type == ItemID.GolemFist)

            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#3", "20% chance to fire empowered by the Frost Queen or Pumpking"));
                //tooltips.Add(new TooltipLine(Mod, "BuffTime", "Unknown Time"));
            }

            if (item.type == ItemID.YellowMarigold)

            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 10 life")); // I'm gonna be honest, I have no idea what I should put in the first section or what ramifications it may have. it'll prob be fine though
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'Its either the really healthy kind of marigold or the poisonious kind. Pretty hard to tell.'"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "50% chance of recieving either rapid healing or poison for a varied amount of time"));
                //tooltips.Add(new TooltipLine(Mod, "BuffTime", "Unknown Time"));
            }
            if (item.type == ItemID.BlueBerries)

            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 30 life"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'Sweet!'"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "Gives Sugar Rush"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "30 second duration"));
            }
            if (item.type == ItemID.TealMushroom)

            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 25 life"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'Looks juicy'"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "Gives Calm"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "1 minute duration"));
            }

            if (item.type == ItemID.PinkPricklyPear)

            {
                //tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 25 life"));
                //tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'Eat carefully!'"));
                //tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "Minor improvements to all stats"));
                //tooltips.Add(new TooltipLine(Mod, "fdfdf", "2 minutes"));
            }

            if (item.type == ItemID.OrangeBloodroot)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 10 life"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'Dissuades predators with its liquid cortisol. That clearly didn't stop you though.'"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "Consuming makes you panic"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "15 second duration"));
            }

            if (item.type == ItemID.SkyBlueFlower)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 25 mana"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                //tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "''"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "50% chance of recieving either Mana Power or Mana Regeneration"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "45 second duration"));
            }

            if (item.type == ItemID.GreenMushroom)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 175 life"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                //tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "''"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "Intense healing short-term, but gives poison and potion sickness"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "1 minute duration"));
            }

            if (item.type == ItemID.StrangePlant1)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 50 mana"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'What will happen if you eat this?'"));
                tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "??? Effect"));
                tooltips.Add(new TooltipLine(Mod, "fdfdf", "Unkown duration"));
            }
            if (item.type == ItemID.ChristmasTreeSword || item.type == ItemID.BatScepter || item.type == ItemID.TheHorsemansBlade || item.type == ItemID.RavenStaff || item.type == ItemID.CandyCornRifle ||
                item.type == ItemID.JackOLanternLauncher || item.type == ItemID.ScytheWhip || item.type == ItemID.StakeLauncher || item.type == ItemID.Razorpine || item.type == ItemID.BlizzardStaff
                || item.type == ItemID.NorthPole || item.type == ItemID.SnowmanCannon)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip#1", $"[i:{ModContent.ItemType<HellborneIcon>()}] [c/8b82e7:Golem is weak to this weapon.]"));
            }
        }

        public override void SetDefaults(Item item)
        {
          
            if (item.type == ItemID.EmpressBlade)
            {
                item.material = true;
            }

            if (item.type == ItemID.FirstFractal)
            {
                item.DamageType = DamageClass.Summon;
                item.damage = 245;

            }
            if (item.type == ItemID.Prismite)
            {

            }
            if (item.type == ItemID.PinkPricklyPear)
            {
                ItemID.Sets.FoodParticleColors[item.type] = new Color[4]
                {
                new Color(255,182,193),
                new Color(144,238,144),
                new Color(173,255,47),
                new Color(255,228,225),
                };
                item.DefaultToFood(22, 22, BuffID.WellFed, 7200);
                item.healLife = 25;
            }

            /* if (item.type == ItemID.YellowMarigold)
             {
                 ItemID.Sets.FoodParticleColors[item.type] = new Color[2]
                 {
                 new Color(200, 100, 20),
                 new Color(152, 93, 30)
                 };
                 item.consumable = true;
                 item.useTime = 15;
                 item.useTurn = true;
                 item.useStyle = ItemUseStyleID.EatFood;
                 item.UseSound = SoundID.Item2;
                 item.useAnimation = 15;
                 item.healLife = 10;
             }
             if (item.type == ItemID.BlueBerries)
             {
                 ItemID.Sets.FoodParticleColors[item.type] = new Color[3]
                 {
                 new Color(5, 15, 180),
                 new Color(10, 20, 200),
                 new Color(4, 11, 250)
                 };
               //  item.consumable = true;
                // item.useTime = 15;
                // item.useTurn = true;
                // item.useStyle = ItemUseStyleID.EatFood;
                 item.UseSound = SoundID.Item2;
                 item.useAnimation = 15;
                 item.healLife = 30;
             } 
             if (item.type == ItemID.OrangeBloodroot)
             {
                 ItemID.Sets.FoodParticleColors[item.type] = new Color[3]
                 {
                 new Color(150, 25, 10),
                 new Color(100, 12, 2),
                 new Color(255, 30, 10)
                 };
                // item.consumable = true;
                 item.useTime = 15;
                 item.useTurn = true;
                 item.useStyle = ItemUseStyleID.EatFood;
                 item.UseSound = SoundID.Item2;
                 item.useAnimation = 15;
                 item.healLife = 10;
             }
             if (item.type == ItemID.SkyBlueFlower)
             {
                 ItemID.Sets.FoodParticleColors[item.type] = new Color[3]
                 {
                 new Color(5, 15, 180),
                 new Color(10, 20, 200),
                 new Color(4, 11, 250)
                 };
                // item.consumable = true;
                 item.useTime = 15;
                 item.useTurn = true;
                 item.useStyle = ItemUseStyleID.EatFood;
                 item.UseSound = SoundID.Item2;
                 item.useAnimation = 15;
                 item.healLife = 25;
             } */
        }
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.type == ItemID.YellowMarigold)
            {
                player.HealEffect(10, true);
                player.statLife += 10;
                if (Main.rand.NextBool() == true)
                {
                    player.AddBuff(BuffID.Poisoned, Main.rand.Next(150, 1000));
                    player.AddBuff(BuffID.WellFed, 1);
                }
                else
                {
                    player.AddBuff(BuffID.RapidHealing, Main.rand.Next(200, 1400));
                    player.AddBuff(BuffID.WellFed, 1);
                }
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.YellowGreen, 1f);
                }
                SoundEngine.PlaySound(SoundID.Item2, player.position);
                item.stack -= 1;
            }
            if (item.type == ItemID.BlueBerries)
            {
                player.HealEffect(30, true);
                player.statLife += 30;
                player.AddBuff(BuffID.SugarRush, 1800);
                player.AddBuff(BuffID.WellFed, 1);
                SoundEngine.PlaySound(SoundID.Item2, player.position);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.DarkBlue, 1f);
                }
                item.stack -= 1;
            }

            if (item.type == ItemID.GreenMushroom)
            {
                if (player.HasBuff(BuffID.PotionSickness))
                {
                    return false;
                }
                else
                {
                    player.HealEffect(175, true);
                    player.statLife += 175;
                    player.AddBuff(BuffID.Poisoned, 2000);
                    player.AddBuff(BuffID.PotionSickness, 3600);
                    player.AddBuff(BuffID.WellFed, 1);
                    SoundEngine.PlaySound(SoundID.Item2, player.position);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.DarkBlue, 1f);
                    }
                    item.stack -= 1;
                }
            }

            if (item.type == ItemID.TealMushroom)
            {
                player.HealEffect(20, true);
                player.statLife += 20;
                player.AddBuff(BuffID.Calm, 3600);
                player.AddBuff(BuffID.WellFed, 1);
                SoundEngine.PlaySound(SoundID.Item2, player.position);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.LightBlue, 1f);
                }
                item.stack -= 1;
            }
            if (item.type == ItemID.OrangeBloodroot)
            {
                player.HealEffect(10, true);
                player.statLife += 10;
                player.AddBuff(BuffID.Panic, 900);
                player.AddBuff(BuffID.WellFed, 1);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.Red, 1f);
                }
                SoundEngine.PlaySound(SoundID.Item2, player.position);
                item.stack -= 1;
            }
            if (item.type == ItemID.SkyBlueFlower)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(player.Top, 2, 2, DustID.FoodPiece, 0f, 0f, 0, Color.SkyBlue, 1f);
                }
                player.ManaEffect(25);
                
                player.statMana += 25;
                if (Main.rand.NextBool() == true)
                {
                    player.AddBuff(BuffID.ManaRegeneration, 2700);
                    player.AddBuff(BuffID.WellFed, 1);
                }
                else
                {
                    player.AddBuff(BuffID.MagicPower, 2700);
                    player.AddBuff(BuffID.WellFed, 1);
                }
                SoundEngine.PlaySound(SoundID.Item2, player.position);
                item.stack -= 1;
            }
            return default;
        }
        public override void OnConsumeItem(Item item, Player player)
        {
           
            /* if (item.type == ItemID.YellowMarigold)
             {
                 if (Main.rand.NextBool() == true)
                 {
                     player.AddBuff(BuffID.Poisoned, Main.rand.Next(200, 1600));
                 }
                 else
                 {
                     player.AddBuff(BuffID.RapidHealing, Main.rand.Next(200, 1600));
                 }
             }

             if (item.type == ItemID.BlueBerries)
             {
                 player.AddBuff(BuffID.SugarRush, 1800);
             }

             if (item.type == ItemID.OrangeBloodroot)
             {
                 player.AddBuff(BuffID.Panic, 900);
             }

             if (item.type == ItemID.SkyBlueFlower)
             {
                 if (Main.rand.NextBool() == true)
                 {
                     player.AddBuff(BuffID.ManaRegeneration, 2700);
                 }
                 else
                 {
                     player.AddBuff(BuffID.MagicPower, 2700);
                 }
             }
            */
        }
    }
}
        



