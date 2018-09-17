using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadAssetBundle : MonoBehaviour {
	void OnGUI(){
		if(GUILayout.Button("LoadAssetBundle")){
			DateTime t0 = DateTime.Now;

			AssetBundle manifestBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/Windows/Windows");

			if (manifestBundle != null) {
				AssetBundleManifest manifest = (AssetBundleManifest) manifestBundle.LoadAsset ("AssetBundleManifest");
				//TODO 加载
			}
		}
	}
}
