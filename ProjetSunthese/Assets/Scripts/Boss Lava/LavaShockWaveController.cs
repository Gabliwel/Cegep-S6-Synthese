using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveController : MonoBehaviour
{
    Vector3 originalScale;
    // Start is called before the first frame update
    void Awake()
    {
        originalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += transform.localScale * Time.deltaTime/3;
    }

    private void OnDisable()
    {
        ShrinkBackToOriginal();
    }
    private void ShrinkBackToOriginal()
    {
        transform.localScale = originalScale;
    }
}
