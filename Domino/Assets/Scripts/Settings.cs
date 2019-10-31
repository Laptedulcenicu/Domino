using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Color inactive;
    public Color active;
    public Button sound;
    public Button vibration;


    void Awake ( )
    {
        ChangeColor ( sound, PlayerPrefs.GetInt ( "sound", 1 ) );
        ChangeColor ( vibration, PlayerPrefs.GetInt ( "vibration", 1 ) );

    }

    public void ChangeSound ( )
    {
        if (PlayerPrefs.GetInt ( "sound", 1 ) == 0)
            PlayerPrefs.SetInt ( "sound", 1 );
        else
            PlayerPrefs.SetInt ( "sound", 0 );

        ChangeColor ( sound, PlayerPrefs.GetInt ( "sound", 1 ) );
    }




    public void ChangeVibration ( )
    {
        if (PlayerPrefs.GetInt ( "vibration", 1 ) == 0)
            PlayerPrefs.SetInt ( "vibration", 1 );
        else
            PlayerPrefs.SetInt ( "vibration", 0 );

        ChangeColor ( vibration, PlayerPrefs.GetInt ( "vibration", 1 ) );
    }

    private void ChangeColor ( Button button, int state )
    {
        if (state == 0)
            button.image.color = inactive;
        else
            button.image.color = active;
    }
}
