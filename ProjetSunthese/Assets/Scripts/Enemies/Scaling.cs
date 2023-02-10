using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] int currentScaling;

    public static Scaling instance;
    void Start()
    {
        if(currentScaling <= 0)
        {
            currentScaling = 1;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public int SendScaling()
    {
        return currentScaling;
    }

    public void ScalingIncrease()
    {
        currentScaling++;
    }
}
