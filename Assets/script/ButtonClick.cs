using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeScene () {
		SceneManager.LoadScene("THETA");
	}

	public void changeScene2 () {
		SceneManager.LoadScene ("Title");
	}

	public void changeScene3 () {
		SceneManager.LoadScene ("HowToUse");
	}
}
