using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
	
	public int SceneIndex;

	public void LoadByIndex(string characterName)
    {
		print (123);

		PlayerPrefs.SetString ("Player", characterName);
		SceneManager.LoadScene(SceneIndex);

    }
}
