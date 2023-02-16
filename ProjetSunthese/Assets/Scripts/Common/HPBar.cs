using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Transform green;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
    public void UpdateHp(float amount, float maxHp = 100)
    {
        green.transform.localScale = new Vector3(amount / maxHp, green.localScale.y, green.localScale.z);
    }
}
