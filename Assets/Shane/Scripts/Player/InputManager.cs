using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

//Calls events for some inputs player can do - interact, attack, dash.
//Other scripts can listen out for these events and do whatever in response.

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("Movement")]
    [SerializeField] private FixedJoystick fixedJoystick;
    public static bool IsMovementInput { get { return MovementInput != Vector3.zero; } }
    public static Vector3 MovementInput { get; private set; }
    private Camera mainCamera;

    [Header("Interacting")]
    [SerializeField] private string interactKeyBindName;
    [SerializeField] private Button btnInteract;
    private Func<KeyCode, bool> interactInputDelegate; //Not used yet. Need this if certain interaction areas you need to interact by holding button instead of single press.
    public static event Action InteractInputEvent;
    public bool InteractButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[interactKeyBindName]);} }

    [Header("Attacking")]
    [SerializeField] private string attackKeyBindName;
    [SerializeField] private Button btnAttack;
    public static event Action AttackInputEvent;
    public bool AttackButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[attackKeyBindName]);} }

    [Header("Dashing")]
    [SerializeField] private string dashKeyBindName;
    [SerializeField] private Button btnDash;
    public static event Action DashInputEvent;
    public bool DashButtonPressed { get { return Input.GetKeyDown(KeyBindsManager.keyBinds[dashKeyBindName]);} }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;

#if UNITY_ANDROID || UNITY_IOS
        btnInteract.onClick.AddListener(OnInteractInput);
        btnAttack.onClick.AddListener(OnAttackInput);
        btnDash.onClick.AddListener(OnDashInput);
#endif
    }

    //Raising input events.
    private void OnInteractInput() => InteractInputEvent?.Invoke();
    private void OnAttackInput() => AttackInputEvent?.Invoke();
    private void OnDashInput()
    {
        if(IsMovementInput)
            DashInputEvent?.Invoke();
    }

    private void Update()
    {
        #region Check For Movement
#if UNITY_ANDROID || UNITY_IOS
        MovementInput = new Vector3(fixedJoystick.Horizontal, 0, fixedJoystick.Vertical);

#elif UNITY_STANDALONE
        MovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        #endif

        MovementInput = mainCamera.transform.TransformDirection(MovementInput) * Time.deltaTime;
        MovementInput = new Vector3(MovementInput.x, 0, MovementInput.z).normalized;
        #endregion

        #region Check For Attack

#if UNITY_STANDALONE

        if (Input.GetKeyDown(KeyBindsManager.keyBinds[attackKeyBindName]))
            OnAttackInput();
        //if (Input.GetKeyDown(keyAttack))
        //    OnAttackInput();
#endif

        #endregion

        #region Check For Dash

#if UNITY_STANDALONE

        if (Input.GetKeyDown(KeyBindsManager.keyBinds[dashKeyBindName]))
            OnDashInput();
        //if (Input.GetKeyDown(keyDash))
        //    OnDashInput();
#endif

        #endregion

        #region Check For Interaction


        //if (Input.GetKeyDown(keyInteract))
        //    OnInteractInput();

        if (Input.GetKeyDown(KeyBindsManager.keyBinds[interactKeyBindName]))
            OnInteractInput();

        #endregion
    }

    private void OnDestroy()
    {

        Delegate[] attackInputEventListeners = AttackInputEvent.GetInvocationList();

        for (int i = 0; i < attackInputEventListeners.Length; i++)
        {
            Delegate listener = attackInputEventListeners[i];

            AttackInputEvent -= (Action) attackInputEventListeners[i];
        }

        //Unsub events if on mobile.
#if UNITY_ANDROID || UNITY_IOS

        btnInteract.onClick.RemoveListener(OnInteractInput);
        btnAttack.onClick.RemoveListener(OnAttackInput);
        btnDash.onClick.RemoveListener(OnDashInput);

#endif
    }



}
