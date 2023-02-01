using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAttackZone : MonoBehaviour
{
    [SerializeField] private float speedInSec;
    [SerializeField] private float firstScale;
    [SerializeField] private float lastScale;

    private bool isActive = false;

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);   
    }

    public void Launch()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        float newScale = Mathf.Lerp(firstScale, lastScale, Time.deltaTime / speedInSec);
        transform.localScale = new Vector3(newScale, newScale, 1);



        yield return null;
    }

    public bool CanAttack()
    {
        return !isActive;
    }
}
