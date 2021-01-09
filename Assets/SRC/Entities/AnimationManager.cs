using System.Collections.Generic;


namespace Entities
{

    public class AnimationManager
    {

        private System.Action<string> animationChanged = delegate{};
        private string _curAnim;
        private Dictionary<string,string> _animations = new Dictionary<string, string>();

        public AnimationManager(System.Action<string> listener)
        {
            animationChanged = listener;
        }

        public void AddAnimation(string name, string animation)
        {
            if (_animations.ContainsKey(name))
            {
                Logger.Error("Animation: "+name+" already is exists");
                return;
            }

            _animations.Add(name, animation);
        }

        public string GetAnimation(string name)
        {
            if (!_animations.ContainsKey(name))
            {
                Logger.Error("Animation: "+name+" is not exists");
                return null;
            }

            return _animations[name];
        }

        public void ChangeAnimation(string newAnim)
        {
            if (_curAnim == newAnim)
                return;
            
            _curAnim = newAnim;
            var anim = GetAnimation(_curAnim);
            if (anim != null)
                animationChanged(anim);
        }

    }

}