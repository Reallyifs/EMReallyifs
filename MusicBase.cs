using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EMReallyifs
{
    public abstract class MusicBase : ModSound, ILanguageLoad
    {
        public abstract bool CanPlay(Player player);

        internal abstract void PlayNow(Player player);

        public abstract void LanguageAdd(List<(string, string[])> array);
    }
}