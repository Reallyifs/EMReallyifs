using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.Utilities;

namespace EMReallyifs
{
    public static class DailyDay
    {
        public static bool Reseting
        {
            get;
            private set;
        }

        public static string DailyText
        {
            get;
            private set;
        }

        public static UnifiedRandom SelfRandom
        {
            get;
            private set;
        }

        static List<string> AllTextCn;
        static List<string> AllTextEn;
        static List<string> Almost;
        static List<string> Arcaea;
        static List<(string, string)> NorList;
        static List<string> Composers;
        static List<string> RhythmGame;
        static List<string> OthersSaid;

        internal static void Load()
        {
            ResetAllList(false);
            SelfRandom = new UnifiedRandom(DateTime.Today.DayOfYear);

            int luckyNumber = SelfRandom.Next(0, 101);
            AllTextCn = new List<string>()
            {
                "“" + OthersSaid[SelfRandom.Next(OthersSaid.Count)] + "”",
                "今日幸运数字：" + luckyNumber,
                "快来试试 <" + RhythmGame[SelfRandom.Next(RhythmGame.Count)] + ">",
                "你又" + Almost[SelfRandom.Next(Almost.Count)] + "啦！",
                Arcaea[SelfRandom.Next(Arcaea.Count)],
                Composers[SelfRandom.Next(Composers.Count)] + "，永远滴神"
            };
            do { luckyNumber = SelfRandom.Next(0, 101); } while (luckyNumber == 13);
            AllTextEn = new List<string>()
            {
                "\"" + OthersSaid[SelfRandom.Next(OthersSaid.Count)] + "\"",
                "Today's lucky number: " + luckyNumber,
                "Also try <" + RhythmGame[SelfRandom.Next(RhythmGame.Count)] + ">",
                "You " + Almost[SelfRandom.Next(Almost.Count)] + " again!",
                Arcaea[SelfRandom.Next(Arcaea.Count)],
                Composers[SelfRandom.Next(Composers.Count)] + ", god of forever"
            };

            NorList.ForEach(delegate ((string, string) itemString)
            {
                AllTextEn.Add(itemString.Item1);
                AllTextCn.Add(string.IsNullOrEmpty(itemString.Item2) ? itemString.Item1 : itemString.Item2);
            });

            List<List<string>> array = new List<List<string>>()
            {
                AllTextCn,
                AllTextEn
            };

            array.ForEach(delegate (List<string> listThis)
            {
                if (EMReallyifs.DeveloperMode)
                {
                    for (int i = 0; i < listThis.Count; i++)
                        listThis[i] += $" —— index: {i}";
                }
            });

            List<string> selectList = array[GameCulture.Chinese.IsActive.ToInt()];

            SelfRandom = new UnifiedRandom(DateTime.Today.DayOfYear);
            DailyText = selectList[SelfRandom.Next(selectList.Count)];
        }

        internal static void Unload()
        {
            ResetAllList(true);
            DailyText = null;
            AllTextCn = null;
            AllTextEn = null;
            SelfRandom = null;
        }

        static void ResetAllList(bool resetingSet = true)
        {
            Reseting = resetingSet;
            Type dailyDayType = typeof(DailyDay);
            BindingFlags searchFor = BindingFlags.NonPublic | BindingFlags.Static;
            foreach(FieldInfo info in dailyDayType.GetFields(searchFor))
            {
                if (info.Name == "AllTextCn" || info.Name == "AllTextEn")
                    continue;
                dailyDayType.GetMethod($"{info.Name}Reset", searchFor)?.Invoke(null, null);
                if (EMReallyifs.DeveloperMode)
                    EMReallyifs.Instance.Logger.Debug($"{info.Name} Reset for {resetingSet}");
            }
        }

        static void AlmostReset()
        {
            Almost = !Reseting ? new List<string>()
            {
                "1 good",
                "1 far",
                "1 lost",
                "1 miss",
                "性"
            } : null;
        }

        static void ArcaeaReset()
        {
            if (!Reseting)
            {
                Arcaea = new List<string>();
                ArcaeaAdd("Sheriruth", new List<string>()
                {
                    "拒绝",
                    "射日如桃花",
                    "黑魔王"
                });
                ArcaeaAdd("PRAGMATISM", new List<string>()
                {
                    "实用主义",
                    "幽灵",
                    "白魔王"
                });
                ArcaeaAdd("Grievous Lady", new List<string>()
                {
                    "伤心的女士",
                    "病女",
                    "里黑"
                });
                ArcaeaAdd("Fracture Ray", new List<string>()
                {
                    "破碎的光线",
                    "骨折光",
                    "里白"
                });
                ArcaeaAdd("Ringed Genesis", new List<string>()
                {
                    "环状成因",
                    "黑白魔王"
                });
                ArcaeaAdd("Tempestissmo", new List<string>()
                {
                    "风暴",
                    "猫魔王（？"
                });
                ArcaeaAdd("Arcahv", new List<string>() { "聚集" });
                Arcaea.AddRange(new string[]
                {
                    "BABCABC TO ABCABBAA, BABCABC TO ABAABBCA",
                    "完形填空完形填空完形填空完形填空填空填空填空填空 Ah Crazy"
                });
                return;
                void ArcaeaAdd(string text, List<string> array) => Arcaea.Add($"{text} - {array[SelfRandom.Next(array.Count)]}");
            }
            Arcaea = null;
        }

        static void NorListReset()
        {
            NorList = !Reseting ? new List<(string, string)>()
            {
                ("Edelritter(x) Nhato and Taishi(?)", null),
                ("Endorfin.(x) sky_delta and Aitsuki Nakuru(?)", null),
                ("here you arrrrrrrrrrrrrrrrr--", "你在zheeeeeeeeeeeee——"),
                ("Hikari and Tairitsu fight, i t ' s  m e  w h o  d i e d .", "光与对立厮杀，死 的 事 我（"),
                ("I know the truth, but why is this pigeon so big?", "道理我都懂，但是这只鸽子为什么这么大？"),
                ("Ko~ko~da~yo~~~~~~", null),
                ("lbwnb", null),
                ("This is rain. I walked in the rain, but failed to walk into your heart.", "这是场雨。我走在雨中，却没能走进你的心里。"),
                ("tips好难想，不想了", null),
                ("Today's text: Today's text: Today's text: Today's text: Today's text:", null),
                ("WHAT HAPPENED TO MY MOD???!?!?!?!?", "你对我的Mod做了什么？！？！？？！？！)"),
                ("あの日見た花の名前を僕達はまだ知らない。", "《我们仍未知道那天所看见的花的名字。》"),
                ("大括号不换行的跟换行的打，变量名首字母大写的跟不大写的打，私有域前面加下划线的跟不加下划线的打", null),
                ("口古", null),
                ("你盘子又碎啦", null),
                ("你知道吗：我不知道", null),
                ("我很可爱，请给我钱.jpg", null),
                ("我祝你打牌3456没有7", null),
                ("再氪一单嘛.jpg", null),
                ("爪巴", null)
            } : null;
        }

        static void ComposersReset()
        {
            Composers = !Reseting ? new List<string>()
            {
                "ak+q",
                "ARForest",
                "Blacklolita",
                "BlackY",
                "Cosmograph",
                "EBIMAYO",
                "EDP",
                "ELE SHEEP",
                "Feryquitous",
                "HyuN",
                "John Stanford",
                "Juggernaut.",
                "Jun Kuroda",
                "Laur",
                "LeaF",
                "M2U",
                "MYUKEE.",
                "Nitro",
                "Powerless",
                "Puru",
                "REDALiCE",
                "Sakuzyo",
                "Se-U-Ra",
                "Silentroom",
                "sky_delta",
                "Sound Souler",
                "Sta",
                "t+pazolite",
                "Taishi",
                "Team Grimoire",
                "USAO",
                "Virtual Self",
                "WHITEFISTS",
                "WyvernP",
                "xi",
                "姜米條"
            } : null;
        }

        static void RhythmGameReset()
        {
            RhythmGame = !Reseting ? new List<string>()
            {
                "Arcaea",
                "BanG Dream",
                "Beat Blader",
                "CHUNITHM",
                "Cytoid",
                "Cytus II",
                "Cytus",
                "DANCERUSH",
                "DEMMO",
                "Dynamite",
                "Dynamix",
                "Groove Coaster",
                "jubeat",
                "Lanota",
                "maimai",
                "Malody",
                "Muse Dash",
                "osu!",
                "Phigros",
                "polytone",
                "Project FX",
                "Project:Muse",
                "Rizline",
                "Roteano",
                "SOUND VOLTEX",
                "Tone Sphere",
                "VOEZ",
                "钢琴师",
                "节奏大师",
                "同步音律喵塞克"
            } : null;
        }

        static void OthersSaidReset()
        {
            OthersSaid = !Reseting ? new List<string>()
            {
                "No one understands COVID-19 better than me",
                "工作量我暂且蒙在鼓里",
                "林，你还好吗。",
                "林，你还好吗？",
                "林，你还在吗？",
                "时间回到过去，开始即为结局。",
                "想知道为什么吗？我加了一点神奇的仙女魔法哦~",
                "像素塔真的存在吗。"
            } : null;
        }
    }
}