using System;
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
        _NextGraceEndTime = float.MinValue;
        _IsAlive = true;
        _HeartCount = 5;
    }

    void Update()
    {
        if (_IsAlive)
        {
            _DirectionOfMove = (_CurrentMove - _TargetMove).normalized;
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
    }

    #endregion

    #region Attributes

    CharacterController _Controller;
    private Vector2 _TargetMove;
    private Vector2 _CurrentMove;
    private Vector2 _DirectionOfMove;
    private Vector2 _SmoothMoveVelocity;
    private float _NextDashEndTime;
    private float _NextGraceEndTime;
    private bool _IsAlive;
    private Weapon _CurrentWeapon;

    [SerializeField]
    [Tooltip("Number of hearts.")]
    int _HeartCount = 5;

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
    [Tooltip("Monster hit grace duration (so the player is not constantly hurting).")]
    float _GraceDuration = 1.2f;

    
    [SerializeField]
    [Tooltip("Duration of dash in seconds.")]
    float _DashDuration = 0.3f;

    [SerializeField]
    [Tooltip("Array of weapons that the player can use randomly")]
    Weapon[] _Weapons;

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
        if (context.performed && _IsAlive)
        {
            HitAt(context, new Vector2(transform.position.x, transform.position.y));
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && _IsAlive)
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
        if (_CurrentWeapon == null)
        {
            _CurrentWeapon = _Weapons[0];
        }
        _CurrentWeapon.Attack(_Target, _DirectionOfMove);
        var rand = new System.Random();
        _CurrentWeapon = _Weapons[rand.Next(_Weapons.Length)];
        // TODO display current weapon...
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Monster") && Time.time > _NextGraceEndTime)
        {
              HurtByMonster();
              _NextGraceEndTime = Time.time + _GraceDuration;
        }
    }

    private void HurtByMonster()
    {
      if (_IsAlive)
      {
          _HeartCount -= 1;
          Debug.Log("Lost a heart");
          //TODO : update UI
          if (_HeartCount <= 0)
          {
              _HeartCount = 0;
              Die();
          }
      }
    }

    private void Die()
    {
        _IsAlive = false;
        Debug.Log("Game Over");
        //TODO : end the game.
    }

    #endregion
}
