using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager: MonoBehaviour
{
    [SerializeField] private List<GameObject> dropList;
    private GameObject currentDrop;
    private int randomItem;
    private Vector2 currentPlayerPosition;
    public static DropManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Toujours appeler SelectRandomItem avant DropItem, fait dans le but de pouvoir mettre une action entre les deux appels
    /// </summary>
    /// <param name="enemyPosition"></param>
    /// <param name="playerPosition"></param>
    public void DropItem(Vector2 enemyPosition, Vector2 playerPosition)
    {
        if(dropList.Count > 0)
        {
            currentDrop.transform.position = enemyPosition;
            currentDrop.GetComponent<DropMover>().GetVectorDirection(DefineDirectionOfVector(enemyPosition, playerPosition));
            dropList.RemoveAt(randomItem);
            currentDrop.SetActive(true);
        }
    }

    public Vector2 DefineDirectionOfVector(Vector2 enemyPosition, Vector2 playerPosition)
    {
        return enemyPosition - playerPosition;
    }

    public void SelectRandomItem()
    {
        if(dropList.Count > 0)
        {
            randomItem = Random.Range(0, dropList.Count - 1);
            currentDrop = dropList[randomItem];
        }
    }
}
