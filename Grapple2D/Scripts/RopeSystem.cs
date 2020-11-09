using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class RopeSystem : MonoBehaviour
{
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public PlayerController playerMove;
    private bool ropeAttached;
    private Vector2 playerPos;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    public float climbSpeed = 5f;
    //private bool isColliding; // Look in onCollisionEnter and Exit at the bottom!

    public LineRenderer ropeRend;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDist = 6f;
    private List<Vector2> ropePoss = new List<Vector2>();
    private bool distSet;

    private void Awake()
    {
        ropeJoint.enabled = false;
        playerPos = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDir = worldMousePos - transform.position;
        var aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDir = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPos = transform.position; 

        if (!ropeAttached)
        {
            SetCrosshairPos(aimAngle);
        }
        else
        {
            crosshairSprite.enabled = false;
        }

        HandleInput(aimDir);
        UpdateRopePos();
        HandleRopeLength();
    }

    private void SetCrosshairPos(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crosshairPos = new Vector3(x, y, 1);
        crosshair.transform.position = crosshairPos;
    }

    private void HandleInput(Vector2 aimDir)
    {
        if(Input.GetMouseButtonDown(0))
        {
            // if (ropeAttached) return;
            ropeRend.enabled = true;
            playerMove.isSwinging = true;

            var hit = Physics2D.Raycast(playerPos, aimDir, ropeMaxCastDist, ropeLayerMask);

            if(hit.collider != null)
            {
                ropeAttached = true;
                if(!ropePoss.Contains(hit.point))
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 0.5f), ForceMode2D.Impulse);
                    ropePoss.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPos, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }
            }
            else
            {
                ropeRend.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
            ResetRope();
    }

    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMove.isSwinging = false;
        ropeRend.positionCount = 2;
        ropeRend.SetPosition(0, transform.position);
        ropeRend.SetPosition(1, transform.position);
        ropePoss.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }

    private void UpdateRopePos()
    {
        if (!ropeAttached)
            return;

        ropeRend.positionCount = ropePoss.Count + 1;

        for (var i = ropeRend.positionCount = ropePoss.Count - 1; i >= 0; i--)
        {
            if (i != ropeRend.positionCount - 1)
            {
                ropeRend.SetPosition(i, ropePoss[i]);

                if (i == ropePoss.Count - 1 || ropePoss.Count == 1)
                {
                    var ropePos = ropePoss[ropePoss.Count - 1];
                    if (ropePoss.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePos;
                        if (!distSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePos);
                            distSet = true;
                        }
                        else
                        {
                            ropeHingeAnchorRb.transform.position = ropePos;
                            if (!distSet)
                            {
                                ropeJoint.distance = Vector2.Distance(transform.position, ropePos);
                                distSet = true;
                            }
                        }
                    }
                    else if (i - 1 == ropePoss.IndexOf(ropePoss.Last()))
                    {
                        ropePos = ropePoss.Last();
                        ropeHingeAnchorRb.transform.position = ropePos;
                        if (!distSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePos);
                            distSet = true;
                        }
                    }
                }
                else
                    ropeRend.SetPosition(i, transform.position);
            }
        }
    }

    private void HandleRopeLength()
    {
        if (Input.GetKey(KeyCode.LeftShift) && ropeAttached)
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        if (Input.GetKey(KeyCode.LeftControl) && ropeAttached)
            ropeJoint.distance += Time.deltaTime * climbSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //isColliding = false;
    }
}
