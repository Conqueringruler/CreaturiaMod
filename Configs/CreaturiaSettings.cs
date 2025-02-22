using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader.Config;


namespace Creaturia.Configs
{
    public class CreaturiaSettings : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Settings")]

        [ReloadRequired]
        [OptionStrings(new string[] { "Disabled", "Enabled" })]
        [SliderColor(141, 56, 0)]
        [DrawTicks]
        [DefaultValue("Enabled")]
        public string GolemFists;




    }
}
