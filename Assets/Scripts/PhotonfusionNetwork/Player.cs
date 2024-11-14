using Fusion;
using Fusion.Addons.KCC;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] modelParts;
    //[SerializeField] private Transform camTarget;
    [SerializeField] private AudioSource source;

    public double Score => Math.Round(transform.position.y, 1);
    public bool IsReady; // Server is the only one who cares about this

    [Networked] public string Name { get; private set; }

    public InputManager inputManager;

    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController charactercontroller;

    [SerializeField] private Vector3 nowVelocity;
    [SerializeField] private bool IsMove;

    [Header("Player Health & Energy")]
    [SerializeField] private float playerHealth = 8000f;
    public float presentHealth;

    [Header("Test")]
    [SerializeField] private InputActionAsset playerInputAction;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            inputManager.LocalPlayer = this;
            Name = PlayerPrefs.GetString("Photon.Menu.Username");
            RPC_PlayerName(Name);
           // CameraFollow.Singleton.SetTarget(camTarget);
            UIManager.Singleton.LocalPlayer = this;
        }
    }
    public override void FixedUpdateNetwork()
    {
        /*if (input.Buttons.IsSet(InputButton.W) || input.Buttons.IsSet(InputButton.S)
                || input.Buttons.IsSet(InputButton.A) || input.Buttons.IsSet(InputButton.D))
        {
            Debug.Log("Player 이동하고있는경우>>");
            anim.SetBool("IsRunning", true);
        }
        else if (!input.Buttons.IsSet(InputButton.W) && !input.Buttons.IsSet(InputButton.S)
            && !input.Buttons.IsSet(InputButton.A) && !input.Buttons.IsSet(InputButton.D))
        {
            Debug.Log("Player 멈춰있는경우>>");
            anim.SetBool("IsRunning", false);
        }*/
        // nowVelocity = new Vector3(charactercontroller.velocity.x, charactercontroller.velocity.y, charactercontroller.velocity.z);
        nowVelocity = new Vector3(playerInputAction.actionMaps[3].actions[4].ReadValue<Vector2>().x, 0, playerInputAction.actionMaps[3].actions[4].ReadValue<Vector2>().y);

        Debug.Log("Player characterController.velocity>>" + nowVelocity);
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

        //UpdateCamTarget();
    }

    public override void Render()
    {
       /* if (kcc.Settings.ForcePredictedLookRotation)
        {
            Vector2 predictedLookRotation = baseLookRotation + inputManager.AccumulatedMouseDelta * lookSensitivity;
            kcc.SetLookRotation(predictedLookRotation);
        }*/

        //UpdateCamTarget();
    }
   /* private void UpdateCamTarget()
    {
        camTarget.localRotation = Quaternion.Euler(kcc.GetLookRotation().x, 0f, 0f);
    }*/
    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority | RpcTargets.StateAuthority)]
    public void RPC_SetReady()
    {
        IsReady = true;
        if (HasInputAuthority)
            UIManager.Singleton.DidSetReady();
    }
    /*public void Teleport(Vector3 position, Quaternion rotation)
    {
        kcc.SetPosition(position);
        kcc.SetLookRotation(rotation);
    }*/

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_PlayerName(string name)
    {
        Name = name;
    }
}

