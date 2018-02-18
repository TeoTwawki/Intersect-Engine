﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Migration.UpgradeInstructions.Upgrade_5.Intersect_Convert_Lib.GameObjects.
    Switches_and_Variables
{
    public class ServerSwitchBase : DatabaseObject
    {
        //Core info
        public new const string DATABASE_TABLE = "server_switches";

        public new const GameObject OBJECT_TYPE = GameObject.ServerSwitch;
        protected static Dictionary<int, DatabaseObject> sObjects = new Dictionary<int, DatabaseObject>();

        public string Name = "New Global Switch";
        public bool Value;

        public ServerSwitchBase(int id) : base(id)
        {
        }

        public override void Load(byte[] packet)
        {
            var myBuffer = new Upgrade_10.Intersect_Convert_Lib.ByteBuffer();
            myBuffer.WriteBytes(packet);
            Name = myBuffer.ReadString();
            Value = Convert.ToBoolean(myBuffer.ReadInteger());
            myBuffer.Dispose();
        }

        public byte[] Data()
        {
            var myBuffer = new Upgrade_10.Intersect_Convert_Lib.ByteBuffer();
            myBuffer.WriteString(Name);
            myBuffer.WriteInteger(Convert.ToInt32(Value));
            return myBuffer.ToArray();
        }

        public static ServerSwitchBase GetSwitch(int index)
        {
            if (sObjects.ContainsKey(index))
            {
                return (ServerSwitchBase) sObjects[index];
            }
            return null;
        }

        public static string GetName(int index)
        {
            if (sObjects.ContainsKey(index))
            {
                return ((ServerSwitchBase) sObjects[index]).Name;
            }
            return "Deleted";
        }

        public override byte[] GetData()
        {
            return Data();
        }

        public override string GetTable()
        {
            return DATABASE_TABLE;
        }

        public override GameObject GetGameObjectType()
        {
            return OBJECT_TYPE;
        }

        public static DatabaseObject Get(int index)
        {
            if (sObjects.ContainsKey(index))
            {
                return sObjects[index];
            }
            return null;
        }

        public override void Delete()
        {
            sObjects.Remove(GetId());
        }

        public static void ClearObjects()
        {
            sObjects.Clear();
        }

        public static void AddObject(int index, DatabaseObject obj)
        {
            sObjects.Remove(index);
            sObjects.Add(index, obj);
        }

        public static int ObjectCount()
        {
            return sObjects.Count;
        }

        public static Dictionary<int, ServerSwitchBase> GetObjects()
        {
            Dictionary<int, ServerSwitchBase> objects =
                sObjects.ToDictionary(k => k.Key, v => (ServerSwitchBase) v.Value);
            return objects;
        }
    }
}