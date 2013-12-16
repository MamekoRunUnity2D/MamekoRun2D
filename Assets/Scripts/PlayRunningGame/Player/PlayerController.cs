using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region public members.
	public float		SpeedRight	= 0.1f;			// The fastest the player can travel in the x axis.
	public float		JumpForce;
	public AudioClip[]	JumpClips;					// Array of clips for when the player jumps.
	public AudioClip	DieClips;					// Array of clips for when the player jumps.

	public bool		isDead			= false;
	#endregion

	#region private members.
	private float		_MovingDistanceLocalPosition;
	private float		_MovingDistanceMeter;
	private Transform	_groundCheck;				// A position marking where to check if the player is grounded.

	private bool		_isGrounded		= false;
	private bool		_isJumpEnabaled	= false;

	private enum PlayerJumpStatus {
		Normal	= 0,
		Double,
		Combo,	
	}
	#endregion

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		// Setting up references.
		_groundCheck = transform.Find("groundCheck");
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {

		// 地上にいるか判定.
		_isGrounded = Physics2D.Linecast( transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground") );

#if UNITY_EDITOR
		if ( Input.GetMouseButton(0) && _isGrounded ) {
			_isJumpEnabaled	= true;
		}
		
#else
		if ( Input.touchCount > 0 ) {
			
			foreach ( Touch touch in Input.touches ) {
				
				// タッチ or ムーブの場合.
				if ( 
				    Input.GetTouch(0).phase == TouchPhase.Began
				    ) {
					if ( _isGrounded ) {
						_isJumpEnabaled	= true;
					}
				}
			}
		}
#endif
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate ( ) {

		// プレイ不可状態.
		if ( isDead ) {
			AudioSource.PlayClipAtPoint( DieClips, transform.position );
			Destroy ( this.gameObject );
		}
		// プレイ可能状態.
		else {

			transform.Translate( new Vector2( SpeedRight, 0.0f ) );

			// ジャンプ可能な場合.
			if ( _isJumpEnabaled  ) {

				// プレイヤージャンプ処理.
				Jump( JumpForce );

				//  ジャンプSE再生.
				AudioSource.PlayClipAtPoint( JumpClips[ (int)PlayerJumpStatus.Normal ], transform.position );
				_isJumpEnabaled	= false;
			}
		}
	}

	/// <summary>
	/// プレイヤージャンプ処理.
	/// </summary>
	void Jump( float jumpforce ) {
		rigidbody2D.AddForce( new Vector2( 0f, jumpforce ) );
	}
}
