using System;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace WalrusPlugins
{
    public class Heal : MarshalByRefObject, IPluginChatCommand
    {
        private Keys healKey;

        public Heal()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("Heal", "healKey", "H", writeIt: true), out healKey))
            {
                healKey = Keys.H;
            }

            Loader.RegisterHotkey(() => HealPlayer(), healKey, ignoreModifierKeys: true);
        }

        private void HealPlayer()
        {
            var player = Main.player[Main.myPlayer];
            int amt = player.statLifeMax2 - player.statLife;
            player.statLife += amt;
            player.HealEffect(amt, true);
            Main.NewText("Healed " + amt.ToString() + ".");
        }

        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "heal") 
            {
                return false;
            }
            HealPlayer();
            return true;
        }
    }
}