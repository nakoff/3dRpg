using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public abstract class BaseEntity
    {
        public ENTITY_TYPE Type { get; private set; }
        public uint Id { get; private set; }
        public bool IsAlive = true;

        public BaseEntity(ENTITY_TYPE type)
        {
            Type = type;
            Id = EntityManager.IdIncrement;
            EntityManager.AddEntity(this);
        }


        public virtual void OnUpdate(float dt) {}

        public abstract void OnDelete();

    }

    public class EntityManager: MonoBehaviour
    {

        public static uint IdIncrement { get; private set; } = 1;

        private static EntityManager _instance;
        private Dictionary<ENTITY_TYPE,List<BaseEntity>> _entities = new Dictionary<ENTITY_TYPE, List<BaseEntity>>();


        private void Awake() {
            _instance = this;
        } 

        public void OnUpdate(float dt)
        {
            foreach (var list in _entities.Values)
            {
                foreach (var entity in list)
                {
                    entity.OnUpdate(dt);
                }
            }
        }


        public static void AddEntity(BaseEntity entity)
        {
            if (!_instance._entities.ContainsKey(entity.Type)) 
                _instance._entities.Add(entity.Type, new List<BaseEntity>());

            _instance._entities[entity.Type].Add(entity);
            IdIncrement++;
        }

        private (string,BaseEntity) GetEntity(ENTITY_TYPE type, uint id)
        {
            string err = null;
            BaseEntity entity = null;

            if (_entities.ContainsKey(type))
                err = "entity type: "+type.ToString()+" is not exists";
            
            foreach (var e in _entities[type])
            {
                if (e.Id == id) entity = e; break;
            }

            if (entity == null)
                err = "entity type: "+type+" id:"+id+" is not exists";
            
            return (err, entity);
        }

        public static T CreateView<T>(string path) where T: class
        {
            var go = Instantiate(Resources.Load(path)) as GameObject;
            return go.GetComponent<T>();
        }

    }
}