using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ITakeDamage
{
    public Rigidbody2D rb;

    private int maxHp = 100;
    private int hp;
    private int armor = 0;
    private int evasion = 1;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
