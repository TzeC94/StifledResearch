using System;
using System.Collections.Generic;

[Serializable]
public class MainMenuData {

    public int totalLevel = 9;
    public List<bool> levelUnlockStatus = new List<bool>();

    public MainMenuData() {

        levelUnlockStatus.Add(true);

        for(int i = 1; i < totalLevel; i++) {

            levelUnlockStatus.Add(false);

        }

    }

    public void UnlockLevel(int no) {

        if(no < levelUnlockStatus.Count - 1) {

            levelUnlockStatus[no] = true;

        }

    }

}
