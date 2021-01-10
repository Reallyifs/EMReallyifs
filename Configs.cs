using EMReallyifs.Sounds.Music;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EMReallyifs
{
    public class Configs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.EMReallyifs.Header.ShowMenu")]

        [Label("$Mods.EMReallyifs.ShowHelpInMenuLabel")]
        [DefaultValue(true)]
        public bool showHelpInMenu;

        [Label("$Mods.EMReallyifs.ShowDailyTextInMenuLabel")]
        [Tooltip("$Mods.EMReallyifs.ShowDailyTextInMenuTooltip")]
        [DefaultValue(true)]
        public bool showDailyTextInMenu;

        [Header("$Mods.EMReallyifs.Header.Music")]

        [Label("$" + AF.GetName)]
        [Tooltip("$" + AF.GetTooltip)]
        [DefaultValue(false)]
        public bool configAF;
    }
}