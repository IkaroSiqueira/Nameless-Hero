using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region Methods

    public void HurtByPlayer()
    {
        gameObject.SetActive(false);
        // TODO manage monsters properly.
    }

    #endregion
}
