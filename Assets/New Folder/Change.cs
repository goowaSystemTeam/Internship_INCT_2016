using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene(){
		SceneManager.LoadScene("Save");
	}
}
