using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour
{
    public void changeScene(string nextLevel) {
		if (nextLevel != null) {
			SceneManager.LoadScene(nextLevel);
		}
	}
}
