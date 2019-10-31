using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    private IEnumerator coroutine;
    Color color;
    public Color setColor = new Color ( 0, 0, 0,0 );
    bool tached;   
    MeshRenderer renderer;
    MaterialPropertyBlock propertyBlock;
    public Color Intermediate;
    




    // Start is called before the first frame update
    void Awake ( )
    {

        propertyBlock = new MaterialPropertyBlock ( );
        renderer = gameObject.GetComponent<MeshRenderer> ( );
        propertyBlock.SetColor ( "_Color", Intermediate );
        renderer.SetPropertyBlock ( propertyBlock );

        if (setColor != new Color ( 0, 0, 0, 0 ))
        {
              color = setColor;

        }
        else
        {

            color = Intermediate;
        }

    }

    public void TakeData ( Color data )
    {
        Intermediate = data;
    }

    private void OnCollisionEnter ( Collision collision )
    {
        if (!tached)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            {
                SoundPlayer.Instance.PlaySound ( "domino" );
                if (PlayerPrefs.GetInt ( "vibration", 1 ) != 0)
                {
                    Vibration.Vibrate ( 20 );
                }
                propertyBlock.SetColor ( "_Color", Color.yellow );
                renderer.SetPropertyBlock ( propertyBlock );
                
                coroutine = WaitAndPrint ( 0.5f );
                StartCoroutine ( coroutine );
                GameManager.Instance.numberOfDomino -=1;
                tached = true;
                GameManager.Instance.waitTime = 2;

            }
        }

    }

    private IEnumerator WaitAndPrint ( float waitTime )
    {
        while (true)
        {
            yield return new WaitForSeconds ( waitTime );
            propertyBlock.SetColor ( "_Color", color );
            renderer.SetPropertyBlock ( propertyBlock );
        }
    }



}
