using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadScene2 (){
		SceneManager.LoadScene("Home");
	}
}
