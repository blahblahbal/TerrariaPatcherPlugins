using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace BlahPlugins
{
	public class DropRates : MarshalByRefObject, IPluginNPCLoot
	{
		private byte factor = 1;
        private bool rare = false;
		private Keys inc, dec, toggle;
        private bool recursionFlag = false;

		public DropRates()
		{
			if (!Keys.TryParse(IniAPI.ReadIni("DropRates", "IncKey", "P", writeIt: true), out inc)) inc = Keys.P;
            if (!Keys.TryParse(IniAPI.ReadIni("DropRates", "DecKey", "O", writeIt: true), out dec)) dec = Keys.O;
            if (!Keys.TryParse(IniAPI.ReadIni("DropRates", "Rare", "RightControl", writeIt: true), out toggle)) toggle = Keys.RightControl;
            factor = byte.Parse(IniAPI.ReadIni("DropRates", "Factor", "1", writeIt: true));

            Color yellow = new Color(250, 250, 50);
			Loader.RegisterHotkey(() =>
			{
				if (factor < 20) factor++;
                IniAPI.WriteIni("DropRates", "Factor", factor.ToString());
                Main.NewText("Drop Rates multiplied by " + factor, yellow.R, yellow.G, yellow.B, false);
			}, inc);

			Loader.RegisterHotkey(() =>
			{
                if (factor > 1) factor--;
                IniAPI.WriteIni("DropRates", "Factor", factor.ToString());
                Main.NewText("Drop Rates multiplied by " + factor, yellow.R, yellow.G, yellow.B, false);
			}, dec);

            Loader.RegisterHotkey(() =>
            {
                rare = !rare;
                IniAPI.WriteIni("DropRates", "Rare", rare.ToString());
                Main.NewText("Rare Drops Only " + rare ? "On" : "Off", yellow.R, yellow.G, yellow.B, false);
            }, toggle);
        }
        public bool OnNPCLoot(NPC npc)
        {
            if (!rare)
            {
                if (recursionFlag) return false; // flag is set, avoid recursion

                recursionFlag = true;
                for (int i = 0; i < factor; i++)
                    npc.NPCLoot();
                recursionFlag = false;

                return true;
            }
            else
            {
                if (factor > 1)
                {
                    int hundred = 100 / factor;
                    int oneFifty = 150 / factor;
                    int oneSeventyFive = 175 / factor;
                    int twoHundred = 200 / factor;
                    int twoFifty = 250 / factor;
                    int threeHundred = 300 / factor;
                    int fourHundred = 400 / factor;
                    int fiveHundred = 500 / factor;
                    int thousand = 1000 / factor;
                    int twoThousand = 2000 / factor;
                    int twentyFiveHundred = 2500 / factor;
                    int fourThousand = 4000 / factor;
                    int eightThousand = 8000 / factor;
                    if (Main.hardMode && npc.value > 0)
                    {
                        if (Main.rand.Next(twentyFiveHundred) == 0 && Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1533, 1, false, 0, false);
                        }
                        if (Main.rand.Next(twentyFiveHundred) == 0 && Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneCorrupt)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1534, 1, false, 0, false);
                        }
                        if (Main.rand.Next(twentyFiveHundred) == 0 && Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneCrimson)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1535, 1, false, 0, false);
                        }
                        if (Main.rand.Next(twentyFiveHundred) == 0 && Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneHoly)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1536, 1, false, 0, false);
                        }
                        if (Main.rand.Next(twentyFiveHundred) == 0 && Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1537, 1, false, 0, false);
                        }
                    }
                    if (npc.type >= 212 && npc.type <= 215)
                    {
                        if (Main.rand.Next(eightThousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 905, 1, false, -1, false);
                        }
                        if (Main.rand.Next(fourThousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 855, 1, false, -1, false);
                        }
                        if (Main.rand.Next(twoThousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 854, 1, false, -1, false);
                        }
                        if (Main.rand.Next(twoThousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2584, 1, false, -1, false);
                        }
                        if (Main.rand.Next(thousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3033, 1, false, -1, false);
                        }
                    }
                    if (npc.type == 216)
                    {
                        if (Main.rand.Next(twoThousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 905, 1, false, -1, false);
                        }
                        if (Main.rand.Next(thousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 855, 1, false, -1, false);
                        }
                        if (Main.rand.Next(fiveHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 854, 1, false, -1, false);
                        }
                        if (Main.rand.Next(fiveHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2584, 1, false, -1, false);
                        }
                        if (Main.rand.Next(twoFifty) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3033, 1, false, -1, false);
                        }
                    }
                    if (npc.type == 110 && Main.rand.Next(twoHundred) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 682, 1, false, -1, false);
                    }
                    if (npc.type == 154 && Main.rand.Next(hundred) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1253, 1, false, -1, false);
                    }
                    if (npc.type == 198 || npc.type == 199 || npc.type == 226)
                    {
                        if (Main.rand.Next(thousand) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1172, 1, false, -1, false);
                        }
                    }
                    if (npc.type == 120 && Main.rand.Next(fiveHundred) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1326, 1, false, -1, false);
                    }
                    if (npc.type == 49 && Main.rand.Next(twoFifty) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1325, 1, false, -1, false);
                    }
                    if (npc.type == 185 && Main.rand.Next(oneFifty) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 951, 1, false, -1, false);
                    }
                    if (npc.type >= 269 && npc.type <= 280)
                    {
                        if (Main.rand.Next(fourHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1183, 1, false, -1, false);
                        }
                        else if (Main.rand.Next(threeHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1266, 1, false, -1, false);
                        }
                        else if (Main.rand.Next(twoHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 671, 1, false, -1, false);
                        }
                    }
                    if (Main.bloodMoon && Main.hardMode && Main.rand.Next(thousand) == 0 && npc.value > 0f)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1314, 1, false, -1, false);
                    }
                    if (npc.type == 21 || npc.type == 201 || npc.type == 202 || npc.type == 203 || npc.type == 322 || npc.type == 323 || npc.type == 324 || (npc.type >= 449 && npc.type <= 452))
                    {
                        if (Main.rand.Next(fiveHundred) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1274, 1, false, -1, false);
                        }
                    }
                    else if (npc.type == 6)
                    {
                        if (Main.rand.Next(oneSeventyFive) == 0)
                        {
                            int num34 = Main.rand.Next(3);
                            if (num34 == 0)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 956, 1, false, -1, false);
                            }
                            else if (num34 == 1)
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 957, 1, false, -1, false);
                            }
                            else
                            {
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 958, 1, false, -1, false);
                            }
                        }
                    }
                }
                return false;
            }
        }
    }
}