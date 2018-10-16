using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float speed;
	public GameObject player;
	public bool rotate = true;

	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		if (rotate) {
			Quaternion turn = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * speed, Vector3.left);
			offset = turn * offset;
		}

		transform.position = player.transform.position + offset;
		transform.LookAt(player.transform);
	}
}