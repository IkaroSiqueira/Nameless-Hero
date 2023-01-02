using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Attributes

    protected Vector3 _AreaCenter;
    [SerializeField] protected float _AreaRange = 2f;

    #endregion
    
    #region Methods

    public abstract void Attack(Vector2 target, Vector3 dir);

    #endregion
}
