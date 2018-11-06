using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{

    public Transform FromPosition;
    public Transform TargetPosition;

    public Transform Target;


    public float angle = 0;
    public float moveSpeed = 10.0f;

    Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

        moveDirection = TargetPosition.position - FromPosition.position;
        moveDirection.Normalize();

        angle = Vector3.Dot(FromPosition.forward.normalized, moveDirection) * Mathf.Rad2Deg;

        Target.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
       

    }
	
	void Update()
    {
        Target.position += moveDirection * Time.deltaTime * moveSpeed;
    }
}
