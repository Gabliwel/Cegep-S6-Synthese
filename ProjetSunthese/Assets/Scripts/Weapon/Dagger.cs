using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Melee
{
    bool shouldFlip;
    int savedRotationOffset;
    protected override void Start()
    {
        base.Start();
        savedRotationOffset = ROTATION_OFFSET;
    }
    public void Slash()
    {
        flipped = (Mathf.Abs(Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg)) < 90;
        StartCoroutine(Attack());
    }

    protected override void Update()
    {
        base.Update();
        ManageFlip();
    }

    void ManageFlip()
    {
        if (orbit)
        {
            shouldFlip = Mathf.Abs(Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg) > 90;
            if (shouldFlip)
            {
                ROTATION_OFFSET = -savedRotationOffset;
            }
            else ROTATION_OFFSET = savedRotationOffset;
        }
    }

    public void StartOrbit()
    {
        orbit = true;
    }

    public void StopOrbit()
    {
        orbit = false;
    }
    //prevent left click from attacking
    public override void StartAttack()
    {
        flipped = (Mathf.Abs(Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg)) < 90;
    }

    public bool GetShouldFlip()
    {
        return shouldFlip;
    }

    public void SetLayer(string layer)
    {
        GetComponent<SpriteRenderer>().sortingLayerName = layer;
        gameObject.layer = gameObject.layer;

    }
}
