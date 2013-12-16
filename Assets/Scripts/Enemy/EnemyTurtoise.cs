using UnityEngine;
using System.Collections;

public class EnemyTurtoise : MonoBehaviour {

	#region public members.
	public AudioClip	DeadClip;					// Array of clips for when the player jumps.
	#endregion

	#region private members.
	private GameObject	_gameManager;
	private GameObject	_player;
	#endregion
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		_gameManager	= GameObject.Find ( "_GameManager" ).gameObject;
		_player			= GameObject.Find ( "Player" ).gameObject;
	}

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnTriggerEnter2D( Collider2D coll ) {

		if ( coll.gameObject.name.Contains( "Player" ) ) {
			Debug.Log ( "coll.gameObject.transform.localPosition.y =" + coll.gameObject.transform.localPosition.y );
			Debug.Log ( "this.gameObject.transform.localPosition.y =" + this.gameObject.transform.localPosition.y );

			//  当たり判定の高さ が 敵キャラの真ん中より低い場合.
			if ( coll.gameObject.transform.localPosition.y < this.gameObject.transform.localPosition.y ) {
				// プレイヤー死判定.
				_player.GetComponent<PlayerController>().isDead	= true;
			}
			else {
				// 敵キャラ倒されたSE再生.
				AudioSource.PlayClipAtPoint( DeadClip, transform.position );

				_player.SendMessage( "Jump", _player.GetComponent<PlayerController>().JumpForce * 1.5f );
				
				// 敵キャラ消滅.
				Destroy ( this.gameObject );
			}
		}
	}
}
