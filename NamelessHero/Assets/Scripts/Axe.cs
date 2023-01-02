using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void Attack(Vector2 target, Vector3 dir)
    {
        _AreaCenter = new Vector3(target.x, target.y , 0f);
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(_AreaCenter, _AreaRange, (1 << LayerMask.NameToLayer("Monster")));

        foreach (var hitCollider in hitColliders)
        {
            //Get a reference to a health script attached to the collider we hit
            Monster mons = hitCollider.GetComponent<Monster>();
            //If there was a health script attached
            if (mons != null)
            {
                mons.HurtByPlayer();
            }
        }
    }

    void OnDrawGizmos()
    {
        if (_AreaCenter != null)
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_AreaCenter, _AreaRange);
        }
    }
}
