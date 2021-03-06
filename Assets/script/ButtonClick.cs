﻿using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Image_theta {
	//public static Image thetaImage;
	public static Sprite sprite1;
}
	

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

	public void takePic () {
		StartCoroutine (StartThetaS ());
	}

	public void screenshot () {
		Application.CaptureScreenshot ("Screenshot.png");
	}


	IEnumerator StartThetaS () {
		string url = "http://192.168.1.1/osc/info";
		WWW www = new WWW(url);
		yield return www;
		Debug.Log(www.text);

		Dictionary<string, string> header = new Dictionary<string, string>();
		url = "http://192.168.1.1/osc/commands/execute";
		string jsonStr = "{\"name\": \"camera.startSession\"}";
		byte[] postBytes = Encoding.Default.GetBytes (jsonStr);
		www = new WWW (url, postBytes, header);
		yield return www;
		JsonNode json = JsonNode.Parse(www.text);
		string sessionId = json["results"]["sessionId"].Get<string>();
		Debug.Log(sessionId);

		jsonStr = "{\"name\": \"camera.takePicture\", \"parameters\": {\"sessionId\": \"" + sessionId + "\"}}";
		postBytes = Encoding.Default.GetBytes (jsonStr);
		www = new WWW (url, postBytes, header);
		yield return www;
		Debug.Log(www.text);

		string fileUri = "";
		url = "http://192.168.1.1/osc/state";
		jsonStr = "{}";
		postBytes = Encoding.Default.GetBytes (jsonStr);
		while(fileUri == "") {
			www = new WWW (url, postBytes, header);
			yield return www;
			Debug.Log(www.text);
			json = JsonNode.Parse(www.text);
			fileUri = json["state"]["_latestFileUri"].Get<string>();
		}
		Debug.Log(fileUri);


		yield return new WaitForSeconds(3); 

		url = "http://192.168.1.1/osc/commands/execute";
		jsonStr = "{\"name\": \"camera.getImage\", \"parameters\": {\"fileUri\": \"" + fileUri + "\"}}";
		postBytes = Encoding.Default.GetBytes (jsonStr);
		www = new WWW (url, postBytes, header);
		yield return www;


//		RawImage rawImage = GetComponent<RawImage>();
//		rawImage.texture = www.textureNonReadable;
//		rawImage.SetNativeSize();

//		Texture2D texture = www.textureNonReadable;
//		Sprite texture_sprite = Sprite.Create (texture, new Rect (0, 0, 5376, 2688), Vector2.zero);
//		thetaImage.sprite = texture_sprite;

		//Texture2D texture = new Texture2D(2048, 1024);
		Texture2D texture = new Texture2D(5376, 2688);
		texture.LoadImage(www.bytes);
		Sprite texture_sprite = Sprite.Create (texture, new Rect (0, 0, 5376, 2688), Vector2.zero);
		//Image_theta.thetaImage.sprite1 = texture_sprite;
		Debug.Log(texture_sprite);
		Image_theta.sprite1 = texture_sprite;

		if (Image_theta.sprite1 == null)
			Debug.Log ("Image_theta is NULL");
		else
			Debug.Log("Image_theta isn't NULL");


		jsonStr = "{\"name\": \"camera.closeSession\", \"parameters\": {\"sessionId\": \"" + sessionId + "\"}}";
		postBytes = Encoding.Default.GetBytes (jsonStr);
		www = new WWW (url, postBytes, header);
		yield return www;
		Debug.Log(www.text);

		SceneManager.LoadScene ("checkPic");
	}
}
