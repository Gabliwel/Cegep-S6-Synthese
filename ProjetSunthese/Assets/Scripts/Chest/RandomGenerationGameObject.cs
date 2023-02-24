using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.AI;
using Billy.VectorAndLayer;


namespace Billy.VectorAndLayer
{
    public class PositionWithLayer
    {
        private string layer;
        private Vector3 position;
        public PositionWithLayer(string layer, Vector3 position)
        {
            this.layer = layer;
            this.position = position;
        }

        public string Layer
        {
            get { return layer; }
        }

        public Vector3 Position
        {
            get { return position; }
        }
    }
}

public class RandomGenerationGameObject : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private List<Vector3> posibleSpawnLayer1;
    [SerializeField] private List<Vector3> posibleSpawnLayer2;
    [SerializeField] private List<Vector3> posibleSpawnLayer3;
    [SerializeField] private int mapMinChest;
    [SerializeField] private bool usingNavmesh;
    private int chanceOfChest = 90;
    private bool stopSpawn = false;
    private List<PositionWithLayer> globalList;

    private ChestDropManager chestDropManager;

    private void Awake()
    {
        chestDropManager = gameObject.GetComponent<ChestDropManager>();
        globalList = new List<PositionWithLayer>();
        AddToGlobalList(posibleSpawnLayer1, "Layer 1");
        AddToGlobalList(posibleSpawnLayer2, "Layer 2");
        AddToGlobalList(posibleSpawnLayer3, "Layer 3");
    }

    private void AddToGlobalList(List<Vector3> list, string layer)
    {
        foreach(Vector3 v in list)
        {
            globalList.Add(new PositionWithLayer(layer, v));
        }
    }

    void Start()
    {
        for (int i = 0; i < mapMinChest; i++)
        {
            RandomSpawn();
        }

        while (!stopSpawn)
        {
            if (globalList.Count > 0)
            {
                int spawnRolledNumber = Random.Range(0, 100);

                if (spawnRolledNumber < chanceOfChest)
                {
                    RandomSpawn();
                    chanceOfChest -= 15;
                    if (chanceOfChest < 15)
                    {
                        chanceOfChest = 15;
                    }
                }
                else
                {
                    stopSpawn = true;
                }
            }
            else
            {
                stopSpawn = true;
            }
        }
    }

    private void RandomSpawn()
    {
        int chosen = Random.Range(0, globalList.Count - 1);

        Vector3 spawnChosen = globalList[chosen].Position;

        ChestGameObject chestClone = Instantiate(chest).GetComponent<ChestGameObject>();
        
        chestClone.SetChest(chestDropManager, globalList[chosen].Layer);
        Debug.Log(chestDropManager);
        if(usingNavmesh) chestClone.gameObject.AddComponent<NavMeshObstacle>();
        chestClone.transform.position = spawnChosen;
        chestClone.gameObject.SetActive(true);

        globalList.Remove(globalList[chosen]);
    }
}
