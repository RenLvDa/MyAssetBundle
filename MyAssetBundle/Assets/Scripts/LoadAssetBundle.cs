using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadAssetBundle : MonoBehaviour {
	void OnGUI(){
		if(GUILayout.Button("LoadAssetBundle")){
			DateTime t0 = DateTime.Now;

			AssetBundle manifestBundle = AssetBundle.LoadFromFile(Application.dataPath + "/Output/Windows/Windows");

			if (manifestBundle != null) {
				AssetBundleManifest manifest = (AssetBundleManifest) manifestBundle.LoadAsset ("AssetBundleManifest");

				//获取依赖文件列表
				string[] cubedepends = manifest.GetAllDependencies ("cube.prefab");
				foreach (var item in cubedepends) {
					Debug.Log ("depends" + item);
				}
				AssetBundle[] dependsAssetbundle = new AssetBundle[cubedepends.Length];
				for (int index = 0; index < cubedepends.Length; index++) {
					//加载所有的依赖文件
					dependsAssetbundle [index] = 
						AssetBundle.LoadFromFile (Application.dataPath + "/Output/Windows/" + cubedepends [index]);
				}
				DateTime t1 = DateTime.Now;

				//加载我们需要的文件
				AssetBundle cubeBundle = AssetBundle.LoadFromFile(Application.dataPath + "/Output/Windows/cube.prefab");
				GameObject cube = cubeBundle.LoadAsset ("cube") as GameObject;
				if (cube != null) {
					Instantiate (cube, new Vector3 (0, 0, 0), Quaternion.identity);
					DateTime t2 = DateTime.Now;
					Debug.Log ("LoadView Time1:" + (t1 - t0).TotalMilliseconds + "Time2:" + (t2 - t1).TotalMilliseconds);
				}
			}
		}
	}
}
