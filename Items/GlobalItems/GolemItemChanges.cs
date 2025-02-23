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
using Creaturia.Configs;


namespace Creaturia.Items.GlobalItems
{
    public class GolemItemChanges : GlobalItem
    {
        public override bool InstancePerEntity => true;
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

        public bool GolemFists = ModContent.GetInstance<CreaturiaSettings>().GolemFists.Contains("Enabled");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            if (GolemFists)
            {
                if (item.type == ItemID.GolemFist)

                {

                    tooltips.Add(new TooltipLine(Mod, "Tooltip#3", "20% chance to fire empowered by the Frost Queen or Pumpking"));
                    //tooltips.Add(new TooltipLine(Mod, "BuffTime", "Unknown Time"));
                }



                /*  if (item.type == ItemID.StrangePlant1)
                  {
                      tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Restores 50 mana"));
                      tooltips.Add(new TooltipLine(Mod, "Tooltip#6", "Consumable with Right Click"));
                      tooltips.Add(new TooltipLine(Mod, "Tooltip#7", "'What will happen if you eat this?'"));
                      tooltips.Add(new TooltipLine(Mod, "Tooltip#8", "??? Effect"));
                      tooltips.Add(new TooltipLine(Mod, "fdfdf", "Unkown duration"));
                  } */

                if (item.type == ItemID.ChristmasTreeSword || item.type == ItemID.BatScepter || item.type == ItemID.TheHorsemansBlade || item.type == ItemID.RavenStaff || item.type == ItemID.CandyCornRifle ||
                    item.type == ItemID.JackOLanternLauncher || item.type == ItemID.ScytheWhip || item.type == ItemID.StakeLauncher || item.type == ItemID.Razorpine || item.type == ItemID.BlizzardStaff
                    || item.type == ItemID.NorthPole || item.type == ItemID.SnowmanCannon)

                {

                    tooltips.Add(new TooltipLine(Mod, "Tooltip#1", $"[i:{ModContent.ItemType<HellborneIcon>()}] [c/8b82e7:Golem is weak to this weapon.]"));

                }
            }
        }

    }

}
        



