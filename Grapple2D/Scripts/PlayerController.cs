using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using static GameCore;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public DistanceJoint2D rope;
    public Collider2D groundCheck;
    Vector3 crouch = new Vector3(0.02f, 0.27f);
    bool isGrounded;

    public RopeSystem ropeSys;
    public bool isSwinging = false;

    public float speed = 3f;
    public float maxSpeed = 10f;
    public float airSpeed = 2f;
    public float airSwingSpeed = .2f;
    public float bounce = 150f;

    int deathCount;

    void Update()
    {
        if (!groundCheck.IsTouchingLayers(LayerMask.GetMask("Wall", "Ground")))
        {
            isGrounded = false;
        }
        if (groundCheck.IsTouchingLayers(LayerMask.GetMask("Wall", "Ground")))
        {
            isGrounded = true;
        }

        //    WALKING
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(new Vector2(speed, 0));
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(new Vector2(-speed, 0));
            // GetKeyUp.. stops movement
            if (Input.GetKeyUp(KeyCode.A))
                rb.velocity = rb.velocity.normalized * 1;
            if (Input.GetKeyUp(KeyCode.D))
                rb.velocity = rb.velocity.normalized * 1.2f;
            // speed limit 
            if (rb.velocity.magnitude > maxSpeed)
                rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (!isGrounded && !isSwinging)
        {   // control in-air 
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(new Vector2(airSpeed, 0));
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(new Vector2(-airSpeed, 0));
        }
        if (!isGrounded && isSwinging)
        {   // control while swinging
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(new Vector2(airSwingSpeed, 0f));
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(new Vector2(-airSwingSpeed, 0f));
        }

        //    JUMP AND CROUCH
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, bounce));
        } //maybe make a double jump and a Dash
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.localScale -= crouch;
            if (isGrounded)
                rb.AddForce(new Vector2(0, -bounce));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            player.transform.localScale += crouch;
        } 

        //    RUSH TO ROPE END
        //if (Input.GetMouseButton(1) && isSwinging)
        
            // rush to connectedAnchor
            // Rad2deg??
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(player);
            deathCount++;
        }
    }
}