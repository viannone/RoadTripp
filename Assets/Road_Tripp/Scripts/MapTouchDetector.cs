using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadTripp;

public class MapTouchDetector : MonoBehaviour
{
    public Camera _mapCamera;

    private void Update()
    {
        var touchPosition = Vector3.zero;
        bool touchDetected = false;

        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                touchPosition = Input.touches[0].position;
                touchDetected = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            touchDetected = true;
        }

        if (touchDetected)
        {
            //if (_placingStructure)
            //{
            //    PlaceStructure(touchPosition);
            //}
            //else //don't check if there's already a touch
            //{
                CheckInteraction(touchPosition);
            //}
        }
    }

    private void CheckInteraction(Vector3 touchPosition)
    {
        var touchRay = _mapCamera.ScreenPointToRay(touchPosition);

        // raycast into scene and see if we hit a map feature
        if (!Physics.Raycast(touchRay, out var hitInfo))
        {
            return;
        }

        // check if the collider we hit is a feature
        var interactableComponent = hitInfo.collider.GetComponent<RT_InteractableObject>();
        if(interactableComponent == null)
        {
            Debug.LogError("Only RT_Interactables should be on the map.");
        }
        else
        {
            Debug.Log("TOUCHED " + interactableComponent.gameObject.name);
            interactableComponent.OnClickBehavior();
        }
        return;
        //if (hitResourceItem == null)
        //{
        //    return;
        //}

        //// check if this resource has any units available to consume
        //if (!hitResourceItem.ResourcesAvailable)
        //{
        //    return;
        //}

        //// award the player resources for finding this map resource
        //int amount = hitResourceItem.GainResources();

        //// spawn an animated floating text to show resources being gained
        //var floatingTextPosition = hitInfo.point + Vector3.up * 20.0f;
        //var forward = floatingTextPosition - _mapCamera.transform.position;
        //var rotation = Quaternion.LookRotation(forward, Vector3.up);
        //Do stuff here
    }
}
