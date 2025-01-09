using System.Collections;
using SpatialSys.UnitySDK;
using UnityEngine;

public class GrabObjectSpawner : MonoBehaviour
{
    public SpatialNetworkObject objectPrefab;
    private bool created = false;
    private void Update()
    {
        if (SpatialBridge.networkingService.isConnected && !created)
        {
            StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject()
    {
        created = true;
        Vector3 localPosition = gameObject.transform.position;

        Vector3 spawnPos = localPosition;

        SpawnNetworkObjectRequest request = SpatialBridge.spaceContentService.SpawnNetworkObject(objectPrefab, spawnPos);
        yield return request;

        //if (request.succeeded)
        //{
        //    SpatialNetworkObject networkObject = request.networkObject;
        //}
    }
}
