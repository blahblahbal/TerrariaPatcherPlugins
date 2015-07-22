using System;
using PluginLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;

namespace BlahPlugins
{
    public class WarpPoints : MarshalByRefObject, IPluginPlayerUpdate
    {
        private Keys hotkey;
        private byte m = 1;
        private Mode mode = Mode.LeftOcean;
        enum Mode
        {
            LeftOcean = 1,
            RightOcean = 2,
            Hell = 3,
            Random = 4
        }

        public WarpPoints()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("WarpPoints", "Hotkey", "K", writeIt: true), out hotkey)) hotkey = Keys.K;
            mode = (Mode)int.Parse(IniAPI.ReadIni("WarpPoints", "Mode", "1", writeIt: true));

            Loader.RegisterHotkey(() =>
            {
                mode++;
                if (mode > Mode.Random) mode = Mode.LeftOcean;
                if (mode == Mode.LeftOcean) m = 1;
                if (mode == Mode.RightOcean) m = 2;
                if (mode == Mode.Hell) m = 3;
                if (mode == Mode.Random) m = 4;
                IniAPI.WriteIni("WarpPoints", "Mode", m.ToString());
                Main.NewText("Warp Point: " + mode.ToString(), 255, 235, 150, false);
            }, hotkey);
        }

        public void OnPlayerUpdate(Player player)
        {
            if (player.inventory[player.selectedItem].type == ItemID.CellPhone && Main.mouseRight && Main.mouseRightRelease)
            {
                if (Main.mouseItem.type == ItemID.CellPhone) return;
                if (mode == Mode.LeftOcean)
                {
                    // left ocean
                    player.noFallDmg = true;
                    player.Teleport(new Vector2(200 * 16, (float)(Main.worldSurface / 2f) * 16f), 2);
                    player.fallStart = (int)(player.position.Y / 16f);
                    player.immuneTime = 100;
                    if (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].active())
                    {
                        while (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y += 16f;
                        }
                    }
                    else
                    {
                        while (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y -= 16f;
                        }
                    }
                    player.noFallDmg = false;
                    if (Main.netMode == 1) NetMessage.SendTileSquare(player.whoAmI, 200, (int)Main.worldSurface / 2, 10);
                }
                else if (mode == Mode.RightOcean)
                {
                    // right ocean
                    player.noFallDmg = true;
                    player.Teleport(new Vector2((Main.maxTilesX - 200) * 16, (float)(Main.worldSurface / 2f) * 16f), 2);
                    player.fallStart = (int)(player.position.Y / 16f);
                    player.immuneTime = 100;
                    if (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].active())
                    {
                        while (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y += 16f;
                        }
                    }
                    else
                    {
                        while (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y -= 16f;
                        }
                    }
                    player.noFallDmg = false;
                    if (Main.netMode == 1) NetMessage.SendTileSquare(player.whoAmI, Main.maxTilesX - 200, (int)Main.worldSurface / 2, 10);
                }
                else if (mode == Mode.Hell)
                {
                    // hell
                    player.noFallDmg = true;
                    player.Teleport(new Vector2((Main.maxTilesX / 2) * 16, (float)(Main.maxTilesY - 180) * 16f), 2);
                    player.fallStart = (int)(player.position.Y / 16f);
                    player.immuneTime = 100;
                    if (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].active())
                    {
                        while (!Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y += 16f;
                            if ((int)(player.position.Y / 16f) > Main.maxTilesY)
                            {
                                player.position.Y = (float)(Main.maxTilesY * 16) - 130f;
                                break;
                            }
                        }
                    }
                    else
                    {
                        while (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 4].active())
                        {
                            player.position.Y -= 16f;
                        }
                    }
                    player.noFallDmg = false;
                    if (Main.netMode == 1) NetMessage.SendTileSquare(player.whoAmI, Main.maxTilesX / 2, (int)Main.maxTilesY - 180, 10);
                }
                else if (mode == Mode.Random)
                {
                    if (Main.netMode == 0)
                    {
                        player.TeleportationPotion();
                    }
                    else if (Main.netMode == 1 && player.whoAmI == Main.myPlayer)
                    {
                        NetMessage.SendData(73, -1, -1, "", 0, 0f, 0f, 0f, 0);
                    }
                }
                for (int num91 = 0; num91 < 70; num91++)
                {
                    Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 150, default(Color), 1.5f);
                }
            }
        }
    }
}