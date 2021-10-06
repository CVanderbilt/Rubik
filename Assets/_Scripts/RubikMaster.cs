using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikMaster : MonoBehaviour
{
	public enum face{ F, R, B, L, U, D };
	public enum movs{
		U, //upper
		u, //double-layer U
		R, //right
		r, //double-layer R
		F, //front
		f, //double-layer F
		D, //down
		d, //double-layer D
		L, //left
		l, //double-layer L
		B, //back
		b, //double-layer B
		M, //middle
		E, //equator
		S, //side
		x, y, z
	};
    public enum color{
		WHITE, YELLOW,
		RED, ORANGE,
		BLUE, GREEN
	};

	private InputManager inputManager;
	private Vector3 vwhite = new Vector3(0, 1, 0);
	private Vector3 vyellow = new Vector3(0, -1, 0);
	private Vector3 vgreen = new Vector3(-1, 0, 0);
	private Vector3 vblue = new Vector3(1, 0, 0);
	private Vector3 vred = new Vector3(0, 0, -1);
	private Vector3 vorange = new Vector3(0, 0, 1);

	public Material[] mats;

	public Center[] centers;

	public Square[] squares;

	public float rotSpeed = 15;

	public Material GetMaterial(RubikMaster.color c)
	{
		return (mats[(int)c]);
	}

	public Vector3 GetVector(RubikMaster.color c)
	{
		switch (c)
		{
			case RubikMaster.color.WHITE: return (this.vwhite);
			case RubikMaster.color.YELLOW: return (this.vyellow);
			case RubikMaster.color.GREEN: return (this.vgreen);
			case RubikMaster.color.RED: return (this.vred);
			case RubikMaster.color.ORANGE: return (this.vorange);
			default: return (this.vblue);
		}
	}

	public string GetName(RubikMaster.color c)
	{
		switch (c)
		{
			case RubikMaster.color.WHITE: return ("w");
			case RubikMaster.color.YELLOW: return ("y");
			case RubikMaster.color.GREEN: return ("g");
			case RubikMaster.color.RED: return ("r");
			case RubikMaster.color.ORANGE: return ("o");
			default: return ("b");
		}
	}

	public void Rotate(RubikMaster.face f, int r)
	{
		for (int i = 0; i < 6; i++)
			if (centers[i].face == f)
			{
				centers[i].StartRotation(r);
				return ;
			}
	}

	private void Start() {
		Rotations.rotSpeed = rotSpeed;
		inputManager = GetComponent<InputManager>();
		if (!inputManager)
			Debug.Log("Input manager missing");
	}
	private void LateUpdate() {
		Rotations.rotSpeed = rotSpeed;
	}

	public void PrintInfo()
	{
		for (int i = 0; i < centers.Length; i++)
		{
			Debug.Log(centers[i].color);
			Debug.Log(centers[i].transform.forward);
			Debug.Log("-------------------");
		}
	}

}
