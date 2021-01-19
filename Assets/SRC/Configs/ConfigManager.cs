using SimpleJSON;
using System.Collections.Generic;

namespace Configs
{
    public enum CNF_OBJECT_TYPE { MODIFIER, }

    public interface ICnfObject
    {
        CNF_OBJECT_TYPE Type { get; }
        string Id { get; }
    }

    public static class ConfigManager
    {

        private static Dictionary<CNF_OBJECT_TYPE,Dictionary<string,ICnfObject>> _cnfDB = new Dictionary<CNF_OBJECT_TYPE, Dictionary<string, ICnfObject>>();

        public static string Add(ICnfObject obj) 
        {
            if (!_cnfDB.ContainsKey(obj.Type))
                _cnfDB.Add(obj.Type, new Dictionary<string, ICnfObject>());
            
            if (_cnfDB[obj.Type].ContainsKey(obj.Id))
                return "object, type: "+obj.Type.ToString()+" id: "+obj.Id+" already is exists";
            
            _cnfDB[obj.Type].Add(obj.Id, obj);

            return null;
        }
    }
}