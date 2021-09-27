using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
        // Start is called before the first frame update
	public RubikMaster master;
    private float rotating;
	private RubikMaster.movs movement;
	public float customRotSpeed = 0;
	public static float rotSpeed = 5;
	private int direction;
	private bool isHorizontalRot;

    // Update is called once per frame
    private void Update()
    {
        if (rotating != 0)
			ApplyRotation();
		
    }

	private void FixRotation()
	{
			Vector3 ea = transform.eulerAngles;

			int x = (int)ea.x;
			int y = (int)ea.y;
			int z = (int)ea.z;

			int mod;

			mod = x % 90;
			if (mod != 0)
			{
				if (mod > 45)
					x += (90 - mod);
				else
					x -= mod;
			}
			ea.x = (float)x;

			mod = y % 90;
			if (mod != 0)
			{
				if (mod > 45)
					y += (90 - mod);
				else
					y -= mod;
			}
			ea.y = (float)y;

			mod = z % 90;
			if (mod != 0)
			{
				if (mod > 45)
					z += (90 - mod);
				else
					z -= mod;
			}
			ea.z = (float)z;

			transform.eulerAngles = ea;
	}

	/*
	*	Rotacion no bloquante.
	*/
	private void ApplyRotation()
	{
		float speed = customRotSpeed == 0 ? rotSpeed : customRotSpeed;
		if (rotating > 0)
		{
			float step = Time.deltaTime * speed;
			step = step > rotating ? rotating : step;
			rotating -= step;
			Vector3 ea = transform.eulerAngles;

			//transform.rotation = Quaternion.Euler(step, 0, 0) * transform.rotation;

			switch (movement)
			{
				case RubikMaster.movs.x:
					transform.rotation = Quaternion.Euler(step, 0, 0) * transform.rotation; break ;
				case RubikMaster.movs.y:
					transform.rotation = Quaternion.Euler(0, step, 0) * transform.rotation; break ;
				default:
					transform.rotation = Quaternion.Euler(0, 0, -step) * transform.rotation; break ;
			}
			
			if (rotating == 0)
			{
				FixRotation();
				master.PrintInfo();
			}
		}
	}

	//tiene 6 rotaciones diferentes: left, right, up, down.(cambiando de cara)
	//								left, right (sin cambiar cara) eje z
	//como dependiendo de donde estemos mirando cambia la perspectiva
	//podemos pasar de parametros el color que estamos mirando y el color al que
	//queremos movernos, y la función deduce.
	//public void StartRotation(RubikMaster.color face, RubikMaster.color target)
	public void StartRotation(RubikMaster.movs m, int n)
	{

		if (rotating != 0)
			return ;

		movement = m;
		rotating = 90 * n;
		rotating *= rotating < 0 ? -1 : 1;
		direction = n > 0 ? 1 : -1;
	}

	public void StartMov(RubikMaster.movs m, int n)
	{
		//0, 0, 1 -> 0, 0 -1

		// 0  0 -1 cara principal F
		// 0  1  0 cara arriba U
		if (m == RubikMaster.movs.x || m == RubikMaster.movs.y || m == RubikMaster.movs.z)
			StartRotation(m, n);
		else
			switch (m)
			{
				case RubikMaster.movs.U:
					master.Rotate(master.GetColorFromVector(new Vector3(0, 1, 0)), 1);
					break ;
				default:
					Debug.Log("Movement not implemented");
					break ;
			}
	}
}
