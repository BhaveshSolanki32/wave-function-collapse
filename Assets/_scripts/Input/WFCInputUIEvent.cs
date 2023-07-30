using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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


    public void OnDropDownToggle(GameObject _toggle)
    {
        StartCoroutine(etes(_toggle));
        if(transform.localPosition.y !=-59)
        _toggle.transform.parent.localPosition = new(0,-59,0);
        else
        _toggle.transform.parent.localPosition = new(0,73.63214f, 0);
        
        Transform _checkMarkTransfrom = _toggle.transform.GetChild(0).GetChild(0);

        if (_checkMarkTransfrom.gameObject.name != "Checkmark") Debug.LogError("toggle child (checkmark) sibiling index changed", _toggle);
        
        _checkMarkTransfrom.localEulerAngles = new(0, 0, _checkMarkTransfrom.localEulerAngles.z + 180);
    }
    IEnumerator etes(GameObject go)
    {
        while (true)
        {
            print(go.transform.parent.localPosition);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void OnDelaySliderChange(Slider _sldier)
    {
        wFCTrigger.Delay=_sldier.value;
    }


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

        wFCTrigger.OnWaveFunctionCollapsed += waveFunctionIsCollapsed;
        OnStartBtnPressed += wFCTrigger.StartWFC;

        if (isWaveFunctionCollapsed)
        {
            uICanvas.transform.GetChild(0).gameObject.SetActive(false);
            uICanvas.transform.GetChild(1).gameObject.SetActive(true);
            OnStartBtnPressed?.Invoke();            
        }
        
    }

    public void ModeChange()
    {
        tileMap.GetComponent<WFCTypeHandler>().ModeChange(isCurrentlyNormalMode);
        if (isCurrentlyNormalMode)
        {
            textMeshProModeChangeButton.text = "B�zier Mode";
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
        uICanvas.transform.GetChild(0).gameObject.SetActive(true);
        uICanvas.transform.GetChild(1).gameObject.SetActive(false);
    }

}
