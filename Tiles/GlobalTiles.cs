using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Creaturia.NPCs.Town;
using Creaturia.NPCs.Enemies;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles.EnemyMelee;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Creaturia;
using Terraria.GameContent.ItemDropRules;
namespace Creaturia.Tiles
{
    public class GlobalTiles : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);

            
        }
    }
}
