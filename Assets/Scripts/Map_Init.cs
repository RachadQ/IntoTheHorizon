using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class Map_Init : MonoBehaviour,IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "1486551";
#elif UNITY_ANDROID
    private string gameId = "1486550";
#endif


   
    public GameObject inMenuUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject adButton;
    public GameObject player;
    public GameObject restartButton;
    private bool hasGameStarted = false;
    private bool AdReward = false;
    
    public string myPlacementId = "rewardedVideo";

    public Button ad;

    void Awake()
    {

        if (player == null)
        {
            
        }
        Shader.SetGlobalFloat("_Curvature", 2.0f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
        Application.targetFrameRate = 60;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);


    }
    // Start is called before the first frame update
    void Start()
    {
        
        inMenuUI = GameObject.FindGameObjectWithTag("inMenu");
        player = GameObject.FindGameObjectWithTag("player");
        gameOverUI = GameObject.FindGameObjectWithTag("GameOver");
        adButton = GameObject.FindGameObjectWithTag("AdButton");
        restartButton = GameObject.FindGameObjectWithTag("restart");
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        
        inMenuUI.gameObject.SetActive(true);
        //make sure the rest of Ui is off
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        
        StartCoroutine(ShowBannerWhenInitialized());
    }

    public void PlayButton()
    {
        if (hasGameStarted == true)
        {
            StartCoroutine(StartGame(1.0f));
        }
        else
        {
            StartCoroutine(StartGame(0.0f));
        }
       
    }

    public void PauseButton()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        inMenuUI.gameObject.SetActive(true);
        //make sure the rest of Ui is off
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        
    }

    public void GameOver()
    {
        
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<SphereCollider>().enabled = false;
        hasGameStarted = true;
        inMenuUI.gameObject.SetActive(false);
        //make sure the rest of Ui is off
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
        if (AdReward == true)
        {
            adButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            adButton.GetComponent<Button>().enabled = false;
            adButton.GetComponent<Animator>().enabled = false;
            restartButton.GetComponent<Animator>().enabled = true;

        }
        
    }

   
    public void RestartGameButton()
    {
        Advertisement.RemoveListener(this);
        SceneManager.LoadScene(0);
    }

 
    public void ShowAd()
    {
       
        if (Advertisement.IsReady())
        {

            Advertisement.Show(myPlacementId);
           
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

   
    IEnumerator StartGame(float waitTime)
    {
       
        inMenuUI.gameObject.SetActive(false);
        //make sure the rest of Ui is off
        inGameUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
       
        yield return new WaitForSeconds(waitTime);
        //enable players constraints and collider
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        yield return new WaitForSeconds(0.15f);
        player.GetComponent<SphereCollider>().enabled = true;
       
        


    }

    // Implement IUnityAdsListener interface methods:
    

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       
       
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

        if (showResult == ShowResult.Finished)
        {
            Debug.Log("ad Successful");
            AdReward = true;
           
            StartCoroutine(StartGame(1.5f));
            

        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Skip before reach the end");
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
          
        }
       
    }
   


    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);

        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("bannerPlacement");
       
    }
}
