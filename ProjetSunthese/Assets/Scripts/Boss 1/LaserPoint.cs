using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        DeactivateSensor();
    }

    public void StartMovement(Vector3 pivotPoint)
    {
        StartCoroutine(Rotate(pivotPoint));
    }

    private IEnumerator Rotate(Vector3 pivotPoint)
    {
        ActivateSensor();
        while (true)
        {
            transform.RotateAround(pivotPoint, new Vector3(0, 0, 1), Time.deltaTime * 50);
            yield return null;
        }
    }

    private void OnPlayerSense(Player otherObject)
    {
        Debug.Log("in");
    }

    private void OnPlayerUnsense(Player otherObject)
    {
        Debug.Log("out");
    }

    void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }
}
