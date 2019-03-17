using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoZoneScript : MonoBehaviour
{
    public bool canSave;
    // Start is called before the first frame update
    void Start()
    {
        canSave = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canSave = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canSave = false;
        }
    }
}
