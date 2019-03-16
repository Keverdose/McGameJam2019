using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Harvest{Milk, Egg, Cheese};

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Player[] players;
    public Harvest[] harvest;
    public GameObject basket;

    // Start is called before the first frame update
    void Start()
    {
        harvest = new Harvest[3];
        harvest[0] = Harvest.Milk;
        harvest[1] = Harvest.Egg;
        harvest[2] = Harvest.Cheese;

        //Init players
        //
    }

    // Update is called once per frame
    void Update()
    {

        
    }



    void printA()
    {
        foreach(Harvest h in harvest)
        {
            print(h);
        }
    }


    void spawnEnemy()
    {
        UnityEngine.Random.Range(0, enemies.Length);

    }
}