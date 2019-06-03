using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        // GameObject itemInstance;

        Vector2 playerPos = new Vector2(player.position.x, player.position.y - 2);
        // itemInstance = Instantiate(item, playerPos, Quaternion.identity) as GameObject;
        // itemInstance.GetComponent<Rigidbody2D>().AddForce(itemInstance.transform.up * 0);
    }
}

public class ObjectDrop : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    public GameObject item;
    private Transform player;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}