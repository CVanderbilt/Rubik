using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikMaster : MonoBehaviour
{
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

	public RubikMaster.color GetColorFromVector(Vector3 v)
	{
		for (int i = 0; i < centers.Length; i++)
			if (centers[i].transform.forward == v)
				return (centers[i].color);
		return (centers[0].color);

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

	public void Rotate(RubikMaster.color c, int r)
	{
		Center center = centers[(int)c];
		int count = 0;
		Vector3 vFace = center.transform.forward;

		for (int i = 0; i < squares.Length; i++)
		{
			Square s = squares[i];
			
			if (s.IsInFace(vFace))
			{
				s.transform.parent = center.transform;
				//s.transform.SetParent(center.transform);
				count++;
			}

			if (count == 8)
			{
				Debug.Log("sale bien");
				break ;
			}
		}

		Rotations rot = center.GetComponent<Rotations>();
		if (rot != null)
			rot.StartRotation(r);

	}

	private void Start() {
		Rotations.rotSpeed = rotSpeed;
		inputManager = GetComponent<InputManager>();
		if (!inputManager)
			Debug.Log("Input manager missing");
	}
	private void Update() {
		Rotations.rotSpeed = rotSpeed;
		if (Input.GetKey(KeyCode.Space))
		{
			Rotate(RubikMaster.color.BLUE, 1);
		}
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
