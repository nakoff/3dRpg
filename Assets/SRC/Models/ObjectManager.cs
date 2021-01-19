using System.Collections.Generic;

namespace Models
{
    public interface IObject
    {
        event System.Action<int> update;
        void EmitEvent(int change);
        int Type { get; }
        uint Id { get; }
        int ParentType { get; }
        uint ParentId { get; }
    }

    public static class ObjectManager
    {
        private static Dictionary<int, Dictionary<uint,IObject>> _db = new Dictionary<int, Dictionary<uint, IObject>>();
        public static uint IdIncrement { get; private set; } = 1;
        public static System.Action<IObject> ObjectAdded = delegate {};

        //return err or null
        public static string AddObject(IObject obj)
        {
            string err = null;
            var type = (int)obj.Type;
            var id = obj.Id;

            if (!_db.ContainsKey(type)) { _db.Add(type, new Dictionary<uint, IObject>()); }

            if (!_db[type].ContainsKey(id)) 
            { 
                _db[type].Add(id, obj); 
                IdIncrement++;
            }
            else
            {
                err = "obj Type:"+type.ToString()+" Id:"+id.ToString()+" already is exist";
            }

            ObjectAdded(obj);
            return err;
        }

        // return (err, IObject)
        public static (string, IObject) GetObject(int type, uint id)
        {
            string err = null;
            IObject obj = null;

            if (!_db.ContainsKey(type)) { err = "Obj Type:"+type+" is not exists"; }
            else if (!_db[type].ContainsKey(id)) { err = "Obj Type:"+type+" Id:"+id+" is not exists"; }
            else { obj = _db[type][id]; }

            return (err, obj);
        }

        public static List<IObject> GetObjects(int type)
        {
            var list = new List<IObject>();
            if (_db.ContainsKey(type))
            {
                foreach (var obj in _db[type].Values) { list.Add(obj); }
            }

            return list;
        }
    }
}
