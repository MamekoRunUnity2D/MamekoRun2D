using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	#region public members.
	public float		SpeedRight;			// The fastest the player can travel in the x axis.
	public float		JumpForce;
	public AudioClip[]	JumpClips;					// Array of clips for when the player jumps.
	public AudioClip	DieClips;					// Array of clips for when the player jumps.
	public bool			IsController	= false;
	public bool			IsDead			= false;
	public bool			IsJumpEnabaled	= false;
	#endregion

	#region private members.
	private Transform	groundCheck;				// A position marking where to check if the player is grounded.

	private bool		isGrounded		= false;

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
		// デフォルトスピード.
		SpeedRight	= Player.PlayerConfig.DefaultSpeedRight;

		// Setting up references.
		groundCheck = transform.Find("groundCheck");
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {

		// 操作可能.
		if ( true == IsController ) {
			// 地上にいるか判定.
			isGrounded = Physics2D.Linecast( transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground") );
		}

#if UNITY_EDITOR
		if ( Input.GetMouseButtonDown(0) && isGrounded ) {
			IsJumpEnabaled	= true;
		}
#else
		if ( Input.touchCount > 0 ) {
			
			foreach ( Touch touch in Input.touches ) {
				
				// タッチ or ムーブの場合.
				if ( 
				    Input.GetTouch(0).phase == TouchPhase.Began
				    ) {
					if ( isGrounded ) {
						IsJumpEnabaled	= true;
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
		if ( IsDead ) {
			AudioSource.PlayClipAtPoint( DieClips, transform.position );
			Destroy ( this.gameObject );
		}
		// プレイ可能状態.
		else {

			transform.Translate( new Vector2( SpeedRight, 0.0f ) );

			// ジャンプ可能な場合.
			if ( IsJumpEnabaled  ) {

				// プレイヤージャンプ処理.
				Jump( JumpForce );

				//  ジャンプSE再生.
				AudioSource.PlayClipAtPoint( JumpClips[ (int)PlayerJumpStatus.Normal ], transform.position );
				IsJumpEnabaled	= false;
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
