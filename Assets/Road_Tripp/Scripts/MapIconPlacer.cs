using Niantic.Lightship.Maps.MapLayers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps;
using Niantic.Lightship.Maps.Core.Coordinates;

namespace RoadTripp
{
    public class MapIconPlacer : MonoBehaviour
    {
        [SerializeField] protected LayerGameObjectPlacement _objectPrefab;
        [SerializeField] protected LightshipMapView _lmv;
        public bool AddRandomAroundPlayer;
        public RT_PlayerController DebugPlayerController;
        public float spawnRadius = .0000f;

        const int debugPOIS = 5;

        public virtual IEnumerator Start()
        {
            if(!AddRandomAroundPlayer)
            {
                yield break;
            }
            yield return new WaitForSeconds(5.0f);
            {
                for(int i = 0; i < debugPOIS; ++i)
                {
                    var currentLatLong = DebugPlayerController._currentPlayerLatLong;
                    var latLng = new LatLng(currentLatLong.Latitude + (double)UnityEngine.Random.Range(-spawnRadius, spawnRadius), currentLatLong.Longitude + (double)Random.Range(-spawnRadius, spawnRadius));
                    _objectPrefab.PlaceInstance(_lmv.LatLngToScene(latLng));
                }

            }

        }
    }

}