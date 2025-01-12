using System.Collections;
using SpatialSys.UnitySDK;
using UnityEngine;

public class GrabObjectSpawner : MonoBehaviour
{
    public SpatialNetworkObject objectPrefab;
    private bool created = false;
    private void Update()
    {
        if (SpatialBridge.networkingService.isConnected && !created && SpatialBridge.actorService.localActor.avatar.isBodyLoaded)
        {
            created = true;
            StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject()
    {
        Vector3 localPosition = gameObject.transform.position;

        Vector3 spawnPos = localPosition;
        Quaternion spawnRot = gameObject.transform.rotation;
        SpawnNetworkObjectRequest request = SpatialBridge.spaceContentService.SpawnNetworkObject(objectPrefab, spawnPos, spawnRot);
        yield return request;

        //if (request.succeeded)
        //{
        //    SpatialNetworkObject networkObject = request.networkObject;
        //}
    }
}
