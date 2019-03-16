using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Movement2 : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //No gravity for 2D movement
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");

        float p2_moveX = Input.GetAxis("p2_Horizontal");
        float p2_moveY = Input.GetAxis("p2_Vertical");



        //Vector3 move = new Vector3(moveX, moveY);
        Vector3 move2 = new Vector3(p2_moveX, p2_moveY);

        transform.position += move2 * speed * Time.deltaTime;
    }
}
