using UnityEngine;
using System.Collections;

public class StaminaUp : MonoBehaviour {

	#region public members.
	public AudioClip	Clips;					// Array of clips for when the player jumps.
	#endregion
	
	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerEnter2D( Collider2D coll ) {
		if ( coll.gameObject.CompareTag( "Player" ) ) {
			// コイン取得音再生.
			AudioSource.PlayClipAtPoint( Clips, transform.position );
			
			// Destroy.
			Destroy ( this.gameObject );
		}
	}
}
