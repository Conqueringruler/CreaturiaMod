using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Events;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.IO;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.NPCs.Creatures;
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Creaturia.NPCs.Enemies;
using System.IO;
using Creaturia;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Creaturia.Common.Systems
{
    
    public class CreaturiaSystem : ModSystem
    {


        public override void OnWorldLoad()
        {
            if (NPC.downedSlimeKing) // The one thing that I need ModSystem for is the texture inside King Slime lmao
            {
                TextureAssets.Ninja = ModContent.Request<Texture2D>("Creaturia/NPCs/NinjaOutOfKing");
            }

        }
        public override void OnWorldUnload()
        {
            TextureAssets.Ninja = ModContent.Request<Texture2D>($"Terraria/Images/Ninja");
        }




        public static bool downedHellborne = false; // This is my first time saving whether bosses have been downed and stuff so I hope it all works right

        UserInterface spectralInterface = ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface;
      //  public override void OnWorldLoad()
      //  {
            //downedHellborne = false;
     //   }

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedHellborne)
            {
                tag["downedHellborne"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedHellborne = tag.ContainsKey("downedHellborne");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedHellborne;
            writer.Write(flags);
            // new flag 0 would be here
        }
        public override void UpdateUI(GameTime gameTime)
        {
            ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface?.Update(gameTime);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedHellborne = flags[0]; // Don't forget that the max number of flags is 8; once you reach flag 7 you start from 0 again
            flags = reader.ReadByte();
            // new flag 0 would be here
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "Spectral Watchman UI Layer",
                    delegate {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        ModContent.GetInstance<Creaturia>().SpectralWatchmanUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

    }
}
