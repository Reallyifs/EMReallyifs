using System.Collections.Generic;
using Terraria;

namespace EMReallyifs.Sounds.Music
{
    public sealed class CnC : MusicBase
    {
        public const string GetName = Languages.GetMusicNameStart + ".CnC";
        public const string GetTooltip = Languages.GetMusicTooltipStart + ".CnC";
        public const string GetPlaybackConditions = Languages.GetPlaybackConditionsStart + ".CnC";

        public override bool CanPlay(Player player)
        {
            if (Main.dayTime && Main.raining && player.ZoneSkyHeight)
                return !Functions.InAnyEnvironment(player);
            return false;
        }

        public override void LanguageAdd(List<(string, string[])> array)
        {
            array.AddRange(new (string, string[])[]
            {
                ($"{Languages.AddMusicNameStart}.CnC", new string[]
                {
                    "Cloudy, no Confuse",
                    "多云，无疑"
                }),
                ($"{Languages.AddMusicTooltipStart}.CnC", new string[]
                {
                    "There is no sun... but drizzle is quite romantic.",
                    "没有太阳……但细雨也挺浪漫的。"
                }),
                ($"{Languages.AddPlaybackConditionsStart}.CnC", new string[]
                {
                    "Play when: Daytime, Raining, Not in any environment",
                    "当满足以下条件时播放：白天，下雨，天空，不在任何环境中"
                })
            });
        }

        internal override void PlayNow(Player player)
        {
            Functions.TimeSet(true);
            Functions.RainSet(true);
        }
    }
}