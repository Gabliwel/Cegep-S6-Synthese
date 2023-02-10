using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerHomingBolt : Projectile
{
    [SerializeField] private float rotationSpeed;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.right;
        AdjustRotation();
        if (ttlTimer > 0)
            ttlTimer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    private void AdjustRotation()
    {
        Vector2 targetDirection = player.transform.position - transform.position;

        targetDirection.Normalize();

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

    }
}
