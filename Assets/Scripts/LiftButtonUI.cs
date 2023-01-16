using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LiftButtonUI : MonoBehaviour
{
    GameObject liftButtonObject;
    EventTrigger liftButtonTrigger;
    EventTrigger.Entry pointerDown;
    EventTrigger.Entry pointerUp;

    GameObject playerMovementObject;
    PMovement playerMovement;

    public void InitializeLiftButton()
    {
        FindGameObjects();
        GetObjectsComponent();

        InitializeTriggerEntries();
        SetupTriggerEntries();
        AddListenersToTriggerEntries();

        AddTriggerEntriesToTrigger();
    }

    private void FindGameObjects()
    {
        playerMovementObject = GameObject.FindGameObjectWithTag("PlayerMovement");
        liftButtonObject = GameObject.FindGameObjectWithTag("LiftButton");
    }

    private void GetObjectsComponent()
    {
        playerMovement = playerMovementObject.GetComponent<PMovement>();
        liftButtonTrigger = liftButtonObject.GetComponent<EventTrigger>();
    }

    private void InitializeTriggerEntries()
    {
        pointerDown = new EventTrigger.Entry();
        pointerUp = new EventTrigger.Entry();
    }

    private void SetupTriggerEntries()
    {
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerUp.eventID = EventTriggerType.PointerUp;
    }

    private void AddListenersToTriggerEntries()
    {
        pointerDown.callback.AddListener((data) => { playerMovement.PointerDown(); });
        pointerUp.callback.AddListener((data) => { playerMovement.PointerUp(); });
    }

    private void AddTriggerEntriesToTrigger()
    {
        liftButtonTrigger.triggers.Add(pointerDown);
        liftButtonTrigger.triggers.Add(pointerUp);
    }
}
