using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class Jump : NetworkBehaviour
{
    [SerializeField] private InputActionReference jumpButton;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController _characterController;
    public Vector3 _playerVelocity;

    private void Awake() => _characterController = GetComponent<CharacterController>();

    private void OnEnable() => jumpButton.action.performed += Jumping;

    private void OnDisable() => jumpButton.action.performed -= Jumping;

    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Jumping(InputAction.CallbackContext obj)
    {
        Debug.Log("Jumping>>");
        if (!_characterController.isGrounded) return;
        _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        // player.JumpAnim();
        JumpAnim();
    }
    public void JumpAnim()
    {
        anim.SetBool("Jump", true);
        Invoke(nameof(JumpAfter), 1f);
    }
    private void JumpAfter()
    {
        anim.SetBool("Jump", false);
    }
    public void IsGroundedAnim(bool val_)
    {
        anim.SetBool("IsGround", val_);
    }

    private void Update()
    {
      /*  if (kcc.FixedData.IsGrounded)
        {
            //Debug.Log("Player개체 바닥착지>>");
            anim.SetBool("IsGround", true);
        }
        else
        {
            // Debug.Log("Player개체 공중에 있는경우>>");
            anim.SetBool("IsGround", false);
        }*/

        if (_characterController.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            IsGroundedAnim(true);
        }
        else if (!_characterController.isGrounded)
        {
            IsGroundedAnim(false);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
