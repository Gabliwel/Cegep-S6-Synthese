using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMover : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed = 5;
    private float timeElapsed = 0;

    Vector2 dir;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(isActiveAndEnabled)
        {
            if(timeElapsed >= 0.25)
            {
                if(speed > 0)
                {
                    rigidBody.velocity = speed * dir;
                    speed--;
                    timeElapsed = 0;
                }
                else
                {
                    rigidBody.velocity = new Vector2(0, 0);
                }

            }
        }
    }

    private void OnEnable()
    {
        rigidBody.velocity = dir;
    }

    public void GetVectorDirection(Vector2 vec)
    {
        dir = vec;
    }
}
