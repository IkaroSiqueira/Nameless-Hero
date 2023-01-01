using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///<summary>
///    Class <c>Player</c> represents and manages a player character.
///</summary>
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    #region UnityEvents

    void Awake()
    {
        _Controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _NextDashEndTime = float.MinValue;
    }

    void Update()
    {
        _CurrentMove = Vector2.SmoothDamp(_CurrentMove, _TargetMove, ref _SmoothMoveVelocity, _SmoothMoveSpeed);
        var move = new Vector3(_CurrentMove.x, _CurrentMove.y, 0);
        bool DashComplete = (Time.time > _NextDashEndTime);
        if (DashComplete == true)
        {
            _Controller.Move(move * (Time.deltaTime * _PlayerSpeed));
        } else
        {
            _Controller.Move(move * (Time.deltaTime * _PlayerDashSpeed));
        }
    }

    #endregion

    #region Attributes

    CharacterController _Controller;
    private Vector2 _TargetMove;
    private Vector2 _CurrentMove;
    private Vector2 _SmoothMoveVelocity;
    private float _NextDashEndTime;

    [SerializeField]
    [Tooltip("Player speed.")]
    float _PlayerSpeed = 2.0f;
    [SerializeField]
    [Tooltip("Player dash speed.")]
    float _PlayerDashSpeed = 30.0f;

    [SerializeField]
    [Tooltip("Move smooth damp speed.")]
    float _SmoothMoveSpeed = 0.3f;

    [SerializeField]
    [Tooltip("CoolDown script for the dash.")]
    CoolDown _DashCoolDown;

    
    [SerializeField]
    [Tooltip("Duration of dash in seconds.")]
    float _DashDuration = 0.3f;

    #endregion

    #region InputCallbacks

    public void Move(InputAction.CallbackContext context)
    {
        // Started context doubles the message so we don't need it.
        if (context.started)
        {
            return;
        }

        // Read the move and store value for next update.
        _TargetMove = context.action.ReadValue<Vector2>();
    }

    public void Hit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HitAt(context, new Vector2(transform.position.x, transform.position.y));
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool isDashing = _DashCoolDown.Triggered();
              if(isDashing && Time.time > _NextDashEndTime)
              {
                  _NextDashEndTime = _DashDuration + Time.time;
              }
        }
    }

    #endregion

    #region Methods
    private void HitAt(InputAction.CallbackContext context, Vector2 _Target)
    {
        Debug.Log("Hit at ");
    }

    #endregion
}
