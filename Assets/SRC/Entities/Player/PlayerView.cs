using Datas;
using UnityEngine;

namespace Entities
{
    public interface IPlayerView
    {
        CharacterModel.MOVING Moving { get; set; }
        Vector3 Position { get; set; }
        Vector2 Move { get; set; }
        Vector2 Rotation { get; set; }
    }


    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView: MonoBehaviour, IPlayerView
    {
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

            var p = new Player(new EntityPref("", Vector3.zero));
            p._view = this;
            
        }

        void Update()
        {

            var dt = Time.deltaTime;
            var moveX = new Vector2(Move.x * transform.right.x, Move.x * transform.right.z);
            var moveZ = new Vector2(Move.y * transform.forward.x, Move.y * transform.forward.z);
            var vel = (moveX + moveZ).normalized * Speed * dt;

            _rb.rotation *= Quaternion.Euler(0, Rotation.x * dt * 100, 0);
            _rb.velocity = new Vector3(vel.x, _rb.velocity.y, vel.y);
            
        }

    }

}