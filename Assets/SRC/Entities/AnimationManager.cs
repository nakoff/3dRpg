using System;
using System.Collections.Generic;


namespace Entities
{

    public class AnimationManager
    {

        public System.Action changed = delegate{};
        public System.Action finished = delegate{};
        public string CurAnimationKey;

        private Dictionary<string,string> _animations = new Dictionary<string, string>();


        public AnimationManager()
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

        public void ChangeAnimation(string newAnimKey)
        {
            if (CurAnimationKey == newAnimKey)
                return;
            
            CurAnimationKey = newAnimKey;
            var anim = GetAnimation(CurAnimationKey);
            if (anim != null)
                changed();
        }

        internal void OnAnimationFinished() => finished();
    }

}