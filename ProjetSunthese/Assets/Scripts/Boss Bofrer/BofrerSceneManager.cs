using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrerSceneManager : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawnPosition;
    private void Start()
    {
        Player.instance.transform.position = playerSpawnPosition;
    }
}
