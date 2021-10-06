using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotations : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotating;
	public float customRotSpeed = 0;
	public static float rotSpeed = 5;
	private int direction;

	public delegate void TestDelegate(); // This defines what type of method you're going to call.
	private TestDelegate toCall = null;
    // Update is called once per frame
    private void Update()
    {
        if (rotating != 0)
			ApplyRotation();
		
    }

	private void ApplyRotation()
	{
		float speed = customRotSpeed == 0 ? rotSpeed : customRotSpeed;
		if (rotating > 0)
		{
			float step = Time.deltaTime * speed;

			step = step > rotating ? rotating : step;
			rotating -= step;

			Vector3 ea = transform.eulerAngles;
			ea.z += step * direction;
			transform.eulerAngles = ea;
			if (rotating == 0)
			{
				int count = transform.childCount;
				for(int i = 0; i < count; i++)
				{
					Transform Go = transform.GetChild(0);
					Go.parent = transform.parent.parent;
				}
				if (toCall != null)
					toCall();
				toCall = null;
			}
		}
	}

	public bool StartRotation(int d, TestDelegate callOnMovementEnd)
	{
		if (rotating != 0)
			return false;
		toCall = callOnMovementEnd;
		rotating = 90;
		
		direction = d > 0 ? 1 : -1;
		return (true);
	}
}
