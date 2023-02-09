using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public string layer;
    public string sortingLayer;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;

        other.gameObject.layer = LayerMask.NameToLayer(layer);
        other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;

        //à changer, présent en ce moment pour test rapide (utiliser prefab de warrior)
        other.gameObject.transform.GetChild(0).GetComponent<PlayerLight>().UpdateLightUsage(sortingLayer);
        other.gameObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;

        /*SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.sortingLayerName = sortingLayer;
        }*/
    }
}