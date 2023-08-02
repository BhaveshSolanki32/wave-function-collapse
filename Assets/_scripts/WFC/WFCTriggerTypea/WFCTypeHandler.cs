using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectCollapseTile), typeof(LowestEntropyTracker), typeof(GridData))]
public class WFCTypeHandler : MonoBehaviour
{
    enum WFCTypeEnum { OneAtATimeCollapseWFC, TestSingleCollapse, BulkCollapseWFC};
    WFCTypeEnum wFCType;
    [SerializeField] float delay = 0.2f;


    private void Awake()
    {
        wFCType = WFCTypeEnum.OneAtATimeCollapseWFC;
        SetMode();
    }

    void SetMode()
    {
        Destroy(GetComponent<WFCTrigger>());
        switch (wFCType)
        {
            case WFCTypeEnum.TestSingleCollapse:
                
                gameObject.AddComponent<TestWFCTrigger>().Delay = delay;
                break;
            case WFCTypeEnum.OneAtATimeCollapseWFC:
                gameObject.AddComponent<WFCTrigger>().Delay = delay; ;
                break;
            case WFCTypeEnum.BulkCollapseWFC:
                gameObject.AddComponent<WFCBulkCollapseTrigger>().Delay = delay; ;
                break;
        }
    }


    public void ModeChange(bool _isCurrentlyNormalMode)
    {
        if (_isCurrentlyNormalMode)
        {
            wFCType = WFCTypeEnum.BulkCollapseWFC;
            SetMode();
            _isCurrentlyNormalMode = false;
        }
        else
        {
            wFCType = WFCTypeEnum.OneAtATimeCollapseWFC;
            SetMode();
            _isCurrentlyNormalMode = true;
        }
    }



}
