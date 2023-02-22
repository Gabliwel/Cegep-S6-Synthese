using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class LootManager : MonoBehaviour
{
    [Header("Rarety max rate (order: commun - rare - epic - legendary)")]
    [SerializeField] private float maxCommun = 30;
    [SerializeField] private float maxRare = 60;
    [SerializeField] private float maxEpic = 85;

    [Header("Items")]
    [SerializeField] private ItemWithBaseRarity[] possibleItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
