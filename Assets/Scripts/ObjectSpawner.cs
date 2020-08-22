using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] trianglePrafabs;
    public GameObject[] Coins;
    private Vector3 spawnObjPos;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToHorizion = Vector3.Distance(player.transform.position,spawnObjPos);
        if (distanceToHorizion < 120)
        {
            SpawnTriangles();
            if (Random.Range(1,10) < 4)
            {
                SpawnCoins();
            }
          
        }
        

    }

    //spawn object
    void SpawnTriangles()
    {
        spawnObjPos = new Vector3(0,0,spawnObjPos.z + Random.Range(15,30));
        Instantiate(trianglePrafabs[(Random.Range(0,trianglePrafabs.Length))], spawnObjPos, Quaternion.identity);
    }

    void SpawnCoins()
    {

        int random = Random.Range(0,Coins.Length);
        
        spawnObjPos = new Vector3(0, 0, spawnObjPos.z + Random.Range(1, 30));
        Instantiate(Coins[(Random.Range(0, Coins.Length))], spawnObjPos, Quaternion.identity);
    }
}
