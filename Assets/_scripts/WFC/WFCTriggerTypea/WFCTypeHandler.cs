using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile), typeof(LowestEntropyTracker), typeof(GridData))]
public class WFCTypeHandler : MonoBehaviour
{
    [SerializeField] float _delay = 0.2f;
    enum _wFCTypeEnum { OneAtATimeCollapseWFC, TestSingleCollapse, BulkCollapseWFC};
    _wFCTypeEnum wFCType;


    public void ModeChange(bool isCurrentlyNormalMode)
    {
        if (isCurrentlyNormalMode)
        {
            wFCType = _wFCTypeEnum.BulkCollapseWFC;
            SetMode();
            isCurrentlyNormalMode = false;
        }
        else
        {
            wFCType = _wFCTypeEnum.OneAtATimeCollapseWFC;
            SetMode();
            isCurrentlyNormalMode = true;
        }
    }


    private void Awake()
    {
        wFCType = _wFCTypeEnum.OneAtATimeCollapseWFC;
        SetMode();
    }

    void SetMode()
    {
        Destroy(GetComponent<WFCTrigger>());
        switch (wFCType)
        {
            case _wFCTypeEnum.TestSingleCollapse:
                
                gameObject.AddComponent<TestWFCTrigger>().Delay = _delay;
                break;
            case _wFCTypeEnum.OneAtATimeCollapseWFC:
                gameObject.AddComponent<WFCTrigger>().Delay = _delay; ;
                break;
            case _wFCTypeEnum.BulkCollapseWFC:
                gameObject.AddComponent<WFCBulkCollapseTrigger>().Delay = _delay; ;
                break;
        }
    }

}
