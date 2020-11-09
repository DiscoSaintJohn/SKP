using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameCore : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    // public Text step1;
    public Camera cam;

    Rigidbody2D rb;
    Collider2D coll;
    Vector3 spawn;
    Vector3 playerPos;
    Vector3 camPos;
    Vector3 goalPos;
    public static int keyCount = 0;

    void Start()
    {
        // step1.enabled = true;
        rb = player.GetComponent<Rigidbody2D>();
        spawn = rb.position;
        goalPos = goal.transform.position;
        coll = goal.GetComponent<BoxCollider2D>();
        camPos = cam.transform.position;
    }

    void Update()
    {
        playerPos = player.transform.position;

        if (playerPos.x > goal.transform.position.x)
        {
            camPos.x += 18f;
            cam.transform.position = camPos;
            goalPos.x += 18f;
            goal.transform.position = goalPos;
            spawn.x = rb.position.x + 1f;
        }

        if (playerPos.x < goal.transform.position.x - 18f)
        {
            camPos.x -= 18f;
            cam.transform.position = camPos;
            goalPos.x -= 18f;
            goal.transform.position = goalPos;
        }

        if (playerPos.y < -7)
            GameOver();
    }

    private void GameOver()
    {
        player.transform.position = spawn;
        rb.velocity = rb.velocity.normalized * 0;
    }
}
