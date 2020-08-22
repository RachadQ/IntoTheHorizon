using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

struct playerData
{
    public float PlayerSpeed { get; set; }
    public float DirectionSpeed { get; set; }
}
public class Player : MonoBehaviour
{

    Rigidbody playerRb;
    playerData data;
    public AudioClip ScoreUpdate;
    public AudioClip damage;
    public GameObject sceneManager;
    public Coins maxCoins;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>() as Rigidbody;
        data.PlayerSpeed = 1500;
        data.DirectionSpeed = 20;

       
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {

        //this code will happen in unity editor, standalone or webplayer
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        //move on the x axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //move smoothey on the x axis between -2.5f and 2.5f
        transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), gameObject.transform.position.y, gameObject.transform.position.z), data.DirectionSpeed * Time.deltaTime);
#endif
        playerRb.velocity = Vector3.forward * data.PlayerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * playerRb.velocity.z / 3);
        //Mobile controllers
        //get location touch on screen
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        if (Input.touchCount > 0)
        {
            transform.position = new Vector3(touch.x, transform.position.y, transform.position.z);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("score"))
        {
            GetComponent<AudioSource>().PlayOneShot(ScoreUpdate, 1.0f);
          
        }

        if (other.gameObject.CompareTag("triangle"))
        {
            GetComponent<AudioSource>().PlayOneShot(damage, 1.0f);
            sceneManager.GetComponent<Map_Init>().GameOver();
            maxCoins.UpdateCoins();
            
        }


    }

}
