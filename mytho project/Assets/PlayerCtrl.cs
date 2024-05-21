using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player1;
    public SpriteRenderer spriteRenderer2;

    public Rigidbody2D rb;

    public float moveSpeed = 7f;
    public float forceDamping = 1.2f;

    private Vector2 PlayerInput;
    private Vector2 forceToApply;

    private void Awake()
    {
        forceToApply = Vector2.zero;
    }

    private void Update()
    {
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = PlayerInput * moveSpeed;
        rb.velocity = targetVelocity + forceToApply;

        forceToApply /= forceDamping;

        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
    }

    private void Move(Vector2 dir)
    {
        Move(dir, 0);
    }

    private void Move(Vector2 dir, float offset)
    {
        Vector2 origin = transform.position;

        if (dir == Vector2.up)
        {
            offset = 0.1f;
        }
        else if (dir == Vector2.down)
        {
            offset = -0.5f;
        }
        else if (dir == Vector2.left)
        {
            offset = -0.125f;
        }
        else if (dir == Vector2.right)
        {
            offset = 0.125f;
        }

        origin += offset * Vector2.one;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, moveSpeed * Time.deltaTime);

        if (raycast.collider != null && raycast.collider.CompareTag("Collidable"))
        {
            float distance = Mathf.Abs(raycast.point.y - origin.y);
            distance = Mathf.Min(distance, Mathf.Abs(raycast.point.x - origin.x));
            forceToApply += distance * -dir.normalized * 20f;
        }
    }
}


//sheidzleba mere gamomadges

//private void OnCollisionEnter2D(Collision2D collision)
//{ 
//    if (collision.collider.comparetag("bullet"))
//    {
//        forcetoapply += new vector2(-20, 0);
//        destroy(collision.gameobject);
//    }
//}

