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
		private float jumpBy = 2000.0f;

		public bool isGrounded;


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
			jump = new Vector3(0.0f, 20.0f, 0.0f);
		}

		private void Update()
		{
			currentPos = transform.position;

			Vector3 targetPos = lineRenderer.GetPosition(currentIndex);
			Vector3 dir = (targetPos - currentPos).normalized;
			//Quaternion targetRot = Quaternion.LookRotation(dir);
			float targetRotYaw = Quaternion.LookRotation(dir).y;
			Quaternion targetRot = Quaternion.Euler(0, targetRotYaw, 0);
			//transform.rotation = Quaternion.Euler(0, targetRotYaw, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, smoothing * Time.deltaTime);
			// = new Quaternion(0, rot.y * 100, 0, 0);
			transform.position += dir * speed * Time.deltaTime;

			if(Vector3.Distance(currentPos, targetPos) < 0.1f) 
			{
				currentIndex = (currentIndex + 1) % lineRenderer.positionCount;
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				transform.position += new Vector3(0, jumpBy * Time.deltaTime, 0);
			}
		}


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
