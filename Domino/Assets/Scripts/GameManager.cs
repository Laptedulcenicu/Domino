using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject LosePanel;
    public GameObject WinPanel;
    public GameObject MenuPanel;
    public GameObject GamePanel;
    public GameObject SettingsPanel;
    public GameObject ShopPanel;
    public float numberOfDomino;
    bool win;
    string lastLevel;
    public Text currentLv;
    public int addMoney;
    public Text recivedMoney;
    public Text currentMoney;
    public GameObject Player;

    public float startWaitTime;
    public float waitTime;
    Vector3 positionOfPlayer;
    public GameObject prefabOfPlayer;
    public GameObject continueButton;
    bool firstTry=true;
    

 


    // Start is called before the first frame update
    protected override void Awake ( )
    {
        base.Awake ( );
        
        lastLevel = PlayerPrefs.GetString ( "last", "lv1" );
        if (SceneManager.GetActiveScene ( ).name != lastLevel)
        {
            FadeToScene ( lastLevel );
        }


        currentLv.text ="LEVEL "+ (SceneManager.GetActiveScene ( ).buildIndex + 1).ToString ( );

        recivedMoney.text = (addMoney).ToString ( );
        currentMoney.text = (PlayerPrefs.GetInt ( "Money", 0 )).ToString ( );
        numberOfDomino = FindGameObjectsWithLayer ( 8 );

        Player.GetComponent<FloatingController> ( ).oneDrag = false;
        waitTime = 1000000000000;
        positionOfPlayer = Player.transform.position;
    }
    // Update is called once per frame
    void Update ( )
    {

        if (!win)
        {
            if (numberOfDomino <= 0)
            {
                GamePanel.SetActive ( false );
                WinPanel.SetActive ( true );
                win = true;
                
            }
        }

        if (waitTime <= 0)
        {
            if (numberOfDomino > 0)
            {
                LoseGame ( );

            }
        }
        waitTime -= Time.deltaTime;
    }

    public void FadeToScene ( string sceneName )
    {
        if (sceneName == "lv11")
        {
            sceneName = "lv1";
        }
        SceneManager.LoadScene ( sceneName );
    }

    public void LoseGame ( )
    {
        if (firstTry)
        {
            continueButton.SetActive ( true );
        }
        else
        {
            continueButton.SetActive ( false );
        }
      //  Sounds.PlaySound ( "death" );
        GamePanel.SetActive ( false );
        LosePanel.SetActive ( true );
    }

    public void WinGame ( )
    {

        GamePanel.SetActive ( false );
        WinPanel.SetActive ( true );
    }

    public void NextLevelButton ( )
    {
        int addetmoney = addMoney + PlayerPrefs.GetInt ( "Money", 0 );
        PlayerPrefs.SetInt ( "Money", addetmoney );
        currentMoney.text = (PlayerPrefs.GetInt ( "Money", 0 )).ToString ( );
        PlayerPrefs.SetString ( "last", "lv" + (SceneManager.GetActiveScene ( ).buildIndex + 2).ToString ( ) );

        if ((SceneManager.GetActiveScene ( ).buildIndex + 2) == 61)
        {
            PlayerPrefs.SetString ( "last", "lv" + (1).ToString ( ) );
            SceneManager.LoadScene ( 0 );
        }
        else
        {
            SceneManager.LoadScene ( SceneManager.GetActiveScene ( ).buildIndex + 1 );
        }
    }

    public void Restart ( )
    {
        SceneManager.LoadScene ( SceneManager.GetActiveScene ( ).buildIndex );
    }

    public void OpenSettingPanel ( )
    {
        if (SettingsPanel.activeSelf)
        {
            SettingsPanel.SetActive ( false );
        }
        else
        {
            SettingsPanel.SetActive ( true );
        }
    }

    public void OpenShopPanel ( )
    {
        if (ShopPanel.activeSelf)
        {
            ShopPanel.SetActive ( false );
        }
        else
        {
            ShopPanel.SetActive ( true );
        }
    }

    public void PlayButton ( )
    {
        Player.GetComponent<FloatingController> ( ).oneDrag = true;
        MenuPanel.SetActive ( false );
        GamePanel.SetActive ( true );
    }

    private int FindGameObjectsWithLayer ( int layer )
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ( );
        int objectWithLayer = 0;
        for (var i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].layer == layer)
            {
                objectWithLayer++;
            }
        }

        return objectWithLayer;
    }

    public void addMoneyAds ( )
    {
        int addetmoney = PlayerPrefs.GetInt ( "Money", 0 ) + 100;
        PlayerPrefs.SetInt( "Money", addetmoney);
        currentMoney.text=(PlayerPrefs.GetInt( "Money", 0)).ToString ( );
    }

    public void Continue()
    {
        LosePanel.SetActive ( false );
        GamePanel.SetActive ( true );
        waitTime = 1000000000000;
        Player=Instantiate ( prefabOfPlayer,positionOfPlayer, Quaternion.identity );
        firstTry = false;


    }
}
