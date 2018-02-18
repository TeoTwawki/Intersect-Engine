﻿using System.Collections.Generic;

namespace Intersect.Migration.UpgradeInstructions.Upgrade_6.Intersect_Convert_Lib
{
    public static class Options
    {
        //Game Settings
        public static string GameName = "Intersect";

        public static string Motd = "Welcome to Intersect!";
        public static int ServerPort = 4500;

        //Maxes
        public static int MaxNpcDrops = 10;

        //Player Maxes
        public static int MaxStatValue = 200;

        public static int MaxStats = 5;
        public static int MaxLevel = 100;
        public static int MaxHotbar = 10;
        public static int MaxInvItems = 35;
        public static int MaxPlayerSkills = 35;
        public static int MaxBankSlots = 100;

        //Equipment
        public static int WeaponIndex = -1;

        public static int ShieldIndex = -1;
        public static List<string> EquipmentSlots = new List<string>();
        public static List<string> PaperdollOrder = new List<string>();
        public static List<string> ToolTypes = new List<string>();

        public static List<string> StatusActionMsgs = new List<string>
        {
            "NONE!",
            "SILENCED!",
            "STUNNED!",
            "SNARED!",
            "BLINDED!",
            "STEALTH!",
            "TRANSFORMED!"
        };

        //Combat
        public static int MinAttackRate = 500; //2 attacks per second

        public static int MaxAttackRate = 200; //5 attacks per second
        public static double BlockingSlow = 0.3; //Slow when moving with a shield. Default 30%
        public static int CritChance = 20; //1 in 20 chance to critically strike.
        public static double CritMultiplier = 1.5; //Critical strikes deal 1.5x damage.
        public static int MaxDashSpeed = 200;

        //Maps
        public static int GameBorderStyle; //0 For Smart Borders, 1 for Non-Seamless, 2 for black borders

        public static int MapWidth = 32;
        public static int MapHeight = 26;
        public static int TileWidth = 32;
        public static int TileHeight = 32;
        public static int LayerCount = 5;

        public static void LoadFromServer(Upgrade_10.Intersect_Convert_Lib.ByteBuffer bf)
        {
            //General
            GameName = bf.ReadString();

            //Game Objects
            MaxNpcDrops = bf.ReadInteger();

            //Player Objects
            MaxStatValue = bf.ReadInteger();
            MaxLevel = bf.ReadInteger();
            MaxHotbar = bf.ReadInteger();
            MaxInvItems = bf.ReadInteger();
            MaxPlayerSkills = bf.ReadInteger();
            MaxBankSlots = bf.ReadInteger();

            //Equipment
            int count = bf.ReadInteger();
            for (int i = 0; i < count; i++)
            {
                EquipmentSlots.Add(bf.ReadString());
            }
            WeaponIndex = bf.ReadInteger();
            ShieldIndex = bf.ReadInteger();

            //Paperdoll
            count = bf.ReadInteger();
            for (int i = 0; i < count; i++)
            {
                PaperdollOrder.Add(bf.ReadString());
            }

            //Tool Types
            count = bf.ReadInteger();
            for (int i = 0; i < count; i++)
            {
                ToolTypes.Add(bf.ReadString());
            }

            //Combat
            MinAttackRate = bf.ReadInteger();
            MaxAttackRate = bf.ReadInteger();
            BlockingSlow = bf.ReadDouble();
            CritChance = bf.ReadInteger();
            CritMultiplier = bf.ReadDouble();
            MaxDashSpeed = bf.ReadInteger();

            //Map
            GameBorderStyle = bf.ReadInteger();
            MapWidth = bf.ReadInteger();
            MapHeight = bf.ReadInteger();
            TileWidth = bf.ReadInteger();
            TileHeight = bf.ReadInteger();
        }
    }
}