using UnityEngine;
using System.Collections;

public class BtnGameOver : MonoBehaviour {

	/// <summary>
	/// Raises the click event.
	/// </summary>
	void OnClickButton( ) {
		Application.LoadLevel( (int)Config.SceneList.Top );
	}
}
