using Niantic.Lightship.Maps.MapLayers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps;
using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.Coordinates;
using Niantic.Lightship.Maps.SampleAssets.Player;

namespace RoadTripp
{
    public class MapObjectPlacer : MonoBehaviour
    {
        [SerializeField] protected LayerGameObjectPlacement _layerGameObjectPlacement;
        [SerializeField] protected LightshipMapView _lmv;
        public PlayerLocationController DebugPlayerController;
        public float spawnRadius = .0000f;
        public int spawnCount = 5;

        public IEnumerator Start()
        {
            yield return new WaitForSeconds(5.0f); //Testing
            for(int i = 0; i < spawnCount; i++)
            {
                var posVector = DebugPlayerController.gameObject.transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0.0f, Random.Range(-spawnRadius, spawnRadius));
                _layerGameObjectPlacement.PlaceInstance(_lmv.SceneToLatLng(posVector));
            }
        }
    }

}