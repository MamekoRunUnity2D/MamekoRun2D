using UnityEngine;
using System.Collections;

/// <summary>
/// 背景スクロール.
/// </summary>
public class BgScroll : MonoBehaviour {

	#region private members.
	private float _scrollSpeed	= 0.1f;
	#endregion
	
	 /// <summary>
	 /// Update this instance.
	 /// </summary>
	void Update () { 
		renderer.material.mainTextureOffset = new Vector2 ( renderer.material.mainTextureOffset.x - Time.deltaTime * _scrollSpeed, 0 );
	}
}
