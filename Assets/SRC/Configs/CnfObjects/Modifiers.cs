using System.Collections.Generic;
using SimpleJSON;

namespace Configs
{

    public class Modifiers
    {

        public Modifiers(JSONArray configData)
        {
            foreach (JSONObject item in configData)
            {
                var id = item["id"].Value;
                if (string.IsNullOrEmpty(id))
                    break;
                
                var obj = new ModifierObject(id);
                var err = ConfigManager.Add(obj);
                if (err != null)
                    Logger.Error(err);
            }
        }

    }


    class ModifierObject:ICnfObject
    {

        public CNF_OBJECT_TYPE Type { get; } = CNF_OBJECT_TYPE.MODIFIER;
        public string Id { get; }

        public ModifierObject(string id) 
        {
            Id = id;
        }
    }
}