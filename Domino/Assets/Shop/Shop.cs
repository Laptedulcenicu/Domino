using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public Transform selector;
    public Transform content;
    public GameObject skinHolderPrefab;

    public SkinData[] skins;

   

    private void Awake()
    {
        instance = this;



        for (int i = 0; i < skins.Length; i++)
        {
            Instantiate(skinHolderPrefab, content).GetComponent<SkinHolder>().SetSkinHolder(skins[i]);

        }
    }


    
}
