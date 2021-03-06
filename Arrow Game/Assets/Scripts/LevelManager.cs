using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake() {
        if (LevelManager.instance == null) {
            instance = this;
        } 
    }

    public void GameOver() {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null) {
            _ui.ToggleDeathPanel();
        }
    }

    public void GameWin() {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null) {
            _ui.ToggleWinPanel();
        }
    }
}
