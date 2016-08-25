using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class THETAOSC : MonoBehaviour {

	void Start () {
		StartCoroutine(StartThetaS());
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


		RawImage rawImage = GetComponent<RawImage>();
		rawImage.texture = www.textureNonReadable;
		rawImage.SetNativeSize();

		jsonStr = "{\"name\": \"camera.closeSession\", \"parameters\": {\"sessionId\": \"" + sessionId + "\"}}";
		postBytes = Encoding.Default.GetBytes (jsonStr);
		www = new WWW (url, postBytes, header);
		yield return www;
		Debug.Log(www.text);
	}
}