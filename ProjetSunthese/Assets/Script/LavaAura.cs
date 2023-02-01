using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAura : MonoBehaviour
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
        CalculateAuraTiming();
    }

    public void CalculateAuraTiming()
    {
        Debug.Log(timeElapsed);
        if (timeElapsed > 10)
        {
            DespawnAura();
            timeElapsed = 0;
        }
    }

    public void DespawnAura()
    {
        gameObject.SetActive(false);
    }
}
