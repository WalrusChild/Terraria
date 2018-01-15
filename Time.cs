using System;
using System.Text.RegularExpressions;
using PluginLoader;
using Terraria;

namespace WalrusPlugins
{
    public class Time : MarshalByRefObject, IPluginChatCommand
    {
        private void ChangeTime(string h, string m)
        {
            if (Regex.IsMatch(h, @"^\d+$") && Regex.IsMatch(m, @"^\d+$"))
            {
                if (Convert.ToInt32(h) > 0 && Convert.ToInt32(h) < 24 && Convert.ToInt32(m) >= 0 && Convert.ToInt32(m) < 60)
                {
                    string hh, mm;

                    if (h.Length == 1)
                    {
                        hh = h.Insert(0, "0");
                    }
                    else
                    {
                        hh = h;
                    }

                    if (m.Length == 1)
                    {
                        mm = m.Insert(0, "0");
                    }
                    else
                    {
                        mm = m;
                    }
                    
                    var tint = Convert.ToInt32(hh + mm);
                    // Determine day or night time
                    if (tint > 429 && tint < 1930)
                    {
                        Main.dayTime = true;
                    }
                    else
                    {
                        Main.dayTime = false;
                    }

                    double time;

                    if (Main.dayTime)
                    {
                        time = TimeSpan.Parse("00:" + hh + ":" + mm).TotalSeconds - 270;
                    }
                    else
                    {
                        if (Convert.ToInt32(hh) > 12)
                        {
                            time = TimeSpan.Parse("00:" + (Convert.ToInt32(hh) - 12).ToString() + ":" + mm).TotalSeconds - 450;
                        }
                        else
                        {
                            time = TimeSpan.Parse("00:" + hh + ":" + mm).TotalSeconds + 270;
                        }
                    }
                    Main.time = (time) * 60;
                }
                else
                {
                    Main.NewText("Usage:");
                    Main.NewText("    Note: this command uses 24 hour time.");
                    Main.NewText("    /time <hour> <minute>");
                    Main.NewText("    /time help");
                }
            }
            else
            {
                Main.NewText("Usage:");
                Main.NewText("    Note: this command uses 24 hour time.");
                Main.NewText("    /time <hour> <minute>");
                Main.NewText("    /time help");
            }
        }

        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "time") return false;

            if (args.Length != 2 || args[0] == "help")
            {
                Main.NewText("Usage:");
                Main.NewText("    Note: this command uses 24 hour time.");
                Main.NewText("    /time <hour> <minute>");
                Main.NewText("    /time help");
                return true;
            }
            ChangeTime(args[0], args[1]);
            return true;
        }
    }
}
