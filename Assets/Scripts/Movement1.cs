using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Movement1 : MonoBehaviour
{
    Rigidbody2D rb;
    Animator am;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //No gravity for 2D movement
        rb.gravityScale = 0;
        am = GetComponent<Animator>();
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

        am.SetFloat("vspeed", moveY);
        am.SetFloat("hspeed", moveX);

        Vector3 move = new Vector3(moveX, moveY);

        transform.position += move * speed * Time.deltaTime;
    }
}
