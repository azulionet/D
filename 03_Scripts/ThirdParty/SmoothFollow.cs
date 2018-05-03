using UnityEngine;
using System.Collections;

// http://unitytipsandtricks.blogspot.kr/2013/05/camera-shake.html

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{
		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

		// 		void LateUpdate()
		// 		{
		// 			// Early out if we don't have a target
		// 			if (!target)
		// 				return;
		// 
		// 			// Calculate the current rotation angles
		// 			var wantedRotationAngle = target.eulerAngles.y;
		// 			var wantedHeight = target.position.y + height;
		// 
		// 			var currentRotationAngle = transform.eulerAngles.y;
		// 			var currentHeight = transform.position.y;
		// 
		// 			// Damp the rotation around the y-axis
		// 			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		// 
		// 			// Damp the height
		// 			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		// 
		// 			// Convert the angle into a rotation
		// 			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		// 
		// 			// Set the position of the camera on the x-z plane to:
		// 			// distance meters behind the target
		// 			transform.position = target.position;
		// 			transform.position -= currentRotation * Vector3.forward * distance;
		// 
		// 			// Set the height of the camera
		// 			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);
		// 
		// 			// Always look at the target
		// 			transform.LookAt(target);
		// 		}


		Vector2 vcAdd = Vector2.zero;

		public float a_fDShake = 500.0f;
		public float a_fCX = 0;
		public float a_fCY = 0;

		public void Shake(float a_fDuration, float a_fX = 1.0f, float a_fY = 1.0f)
		{
			StartCoroutine(ShakeCamPos(a_fDuration, a_fX, a_fY));
		}

		IEnumerator ShakeCamPos(float a_fDuration, float a_X, float a_Y)
		{
			float elapsed = 0.0f;
			vcAdd = Vector2.zero;

			while (elapsed < a_fDuration)
			{
				elapsed += Time.deltaTime;

				float percentComplete = elapsed / a_fDuration;
				float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

				// map value to [-1, 1]
				float x = Random.value * 2.0f - 1.0f;
				float y = Random.value * 2.0f - 1.0f;
				x *= damper;
				y *= damper;

				vcAdd = new Vector3((x*a_X* a_fCX) / a_fDShake, (y*a_Y* a_fCY) / a_fDShake);

				yield return null;
			}

			vcAdd = Vector2.zero;
		}

		void FixedUpdate()
		{
			// Calculate the current rotation angles
			var wantedHeight = target.position.y + height;
			var currentHeight = transform.position.y;

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x + vcAdd.x, transform.position.y + height + vcAdd.y, -10);
			
			// Always look at the target
			// transform.LookAt(target);
		}
	}
}