using System;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace WalrusPlugins
{
    public class InstaKill : MarshalByRefObject, IPlugin
    {
        private Keys instaKey;

        public InstaKill()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("InstaKill", "Key", "X", writeIt: true), out instaKey))
            {
                instaKey = Keys.X;
            }
            Loader.RegisterHotkey(() => SummonProjectile(), instaKey, ignoreModifierKeys: true);
        }

        private void SummonProjectile()
        {
            Terraria.Projectile.NewProjectile(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y, 0f, 0f, 255, 100000, 0f, 0);
        }
    }
}