using UnityEngine.UI;
using TMPro;
using UnityEngine;
public class SkinHolder : MonoBehaviour
{
    public Text priceText;
    public Image iconImage;
    public GameObject buyButton;

    private SkinData mySkin;

    public void SetSkinHolder(SkinData skin)
    {
        priceText.text = skin.cost.ToString();
        if (skin.icon != null)
            iconImage.sprite = skin.icon;

        mySkin = skin;

        if (mySkin.isPreinlocked || PlayerPrefs.GetInt(mySkin.name + "State", 0) == 1)
            buyButton.SetActive(false);

        if (PlayerPrefs.GetString("SelectedSkin", "1") == mySkin.name)
            SelectorMove();
    }

    private void SelectorMove()
    {
        Shop.instance.selector.position = transform.position;
        Shop.instance.selector.SetParent(transform);
        Shop.instance.selector.localScale = Vector3.one;
        Shop.instance.selector.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void SelectSkin()
    {
        PlayerPrefs.SetString("SelectedSkin", mySkin.name);
        FindObjectOfType<SkinSelected>().SetSkin(mySkin); 
       

        SelectorMove();
    }

    public void BuySkin()
    {
        if (PlayerPrefs.GetInt("Money",0) < mySkin.cost)
            return;

        AddCoins(-mySkin.cost);
        PlayerPrefs.SetInt(mySkin.name + "State", 1);
        SetSkinHolder(mySkin);
        SelectSkin();

        
    }

       public void AddCoins(int amount){
            int addetmoney=PlayerPrefs.GetInt( "Money", 0)+amount;
            PlayerPrefs.SetInt( "Money", addetmoney);
        GameManager.Instance.currentMoney.text = (PlayerPrefs.GetInt ( "Money", 0 )).ToString();
        //            GameManager.instance.TextMoney();    
    }
}
