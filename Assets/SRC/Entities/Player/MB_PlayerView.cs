using Datas;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public interface IPlayerView
    {
        enum CHANGED { POSITION, };
        Vector3 Position { get; set; }
        void Subscribe(System.Action<CHANGED> listener);
        void Movement(Vector2 vector2, int moveSpeed, float dt);
        void Rotate(Vector2 rotation, float dt);
    }


    [RequireComponent(typeof(Rigidbody))]
    public class MB_PlayerView: MonoBehaviour, IPlayerView
    {
        [SerializeField] private MB_PlayerAnim _playerAnim;
        [SerializeField] private Transform _camPiv;

        public Vector3 Position 
        {
            get => gameObject.transform.position;
            set => gameObject.transform.position = value;
        }
        
        private System.Action<IPlayerView.CHANGED> change = delegate{};
        private Rigidbody _rb;
        private Player _presenter;


        void Start()
        {

            _rb = GetComponent<Rigidbody>();

            _presenter = new Player(this, _playerAnim);
            Cursor.lockState = CursorLockMode.Locked;
            
        }


        public void Subscribe(System.Action<IPlayerView.CHANGED> listener)
        {
            change = listener;
        }

        public void Movement(Vector2 dir, int speed, float dt)
        {
            var v = _rb.velocity;

            var moveX = new Vector2(dir.x * transform.right.x, dir.x * transform.right.z);
            var moveZ = new Vector2(dir.y * transform.forward.x, dir.y * transform.forward.z);
            var vel = (moveX + moveZ).normalized * dt * speed;
            
            _rb.velocity = new Vector3(vel.x, _rb.velocity.y, vel.y);

        }

        public void Rotate(Vector2 rot, float dt)
        {
            _camPiv.transform.localRotation = Quaternion.Euler(rot.x, 0, 0);
            transform.localRotation = Quaternion.Euler(0, rot.y, 0);
        }
    }

}