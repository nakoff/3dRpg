using System.Collections.Generic;
using UnityEngine;

namespace Datas
{
    public class CharacterModel:BaseModel<CharacterModel, CharacterObject>
    {
        public static OBJECT_TYPE Type = OBJECT_TYPE.CHARACTER;
        public static CharacterModel Create(Entities.ENTITY_TYPE parentType, uint parentId) => Create(Type, parentType, parentId); 
        public static CharacterModel Get(uint id) => Get(Type, id);
        public static List<CharacterModel> GetAll() => GetAll(Type);
        public static CharacterModel GetByParent(Entities.ENTITY_TYPE parentType, uint parentId) => GetByParent(Type, (int)parentType, parentId);


        public enum CHANGE { HEALTH, POSITION, ANIMATION, }
        public enum MOVING {STOP, LEFT, RIGHT}
        public uint Id => obj.Id;

        public CharacterModel(CharacterObject obj):base(obj){}

        public Vector3 Position
        {
            get => new Vector3(obj.posX, obj.posY, obj.posZ);
            set
            {
                if (Position == value)
                    return;
                obj.posX = value.x;
                obj.posY = value.y;
                obj.posZ = value.z;
                obj.EmitEvent((int)CHANGE.POSITION);
            }
        }

        public int Heatlh 
        {
            get => obj.health;
            set
            {
                if (obj.health == value)
                    return;
                obj.health = value;
                obj.EmitEvent((int)CHANGE.HEALTH);
            }
        }
        public int MoveSpeed 
        {
            get => obj.moveSpeed;
            set
            {
                if (obj.moveSpeed == value)
                    return;
                obj.moveSpeed = value;
                // obj.EmitEvent((int)CHANGE.HEALTH);
            }
        }

        public string CurAnimation
        {
            get => obj.curAnimation;
            set
            {
                if (obj.curAnimation == value)
                    return;
                obj.curAnimation = value;
                obj.EmitEvent((int)CHANGE.ANIMATION);
            }
        }

    }
}