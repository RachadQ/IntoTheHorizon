using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct data
{
   
    
}
public class PCamera : MonoBehaviour
{
    public GameObject player { get; set; }
    public float offset { get; set; }
  
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        offset = 10f;
    }

    // Update is called once per frame
 

    void LateUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0,gameObject.transform.position.y, player.gameObject.transform.position.z - offset), Time.deltaTime * 100);
    }
}
