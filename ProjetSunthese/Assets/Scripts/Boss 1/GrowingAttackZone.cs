using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAttackZone : BossAttack
{
    [Header("Link")]
    [SerializeField] private Sensor sensor;

    [Header("Base stats")]
    [SerializeField] private float knockBackForce = 2f;
    [SerializeField] private float speedInSec;
    [SerializeField] private Vector3 smallScale;
    [SerializeField] private Vector3 bigScale;

    [Header("Particularity")]
    [SerializeField] private bool reverseOrder;
    [SerializeField] private bool returnToInitial;
    [SerializeField] private float waitSecBeforeReturn;

    private ISensor<Player> playerSensor;

    private void Awake()
    {
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        SetChildState(false);
    }

    void Start()
    {
        transform.localScale = smallScale;
        isAvailable = true;
    }

    public override void Launch()
    {
        isAvailable = false;
        SetChildState(true);
        if (!reverseOrder) StartCoroutine(Attack(smallScale, bigScale));
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


        SetChildState(false);
        transform.localScale = smallScale;
        isAvailable = true;
    }

    private void OnPlayerSense(Player player)
    {
        Vector2 difference = (player.gameObject.transform.position - transform.position).normalized;
        player.KnockBack(difference, knockBackForce);
    }

    private void OnPlayerUnsense(Player player)
    {
        Vector2 difference = (transform.position - player.gameObject.transform.position).normalized;
        player.KnockBack(difference, knockBackForce);
    }

    private void SetChildState(bool state)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }
}
