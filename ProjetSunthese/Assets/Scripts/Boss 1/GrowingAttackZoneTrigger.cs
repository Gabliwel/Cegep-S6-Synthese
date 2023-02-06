using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAttackZoneTrigger : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("in!!!!!!!!");
            Vector2 difference = (collision.transform.position - transform.position).normalized;
            Debug.Log(difference);
            collision.GetComponent<EasyPlayerMovement>().AddKnockBack(difference, knockBackForce);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 difference = (transform.position - collision.transform.position).normalized;
            Debug.Log(difference);
            collision.GetComponent<EasyPlayerMovement>().AddKnockBack(difference, knockBackForce);
        }
    }
}
