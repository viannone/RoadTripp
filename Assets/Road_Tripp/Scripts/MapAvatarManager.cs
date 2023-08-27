using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadTripp
{
    public class MapAvatarManager : MonoBehaviour
    {
        IEnumerator Start()
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
            {
                Debug.LogError("No Location Services");
                BarkSingleton.BarkText("Location Services Not Enabled");
                yield break;
            }

            // Starts the location service.
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait < 1)
            {
                Debug.LogError("Timed out");
                BarkSingleton.BarkText("Location Services Timed Out");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.LogError("Unable to determine device location");
                BarkSingleton.BarkText("Unable to determine device location");
                yield break;
            }
            else
            {
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                BarkSingleton.BarkText("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }
        }

        private void OnDestroy()
        {
            //No need to query location if we're off the map screen
            Input.location.Stop();
        }
    }
}
