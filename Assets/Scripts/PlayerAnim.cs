using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController charactercontroller;

    [SerializeField] private Vector3 nowVelocity;
    [SerializeField] private bool IsMove;

    [Header("Player Health & Energy")]
    [SerializeField] private float playerHealth = 8000f;
    public float presentHealth;

    [Header("Test")]
    [SerializeField] private InputActionAsset playerInputAction;

    // Start is called before the first frame update
    void Start()
    {
    
    }
    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        
        StartCoroutine(showDamage());

        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        //Object.Destroy(gameObject, 1.0f);

        Time.timeScale = 0f;
    }
    IEnumerator showDamage()
    {
        yield return new WaitForSeconds(0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        // nowVelocity = new Vector3(charactercontroller.velocity.x, charactercontroller.velocity.y, charactercontroller.velocity.z);
        nowVelocity = new Vector3(playerInputAction.actionMaps[3].actions[4].ReadValue<Vector2>().x, 0, playerInputAction.actionMaps[3].actions[4].ReadValue<Vector2>().y);

        Debug.Log("characterController.velocity>>" + nowVelocity);
        if (nowVelocity.magnitude > 0)
        {
            IsMove = true;
            anim.SetBool("Run", true);
        }
        else
        {
            IsMove = false;
            anim.SetBool("Run", false);
        }
    }
    public void ShootAnim()
    {
        anim.SetBool("Shoot", true);
        Invoke(nameof(ShootAfter), 0.3f);
    }
    private void ShootAfter()
    {
        anim.SetBool("Shoot", false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Girl"))
        {
            other.GetComponent<Animator>().SetBool("Talking", true);

            // StartCoroutine(ReloadScene());
        }
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
