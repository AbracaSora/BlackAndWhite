using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    
    public enum PlayerFaceAt { Up, Down, Left, Right }
    public PlayerFaceAt faceAt = PlayerFaceAt.Down;
    public static PlayerMove Instance;
    private bool _stop = false;
    
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private void Start()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void UpdateFacing(Vector2 input)
    {
        if (input == Vector2.zero) return;
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            faceAt = input.x > 0 ? PlayerFaceAt.Right : PlayerFaceAt.Left;
        }
        else
        {
            faceAt = input.y > 0 ? PlayerFaceAt.Up : PlayerFaceAt.Down;
        }
    }
    
    void UpdateAnimatorIdleDirection()
    {
        if (_animator.GetFloat(SpeedHash) > 0.01f) return;
        switch (faceAt)
        {
            case PlayerFaceAt.Up:
                _animator.SetFloat(MoveXHash, 0);
                _animator.SetFloat(MoveYHash, 1);
                break;
            case PlayerFaceAt.Down:
                _animator.SetFloat(MoveXHash, 0);
                _animator.SetFloat(MoveYHash, -1);
                break;
            case PlayerFaceAt.Left:
                _animator.SetFloat(MoveXHash, -1);
                _animator.SetFloat(MoveYHash, 0);
                break;
            case PlayerFaceAt.Right:
                _animator.SetFloat(MoveXHash, 1);
                _animator.SetFloat(MoveYHash, 0);
                break;
        }
    }
    
    public void ForbidMovement()
    {
        _stop = true;
        _animator.SetFloat(SpeedHash, 0);
    }
    
    public void AllowMovement()
    {
        _stop = false;
    }
    private void Update()
    {
        if (_stop)
        {
            return;
        }
        _movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical") );
        UpdateFacing(_movement);
        // 向 Animator 传方向
        _animator.SetFloat(MoveXHash, _movement.x);
        _animator.SetFloat(MoveYHash, _movement.y);
        _animator.SetFloat(SpeedHash, _movement.sqrMagnitude);
        UpdateAnimatorIdleDirection();
    }
    
    public Vector2 FacingVector()
    {
        return faceAt switch
        {
            PlayerFaceAt.Up => Vector2.up, PlayerFaceAt.Down => Vector2.down, PlayerFaceAt.Left => Vector2.left, PlayerFaceAt.Right => Vector2.right, _ => Vector2.down
        };
    }
    private void FixedUpdate()
    {
        if (_stop)
        {
            return;
        }
        _rb.MovePosition(_rb.position + _movement * (speed * Time.fixedDeltaTime));
    }
}

