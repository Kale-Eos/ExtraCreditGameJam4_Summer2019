using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectDrop : MonoBehaviour
{
    private Inventory inventory;
    public int i;

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
    
    public void DropFloater()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnFloater();
            GameObject.Destroy(child.gameObject);
        }
    }

    public void DropAntiGrav()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnAntiGrav();
            GameObject.Destroy(child.gameObject);
        }
    }

    public void DropPasser()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnPasser();
            GameObject.Destroy(child.gameObject);
        }
    }
}