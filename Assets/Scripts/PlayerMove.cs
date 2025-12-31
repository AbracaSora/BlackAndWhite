using UnityEngine;

namespace Art.Characters.Gen
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 3f;

        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Animator _animator;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            // 向 Animator 传方向
            _animator.SetFloat("MoveX", _movement.x);
            _animator.SetFloat("MoveY", _movement.y);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * (speed * Time.fixedDeltaTime));
        }
    }
} 

