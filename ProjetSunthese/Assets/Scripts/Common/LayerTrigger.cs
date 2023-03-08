using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public string layer;
    public string sortingLayer;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            other.GetComponent<Enemy>().ChangeLayer(LayerMask.NameToLayer(layer));

        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().ChangeLayer(layer, sortingLayer);
    }
}