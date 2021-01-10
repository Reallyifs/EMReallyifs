using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EMReallyifs.Sounds.Music
{
    /// <summary>
    /// Always Frosted
    /// </summary>
    public sealed class AF : MusicBase
    {
        public const string GetName = Languages.GetMusicNameStart + ".AF";
        public const string GetTooltip = Languages.GetMusicTooltipStart + ".AF";
        public const string GetPlaybackConditions = Languages.GetPlaybackConditionsStart + ".AF";

        internal static int instanceIndex;

        public override bool CanPlay(Player player)
        {
            if (!Main.dayTime && Main.raining && player.ZoneSnow)
                return WorldGen.checkUnderground((int)player.Center.X, (int)player.Center.Y);
            return false;
        }

        public override void LanguageAdd(List<(string, string[])> array)
        {
            array.AddRange(new (string, string[])[]
            {
                ($"{Languages.AddMusicNameStart}.AF", new string[]
                {
                    "Always Frosted",
                    "永是寒霜"
                }),
                ($"{Languages.AddMusicTooltipStart}.AF", new string[]
                {
                    "In the cold...",
                    "在寒冷中……"
                }),
                ($"{Languages.AddPlaybackConditionsStart}.AF", new string[]
                {
                    "Play when: Night, Raining, Snow and Underground",
                    "当满足以下条件时播放：夜晚，下雨，雪地地下"
                })
            });
        }

        internal override void PlayNow(Player player)
        {
            Functions.TimeSet(false);
            Functions.RainSet(true);
        }

        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance.State == SoundState.Playing)
                return null;
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            soundInstance.Pitch = 0.5f;
            return soundInstance;
        }
    }
}