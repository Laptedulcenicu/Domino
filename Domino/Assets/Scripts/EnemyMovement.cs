using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool loop;
    public float startWaitTime;
    public float speed;
    public Transform[] moveSpots; 

    private float waitTime;
    private int spot;

    bool changeDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime; //Set wait time
        spot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[spot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[spot].position) < 0.2f)
        {
            if (loop == false)
            {
                if (waitTime <= 0)
                {
                    if (changeDirection == false)
                    {
                        spot++;
                        if (spot == moveSpots.Length)
                        {
                            changeDirection = true;
                        }
                    }

                    if (changeDirection == true)
                    {
                        spot--;
                        if (spot == 0)
                        {
                            changeDirection = false;
                        }
                    }

                    waitTime = startWaitTime;
                }

                waitTime -= Time.deltaTime;
            }
            else
            {
                if (waitTime <= 0)
                {
                    if (changeDirection == false)
                    {
                        spot++;
                        if (spot == moveSpots.Length)
                        {
                            changeDirection = true;
                        }
                    }

                    if (changeDirection == true)
                    {
                        spot = 0;
                        if (spot == 0)
                        {
                            changeDirection = false;
                        }

                    }

                    waitTime = startWaitTime;
                }
            }
        }
    }
}
