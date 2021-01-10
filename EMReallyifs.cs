using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EMReallyifs
{
    public class EMReallyifs : Mod
    {
        public static EMReallyifs Instance
        {
            get;
            private set;
        }

        public static bool DeveloperMode
        {
            get;
            private set;
        }

        public static string Homepage
        {
            get
            {
                if (Language.ActiveCulture == GameCulture.Chinese)
                    return "https://jq.qq.com/?_wv=1027&k=xecC4DKL";
                return "https://www.fs49.org";
            }
        }

        internal static Dictionary<int, MusicBase> OwnerMusicInstance
        {
            get;
            private set;
        }

        public EMReallyifs()
        {
            Instance = this;
            DeveloperMode = false;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                OwnerMusicInstance = new Dictionary<int, MusicBase>();
                Languages.LanguageAction = new Action<List<(string, string[])>>(delegate { });
                Code.GetTypes().ForEach(delegate (Type type, int index)
                {
                    if (type.IsClass)
                    {
                        if (type.IsSubclassOf(typeof(MusicBase)))
                        {
                            (type.GetField("instanceIndex", BindingFlags.NonPublic | BindingFlags.Static) ??
                            throw new Exception($"{type.FullName} hasnot a static int insanceIndex field.")).SetValue(null, index);
                            OwnerMusicInstance.Add(index, (MusicBase)Activator.CreateInstance(type));
                        }
                        if (type.GetInterface(typeof(ILanguageLoad).Name) != null)
                        {
                            MethodInfo info = type.GetMethod("LanguageAdd", BindingFlags.Public | BindingFlags.Instance);
                            Languages.LanguageAction += CreateTo<Action<List<(string, string[])>>>(info);
                        }
                    }
                    T CreateTo<T>(MethodInfo info) where T : Delegate => (T)info.CreateDelegate(typeof(T));
                }, true);
                DailyDay.Load();
                Main.OnPostDraw += MenuDraw.DrawCurrent;
            }
        }

        public override void PostUpdateInput() => Buttons.Update();

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                Languages.LanguageAction = null;
                DailyDay.Unload();
                Main.OnPostDraw -= MenuDraw.DrawCurrent;
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {

        }
    }
}