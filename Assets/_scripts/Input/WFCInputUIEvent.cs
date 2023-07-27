using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WFCInputUIEvent : MonoBehaviour //handles input from buttons to start and reset tiles acts as link between UI interaction and wfc system
{
    bool isWaveFunctionCollapsed =true;
    public event Action OnStartBtnPressed; //turn off buttons, starwfc
    [SerializeField] Canvas uICanvas;
    [SerializeField] GameObject tileMap;
    [SerializeField] TextMeshProUGUI textMeshProModeChangeButton;
    WFCTrigger wFCTrigger;

    bool isCurrentlyNormalMode=true;


    public void ResetTiles()
    {
        if (isWaveFunctionCollapsed)
        {
            isWaveFunctionCollapsed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
            
    }

    public void StartWFC()
    {
        wFCTrigger = tileMap.GetComponent<WFCTrigger>();
        if (wFCTrigger == null) Debug.LogError("wfc trigger not found in tiles");

        wFCTrigger.OnWaveFunctionollapsed += waveFunctionIsCollapsed;
        OnStartBtnPressed += wFCTrigger.StartWFC;

        if (isWaveFunctionCollapsed)
        {
            uICanvas.gameObject.SetActive(false);
            OnStartBtnPressed?.Invoke();            
        }
        
    }

    public void ModeChange()
    {
        tileMap.GetComponent<WFCTypeHandler>().ModeChange(isCurrentlyNormalMode);
        if (isCurrentlyNormalMode)
        {
            textMeshProModeChangeButton.text = "Bézier Mode";
            isCurrentlyNormalMode = false;
        }
        else
        {
            textMeshProModeChangeButton.text = "Normal Mode";
            isCurrentlyNormalMode = true;
        }
        wFCTrigger = tileMap.GetComponent<WFCTrigger>();
    }

    void waveFunctionIsCollapsed() //turn on the buttons, and mark the bool
    {
        isWaveFunctionCollapsed = true;
        uICanvas.gameObject.SetActive(true);
    }

}
