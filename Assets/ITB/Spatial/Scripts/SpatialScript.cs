using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
public class SpatialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpatialBridge.coreGUIService.DisplayToastMessage("Hello from Unity!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
