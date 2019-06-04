using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    public GameObject Floater;
    [SerializeField]
    public GameObject AntiGrav;
    [SerializeField]
    public GameObject Passer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnFloater()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y - 1);
        Instantiate(Floater, playerPos, Quaternion.identity);
    }

    public void SpawnAntiGrav()
    {

            Vector2 playerPos = new Vector2(player.position.x, player.position.y - 1);
            Instantiate(AntiGrav, playerPos, Quaternion.identity);
    }

    public void SpawnPasser()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y - 1);
        Instantiate(Passer, playerPos, Quaternion.identity);
    }
}
