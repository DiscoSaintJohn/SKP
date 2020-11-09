using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject form;

    public static int keyCount;

    static void Start()
    {
        keyCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            keyCount++;
            Destroy(form);
            Debug.Log("Keys: " + keyCount);
        }
    }
}
