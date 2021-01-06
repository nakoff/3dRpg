using Datas;
using UnityEngine;

namespace Entities
{
    public interface IPlayerView
    {
        CharacterModel.MOVING Moving { get; set; }
        Vector3 Position { get; set; }
        void Movement(Vector2 move, Vector2 rot, float dt);
    }


    [RequireComponent(typeof(Rigidbody))]
    // [RequireComponent(typeof(Animator))]
    public class PlayerView: MonoBehaviour, IPlayerView
    {
        [SerializeField] private Animator _anim;
        public CharacterModel.MOVING Moving { get; set; }
        public Vector3 Position 
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }
        public Vector2 Rotation { get; set; } = Vector2.zero;

        public Vector2 Move { get; set; }
        public float Speed = 100;

        private Rigidbody _rb;

        void Start()
        {

            _rb = GetComponent<Rigidbody>();

            // var p = new Player(this);
            Cursor.lockState = CursorLockMode.Locked;
            
        }

        void Update()
        {
        }

        public void Movement(Vector2 move, Vector2 rot, float dt)
        {

            var v = _rb.velocity;

            var moveX = new Vector2(move.x * transform.right.x, move.x * transform.right.z);
            var moveZ = new Vector2(move.y * transform.forward.x, move.y * transform.forward.z);
            var vel = (moveX + moveZ).normalized * Speed * dt;

            _rb.rotation *= Quaternion.Euler(0, rot.x * dt * 100, 0);
            _rb.velocity = new Vector3(vel.x, _rb.velocity.y, vel.y);

            if (v != _rb.velocity)
            {
                // _anim.SetBool("IsMove", true);
            }
            

        }

    }

}