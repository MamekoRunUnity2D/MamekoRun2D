using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	#region event
	public delegate void RollUpDown();
	public static event RollUpDown StartRollUpDown;
	#endregion event

	#region public members.
	public Camera		ChaserCamera;
	public GameObject	DeadLineY;
	public GameObject	BGMManager;
	public GameObject	Player;
	public GameObject	DistanceScore;
	#endregion

	#region private members.
	private GameObject			_Anchor;
	private Vector3				_playerPosition;
	private PlayerController	_playerController;
	private ChasePlayer			_chasePlayer;
	private UILabel				_uiLabelDistanceScore;
	private bool				_executedFlag	= false;
	private bool				_isCameraEnableChasePlayer;
	#endregion

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake( ) {
		_Anchor					= GameObject.Find ( "Anchor" );
		_playerController		= Player.GetComponent<PlayerController>();
		_chasePlayer			= ChaserCamera.GetComponent<ChasePlayer>();
		_playerPosition			= Player.transform.position;
		_uiLabelDistanceScore	= DistanceScore.GetComponent<UILabel>();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update( ) {

		if ( Player != null ) {
			_playerPosition	= Player.transform.position;

			if ( _playerController != null ) {
				// プレイヤーが死.
				if( _playerController.isDead ) {
					setPlayerDie( );
				}
			}
		}
	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate( ) {

		if ( _playerPosition != null ) {
			if ( _playerPosition.x > 0 ) {
				_uiLabelDistanceScore.text	= (string)System.Convert.ToString( Mathf.Floor( _playerPosition.x / 2 ) );
			}

			// 追跡するプレイヤーのX軸ポジション  が一定位置まで来た場合に追従開始.
			if ( false == _isCameraEnableChasePlayer && ChaserCamera.transform.position.x <= _playerPosition.x + 4 ) {
				_isCameraEnableChasePlayer	= true;
				_chasePlayer.SpeedRight		= _playerController.SpeedRight;

				DeadLineY.SetActive( true );

				//  ロール Up & Down.
				StartRollUpDown();
			}
		}
	}

	/// <summary>
	/// プレイヤーが死判定.
	/// 
	/// BGMストップ.
	/// カメラの移動減速しストップ,
	/// </summary>
	void setPlayerDie( ) {
		if ( false == _executedFlag ) {
			_executedFlag	= true;

			// BGM ストップ.
			BGMManager.audio.Stop( );

			// プレイヤー追跡カメラをブレーキ.
			_chasePlayer.SendMessage( "SetBrakeSpeed" );

			//  ゲームオーバーボタン生成.
			_instantiateBtnGameOver( );
		}
	}

	/// <summary>
	/// _instantiateBtnGameOver
	/// </summary>
	void _instantiateBtnGameOver( ) {
		GameObject _objBtnGameOver	= (GameObject)Instantiate ( Resources.Load( "BtnGameOver" ), Vector3.zero, Quaternion.identity );
		_objBtnGameOver.transform.parent		= _Anchor.transform;
		_objBtnGameOver.transform.localScale	= Vector3.one;
		_objBtnGameOver.transform.localPosition	= Vector3.zero;
	}
}