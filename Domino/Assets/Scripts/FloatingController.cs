using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingController : MonoBehaviour
{
    float RotationSpeed = 10;
    bool pressed;
    bool firstTouch;
    public GameObject ray;
    public GameObject spritePressed;
    public Animator playerAnimation;
    public bool oneDrag=true;
    float Dominos;

    void Start()
    {
        Dominos = GameManager.Instance.numberOfDomino;
    }
    void Update ( )
    {
        if (Input.GetMouseButtonDown ( 0 ))
        {
            if (oneDrag)
            {

            firstTouch = true;             
            ray.SetActive ( true );
            spritePressed.SetActive ( true );
            }
        }
        if (firstTouch)
        {



            if (Input.GetMouseButtonDown ( 0 ))
            {
                pressed = true;

            }

            if (Input.GetMouseButtonUp ( 0 ))
            {
                pressed = false;

            }

            if (pressed)
            {
                
                float x = Input.GetAxis ( "Mouse X" ) * RotationSpeed * Mathf.Deg2Rad;
                transform.RotateAround ( Vector3.down, x );
            }
            else
            {
                oneDrag = false;
                firstTouch = false;
                ray.SetActive ( false );
                spritePressed.SetActive ( false );
               
                gameObject.GetComponent<Rigidbody> ( ).AddForce ( transform.forward * 20, ForceMode.Impulse );
            }
        }
    }

    private void OnCollisionEnter ( Collision other )
    {
        if (other.gameObject.layer == 8|| other.gameObject.layer == 9)
        {       
        playerAnimation.SetTrigger ( "Disolve" );
        }

        
    }
    
    

    public void DisappirPlayer ( )
    {
        if (Dominos == GameManager.Instance.numberOfDomino)
        {
            GameManager.Instance.LoseGame ( );
        }
        gameObject.SetActive ( false );
    }

}
