using UnityEngine;
using System.Collections;

public class BtnStart : MonoBehaviour {

	///<summary>
	/// スタートボタン押下.
	/// </summary>
	void OnClickButton( ) {
		Application.LoadLevel( (int)Config.SceneList.PlayRunningGame );
	}
}
