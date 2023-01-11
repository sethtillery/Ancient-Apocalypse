using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] List<upgradeButton> upgradeButtons;
    

    private void Start()
    {
        HideButtons();      
    }

    public void openPanel(List<UpgradeData> upgradeDatas)
    {               
        Time.timeScale = 0;
        Clean();
        panel.SetActive(true);

        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }

    }

    public void Clean()
    {
        for(int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        GameManager.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
        closePanel();
    }

    public void closePanel()
    {
        HideButtons();

        panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
            upgradeButtons[i].gameObject.SetActive(false);
    }
}
