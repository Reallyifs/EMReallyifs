using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Terraria;
using Terraria.Localization;

namespace EMReallyifs
{
    class MenuDraw : ILanguageLoad
    {
        public void LanguageAdd(List<(string, string[])> array)
        {
            array.AddRange(new (string, string[])[]
            {
                ("MenuDraw.0", new string[]
                {
                    "Thanks for loading <EMReallyifs> !",
                    "感谢加载 <EMReallyifs> ！"
                }),
                ("MenuDraw.1", new string[]
                {
                    "You can select the music you want to play in the game in the <Config> of this Mod",
                    "你可以在此 Mod 的 <Config> 中选择你想要在游戏中播放的音乐"
                }),
                ("MenuDraw.2", new string[]
                {
                    "You can click <here> to join the group and feedback questions",
                    "你可以点击 <这里> 来进群反馈问题"
                }),
                ("MenuDraw.3", new string[]
                {
                    "Finally, have fun!",
                    "最后，玩的开心！"
                }),
                ("MenuDraw.4", new string[]
                {
                    "Today's text: ",
                    "Today's text: "
                }),
                ("MenuDraw.NotImplemented.0", new string[]
                {
                    "Thanks for loading |<EMReallyifs>",
                    "感谢加载 |<EMReallyifs>"
                }),
                ("MenuDraw.NotImplemented.2", new string[]
                {
                    "You can click |<here>",
                    "你可以点击 |<这里>"
                }),
                ("MenuDraw.NotImplemented.3", new string[]
                {
                    "<Not Implemented.>",
                    "<Not Implemented.>"
                })
            });
        }

        internal static void DrawCurrent(GameTime gameTime)
        {
            DynamicSpriteFont font = Main.fontMouseText;
            Main.spriteBatch.SafeSpriteBatch(delegate (SpriteBatch spriteBatch)
            {
                DrawText(spriteBatch);
                Main.DrawCursor(Vector2.Zero);
            });

            void DrawText(SpriteBatch sb)
            {
                int yAdd = 0;
                for (byte b = 0; b < 5; b++)
                {
                    string currentText = Languages.Get($"MenuDraw.{b}");
                    Vector2 textWH = font.MeasureString(currentText);
                    Vector2 textXY = new Vector2(20, 20 + yAdd);
                    Color textColor = b != 4 ? Color.White : Main.DiscoColor * 3f;
                    DrawTextOwner(currentText, textXY, textColor);

                    if (Languages.Exists($"MenuDraw.NotImplemented.{b}"))
                    {
                        string[] colorTexts = Languages.Get($"MenuDraw.NotImplemented.{b}").Split('|');
                        Vector2 colorTextWH = font.MeasureString(colorTexts.First());
                        Vector2 colorTextXY = new Vector2(20 + colorTextWH.X, textXY.Y);
                        colorTextWH = font.MeasureString(colorTexts.Last());

                        switch (b)
                        {
                            case 0:
                                {
                                    textColor = Color.Lerp(Color.White, Main.DiscoColor, 0.1f);
                                    DrawTextOwner(colorTexts.Last(), colorTextXY, textColor);
                                    break;
                                }
                            case 2:
                                {
                                    float amount = 0.1f;
                                    if (Functions.GetRectangle(colorTextXY, colorTextWH).ContainsMouse())
                                    {
                                        amount = Buttons.LeftPressing() ? 0.5f : 0.2f;
                                        if (Buttons.LeftReleased())
                                            Process.Start("");
                                    }
                                    textColor = Color.Lerp(Color.White, Main.DiscoColor, amount);
                                    DrawTextOwner(colorTexts.Last(), colorTextXY, textColor);
                                    break;
                                }
                        }
                    }
                    else if (b == 4)
                    {
                        textXY.X += textWH.X;
                        DrawTextOwner(DailyDay.DailyText, textXY, Color.White);
                    }

                    yAdd += 5 + (int)textWH.Y;
                }


                void DrawTextOwner(string text, Vector2 position, Color textColor, Color hideColor = default(Color))
                {
                    if (hideColor == default(Color))
                        hideColor = Color.Black;
                    Utils.DrawBorderStringFourWay(sb, font, text, (int)position.X, (int)position.Y, textColor, hideColor, Vector2.Zero);
                }
            }
        }
    }
}