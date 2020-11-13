using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, ITakeDamage
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
