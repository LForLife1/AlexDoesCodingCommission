using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonScript : MonoBehaviour
{

    public GameObject inventory;
    public bool inventoryIsClosed;

    // Start is called before the first frame update
    void Start()
    {
        //inventory = ;
        inventoryIsClosed = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryIsClosed)
            {
                inventory.SetActive(true);
                inventoryIsClosed = false;
            }
            else
            {
                inventory.SetActive(false);
                inventoryIsClosed = true;
            }
        }

    }
}
