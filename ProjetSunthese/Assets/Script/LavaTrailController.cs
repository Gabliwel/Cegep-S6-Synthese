using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrailController : MonoBehaviour
{
    float timeElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 10)
        {
            gameObject.SetActive(false);
            timeElapsed = 0;
        }
        
    }

}
