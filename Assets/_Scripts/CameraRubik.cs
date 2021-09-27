using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRubik : MonoBehaviour
{
	public float sensitivity = 30;

	public Transform referencePoint;
	public int movingX;
	public int movingY;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (movingX != 0)
			transform.RotateAround(referencePoint.position, new Vector3(1, 0, 0), sensitivity * Time.deltaTime * movingX);

		if (movingY != 0)
			transform.RotateAround(referencePoint.position, new Vector3(0, 1, 0), sensitivity * Time.deltaTime * movingY);
    }
}
