using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Center : MonoBehaviour
{
    // Start is called before the first frame update
	public Square[] ads; //solo para inicialización
	public RubikMaster master;
	
	public RubikMaster.face face;

	public RubikMaster.color color;

	public Square[] neighbours = new Square[8];
	public int ncount = 8;
	private Rotations r;
	private bool isMoving = false;

	private void Start() {
		r = GetComponent<Rotations>();
		if (r == null)
			Debug.Log("Rotations component missing!!");
		//transform.forward = master.GetVector(color);
		/*for (int i = 0; i < ads.Length; i++)
		{
			ads[i].SetFace(color, master.GetVector(color));
		}*/
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Center")
			return ;
		ncount++;
		for (int i = 0; i < neighbours.Length; i++)
			if (neighbours[i] == null)
			{
				neighbours[i] = other.gameObject.GetComponent<Square>();
				break ;
			}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Center")
			return ;
		Square s = other.gameObject.GetComponent<Square>();
		ncount--;
		int i = Array.IndexOf(neighbours, s);
		if (i < 0){
			Debug.Log(Array.IndexOf(neighbours, s));
			Debug.Log(s.transform.name);
			Debug.Log(this.color);
			Debug.Break();
			return ;}
		//Debug.Log(Array.IndexOf(neighbours, s));
		neighbours[Array.IndexOf(neighbours, s)] = null;
	}

	//public TestDelegate m_methodToCall; // This is the variable holding the method you're going to call.
	public void StartRotation(int d)
	{
		int count = 0;
		if (isMoving)
			return ;
		for (int i = 0; i < neighbours.Length; i++)
			if (neighbours[i] != null && !neighbours[i].isMoving)
				count++;
	
		if (count != 8)
			return ;
		for (int i = 0; i < neighbours.Length; i++)
			if (neighbours[i] != null)
			{
				neighbours[i].transform.parent = transform;
				neighbours[i].isMoving = true;
			}

		if (!r.StartRotation(d, SetMovingFalse))
			return ;
		isMoving = true;
	}

	public void SetMovingFalse()
	{
		for (int i = 0; i < neighbours.Length; i++)
			if (neighbours[i] != null)
			{
				neighbours[i].transform.parent = transform.parent.parent;
				neighbours[i].isMoving = false;
			}
		isMoving = false;
	}
}
