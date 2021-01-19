using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{

    public abstract class BaseModel <TModel,TObject> 
    where TModel: BaseModel<TModel,TObject>
    where TObject: IObject
    {

        public static TModel Create(OBJECT_TYPE type, Entities.ENTITY_TYPE parentType, uint parentId) 
        {
            var obj = (TObject)Activator.CreateInstance(typeof(TObject),(int)type, (int)parentType, parentId);
            ObjectManager.AddObject(obj);

            var model = (TModel)Activator.CreateInstance(typeof(TModel), (TObject)obj);            
            return model; 
        }

        public static TModel Get(OBJECT_TYPE type, uint id) 
        {
            TModel model = default(TModel);
            (var err,var obj) = ObjectManager.GetObject((int)type, id);
            if (obj == null) { Logger.Error(err); }
            else { model = (TModel)Activator.CreateInstance(typeof(TModel), obj); }

            return model;
        } 
        
        public static List<TModel> GetAll(OBJECT_TYPE type)
        {
            var list = new List<TModel>();
            foreach (var obj in ObjectManager.GetObjects((int)type))
            {
                var model = (TModel)Activator.CreateInstance(typeof(TModel), obj);
                list.Add(model);
            }

            return list;
        }

        public static TModel GetByParent(OBJECT_TYPE type, int parentType, uint parentId)
        {
            TModel model = null;
            var objects = ObjectManager.GetObjects((int)type);
            // objects =  (from obj in objects where obj.ParentType == parentType && obj.ParentId == parentId select obj).ToList();
            foreach (var obj in objects)
            {
                if (obj.ParentType == parentType && obj.ParentId == parentId)
                {
                    model = (TModel)Activator.CreateInstance(typeof(TModel), obj);
                    break;
                }
            }

            return model;
        }

        public virtual TObject obj {get;}

        private BaseModel(){}
        protected BaseModel(IObject obj)
        {
            this.obj = (TObject)obj;
        }

        protected System.Action<int> update = delegate {};
        public void Subscribe(System.Action<int> listener)
        {
            update = listener;
            obj.update += OnObjectChanged;
        }

        private void OnObjectChanged(int change)
        {
            // update( (TChange)Enum.ToObject(typeof(TChange), change) );
            update(change);
        }

        public void UnSubscribes()
        {
            obj.update -= OnObjectChanged;
        }
    }
}