using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject Explosion;
    public GameObject fitil;
    public float power = 5f;
    public float radius= 5.0f;
    public float upForce = 1.0f;
    bool firstExposion=true;
    // Start is called before the first frame update
    private void OnTriggerEnter ( Collider other )
    {
        if (firstExposion)
        {
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.gameObject.layer == 10)
            {
                Bomb.SetActive ( false );
                Explosion.SetActive ( true );
                fitil.SetActive ( false );
                Collider[] colliders = Physics.OverlapSphere ( gameObject.transform.position, radius );
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody> ( );
                    if (rb != null)
                    {
                        rb.AddExplosionForce ( power, gameObject.transform.position, radius, upForce, ForceMode.Impulse );
                    }
                }
            }
            firstExposion = false;
        }
    }
}
