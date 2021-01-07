using System;
using System.Collections.Generic;

namespace Datas
{

    public abstract class BaseModel <TModel,TObject> 
    where TModel: BaseModel<TModel,TObject>
    where TObject: IObject
    {

        public static TModel Create(OBJECT_TYPE type, Entities.ENTITY_TYPE parentType, uint parentId) 
        {
            var obj = (TObject)Activator.CreateInstance(typeof(TObject),(int)type, (int)parentType, parentId);
            DataManager.AddObject(obj);

            var model = (TModel)Activator.CreateInstance(typeof(TModel), (TObject)obj);            
            return model; 
        }

        public static TModel Get(OBJECT_TYPE type, uint id) 
        {
            TModel model = default(TModel);
            (var err,var obj) = DataManager.GetObject((int)type, id);
            if (obj == null) { Logger.Error(err); }
            else { model = (TModel)Activator.CreateInstance(typeof(TModel), obj); }

            return model;
        } 
        
        public static List<TModel> GetAll(OBJECT_TYPE type)
        {
            var list = new List<TModel>();
            foreach (var obj in DataManager.GetObjects((int)type))
            {
                var model = (TModel)Activator.CreateInstance(typeof(TModel), obj);
                list.Add(model);
            }

            return list;
        }

        protected virtual TObject obj {get;}

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