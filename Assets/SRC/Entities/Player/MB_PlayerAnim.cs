using UnityEngine;

namespace Entities
{

    public class MB_PlayerAnim : MonoBehaviour, IAnimatedView
    {
        [SerializeField] private Animator _animator;

        private int _curAnim;
        private System.Action<IAnimatedView.CHANGED> update = delegate{};
        private AnimatorStateInfo _animatorState;
        private bool _isAnimationFinished;

        private void Start()
        {

            _curAnim = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        }

        private void Update()
        {
            // if (_curAnim != _animatorState.fullPathHash) 
            // {
            //     _curAnim = _animatorState.fullPathHash;
            //     update(IAnimatedView.CHANGED.ANIMATION);
            // }
            // Debug.Log(_animatorState.normalizedTime);

            _animatorState = _animator.GetCurrentAnimatorStateInfo(0);

            if (!_animatorState.loop && !_isAnimationFinished){
                if (_animatorState.normalizedTime >= 1f)
                {
                    update(IAnimatedView.CHANGED.ANIMATION_FINISHED);
                    _isAnimationFinished = true;
                }
            }
        }

        public void Subscribe(System.Action<IAnimatedView.CHANGED> listener)
        {
            update = listener;
        }

        public void ChangeAnimation(string animName)
        {
            _animator.Play(animName, 0);
            _isAnimationFinished = false;
        }
    }
}