using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Rigidbody2D rb;

    private int meleeDmg = 10;
    private int rangeDmg = 10;

    //private float knockback = 1f;
    //private int pierce = 0;

    //private float rKnockback = 1f;
    //private int rPierce = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {

    }
}
