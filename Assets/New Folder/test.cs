using UnityEngine;
using System.Collections;

public class CtrlPlugins : MonoBehaviour
{
	readonly string PLUGIN_CLASS_PATH = "jp.plugincontroller.PluginConnector";
	public string GetText()
	{
		string strGotText = "";
		// DCIMディレクトリのパスを取得.
		using(AndroidJavaClass clsPlugin = new AndroidJavaClass(PLUGIN_CLASS_PATH))
		{
			strGotText = clsPlugin.CallStatic("GetDcimPath");
		}
		return strGotText;
	}
	public void ShowToast()
	{
		// トーストを表示する.
		using(AndroidJavaClass clsPlugin = new AndroidJavaClass(PLUGIN_CLASS_PATH))
		{
			clsPlugin.CallStatic("ShowToast");
		}
	}
	void OnGUI()
	{
		if(GUI.Button(new Rect(300f, 20f, 200f, 200f), "GetData"))
		{
			_ctrPlugins.ShowImageView();
		}
}