using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMPro.TextMeshProUGUI levelText;

    public void updateExpSlider(int current, int target)
    {
        slider.maxValue = target;
        slider.value = current;
    }

    public void setLevelText(int level)
    {
        levelText.text = "LEVEL: " + level.ToString();
    }
}
