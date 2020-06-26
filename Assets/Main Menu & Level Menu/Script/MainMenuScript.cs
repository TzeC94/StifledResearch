using System.Collections.Generic;
using UnityEngine;
using SaveGameFree;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MainMenuScript : MonoBehaviour {

    [Header("Level Data")]
    public MainMenuData mainMenuData;
    public List<Button> levelButton = new List<Button>();

    [Header("Controller")]
    public Toggle controllerToggle;

    [Header("Google Play")]
    public GameObject googleplayPrefab;

    void Awake() {

        mainMenuData = new MainMenuData();

        Saver.Initialize(FormatType.Binary, PathType.PersistentDataPath);
        mainMenuData = Saver.Load<MainMenuData>(GameKeyScript.StorageKey.mainMenuData.ToString());
        InitilizeLevelDetails();

        if(GameObject.FindWithTag("GooglePlay") == null)
            Instantiate(googleplayPrefab);


    }

    void InitilizeLevelDetails() {
        
        for(int i = 0; i < mainMenuData.levelUnlockStatus.Count; i++) {

            levelButton[i].interactable = mainMenuData.levelUnlockStatus[i];

        }

    }

    public void LoadLevel(int level) {

        PlayerPrefs.SetInt(GameKeyScript.StorageKey.currentLevel.ToString(), level);    //Set current level
        SceneManager.LoadScene("Level Details");

    }

    public void SwitchControllerMode() {

        CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);

    }

    public void SwitchTouchMode() {

        CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Touch);

    }

    public void ControllerModeDetection() {

        if(controllerToggle.isOn) {

            SwitchControllerMode();

        }else {

            SwitchTouchMode();

        }

    }

    public void RateGame() {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.RIKIStudio.Feeling");

    }

}
