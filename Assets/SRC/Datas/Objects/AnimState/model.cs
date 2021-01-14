
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Datas
{
    public class AnimStateModel:BaseModel<AnimStateModel, AnimStateObject>
    {
        public static OBJECT_TYPE Type = OBJECT_TYPE.ANIM_STATE;
        public static AnimStateModel Create(Entities.ENTITY_TYPE parentType, uint parentId) => Create(Type, parentType, parentId); 
        public static AnimStateModel Get(uint id) => Get(Type, id);
        public static List<AnimStateModel> GetAll() => GetAll(Type);
        public static AnimStateModel GetByParent(Entities.ENTITY_TYPE parentType, uint parentId) => GetByParent(Type, (int)parentType, parentId);


        public enum CHANGE { CUR_ANIMATION, ACTION, IS_PLAYING, }
        public uint Id => obj.Id;

        public AnimStateModel(AnimStateObject obj):base(obj){}

        public int PrevAnimation => obj.curAnim;
        public int CurAnimation 
        {
            get => obj.curAnim;
            set
            {
                if (CurAnimation == value)
                    return;
                obj.prevAnim = CurAnimation;
                obj.curAnim = value;
                obj.EmitEvent((int)CHANGE.CUR_ANIMATION);
            }
        }

        public int Action
        {
            get => obj.action;
            set
            {
                obj.action = value;
                obj.EmitEvent((int)CHANGE.ACTION);
            }
        }

        public bool IsPlaying
        {
            get => obj.isPlaying;
            private set
            {
                if (IsPlaying == value)
                    return;
                obj.isPlaying = value;
                obj.EmitEvent((int)CHANGE.IS_PLAYING);
            }
        }

        public void _AnimationStart() => IsPlaying = true;
        public void _AnimationFinished() => IsPlaying = false;

    }
}