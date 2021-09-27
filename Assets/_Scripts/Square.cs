using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
	private bool running = false;
	[System.Serializable]
	public struct Vc{
		public RubikMaster.color c;
		public GameObject o;
	}

	public RubikMaster master;

	public Vc[] vc;
	private int faces = 0;

	public void SetFace(RubikMaster.color c, Vector3 v)
	{
		if (faces >= this.vc.Length)
			return ;
		vc[faces].c = c;
		vc[faces].o = new GameObject(master.GetName(c));
		vc[faces].o.transform.forward = v;
		vc[faces].o.transform.parent = transform;
		faces++;
	}
	private void Start() {
		running = true;
	}

	private void OnDrawGizmos() {
		if (!running)
			return ;
		for (int i = 0; i < vc.Length; i++)
			Debug.DrawRay(transform.position, vc[i].o.transform.forward, master.GetMaterial(vc[i].c).color);
	}

	public bool IsInFace(Vector3 vFace)
	{
		for (int i = 0; i < vc.Length; i++)
		{
			if (vc[i].o.transform.forward == vFace)
				return (true);
		}
		return (false);
	}
}
