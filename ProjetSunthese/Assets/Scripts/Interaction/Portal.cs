using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    private bool hasInteracted = false;
    private float duration = 3;
    private float adddedAngle = 3;
    private Animator anim;
    private Vector3 safePostion = new Vector3(999, 999, 0);

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact(Player player)
    {
        if (hasInteracted) return;
        hasInteracted = true;

        StartCoroutine(PortalAction(player));
    }

    private IEnumerator PortalAction(Player player)
    {
        Camera.main.gameObject.GetComponent<FollowPlayer>().SetTarget(transform);
        player.transform.position = transform.position;

        player.BlocAttack(true);
        player.BlocMovement(true);

        float counter = 0;

        Vector3 startScaleSize = player.transform.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            player.transform.localEulerAngles = new Vector3(0, 0, player.transform.localEulerAngles.z + adddedAngle);
            player.transform.localScale = Vector3.Lerp(startScaleSize, Vector3.zero, counter / duration);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        player.transform.position = safePostion;
        player.transform.localScale = startScaleSize;
        player.transform.localEulerAngles = Vector3.zero;
        anim.SetTrigger("Close");

        yield return new WaitForSeconds(1f);

        player.BlocAttack(false);
        player.BlocMovement(false);
        GameManager.instance.SetNextLevel();
    }
}
