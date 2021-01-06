using Datas;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public interface IPlayerView
    {
        enum CHANGED { POSITION, };
        Vector3 Position { get; set; }
        void Movement(Vector2 move, Vector2 rot, float dt);
        void Subscribe(System.Action<CHANGED> listener);
    }


    [RequireComponent(typeof(Rigidbody))]
    // [RequireComponent(typeof(Animator))]
    public class PlayerView: MonoBehaviour, IPlayerView
    {
        [SerializeField] private Animator _animator;
        public Vector3 Position 
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }
        public float Speed = 100;

        
        private System.Action<IPlayerView.CHANGED> change = delegate{};
        private Rigidbody _rb;
        private Player _presenter;

        private enum ANIMATION { RUN_FORWARD, IDLE, }
        private ANIMATION _curAnim;
        private Dictionary<ANIMATION,string> _animations = new Dictionary<ANIMATION, string>
        {
            [ANIMATION.IDLE] = "Standing Idle",
            [ANIMATION.RUN_FORWARD] = "Standing Run Forward",
        };

        void Start()
        {

            _rb = GetComponent<Rigidbody>();

            _presenter = new Player(this);
            Cursor.lockState = CursorLockMode.Locked;
            
        }


        public void Subscribe(System.Action<IPlayerView.CHANGED> listener)
        {
            change = listener;
        }

        public void Movement(Vector2 move, Vector2 rot, float dt)
        {

            var v = _rb.velocity;

            var moveX = new Vector2(move.x * transform.right.x, move.x * transform.right.z);
            var moveZ = new Vector2(move.y * transform.forward.x, move.y * transform.forward.z);
            var vel = (moveX + moveZ).normalized * Speed * dt;

            _rb.rotation *= Quaternion.Euler(0, rot.x * dt * 100, 0);
            _rb.velocity = new Vector3(vel.x, _rb.velocity.y, vel.y);

            if (Vector3.Dot(vel, _rb.velocity) > 0)
            {
                change(IPlayerView.CHANGED.POSITION);
                ChangeAnimation(ANIMATION.RUN_FORWARD);
            }
            else
            {
                ChangeAnimation(ANIMATION.IDLE);
            }
            

        }

        private void ChangeAnimation(ANIMATION anim)
        {
            if (_curAnim == anim) 
                return;
            
            _animator.Play(_animations[anim]);
            _curAnim = anim;
        }

    }

}