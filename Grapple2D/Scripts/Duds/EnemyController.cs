using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject enemy;
    public float speed = 5f;

    public Vector2 minX;
    public Vector2 maxX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position == maxX)
        {
            transform.position = Vector2.MoveTowards(transform.position, minX, speed * Time.deltaTime);
        }
        if (rb.position == minX)
        {
            transform.position = Vector2.MoveTowards(transform.position, maxX, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundCheck")
        {
            Destroy(enemy);
        }
    }
}
