using Models;
using UnityEngine;
using System.Collections.Generic;

namespace Entities
{

    public class AnimController
    {

        private Animator _animator;
        private AnimStateModel _animState;
        private bool _isAnimationFinished;
        private Dictionary<int,string> _animations = new Dictionary<int, string>();

        public AnimController(Animator animator, AnimStateObject obj)
        {
            _animator = animator;
            _animState = new AnimStateModel(obj);
            _animState.Subscribe(change =>
            {
                switch ((AnimStateModel.CHANGE)change)
                {
                    case AnimStateModel.CHANGE.CUR_ANIMATION:
                        ChangeAnimation(_animState.CurAnimation);
                        break;

                    case AnimStateModel.CHANGE.ACTION:
                        Logger.Print("ANIM_ACTION"+_animState.Action.ToString());
                        break;
                }
            });

        }

        public bool AddAnimation(int key, string animation)
        {
            if (_animations.ContainsKey(key))
                return false;
            
            _animations.Add(key, animation);
            return true;
        }

        public string GetAnimationOrNull(int key)
        {
            if (!_animations.ContainsKey(key))
                return null;
            
            return _animations[key];
        }

        private bool ChangeAnimation(int key)
        {
            var anim = GetAnimationOrNull(key);
            if (anim == null)
                return false;

            _animator.Play(anim, 0);
            _isAnimationFinished = false;
            _animState._AnimationStart();
            return true;
        }        

        public void Update(float dt)
        {
            var _animatorState = _animator.GetCurrentAnimatorStateInfo(0);

            if (!_animatorState.loop && !_isAnimationFinished){
                if (_animatorState.normalizedTime >= 1f)
                {
                    _isAnimationFinished = true;
                    _animState._AnimationFinished();
                }
            } 
        }

        public void Action(int action) 
        {
            _animState._AnimationAction(action);
        }
    }
}