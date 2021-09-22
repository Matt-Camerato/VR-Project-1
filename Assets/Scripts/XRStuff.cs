using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRStuff : MonoBehaviour
{
    [SerializeField] private OrbSpawner orbspawner;

    private bool holdingPinkRing = false;
    private bool holdingBlueRing = false;

    public void ObjectAttached(SelectEnterEventArgs arg0)
    {
        arg0.interactor.GetComponent<XRInteractorLineVisual>().enabled = false;

        if(arg0.interactable.name == "PinkRing")
        {
            holdingPinkRing = true;
        }
        else if(arg0.interactable.name == "BlueRing")
        {
            holdingBlueRing = true;
        }
    }

    public void ObjectDetached(SelectExitEventArgs arg0)
    {
        arg0.interactor.GetComponent<XRInteractorLineVisual>().enabled = true;

        if (arg0.interactable.name == "PinkRing")
        {
            holdingPinkRing = false;
        }
        else if (arg0.interactable.name == "BlueRing")
        {
            holdingBlueRing = false;
        }
    }

    private float replayCountdown = 5;

    private void Update()
    {
        if(holdingBlueRing && holdingPinkRing) { Debug.Log("Holding both rings"); }

        if (!orbspawner.gameIsRunning)
        {
            if (holdingBlueRing && holdingPinkRing)
            {
                replayCountdown -= Time.deltaTime;
                if(replayCountdown <= 0)
                {
                    orbspawner.PlayAgain();
                }
            }
            else
            {
                replayCountdown = 5;
            }
        }
        else
        {
            replayCountdown = 5;
        }
    }
}

