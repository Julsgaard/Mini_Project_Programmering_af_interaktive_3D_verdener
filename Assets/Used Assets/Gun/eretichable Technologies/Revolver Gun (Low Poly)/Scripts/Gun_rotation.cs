using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_rotation : MonoBehaviour
{
	public float RotateSpeed = 25.0f;
	

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
	}
}
