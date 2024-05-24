using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player1;
    public SpriteRenderer spriteRenderer2;
    public GameObject back;
    public Rigidbody2D rb;

    public float moveSpeed = 7f;
    public float forceDamping = 1.2f;

    private Vector2 playerInput;
    private Vector2 forceToApply;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        forceToApply = Vector2.zero;
    }

    private void Update()
    {
        // Check if 'back' is active; if so, prevent player input
        if (back.activeSelf)
        {
            playerInput = Vector2.zero;
        }
        else
        {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = playerInput * moveSpeed;
        rb.velocity = targetVelocity + forceToApply;

        forceToApply /= forceDamping;

        if (forceToApply.magnitude <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        if (playerInput != Vector2.zero)
        {
            Move(playerInput);
        }
    }

    private void Move(Vector2 dir)
    {
        Move(dir, 0);
    }

    private void Move(Vector2 dir, float offset)
    {
        Vector2 origin = transform.position;

        if (back.activeSelf) 
        {
            return;
        }

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

        origin += offset * dir;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, moveSpeed * Time.fixedDeltaTime);

        if (raycast.collider != null && raycast.collider.CompareTag("Collidable"))
        {
            float distance = Mathf.Min(Mathf.Abs(raycast.point.x - origin.x), Mathf.Abs(raycast.point.y - origin.y));
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

