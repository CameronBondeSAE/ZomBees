using System.Collections;
using System.Collections.Generic;
using AlexM;
using UnityEngine;

// public class InputManager
// {
// 	public Vector2 moveDirection;
// 	
// }

public class PlayerBase : MonoBehaviour
{

	//[HideInInspector]
	public InputManager   _inputManager;
	[HideInInspector]
	public Rigidbody      _rb;
	[HideInInspector]
	public Vector2        _movementInput;
	[HideInInspector]
	public Vector3        _movement;
	[HideInInspector]
	public Vector3        _fwdDirection;
	[HideInInspector]
	public Vector3        _rightDirection;
	// [HideInInspector]
	public bool           _isGrounded;
	[HideInInspector]
	public PhysicMaterial _slip;
	[HideInInspector]
	public PhysicMaterial _grip;
	// [HideInInspector]
	public SphereCollider _gripStub;
	[HideInInspector]
	public bool           _isSprinting;
	[HideInInspector]
	public RaycastHit     _groundHitInfo;
	[HideInInspector]
	public float _groundAngle, _groundAngleOffset;

}