using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rad_GuiManager : MonoBehaviour {

    public GameObject gmeOverPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    /// <summary>
    /// called from Button, set main menu panel on and turns off the rest
    /// </summary>
    public void MainMenuPanelOn()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// called frm button, turns on settings
    /// </summary>
    public void SettingsPanelOn()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void GameOverPanelOn()
    {
        gmeOverPanel.SetActive(true);
    }


    public void GameOverPanelOff()
    {
        gmeOverPanel.SetActive(false);
    }
}
