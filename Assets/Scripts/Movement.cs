using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed; 
    // Start is called before the first frame update
    void Start()
    {

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
