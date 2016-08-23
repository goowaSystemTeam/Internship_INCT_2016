using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick() {
		LoadGraphScreen( 50, 100, "a.png", TRUE );
	}
}
