using UnityEngine;
using SaveGameFree;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    //Player
    public GameObject playerObject;
    [HideInInspector]
    public PlayerBehaviourScript playerBehaviour;

    //Database
    private MainMenuData mainMenuData;

    [Header("Test")]
    public bool useTest;
    public int testThrowAmount = 100;

    void Awake() {

        instance = this;

        playerBehaviour = playerObject.GetComponent<PlayerBehaviourScript>();

        //get database
        mainMenuData = new MainMenuData();
        Saver.Initialize(FormatType.Binary, PathType.PersistentDataPath);
        mainMenuData = Saver.Load<MainMenuData>(GameKeyScript.StorageKey.mainMenuData.ToString());

    }

    // Use this for initialization
    void Start () {
		
        if(useTest) {

            playerBehaviour.throwSoundBehaviour.SetThrowAmount(testThrowAmount);

        }else {

            int throwAmount = PlayerPrefs.GetInt(GameKeyScript.StorageKey.throwAmountThisLevel.ToString());
            playerBehaviour.throwSoundBehaviour.SetThrowAmount(throwAmount);
            UIScript.instance.UpdateRemainingText(throwAmount);

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerWin() {

        playerBehaviour.LockPlayer();

        //Unlock next level
        int currentLevel = PlayerPrefs.GetInt(GameKeyScript.StorageKey.currentLevel.ToString());
        mainMenuData.UnlockLevel(currentLevel);
        Saver.Save(mainMenuData, GameKeyScript.StorageKey.mainMenuData.ToString());

        UIScript.instance.ActivateWin();

    }

    public void PlayerDead() {

        UIScript.instance.ActivateDead();

    }

}
