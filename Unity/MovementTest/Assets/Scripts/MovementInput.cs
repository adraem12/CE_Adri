using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour 
{
	public float inputX;
	public float inputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	[Range(0, 0.5f)] public float desiredRotationSpeed;
	public Animator anim;
	public float speed;
    [Range(0, 1f)] public float allowPlayerRotation;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	private float verticalVel;
	private Vector3 moveVector;

	private Vector3 rightFootPosition, rightFootIKPosition, leftFootPosition, leftFootIKPosition;
	private Quaternion leftFootIKRotation, rightFootIKRotation;
	private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;
    [Header("Feet Grounder")]
	public bool enableFeetIK = true;
	[Range(0, 2f)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
	[Range(0, 2f)][SerializeField] private float raycastDownDistane = 1.5f;
	[SerializeField] private LayerMask environmentLayer;
	[SerializeField] private float pelvisOffset = 0f;
	[Range(0, 1f)] private float pelvisUpAndDownSpeed = 0.28f;
	[Range(0, 1f)] private float feetToIKPositionSpeed = 0.5f;
	public string leftFootAnimVariableName = "LeftFootCurve";
	public string rightFootAnimVariableName = "RightFootCurve";
	public bool useProIKFeature = false;
	public bool showSolverDebug = true;

    [Header("Animation Smoothing")]
	[Range(0, 1f)] public float horizontalAnimSmoothTime;
	[Range(0, 1f)] public float verticalAnimTime;
	[Range(0, 1f)] public float startAnimTime;
	[Range(0, 1f)] public float stopAnimTime;

    void Start () 
	{
		anim = GetComponent<Animator> ();
		cam = Camera.main;
		controller = GetComponent<CharacterController> ();
	}

	void Update () 
	{
		InputMagnitude ();

		//If you don't need the character grounded then get rid of this part.
		isGrounded = controller.isGrounded;
		if (isGrounded)
			verticalVel -= 0;
		else
			verticalVel -= 2;

		moveVector = new Vector3 (0, verticalVel, 0);
		controller.Move (moveVector);
		//
	}

	void PlayerMoveAndRotation() 
	{
		inputX = Input.GetAxis ("Horizontal");
		inputZ = Input.GetAxis ("Vertical");

		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		desiredMoveDirection = forward * inputZ + right * inputX;

		if (blockRotationPlayer == false)
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
	}

	void InputMagnitude() 
	{
		//Calculate Input Vectors
		inputX = Input.GetAxis ("Horizontal");
		inputZ = Input.GetAxis ("Vertical");

		anim.SetFloat ("InputZ", inputZ, 0.0f, Time.deltaTime * 2f);
		anim.SetFloat ("InputX", inputX, 0.0f, Time.deltaTime * 2f);

		//Calculate the Input Magnitude
		speed = new Vector2(inputX, inputZ).sqrMagnitude;

		//Physically move player
		if (speed > allowPlayerRotation)
		{
			anim.SetFloat ("InputMagnitude", speed, 0.0f, Time.deltaTime);
			PlayerMoveAndRotation ();
		}
		else if (speed < allowPlayerRotation)
			anim.SetFloat ("InputMagnitude", speed, 0.0f, Time.deltaTime);
	}

    private void FixedUpdate()
    {
        if (!enableFeetIK || anim == null)
			return;
		AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
		AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);
		// find and raycast to the ground to find positions
		FeetPositionSolver(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation);
		FeetPositionSolver(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!enableFeetIK || anim == null)
            return;
		MovePelvisHeight();
		// feet ik position and rotation
		anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
		if (useProIKFeature)
			anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
		MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);
		//
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        if (useProIKFeature)
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
    }

	private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
	{
		Vector3 targetIKPosition = anim.GetIKPosition(foot);
		if (positionIKHolder != Vector3.zero)
		{
			targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
			positionIKHolder = transform.InverseTransformPoint(positionIKHolder);
			float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, feetToIKPositionSpeed);
			targetIKPosition.y += yVariable;
			lastFootPositionY = yVariable;
			targetIKPosition = transform.TransformPoint(targetIKPosition);
			anim.SetIKRotation(foot, rotationIKHolder);
		}
		anim.SetIKPosition(foot, targetIKPosition);
	}

	private void MovePelvisHeight()
	{
		if (rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastPelvisPositionY == 0)
		{
			lastPelvisPositionY = anim.bodyPosition.y;
			return;
		}
		float leftOffsetPosition = leftFootIKPosition.y - transform.position.y;
		float rightOffsetPosition = rightFootIKPosition.y - transform.position.y;
		float totalOffset = (leftOffsetPosition < rightOffsetPosition) ? leftOffsetPosition : rightOffsetPosition;
		Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;
		newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);
		anim.bodyPosition = newPelvisPosition;
		lastPelvisPositionY = anim.bodyPosition.y;
    }

	private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPositions, ref Quaternion feetIKRotations)
	{
        // raycast handling
        if (showSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistane + heightFromGroundRaycast), Color.yellow);
        if (Physics.Raycast(fromSkyPosition, Vector3.down, out RaycastHit feetOutHit, raycastDownDistane + heightFromGroundRaycast, environmentLayer))
		{
			feetIKPositions = fromSkyPosition;
			feetIKPositions.y = feetOutHit.point.y + pelvisOffset;
			feetIKRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
			return;
		}
		feetIKPositions = Vector3.zero;
	}

	private void AdjustFeetTarget (ref Vector3 feetPositions, HumanBodyBones foot)
	{
		feetPositions = anim.GetBoneTransform(foot).position;
		feetPositions.y = transform.position.y + heightFromGroundRaycast;
	}
}