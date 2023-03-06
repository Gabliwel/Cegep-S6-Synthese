using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbersManager : MonoBehaviour
{
    public static DamageNumbersManager instance;
    [SerializeField] private DamageNumber damageNumberPrefab;
    [SerializeField] private int damageNumbersNb;
    [SerializeField] private float minRandomPosition;
    [SerializeField] private float maxRandomPosition;
    private Vector3 textOffset;
    private DamageNumber[] damageNumbers;
    private Color defaultColor = Color.white;
    private Color playerHarmedColor = Color.red;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        damageNumbers = new DamageNumber[damageNumbersNb];
        for (int i = 0; i < damageNumbersNb; i++)
        {
            damageNumbers[i] = Instantiate(damageNumberPrefab, transform).GetComponent<DamageNumber>();
            damageNumbers[i].gameObject.SetActive(false);
        }
    }

    public void CallText(float ammount, Vector3 position, bool isPlayer)
    {
        if (isPlayer)
            CallText(ammount, position, playerHarmedColor);
        else
            CallText(ammount, position, defaultColor);
    }

    public void CallText(float ammount, Vector3 position, Color color)
    {
        DamageNumber text = GetAvailableDamageNumber();
        RandomizeOffset();
        text.transform.position = position + textOffset;
        text.gameObject.SetActive(true);
        ammount = Mathf.Floor(ammount);
        text.SetText(ammount.ToString(), color);
    }

    private DamageNumber GetAvailableDamageNumber()
    {
        foreach (DamageNumber text in damageNumbers)
        {
            if (!text.gameObject.activeSelf)
                return text;
        }

        damageNumbers[damageNumbers.Length - 1].gameObject.SetActive(false);
        return damageNumbers[damageNumbers.Length - 1];
    }

    private void RandomizeOffset()
    {
        textOffset.x = Random.Range(minRandomPosition, maxRandomPosition);
        textOffset.y = Random.Range(minRandomPosition, maxRandomPosition);

    }
}
