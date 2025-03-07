using UnityEngine;
using System.Collections;
using UnityEngine.Splines;
using System.ComponentModel;
using Unity.Mathematics;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;
		[SerializeField] SplineContainer splineContainer;
		private SplinePath path;
		[SerializeField] LineRenderer lineRenderer;
		private Rigidbody rb;

		private Vector3 currentPos;
		private int currentIndex;

		public float speed = 1.0f, smoothing = 2.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
		private float progressRatio, progress, totalLength;
		float distToGround;
		private Vector3 jump;
		private float jumpBy = 10.0f;
		private bool grounded = true;
		//public float cameraJumpPos = 9.5f, cameraStandardPos = 9.9f;
		private float cameraStandardPos = 9.9f;

		[SerializeField] Transform cam;

		//public bool isGrounded;


		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
			rb = GetComponent<Rigidbody>();
			anim.SetInteger("AnimationPar", 1);

			//Matrix4x4 localToWorldMatrix = splineContainer.transform.localToWorldMatrix;
			//
			//path = new SplinePath(new[]
			//{
			//	new SplineSlice<Spline>(splineContainer.Splines[0], new SplineRange(0,13), localToWorldMatrix),
			//	new SplineSlice<Spline>(splineContainer.Splines[1], new SplineRange(0,18), localToWorldMatrix),
			//	
			//});
			//
			//StartCoroutine(Follow());
			jump = new Vector3(0.0f, 2.0f, 0.0f);
		}

		private void Update()
		{
			currentPos = transform.position;

			Vector3 targetPos = lineRenderer.GetPosition(currentIndex);
			//Vector3 rotDir = (targetPos - currentPos).normalized;
			Vector3 rotDir = new Vector3((targetPos.x - currentPos.x), 0, (targetPos.z - currentPos.z)).normalized;

			Quaternion targetRot = Quaternion.LookRotation(rotDir);
			Quaternion newRot = Quaternion.Slerp(transform.rotation, targetRot, smoothing * Time.deltaTime);
			/*if (grounded) */transform.rotation = Quaternion.Euler(0, newRot.eulerAngles.y, 0);

			//transform.position += dir * speed * Time.deltaTime;

			Vector3 moveDir = new Vector3(targetPos.x - currentPos.x, 0, targetPos.z - currentPos.z).normalized;

			Vector3 newPos = new Vector3(moveDir.x * speed * Time.deltaTime, 0, moveDir.z * speed * Time.deltaTime);
			transform.position += new Vector3(newPos.x, 0, newPos.z);


			if (Vector3.Distance(new Vector3(currentPos.x, 0, currentPos.z), new Vector3(targetPos.x, 0, targetPos.z)) < 0.1f) 
			{
				currentIndex = (currentIndex + 1) % lineRenderer.positionCount;
			}

			if (Input.GetKeyDown(KeyCode.Space) && grounded)
			{
				//transform.position += new Vector3(0, jumpBy * Time.deltaTime, 0);
				rb.AddForce(jump * jumpBy, ForceMode.Impulse);
				grounded = false;
				//StartCoroutine(CameraJumpChange());
			}

			cam.position = new Vector3(cam.position.x, cameraStandardPos, cam.position.z);

		}

		//bool IsGrounded() { return GetComponent<Rigidbody>().velocity.y <= 0.1f; }

		//private void OnTriggerEnter(Collider other)
		//{
		//	grounded = true;
		//}

		private void OnTriggerStay(Collider other)
		{
			grounded = true;
		}

		private void OnTriggerExit(Collider other)
		{
			grounded = false;
		}

		//IEnumerator CameraJumpChange()
		//{
		//	cam.position = new Vector3(cam.position.x, cameraJumpPos, cam.position.z);
		//	yield return new WaitForSeconds(1.5f);
		//	cam.position = new Vector3(cam.position.x, cameraStandardPos, cam.position.z);
		//}

		//IEnumerator Follow()
		//{
		//	for(int n = 0; ; ++n)
		//	{
		//		progress = 0;
		//		while (progressRatio <= 1f)
		//		{
		//			float3 pos = path.EvaluatePosition(progressRatio);
		//			float3 direction = path.EvaluateTangent(progressRatio);
		//
		//			transform.position = pos;
		//			transform.LookAt(pos + direction);
		//
		//			progressRatio += speed * Time.deltaTime;
		//
		//			progress = progressRatio * totalLength;
		//			yield return null;
		//		}
		//	}
		//}
	}
}
