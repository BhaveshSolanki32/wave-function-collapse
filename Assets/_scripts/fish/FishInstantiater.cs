using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInstantiater : MonoBehaviour
{
    GameObject _tileMap;


    private void Awake()
    {
        _tileMap = transform.root.gameObject;
        FindObjectOfType<WFCInputUIEvent>().OnStartBtnPressed += wFCStartAssign;
    }
    private void OnDestroy()
    {
        _tileMap.GetComponent<WFCTrigger>().OnWaveFunctionCollapsed -= this.instantiateFish;
    }

    private void wFCStartAssign()
    {
        _tileMap.GetComponent<WFCTrigger>().OnWaveFunctionCollapsed += instantiateFish;
    }

    void instantiateFish()
    {
        transform.rotation = Quaternion.Euler(0,0, Random.Range(0, 360));
        gameObject.AddComponent<FishRaycast>();
        gameObject.AddComponent<FishMove>();
    }

    
}
