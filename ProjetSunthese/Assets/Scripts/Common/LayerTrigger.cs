using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public string layer;
    public string sortingLayer;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;
        other.GetComponent<Player>().ChangeLayer(layer, sortingLayer);
    }
}