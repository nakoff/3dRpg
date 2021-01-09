using UnityEngine;

namespace Entities
{

    public class MB_PlayerAnim : MonoBehaviour, IAnimatedView
    {
        [SerializeField] private Animator _animator;

        public void ChangeAnimation(string animName)
        {
            _animator.Play(animName);
        }
    }
}