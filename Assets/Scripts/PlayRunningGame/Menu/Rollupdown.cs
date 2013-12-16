using UnityEngine;
using System.Collections;

public class Rollupdown : MonoBehaviour {

	#region public members.
	public GameObject HeaderRoll;
	public GameObject FooterRoll;
	#endregion

	#region private members.
	private const float RollSpeed	= 0.03f;

	private Roll Header;
	private Roll Footer;
	#endregion

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		Header	= HeaderRoll.GetComponent<Roll>();
		Footer	= FooterRoll.GetComponent<Roll>();
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable( ) {
		GameManager.StartRollUpDown += StartRollUpDown;
	}
	/// <summary>
	/// Disable this instance.
	/// </summary>
	void Disable( ) {
		GameManager.StartRollUpDown -= StartRollUpDown;
	}

	/// <summary>
	/// ロール Up & Down開始.
	/// </summary>
	void StartRollUpDown( ) {

		Header.RollMoveSpeed	=   RollSpeed;
		Footer.RollMoveSpeed	= - RollSpeed;

		Header.IsTranslate		= true;
		Footer.IsTranslate		= true;
	}
}
