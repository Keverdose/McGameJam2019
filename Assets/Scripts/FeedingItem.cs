using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingItem : MonoBehaviour
{

    public enum Item { Food, Water };



    // Returns an Integer for the Item that was picked up
    public int getItemNumber() {
        if (this.gameObject.tag == "Food") 
            return 0;
        if (this.gameObject.tag == "Water")
            return 1;

        print("ERROR: Returned Incorrect Item Number");

        return 2;
    }
}
