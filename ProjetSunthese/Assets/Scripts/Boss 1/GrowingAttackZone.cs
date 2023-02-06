using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAttackZone : MonoBehaviour
{
    [Header("Base stats")]
    [SerializeField] private float speedInSec;
    [SerializeField] private Vector3 smallScale;
    [SerializeField] private Vector3 bigScale;

    [Header("Particularity")]
    [SerializeField] private bool reverseOrder;
    [SerializeField] private bool returnToInitial;
    [SerializeField] private float waitSecBeforeReturn;

    void Start()
    {
        transform.localScale = smallScale;
    }

    public void Launch()
    {
        if(!reverseOrder) StartCoroutine(Attack(smallScale, bigScale));
        else StartCoroutine(Attack(bigScale, smallScale));
    }

    private IEnumerator Attack(Vector3 initialScale, Vector3 endScale)
    {
        transform.localScale = initialScale;

        bool doneHalf = false;
        float timeScale = 1;
        if (returnToInitial) timeScale = 2;

        for (float time = 0; time < speedInSec * timeScale; time += Time.deltaTime)
        {
            // For the wait with "Return to Initial"
            if (!doneHalf && returnToInitial && time > speedInSec) {
                transform.localScale = endScale;
                doneHalf = true;
                yield return new WaitForSeconds(waitSecBeforeReturn);
            }

            // Scalling of the zone and mask in the parent object
            if (returnToInitial) transform.localScale = Vector3.Lerp(initialScale, endScale, (Mathf.PingPong(time, speedInSec) / speedInSec));
            else transform.localScale = Vector3.Lerp(initialScale, endScale, time / speedInSec);
            yield return null;
        }

        transform.localScale = smallScale;
    }
}
