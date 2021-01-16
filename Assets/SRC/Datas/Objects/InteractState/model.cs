
using System.Collections.Generic;
using Entities;

namespace Datas
{
    public class InteractStateModel:BaseModel<InteractStateModel, InteractStateObject>
    {
        public static OBJECT_TYPE Type = OBJECT_TYPE.INTERACT_STATE;
        public static InteractStateModel Create(ENTITY_TYPE parentType, uint parentId) => Create(Type, parentType, parentId); 
        public static InteractStateModel Get(uint id) => Get(Type, id);
        public static List<InteractStateModel> GetAll() => GetAll(Type);
        public static InteractStateModel GetByParent(ENTITY_TYPE parentType, uint parentId) => GetByParent(Type, (int)parentType, parentId);

        public class TargetEntity
        {
            public ENTITY_TYPE Type;
            public uint Id;

            public TargetEntity(ENTITY_TYPE type, uint id)
            {
                this.Type = type;
                this.Id = id;
            }
        }

        public enum CHANGE { COLLIDE, }
        public uint Id => obj.Id;

        public InteractStateModel(InteractStateObject obj):base(obj){}


        public TargetEntity Target
        {
            get => new TargetEntity((ENTITY_TYPE)obj.targetType, obj.targetId);
            private set
            {
                obj.targetType = (int)value.Type;
                obj.targetId = value.Id;
                obj.EmitEvent((int)CHANGE.COLLIDE);
            }
        }


        public void _InteractedWith(TargetEntity withEntity) => Target = withEntity;

    }
}