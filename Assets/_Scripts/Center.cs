using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    // Start is called before the first frame update
	public Square[] ads; //solo para inicialización
	public RubikMaster master;

	public RubikMaster.color color;

	private void Start() {
		transform.forward = master.GetVector(color);
		for (int i = 0; i < ads.Length; i++)
		{
			ads[i].SetFace(color, master.GetVector(color));
		}
	}

	private void Update() {

        // The step size is equal to speed times frame time.
		
	}
}
