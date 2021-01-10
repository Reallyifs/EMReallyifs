using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace EMReallyifs
{
    public static class Functions
    {
        public static bool InAnyEnvironment(Player player)
        {
            return InAnyEvil(player) || InAnyTower(player) || InAnyOverworldEnvironment(player) || InAnyUndergroundEnvironment(player);
        }

        public static bool InAnyUndergroundEnvironment(Player player)
        {
            return player.ZoneDungeon || player.ZoneGlowshroom || player.ZoneUndergroundDesert;
        }

        public static bool InAnyOverworldEnvironment(Player player)
        {
            return player.ZoneBeach || player.ZoneDesert || player.ZoneJungle || player.ZoneMeteor || player.ZoneSnow;
        }

        public static bool InAnyEvil(Player player) => player.ZoneCorrupt || player.ZoneCrimson || player.ZoneHoly;

        public static bool InAnyTower(Player player)
        {
            return player.ZoneTowerNebula || player.ZoneTowerSolar || player.ZoneTowerStardust || player.ZoneTowerVortex;
        }

        public static bool InTower(Player player, bool Nebula = false, bool Solar = false, bool Stardust = false, bool Vortex = false)
        {
            return (Nebula && player.ZoneTowerNebula) ||
                (Solar && player.ZoneTowerSolar) ||
                (Stardust && player.ZoneTowerStardust) ||
                (Vortex && player.ZoneTowerVortex);
        }

        public static bool InDirtOrRockLayer(Player player) => player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight;

        public static bool InOverworldOrHigher(Player player) => player.ZoneOverworldHeight || player.ZoneSkyHeight;

        public static bool TimeIn(bool morning = false, bool noon = false, bool afternoon = false, bool eve = false, bool midnight = false,
            bool earlyMorning = false)
        {
            if (morning && Main.dayTime && Main.time < Main.dayLength / 3d)
                return true;
            if (noon && Main.dayTime && Main.time > Main.dayLength / 3d && Main.time < Main.dayLength * (2d / 3d))
                return true;
            if (afternoon && Main.dayTime && Main.time > Main.dayLength * (2d / 3d))
                return true;
            if (eve && !Main.dayTime && Main.time < Main.nightLength / 3d)
                return true;
            if (midnight && !Main.dayTime && Main.time > Main.nightLength / 3d && Main.time < Main.nightLength * (2d / 3d))
                return true;
            if (earlyMorning && !Main.dayTime && Main.time > Main.nightLength * (2d / 3d))
                return true;
            return false;
        }

        public static void RainSet(bool raining, uint rainTimeMax = 500)
        {
            rainTimeMax = Math.Min(rainTimeMax, 500);
            Main.raining = raining;
            Main.rainTime = (int)rainTimeMax;
        }

        public static void TimeSet(bool dayTime, double dayMax = Main.dayLength, double nightMax = Main.nightLength)
        {
            double setTime = dayTime ? dayMax : nightMax;
            setTime = Math.Min(setTime, dayTime ? Main.dayLength : Main.nightLength);
            setTime = Math.Max(0, setTime);
            if (setTime == Main.dayLength && dayTime)
                setTime = 0;
            else if (setTime == Main.nightLength && !dayTime)
                setTime = 0;
            Main.dayTime = dayTime;
            Main.time = setTime;
        }

        public static string TypeName(this object obj) => obj.GetType().Name;

        public static void ForEach<T>(this T[] array, Action<T, int> action, bool sort = false, T[] pre = null, T[] post = null)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (action == null)
                throw new ArgumentNullException("action");
            List<T> newList = array.ToList();
            if (sort)
            {
                newList.Sort(Comparer<T>.Create(delegate (T item1, T item2)
                {
                    if (item1 == null || item2 == null)
                    {
                        if (item2 == null)
                            return -1;
                        if (item1 == null)
                            return 1;
                        return 0;
                    }
                    return item1.TypeName().CompareTo(item2.TypeName());
                }));
            }
            if (pre != null)
            {
                for (int i = pre.Length - 1; i > -1; i--)
                {
                    if (newList.Contains(pre[i]))
                    {
                        newList.Remove(pre[i]);
                        newList.Insert(0, pre[i]);
                    }
                }
            }
            if (post != null)
            {
                for (int i = post.Length - 1; i > -1; i--)
                {
                    if (newList.Contains(post[i]))
                    {
                        newList.Remove(post[i]);
                        newList.Add(post[i]);
                    }
                }
            }
            for (int i = 0; i < newList.Count; i++)
            {
                T item = newList[i];
                action(item, i);
            }
        }

        public static Rectangle GetRectangle(Vector2 position, Vector2 size)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public static bool ContainsMouse(this Rectangle rectangle) => rectangle.Contains(Main.mouseX, Main.mouseY);

        public static void SafeSpriteBatch(this SpriteBatch spriteBatch, Action<SpriteBatch> action)
        {
            bool sureEnd;
            try
            {
                spriteBatch.End();
                sureEnd = true;
            }
            catch { sureEnd = false; }
            spriteBatch.Begin();
            action?.Invoke(spriteBatch);
            if (!sureEnd)
                spriteBatch.End();
        }
    }
}