using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public GameObject[] gate;
    public static Key keys;
    public float dist = 2f;

    float currentDist;
    public Vector2[] gatePos;
    private Vector2[] Open;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= gate.Length; i++)
        {
            gatePos[i] = gate[i].transform.position;
            Open[i] = gatePos[i] + Vector2.up * dist;
        }
    }

    void FixedUpdate()
    {
        if (Key.keyCount >= 4)
        {
            currentDist += Time.fixedDeltaTime;

            if (currentDist >= speed)
            {
                currentDist = speed;
            }

            float Perc = currentDist / speed;
            gate[0].transform.position = Vector2.Lerp(gatePos[0], Open[0], Perc);
        }

        if (Key.keyCount >= 7)
        {
            Rigidbody2D rb1 = gate[1].GetComponent<Rigidbody2D>();
            rb1.position = Vector2.up * 2 * Time.fixedDeltaTime;
        }

        if (Key.keyCount >= 10)
        {

        }

        if (Key.keyCount >= 13)
        {

        }
    }
}
