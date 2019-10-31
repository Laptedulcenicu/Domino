using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelected : MonoBehaviour
{

      private Material mySkinPrefab;
    // Start is called before the first frame update
 private void Awake() {

 SetSkin((SkinData)Resources.Load("Skins/"+PlayerPrefs.GetString("SelectedSkin", "1")));

    
}
  public void SetSkin(SkinData skin)
    { 
        mySkinPrefab = skin.prefab;
        gameObject.GetComponent<MeshRenderer> ( ).material = mySkinPrefab;
    }

}
