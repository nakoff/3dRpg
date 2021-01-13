using System;
using System.Collections.Generic;


namespace Entities
{

    public class AnimationController
    {

        public System.Action changed = delegate{};
        public System.Action finished = delegate{};
        public string CurAnimationKey;

        private Dictionary<string,string> _animations = new Dictionary<string, string>();


        public AnimationController()
        {

        }

        public void AddAnimation(string key, string animation)
        {
            if (_animations.ContainsKey(key))
            {
                Logger.Error("Animation: "+key+" already is exists");
                return;
            }

            _animations.Add(key, animation);
        }

        public string GetAnimation(string key)
        {
            if (!_animations.ContainsKey(key))
            {
                Logger.Error("Animation: "+key+" is not exists");
                return null;
            }

            return _animations[key];
        }

        public void ChangeAnimation(string key)
        {
            if (CurAnimationKey == key)
                return;
            
            CurAnimationKey = key;
            var animation = GetAnimation(CurAnimationKey);
            if (animation != null)
                changed();
        }

        internal void OnAnimationFinished() => finished();
    }

}