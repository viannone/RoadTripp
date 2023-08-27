using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps;
using Niantic.Lightship.Maps.SampleAssets.Player;
using Niantic.Lightship.Maps.Core.Coordinates;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

/*
 * The way this works is that the character stays in the center of the screen and the world moves around the character.
 */

namespace RoadTripp
{
    public class RT_PlayerController : PlayerLocationController
    {
        public GameObject playerPrefab;

        public override void Update()
        {
            // Update the map view position based on where our player is.
            // This will actually be last frame's position, but the map
            // update needs to happen first as the player is positioned
            // on the map relative to the offset to the tile parent node.
            UpdateMapViewPosition();

            // Maintain the player's position on the map, and interpolate
            // to new coordinates as they come in.  Interpolate player's
            // map position without the camera offset, so that camera
            // movements don't result in lerps.  Jump rather than
            // interpolate if the coordinates are really far.

            var movementVector = _targetMapPosition - _currentMapPosition;
            var movementDistance = movementVector.magnitude;

            switch (movementDistance)
            {
                case > TeleportThreshold:
                    _currentMapPosition = _targetMapPosition;
                    break;

                case > WalkThreshold:
                    {
                        // If the player is not stationary,
                        // rotate to face their movement vector
                        var forward = movementVector.normalized;
                        var rotation = Quaternion.LookRotation(forward, Vector3.up);
                        transform.rotation = rotation;
                        break;
                    }
            }

            _currentMapPosition = Vector3.Lerp(
                _currentMapPosition,
                _targetMapPosition,
                Time.deltaTime);

            if (playerPrefab)
            {
                playerPrefab.transform.position = _targetMapPosition;
            }
        }
    }
}