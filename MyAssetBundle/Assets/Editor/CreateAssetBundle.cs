using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssetBundle : MonoBehaviour {
	[MenuItem("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles(){
		string dir = "AssetBundles";
		if (Directory.Exists (dir) == false) {
			Directory.CreateDirectory (dir);
		}
		BuildPipeline.BuildAssetBundles (dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}
}
