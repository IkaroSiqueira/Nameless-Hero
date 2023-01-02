using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    public override void Attack(Vector2 target, Vector3 dir)
    {
        _AreaCenter = new Vector3(target.x, target.y , 0f) + dir * _AreaRange / 2f;

        Collider[] hitColliders;
        hitColliders = Physics.OverlapBox(_AreaCenter, new Vector3(_AreaRange /2f, _AreaRange /2f, 0.1f), Quaternion.identity, (1 << LayerMask.NameToLayer("Monster")));
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
            // Draw a yellow cube at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_AreaCenter, new Vector3(_AreaRange, _AreaRange, 0.1f));
        }
    }
}
