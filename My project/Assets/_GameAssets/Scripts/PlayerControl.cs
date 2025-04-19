
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement_Settings")]
    [SerializeField] private float movement_speed;
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    [Header("Jump_Settings")]
    [SerializeField] private KeyCode _JumpKey;
    [SerializeField] private float _JumpForce;
    [SerializeField] private float Jumped_Movement_Speed;
    private Rigidbody _playerRigidBody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movement_direction;
    [SerializeField] private bool _canjump;
    [SerializeField] private float _JumpCooldown;
    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;



    void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerRigidBody.freezeRotation = true;
    }
    private void ResetJumping()
    {
        _canjump = true;
    }
    //Inputları fonksiyonla float Değişkenlere Çektik.
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(_JumpKey) && _canjump && Isgrounded())
        {
            SetPlayerJumping();
            _canjump = false;
            //Zıplama Gerçekleşecek
            Invoke(nameof(ResetJumping), _JumpCooldown);
        }
    }

    void Update()
    {
        SetInputs();
    }
    private void SetPlayerMovement()
    {
        _movement_direction = _orientationTransform.forward * _verticalInput +
        _orientationTransform.right * _horizontalInput;
        _playerRigidBody.AddForce(_movement_direction.normalized * movement_speed, ForceMode.Force);

    }
    private void SetPlayerJumping()
    {
        _playerRigidBody.linearVelocity = new UnityEngine.Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);
        _playerRigidBody.AddForce(transform.up * _JumpForce, ForceMode.Impulse);

    }
    void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private bool Isgrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

}
