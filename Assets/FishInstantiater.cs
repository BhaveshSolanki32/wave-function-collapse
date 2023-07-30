using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInstantiater : MonoBehaviour
{
    GameObject tileMap;


    private void Awake()
    {
        tileMap = transform.root.gameObject;
        FindObjectOfType<WFCInputUIEvent>().OnStartBtnPressed += wFCStartAssign;
    }

    private void wFCStartAssign()
    {
        tileMap.GetComponent<WFCTrigger>().OnWaveFunctionCollapsed += instantiateFish;
    }

    void instantiateFish()
    {
        transform.rotation = Quaternion.Euler(0,0, Random.Range(0, 360));
        gameObject.AddComponent<FishRaycast>();
        gameObject.AddComponent<FishMove>();
       

    }

    private void OnDestroy()
    {
        tileMap.GetComponent<WFCTrigger>().OnWaveFunctionCollapsed -= this.instantiateFish;
    }
}
