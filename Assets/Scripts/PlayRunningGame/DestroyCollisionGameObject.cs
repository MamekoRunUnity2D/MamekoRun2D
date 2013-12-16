using UnityEngine;
using System.Collections;

public class DestroyCollisionGameObject : MonoBehaviour {

	#region private members.
	private PlayerController	_playerController;
	#endregion

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		_playerController	= GameObject.Find( "Player" ).gameObject.GetComponent<PlayerController>();
	}

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerEnter2D( Collider2D coll ) {

		if ( coll.gameObject.CompareTag( "Player" ) ) {
			_playerController.isDead	= true;
		}
	}
}