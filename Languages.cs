using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace EMReallyifs
{
    class Languages : ILanguageLoad
    {
        public const string AddMusicNameStart = "MusicName";
        public const string GetMusicNameStart = "Mods.EMReallyifs." + AddMusicNameStart;
        public const string AddMusicTooltipStart = "MusicTooltip";
        public const string GetMusicTooltipStart = "Mods.EMReallyifs." + AddMusicTooltipStart;
        public const string AddPlaybackConditionsStart = "MusicPlaybackConditions";
        public const string GetPlaybackConditionsStart = "Mods.EMReallyifs." + AddPlaybackConditionsStart;

        internal static Action<List<(string, string[])>> LanguageAction;

        public void LanguageAdd(List<(string, string[])> array)
        {
            array.AddRange(new (string, string[])[]
            {
                ("Header.ShowMenu", new string[]
                {
                    "- Show in menu -",
                    "- 界面 -"
                }),
                ("Header.Music", new string[]
                {
                    "- Music in gaming -",
                    "- 音乐 -"
                }),
                ("ShowHelpInMenuLabel", new string[]
                {
                    "Show help in menu",
                    "在界面显示帮助"
                }),
                ("ShowDailyTextInMenuLabel", new string[]
                {
                    "Show daily text in menu",
                    "在界面显示每日一言"
                }),
                ("ShowDailyTextInMenuTooltip", new string[]
                {
                    "Only takes effect when \"Show help in menu\" is turned on",
                    "只在打开“在界面显示帮助”时生效"
                }),
            });
        }

        public static string Get(string key, params string[] args)
        {
            key = $"Mods.EMReallyifs.{key}";
            string resultText = Language.GetTextValue(key, args);
            if (resultText != key)
                return resultText;
            throw new ArgumentException($"Are you sure your \"key\" is spelled right?", "key");
        }

        public static bool Exists(string key) => Language.Exists($"Mods.EMReallyifs.{key}");
    }
}