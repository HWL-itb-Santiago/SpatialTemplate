using System.Collections;
using SpatialSys.UnitySDK;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public SpatialNetworkObject cubePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && SpatialBridge.networkingService.isConnected)
        {
            StartCoroutine(SpawnCube());
        }
    }

    private IEnumerator SpawnCube()
    {
        Vector3 localActorPosition = SpatialBridge.actorService.localActor.avatar.position;
        Vector3 lookDirection = SpatialBridge.cameraService.forward;

        Vector3 spawnPos = localActorPosition + Vector3.up * 1.6f + lookDirection * 5.5f;
        Quaternion spawnRot = Random.rotation;

        SpawnNetworkObjectRequest request = SpatialBridge.spaceContentService.SpawnNetworkObject(cubePrefab, spawnPos, spawnRot);
        yield return request;

        //if (request.succeeded)
        //{
        //    SpatialNetworkObject networkObject = request.networkObject;
        //}
    }
}