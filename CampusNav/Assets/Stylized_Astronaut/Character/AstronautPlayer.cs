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

		public float speed = 1.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;
		private float progressRatio, progress, totalLength;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
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
		}

		IEnumerator Follow()
		{
			for(int n = 0; ; ++n)
			{
				progress = 0;
				while (progressRatio <= 1f)
				{
					float3 pos = path.EvaluatePosition(progressRatio);
					float3 direction = path.EvaluateTangent(progressRatio);

					transform.position = pos;
					transform.LookAt(pos + direction);

					progressRatio += speed * Time.deltaTime;

					progress = progressRatio * totalLength;
					yield return null;
				}
			}
		}
	}
}
