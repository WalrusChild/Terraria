using System;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using PluginLoader;
using Terraria;
using Terraria.ID;

namespace WalrusPlugins
{
    public class Summon : MarshalByRefObject, IPluginChatCommand
    {
        private void Spawn(int id)
        {
            if (id < 1 || id > 579) return;
            Terraria.NPC.NewNPC(Convert.ToInt32(Main.mouseX + Main.screenPosition.X), Convert.ToInt32(Main.mouseY + Main.screenPosition.Y), id);
        }

        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "summon") return false;

            if (args.Length != 1 || args[0] == "help")
            {
                Main.NewText("Usage:");
                Main.NewText("    Note: A full list of NPC IDs can be found at https://terraria.gamepedia.com/NPC_IDs.");
                Main.NewText("    /boss <ID>");
                return true;
            }
            if (Regex.IsMatch(args[0], @"^\d+$"))
            {
                Spawn(Convert.ToInt32(args[0]));
                return true;
            }
            else
            {
                Main.NewText("Invalid ID.");
            }
            return true;
        }
    }
}