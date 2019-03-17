using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingItem : MonoBehaviour
{

    public enum Item { Food, Water };



    // Returns an Integer for the Item that was picked up
    public int getItemNumber() {
        if (this.gameObject.tag == "Food") 
            return 3;
        if (this.gameObject.tag == "Water")
            return 4;
        if (this.gameObject.tag == "TrashCan")
            return 5;

        print("ERROR: Returned Incorrect Item Number");

        return 9;
    }
}
