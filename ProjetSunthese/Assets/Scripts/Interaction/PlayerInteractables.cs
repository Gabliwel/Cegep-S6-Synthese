using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    private Sensor sensor;
    private ISensor<Interactable> interactablesSensor;

    private List<Interactable> closeInteractables;
    private Interactable currentSelected = null;

    private Player player;

    void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        interactablesSensor = sensor.For<Interactable>();
        interactablesSensor.OnSensedObject += OnInteractableSense;
        interactablesSensor.OnUnsensedObject += OnInteractableUnsense;

        player = GetComponent<Player>();
    }

    private void Start()
    {
        closeInteractables = new List<Interactable>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentSelected != null)
        {
            currentSelected.Interact(player);
        }
    }

    private void OnInteractableSense(Interactable interectable)
    {
        if(currentSelected != null)
        {
            currentSelected.ChangeSelectedState(false);
        }

        currentSelected = interectable;
        closeInteractables.Add(interectable);
        interectable.ChangeSelectedState(true);
    }

    private void OnInteractableUnsense(Interactable interectable)
    {
        interectable.ChangeSelectedState(false);
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
        currentSelected.ChangeSelectedState(true);
    }
}
