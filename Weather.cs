using System;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace WalrusPlugins
{
    public class Weather : MarshalByRefObject, IPluginChatCommand
    {
        private Keys clearKey, rainKey, slimeKey;

        public Weather()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("Weather", "Clear", "NumPad1", writeIt: true), out clearKey))
                clearKey = Keys.NumPad1;
            if (!Keys.TryParse(IniAPI.ReadIni("Weather", "Rain", "NumPad2", writeIt: true), out rainKey))
                rainKey = Keys.NumPad2;
            if (!Keys.TryParse(IniAPI.ReadIni("Weather", "Slime", "NumPad3", writeIt: true), out slimeKey))
                slimeKey = Keys.NumPad3;

            Loader.RegisterHotkey(() => ChangeWeather("clear"), clearKey, ignoreModifierKeys: true);
            Loader.RegisterHotkey(() => ChangeWeather("rain"), rainKey, ignoreModifierKeys: true);
            Loader.RegisterHotkey(() => ChangeWeather("slime"), slimeKey, ignoreModifierKeys: true);
        }

        private void ChangeWeather(string weather)
        {
            switch (weather)
            {
                case "clear":
                    Main.StopRain();
                    Main.StopSlimeRain();
                    Main.NewText("Rained cleared.");
                    break;

                case "rain":
                    Main.StartRain();
                    Main.NewText("Rain started.");
                    break;

                case "slime":
                    Main.StartSlimeRain();
                    Main.NewText("Slime rain started.");
                    break;

                default:
                    Main.NewText("Usage:");
                    Main.NewText("    /weather clear");
                    Main.NewText("    /weather rain");
                    Main.NewText("    /weather slime");
                    Main.NewText("    /weather help");
                    break;
            }
        }

        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "weather") return false;

            if (args.Length < 1 || args.Length > 1 || args[0] == "help")
            {
                Main.NewText("Usage:");
                Main.NewText("    /weather clear");
                Main.NewText("    /weather rain");
                Main.NewText("    /weather slime");
                Main.NewText("    /weather help");
                return true;
            }
            return true;
        }
    }
}
