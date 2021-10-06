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
			step *= direction;
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
			}
		}
	}

	private void RotX(int d)
	{
		for (int i = 0; i < 6; i++)
			switch (master.centers[i].face)
			{
				case RubikMaster.face.F: master.centers[i].face = d > 0 ?
					RubikMaster.face.U : RubikMaster.face.D; break ;
				case RubikMaster.face.U: master.centers[i].face = d > 0 ?
					RubikMaster.face.B : RubikMaster.face.F; break ;
				case RubikMaster.face.B: master.centers[i].face = d > 0 ?
					RubikMaster.face.D : RubikMaster.face.U; break ;
				case RubikMaster.face.D: master.centers[i].face = d > 0 ?
					RubikMaster.face.F : RubikMaster.face.B; break ;
				default: break ;
			}
	}

	private void RotY(int d)
	{
		for (int i = 0; i < 6; i++)
			switch (master.centers[i].face)
			{
				case RubikMaster.face.F: master.centers[i].face = d > 0 ?
					RubikMaster.face.L : RubikMaster.face.R; break ;
				case RubikMaster.face.L: master.centers[i].face = d > 0 ?
					RubikMaster.face.B : RubikMaster.face.F; break ;
				case RubikMaster.face.B: master.centers[i].face = d > 0 ?
					RubikMaster.face.R : RubikMaster.face.L; break ;
				case RubikMaster.face.R: master.centers[i].face = d > 0 ?
					RubikMaster.face.F : RubikMaster.face.B; break ;
				default: break ;
			}
	}

	private void RotZ(int d)
	{
		for (int i = 0; i < 6; i++)
			switch (master.centers[i].face)
			{
				case RubikMaster.face.U: master.centers[i].face = d > 0 ?
					RubikMaster.face.R : RubikMaster.face.L; break ;
				case RubikMaster.face.L: master.centers[i].face = d > 0 ?
					RubikMaster.face.U : RubikMaster.face.D; break ;
				case RubikMaster.face.D: master.centers[i].face = d > 0 ?
					RubikMaster.face.L : RubikMaster.face.R; break ;
				case RubikMaster.face.R: master.centers[i].face = d > 0 ?
					RubikMaster.face.D : RubikMaster.face.U; break ;
				default: break ;
			}
	}

	/*
	*	Rota el cubo, pero los vectores deberían actualizarse al principio del movimiento.
	*	Según la m sabemos que tipo de movimiento es y por lo tanto que vectores actualizar.
	*/
	public void StartRotation(RubikMaster.movs m, int n)
	{
		if (rotating != 0)
			return ;

		switch (m)
		{
			case RubikMaster.movs.x: RotX(n); break ;
			case RubikMaster.movs.y: RotY(n); break ;
			default: RotZ(n); break ;
		}

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
					master.Rotate(RubikMaster.face.U, n);
					break ;
				case RubikMaster.movs.F:
					master.Rotate(RubikMaster.face.F, n);
					break ;
				case RubikMaster.movs.B:
					master.Rotate(RubikMaster.face.B, n);
					break ;
				case RubikMaster.movs.D:
					master.Rotate(RubikMaster.face.D, n);
					break ;
				case RubikMaster.movs.R:
					master.Rotate(RubikMaster.face.R, n);
					break ;
				case RubikMaster.movs.L:
					master.Rotate(RubikMaster.face.L, n);
					break ;
				default:
					Debug.Log("Movement not implemented");
					break ;
			}
	}
}
