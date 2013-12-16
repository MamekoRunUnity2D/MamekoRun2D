using UnityEngine;
using System.Collections;

public class Roll: MonoBehaviour {

	#region public members.
	public float	RollMoveSpeed { get; set; }
	public bool		IsTranslate { get; set; }
	#endregion



	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		this.IsTranslate	= false;
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate( ) {
		if ( true == this.IsTranslate ) {
			transform.Translate( new Vector2( 0.0f, this.RollMoveSpeed ) );
		}
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy( ) {
		Destroy( this.gameObject );
	}
}
