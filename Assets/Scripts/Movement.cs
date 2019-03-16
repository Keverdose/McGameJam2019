using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Movement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        //No gravity for 2D movement
        rigidBody.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY);
        transform.position += move * speed * Time.deltaTime;
    }
}
