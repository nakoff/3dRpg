using UnityEngine;

namespace Entities
{
    public enum ANIMATION_EVENT { TEST }

    [RequireComponent(typeof(Animator))]
    public class MB_AnimationView:MonoBehaviour
    {
        public Animator animator => GetComponent<Animator>();

        private AnimController _animController;

        public void Init(AnimController animController)
        {
            _animController = animController;
        }


        public void OnAnimationAction(ANIMATION_EVENT evnt) => _animController.Action((int)evnt);
    }
}