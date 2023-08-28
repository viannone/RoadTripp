using Niantic.Lightship.Maps.MapLayers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps;
using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.Coordinates;

namespace RoadTripp
{
    public class MapIconPlacer : MonoBehaviour
    {
        [SerializeField] protected LayerGameObjectPlacement _objectPrefab;
        [SerializeField] protected LightshipMapView _lmv;
        public bool AddRandomAroundPlayer;
        public RT_PlayerController DebugPlayerController;
        public float spawnRadius = .0000f;
        public int spawnCount = 5;
        public SerializableLatLng overrideLatLng;

        const int debugPOIS = 5;

        public virtual IEnumerator Start()
        {
            if(!AddRandomAroundPlayer)
            {
                yield break;
            }
            yield return new WaitForSeconds(3.0f);
            for(int i = 0; i < spawnCount; i++)
            {
                var posLatLng = new LatLng(overrideLatLng.Latitude + Random.RandomRange(-spawnRadius, spawnRadius), overrideLatLng.Longitude + Random.RandomRange(-spawnRadius, spawnRadius));
                _objectPrefab.PlaceInstance(_lmv.LatLngToScene(posLatLng));
            }
            //foreach(var llg in overrideLatLng)
            //{
            //    if(!_objectPrefab)
            //    {
            //        Debug.LogError("BRUH!");
            //        Debug.Break();
            //    }
            //    if(!_lmv)
            //    {
            //        Debug.LogError("No LMV");
            //        Debug.Break();
            //    }
            //    _objectPrefab.PlaceInstance(_lmv.LatLngToScene(llg));
            //}
            //yield return new WaitForSeconds(5.0f);
            //{
            //    for(int i = 0; i < debugPOIS; ++i)
            //    {
            //        var currentLatLong = DebugPlayerController._currentPlayerLatLong;
            //        var latLng = new LatLng(currentLatLong.Latitude + (double)UnityEngine.Random.Range(-spawnRadius, spawnRadius), currentLatLong.Longitude + (double)Random.Range(-spawnRadius, spawnRadius));
            //        _objectPrefab.PlaceInstance(_lmv.LatLngToScene(latLng));
            //    }

            //}

        }
    }

}