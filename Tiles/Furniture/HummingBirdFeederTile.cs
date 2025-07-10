using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Audio;

namespace Creaturia.Tiles.Furniture
{
    public class HummingBirdFeederTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.AnchorTop = new AnchorData(KindaLanternAchor, TileObjectData.newTile.Width + 1, 0); ;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            AddMapEntry(new Color(160, 4, 16), Language.GetText("MapObject.Lantern"));
            AdjTiles = new int[] { TileID.HangingLanterns };
            
            
        }
        const AnchorType KindaLanternAchor = AnchorType.PlatformNonHammered | AnchorType.SolidBottom;
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
           for (int h = 1; h < 4; h++)
            {

            
            if (TileID.Sets.Platforms[Main.tile[i, j - h].TileType])
            {
                offsetY = -8;
            }
        }
          
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
           
                r = 0.6f;
                g = 0.1f;
                b = 0.2f;
            
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                return;
            }
                Main.SceneMetrics.HasHeartLantern = true;
            
        }
        public override bool RightClick(int i, int j)
        {
            SoundEngine.PlaySound(SoundID.LiquidsHoneyWater);
            Main.LocalPlayer.AddBuff(BuffID.Honey, 40);
            return true;
        }
    }
}
