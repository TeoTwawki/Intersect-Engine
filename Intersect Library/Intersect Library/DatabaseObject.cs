using System;
using System.Linq;
using Intersect.Collections;
using Intersect.Enums;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Intersect.Models
{
    public abstract class DatabaseObject<TObject> : IDatabaseObject where TObject : DatabaseObject<TObject>
    {
        private string mBackup;

        protected DatabaseObject(int index) : this(Guid.NewGuid(), index)
        {
        }

        protected DatabaseObject(Guid guid) : this(guid, Lookup.NextIndex)
        {
        }

        [JsonConstructor]
        protected DatabaseObject(Guid guid, int index)
        {
            Guid = guid;
            Index = index;
        }

        [NotNull] public static DatabaseObjectLookup Lookup => LookupUtils.GetLookup(typeof(TObject));

        [JsonIgnore]
        public GameObjectType Type => LookupUtils.GetGameObjectType(typeof(TObject));

        [JsonIgnore]
        public string DatabaseTable => Type.GetTable();

        [JsonIgnore]
        public Guid Guid { get; }
        [JsonIgnore]
        public int Index { get; }
        [JsonProperty(Order = -4)]
        public string Name { get; set; }

        public virtual void Load(string json) => JsonConvert.PopulateObject(json, this);
       // public virtual void Load(string json);


        public void MakeBackup() => mBackup = JsonData;
        public void DeleteBackup() => mBackup = null;

        public void RestoreBackup()
        {
            if (mBackup != null)
            {
                Load(mBackup);
            }
        }

        [JsonIgnore]
        public virtual string JsonData => JsonConvert.SerializeObject(this,Formatting.Indented);

        public virtual void Delete() => Lookup.Delete((TObject) this);

        public static string GetName(int index) => Lookup.Get(index)?.Name ?? "ERR_DELETED";

        public static string[] GetNameList() =>
            Lookup.Select(pair => pair.Value?.Name ?? "ERR_DELETED").ToArray();
    }
}