using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WFCInputUIEvent : MonoBehaviour //handles input from buttons to start and reset tiles acts as link between UI interaction and wfc system
{
    [SerializeField] Canvas _uICanvas;
    [SerializeField] GameObject _tileMap;
    [SerializeField] TextMeshProUGUI _textMeshProModeChangeButton;
    bool _isWaveFunctionCollapsed = true;
    WFCTrigger _wFCTrigger;
    bool _isCurrentlyNormalMode = true;
    public event Action OnStartBtnPressed; //turn off buttons, starwfc


    public void OnDropDownToggle(GameObject toggle) //hide menu options
    {
        var uiUpPost = -466.37f;
        var uiDownPost = -599;

        if (toggle.transform.parent.localPosition.y != uiDownPost)
            toggle.transform.parent.localPosition = new(0, uiDownPost, 0);
        else
            toggle.transform.parent.localPosition = new(0, uiUpPost, 0);


        var checkMarkTransfrom = toggle.transform.GetChild(0).GetChild(0);

        if (checkMarkTransfrom.gameObject.name != "Checkmark") Debug.LogError("toggle child (checkmark) sibiling index changed", toggle);

        checkMarkTransfrom.localEulerAngles = new(0, 0, checkMarkTransfrom.localEulerAngles.z + 180);
    }
    public void OnDelaySliderChange(Slider sldier)
    {
        _wFCTrigger.Delay = sldier.value;
    }


    public void ResetTiles()
    {
        if (_isWaveFunctionCollapsed)
        {
            _isWaveFunctionCollapsed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void StartWFC()
    {
        _wFCTrigger = _tileMap.GetComponent<WFCTrigger>();
        if (_wFCTrigger == null) Debug.LogError("wfc trigger not found in tiles");

        _wFCTrigger.OnWaveFunctionCollapsed += waveFunctionIsCollapsed;
        OnStartBtnPressed += _wFCTrigger.StartWFC;

        if (_isWaveFunctionCollapsed)
        {
            _uICanvas.transform.GetChild(0).gameObject.SetActive(false);
            _uICanvas.transform.GetChild(1).gameObject.SetActive(true);
            OnStartBtnPressed?.Invoke();
        }

    }

    public void ModeChange()
    {
        _tileMap.GetComponent<WFCTypeHandler>().ModeChange(_isCurrentlyNormalMode);
        if (_isCurrentlyNormalMode)
        {
            _textMeshProModeChangeButton.text = "Bézier Mode";
            _isCurrentlyNormalMode = false;
        }
        else
        {
            _textMeshProModeChangeButton.text = "Normal Mode";
            _isCurrentlyNormalMode = true;
        }
        _wFCTrigger = _tileMap.GetComponent<WFCTrigger>();
    }

    void waveFunctionIsCollapsed() //turn on the buttons, and mark the bool
    {
        _isWaveFunctionCollapsed = true;
        _uICanvas.transform.GetChild(0).gameObject.SetActive(true);
        _uICanvas.transform.GetChild(1).gameObject.SetActive(false);
    }

}
