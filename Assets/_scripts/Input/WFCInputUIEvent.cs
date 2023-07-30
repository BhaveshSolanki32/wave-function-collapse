using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WFCInputUIEvent : MonoBehaviour //handles input from buttons to start and reset tiles acts as link between UI interaction and wfc system
{
    bool isWaveFunctionCollapsed = true;
    public event Action OnStartBtnPressed; //turn off buttons, starwfc
    [SerializeField] Canvas uICanvas;
    [SerializeField] GameObject tileMap;
    [SerializeField] TextMeshProUGUI textMeshProModeChangeButton;
    WFCTrigger wFCTrigger;

    bool isCurrentlyNormalMode = true;


    public void OnDropDownToggle(GameObject _toggle) //hide menu options
    {
        if (_toggle.transform.parent.localPosition.y != -599)
            _toggle.transform.parent.localPosition = new(0, -599, 0);
        else
            _toggle.transform.parent.localPosition = new(0, -466.37f, 0);


        Transform _checkMarkTransfrom = _toggle.transform.GetChild(0).GetChild(0);

        if (_checkMarkTransfrom.gameObject.name != "Checkmark") Debug.LogError("toggle child (checkmark) sibiling index changed", _toggle);

        _checkMarkTransfrom.localEulerAngles = new(0, 0, _checkMarkTransfrom.localEulerAngles.z + 180);
    }
    public void OnDelaySliderChange(Slider _sldier)
    {
        wFCTrigger.Delay = _sldier.value;
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
        uICanvas.transform.GetChild(0).gameObject.SetActive(true);
        uICanvas.transform.GetChild(1).gameObject.SetActive(false);
    }

}
