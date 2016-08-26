using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThetaImage : MonoBehaviour {

	//public Image theta_Image;

	// Use this for initialization
	void Start () {
		Image theta_Image = gameObject.GetComponent<Image> ();
		Debug.Log (theta_Image);
		theta_Image.sprite = Image_theta.sprite1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
