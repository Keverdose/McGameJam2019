using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public Harvest[] harvest;
    public GameObject milkUI;
    public GameObject eggUI;
    public GameObject cheeseUI;
    public int orderIndex;
    public Harvest nextHarvest;
    Queue harvestQueue = new Queue();

    // Start is called before the first frame update
    void Start()
    {
        harvest = new Harvest[3];
        harvest[0] = Harvest.Milk;
        harvest[1] = Harvest.Egg;
        harvest[2] = Harvest.Cheese;
        orderIndex = 0;

        getNewHarvest();
        harvestQueue.Enqueue(harvest[0]);
        harvestQueue.Enqueue(harvest[1]);
        harvestQueue.Enqueue(harvest[2]);
    }

    // Update is called once per frame
    void Update()
    {
        switch (nextHarvest)
        {
            case Harvest.Milk:
                milkUI.SetActive(true);
                eggUI.SetActive(false);
                cheeseUI.SetActive(false);
                break;
            case Harvest.Egg:
                milkUI.SetActive(false);
                eggUI.SetActive(true);
                cheeseUI.SetActive(false);
                break;
            case Harvest.Cheese:
                milkUI.SetActive(false);
                eggUI.SetActive(false);
                cheeseUI.SetActive(true);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        //Missing && player.itemIndex
        if (Input.GetKeyDown(KeyCode.Space) && player != null)
        {
            GetNextHarvest();
        }
    }

    void getNewHarvest()
    {
        for (int i = 0; i < harvest.Length; i++)
        {
            int tmp = (int)harvest[i];
            int r = UnityEngine.Random.Range(i, harvest.Length);
            harvest[i] = harvest[r];
            harvest[r] = (Harvest)tmp;
            harvestQueue.Enqueue(harvest[i]);
        }
    }
    void GetNextHarvest()
    {
        if(harvestQueue.Count > 0)
        {
            harvestQueue.Dequeue();
            int r = UnityEngine.Random.Range(0, harvest.Length);
            nextHarvest = harvest[r];
        }
        else
        {
            getNewHarvest();
        }

    }

}
