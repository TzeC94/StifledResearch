using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class LevelDetails : MonoBehaviour {

    public Text levelNoText;
    public Text throwAmountText;
    public Button adsButton;
    public Text tipsText;

    [Space(3)]
    public List<LevelDetail> levelDetail = new List<LevelDetail>();

    int currentLevel;
    int currentThrowAmount;

    void Awake() {

        currentLevel = PlayerPrefs.GetInt(GameKeyScript.StorageKey.currentLevel.ToString());

        levelNoText.text = "Level " + currentLevel.ToString();
        currentThrowAmount = levelDetail[currentLevel-1].throwAmount;

        if(levelDetail[currentLevel-1].tips != "") {

            tipsText.text = "Tips: " + levelDetail[currentLevel-1].tips;

        }else {

            tipsText.text = "";

        }

        UpdateThrowAmount();

    }

    public void StartGame() {

        SceneManager.LoadScene("Level " + currentLevel);

    }

    public void ShowRewardedAd(){

        if (Advertisement.IsReady("rewardedVideo")){

            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);

        }

    }

    private void HandleShowResult(ShowResult result){

        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                DoubleThrowAmount();
                adsButton.interactable = false;
                break;
        }

    }

    void DoubleThrowAmount() {

        currentThrowAmount *= 2;

        UpdateThrowAmount();

    }

    void UpdateThrowAmount() {

        throwAmountText.text = currentThrowAmount.ToString();

        PlayerPrefs.SetInt(GameKeyScript.StorageKey.throwAmountThisLevel.ToString(), currentThrowAmount);

    }

}

[Serializable]
public class LevelDetail {

    public int throwAmount;
    public string tips;

}
