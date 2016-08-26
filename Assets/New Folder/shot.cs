using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class CaptureScreenshot : MonoBehaviour {

	const string ScreenshotFilename = "src.png";

	#if  UNITY_IPHONE && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _PlaySystemShutterSound ();
	[DllImport("__Internal")]
	private static extern void _WriteImageToAlbum (string path);

	public void CaptureScreenShot () {
	// ネイティブコードからシャッター音を再生。マナーモード時や、ボリュームオフ時もシャッター音を再生したいため。
	_PlaySystemShutterSound ();
	// スクリーンショットを撮影。Documents下に保存される。
	Application.CaptureScreenshot(temporaryScreenshotFilename);
	// スクリーンショットが書き込まれるまで待つ。書き込み完了後、画像をカメラロールへ保存する。
	StartCoroutine(WaitUntilFinishedWriting (()=>{ _WriteImageToAlbum (TemporaryScreenshotPath());}));
	}

	// スクリーンショットの画像が一時的に保存されるパス。
	string TemporaryScreenshotPath () {
	return Application.persistentDataPath + "/" + temporaryScreenshotFilename;
	}

	// スクリーンショットの書き込みが終了するまで、毎フレームファイルの有無を確認する。
	IEnumerator WaitUntilFinishedWriting (Action callback) {
	while (!System.IO.File.Exists (TemporaryScreenshotPath())) {
	Debug.Log(">>>>> Temporary Screenshot have not been written yet.");
	yield return null;
	}
	Debug.Log(">>>> Temporary Screenshot have been Written.");
	callback();
	yield break;
	}

	// カメラロール保存後、ネイティブ側から呼び出される。
	void DidImageWriteToAlbum (string errorDescription) {
	if (string.IsNullOrEmpty(errorDescription)) {
	Debug.Log(">>>>> Image have been Written To Album Successfully.");
	Debug.Log(">>>>> Delete Temporary Screenshot.");
	System.IO.File.Delete (TemporaryScreenshotPath());
	} else {
	Debug.Log(">>>>> An Error Occured. Error Description is..." + errorDescription);
	}
	}
	#else
	public void CaptureScreenShot () {
		// Android端末での処理等。
	}
	#endif
}

[DllImport("__Internal")]
private static extern void _PlaySystemShutterSound ();
[DllImport("__Internal")]
private static extern void _WriteImageToAlbum (string path);

IEnumerator WaitUntilFinishedWriting (Action callback) {
	while (!System.IO.File.Exists (TemporaryScreenshotPath())) {
		Debug.Log(">>>>> Temporary Screenshot have not been written　yet.");
		yield return null;
	}
	Debug.Log(">>>> Temporary Screenshot have been Written.");
	callback();
	yield break;
}
void DidImageWriteToAlbum (string errorDescription) {
	if (string.IsNullOrEmpty(errorDescription)) {
		Debug.Log(">>>>> Image have been Written To Album Successfully.");
		Debug.Log(">>>>> Delete Temporary Screenshot.");
		System.IO.File.Delete (TemporaryScreenshotPath());
	} else {
		Debug.Log(">>>>> An Error Occured. Error Description is..." + errorDescription);
	}
}