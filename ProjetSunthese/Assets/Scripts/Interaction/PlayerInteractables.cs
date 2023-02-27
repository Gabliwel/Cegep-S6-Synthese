using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    [SerializeField] private Sensor sensor;
    private ISensor<Interactable> interactablesSensor;

    [SerializeField] private List<Interactable> closeInteractables;
    private Interactable currentSelected = null;

    private Player player;
    private DescriptionBox descBox;

    private bool textsNotLinked = true;

    private void Start()
    {
        interactablesSensor = sensor.For<Interactable>();
        interactablesSensor.OnSensedObject += OnInteractableSense;
        interactablesSensor.OnUnsensedObject += OnInteractableUnsense;
        player = GetComponent<Player>();
        closeInteractables = new List<Interactable>();
    }

    private void Update()
    {
        Link();


        if (Input.GetKeyDown(KeyCode.E) && currentSelected != null)
        {
            currentSelected.Interact(player);
        }
    }

    private void Link()
    {
        if (!textsNotLinked) return;
        textsNotLinked = false;
        if (GameManager.instance.NeedLinkWithActivePlayer()) descBox = GameObject.FindGameObjectWithTag("DescriptionBox").GetComponent<DescriptionBox>();
    }

    private void OnLevelWasLoaded(int level)
    {
        textsNotLinked = true;
    }

    private void OnInteractableSense(Interactable interectable)
    {
        Debug.Log("Sense");
        if(currentSelected != null)
        {
            currentSelected.ChangeSelectedState(false, descBox);
        }

        currentSelected = interectable;
        closeInteractables.Add(interectable);
        interectable.ChangeSelectedState(true, descBox);
    }

    private void OnInteractableUnsense(Interactable interectable)
    {
        interectable.ChangeSelectedState(false, descBox);
        closeInteractables.Remove(interectable);
        currentSelected = null;

        SearchNewClosestInteractable();
    }

    private void SearchNewClosestInteractable()
    {
        if (closeInteractables.Count <= 0) return;

        Interactable closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Interactable potentialInteract in closeInteractables)
        {
            Vector3 direction = potentialInteract.gameObject.transform.position - currentPosition;
            float sqr = direction.sqrMagnitude;
            if (sqr < closestDistance)
            {
                closestDistance = sqr;
                closest = potentialInteract;
            }
        }
        currentSelected = closest;
        currentSelected.ChangeSelectedState(true, descBox);
    }

    public void SearchNewInterac(Interactable interectable)
    {
        closeInteractables.Remove(interectable);
        currentSelected = null;
        descBox.Close();
        SearchNewClosestInteractable();
    }
}
