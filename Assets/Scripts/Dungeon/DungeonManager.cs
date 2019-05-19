using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DungeonManager : Singleton<DungeonManager>
{
    public bool roomCleared = false;
    public Toggle clearToggle;

    protected override void Start()
    {
        clearToggle.onValueChanged.AddListener((bool val) =>
        {
            ClearToggleValueChanged(val);
        });
    }

    public void SetOffClearToggle()
    {
        clearToggle.isOn = false;
    }

    public void ClearToggleValueChanged(bool val)
    {
        if (val)
        {
            roomCleared = true;
        }
        else
        {
            roomCleared = false;
        }
    }




}
