using Niantic.Lightship.Maps.MapLayers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconPlacer : MonoBehaviour
{
    [SerializeField] private LayerGameObjectPlacement _cubeGOP;
    public Vector2 LatLong;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(5.0f);
        _cubeGOP.PlaceInstance(LatLong);
    }
}

