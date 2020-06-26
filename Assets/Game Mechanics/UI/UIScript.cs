using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    public static UIScript instance;
    
    [Header("Remaining")]
    public Text remainingText;

    [Header("Win")]
    public Animator winAnimator;
    public GameObject winPanel;
    private int winFadeInHash = Animator.StringToHash("FadeIn");

    [Header("Dead")]
    public Animator deadAnimator;
    public GameObject deadPanel;

    void Awake() {

        instance = this;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MenuButton() {

        Time.timeScale = 0f;

    }

    public void ResumeButton() {

        Time.timeScale = 1f;

    }

    public void ExitButton() {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");

    }

    public void ActivateWin() {

        Time.timeScale = 0f;
        winPanel.SetActive(true);
        winAnimator.SetTrigger(winFadeInHash);

    }

    public void NextButton() {

        Time.timeScale = 1f;

        int currentLevel = PlayerPrefs.GetInt(GameKeyScript.StorageKey.currentLevel.ToString());
        currentLevel++;
        PlayerPrefs.SetInt(GameKeyScript.StorageKey.currentLevel.ToString(), currentLevel);

        SceneManager.LoadScene("Level Details");

    }

    public void UpdateRemainingText(int value) {

        remainingText.text = value.ToString();

    }

    public void ActivateDead() {

        deadPanel.SetActive(true);
        deadAnimator.SetTrigger(winFadeInHash);

    }

    public void RestartButton() {

        SceneManager.LoadScene("Level Details");

    }

}
