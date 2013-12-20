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
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionEnter2D( Collision2D coll ) {

		if ( coll.gameObject.name.Contains( "Player" ) ) {
			
			//  当たり判定の高さ が 敵キャラの真ん中より低い場合.
			if ( coll.gameObject.transform.localPosition.y < this.gameObject.transform.localPosition.y ) {
				// プレイヤー死判定.
				_player.GetComponent<PlayerController>().IsDead	= true;
			}
			else {
				// 敵キャラ倒されたSE再生.
				AudioSource.PlayClipAtPoint( DeadClip, transform.position );

				// パーティクル演出.
				GameObject objEffect	= (GameObject) Instantiate( Resources.Load ( "Particles/Misc/Sparks" ), Vector3.zero, Quaternion.identity );
				objEffect.transform.localPosition	= this.gameObject.transform.localPosition;
				
				// 敵キャラ消滅.
				Destroy ( this.gameObject );
			}
		}
	}
}
