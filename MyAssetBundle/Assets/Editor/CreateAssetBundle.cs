using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

//把Resource目录下的资源打包成.data到OutPut目录下
public class CreateAssetBundle{
	
	public static string sourcePath = Application.dataPath + "/Prefabs";
	private const string AssetBundlesOutputPath = "Assets/Output";

	[MenuItem("Assets/Build AssetBundles")]
	public static void BuildAssetBundle(){
		ClearAssetBundleName ();
		Pack (sourcePath);

		string outputpath = Path.Combine(AssetBundlesOutputPath,Platform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
		if (!Directory.Exists (outputpath)) {
			Directory.CreateDirectory (outputpath);
		}
		BuildPipeline.BuildAssetBundles (outputpath, 0, EditorUserBuildSettings.activeBuildTarget);
		Debug.Log ("打包完成");
	}

	static void ClearAssetBundleName(){
		int length = AssetDatabase.GetAllAssetBundleNames ().Length;
		Debug.Log (length);
		string[] oldAssetBundleNames = new string[length];
		for (int i = 0; i < length; i++) {
			oldAssetBundleNames [i] = AssetDatabase.GetAllAssetBundleNames () [i];
		}

		for (int j = 0; j < oldAssetBundleNames.Length; j++) {
			AssetDatabase.RemoveAssetBundleName (oldAssetBundleNames [j], true);
		}
		length = AssetDatabase.GetAllAssetBundleNames ().Length;
		Debug.Log (length);
	}

	static void Pack(string source){
		DirectoryInfo folder = new DirectoryInfo (source);
		FileSystemInfo[] files = folder.GetFileSystemInfos ();
		int length = files.Length;
		for (int i = 0; i < length; i++) {
			if (files [i] is DirectoryInfo) {
				Pack (files [i].FullName);
			} else {
				if (!files [i].Name.EndsWith ("meta")) {
					fileWithDepends (files [i].FullName);
				}
			}
		}
		
	}

	//设置要打包的文件(包括依赖)
	static void fileWithDepends(string source){
		string _source = Replace (source);
		string _assetPath = "Assets" + _source.Substring (Application.dataPath.Length);

		//自动获取依赖项并给其资源设置AssetBundleName
		string[] dps = AssetDatabase.GetDependencies(_assetPath);
		foreach (var dp in dps) {
			if (dp.EndsWith ("cs"))
				continue;
			AssetImporter assetImporter = AssetImporter.GetAtPath (dp);
			string pathTmp = dp.Substring ("Assets".Length + 1);
			string assetName = pathTmp.Substring (pathTmp.IndexOf ("/") + 1);
			assetImporter.assetBundleName = assetName;
		}
	}

	static string Replace(string s){
		return s.Replace ("\\", "/");
	}

	public class Platform{
		public static string GetPlatformFolder(BuildTarget target){
			switch (target) 
			{
				case BuildTarget.Android:
					return "Android";
				case BuildTarget.iOS:
					return "IOS";
				case BuildTarget.StandaloneWindows:
				case BuildTarget.StandaloneWindows64:
					return "Windows";
				case BuildTarget.StandaloneOSXIntel:
				case BuildTarget.StandaloneOSXIntel64:
				case BuildTarget.StandaloneOSX:
					return "OSX";
				default:
					return null;
			}
		}
	}
}
