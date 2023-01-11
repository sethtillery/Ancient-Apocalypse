using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text description;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        description.text = upgradeData.description.ToString();
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
