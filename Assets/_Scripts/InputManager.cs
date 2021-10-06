using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour
{
	public CameraRubik cam;

	public int shifted = 1;
	public KeyCode movLeft = KeyCode.LeftArrow;
	private bool movLeftFlag = false;
	public KeyCode movRight = KeyCode.RightArrow;
	private bool movRightFlag = false;
	public KeyCode movUp = KeyCode.UpArrow;
	private bool movUpFlag = false;
	public KeyCode movDown = KeyCode.DownArrow;
	private bool movDownFlag = false;

	public CubeRotation cubeRotator;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftShift))
			shifted = -1;
		if (Input.GetKeyUp(KeyCode.LeftShift))
			shifted = 1;

		if (Input.GetKeyDown(KeyCode.X))
			cubeRotator.StartMov(RubikMaster.movs.x, shifted);
		if (Input.GetKeyDown(KeyCode.Y))
			cubeRotator.StartMov(RubikMaster.movs.y, shifted);
		if (Input.GetKeyDown(KeyCode.Z))
			cubeRotator.StartMov(RubikMaster.movs.z, shifted);

		if (Input.GetKeyDown(KeyCode.U))
			cubeRotator.StartMov(RubikMaster.movs.U, shifted);
		if (Input.GetKeyDown(KeyCode.D))
			cubeRotator.StartMov(RubikMaster.movs.D, shifted);
		if (Input.GetKeyDown(KeyCode.L))
			cubeRotator.StartMov(RubikMaster.movs.L, shifted);
		if (Input.GetKeyDown(KeyCode.R))
			cubeRotator.StartMov(RubikMaster.movs.R, shifted);
		if (Input.GetKeyDown(KeyCode.F))
			cubeRotator.StartMov(RubikMaster.movs.F, shifted);
		if (Input.GetKeyDown(KeyCode.B))
			cubeRotator.StartMov(RubikMaster.movs.B, shifted);

        if (Input.GetKeyDown(movLeft))
			movLeftFlag = true;
		if (Input.GetKeyDown(movRight))
			movRightFlag = true;
		if (Input.GetKeyDown(movUp))
			movUpFlag = true;
		if (Input.GetKeyDown(movDown))
			movDownFlag = true;

		if (Input.GetKeyUp(movLeft))
			movLeftFlag = false;
		if (Input.GetKeyUp(movRight))
			movRightFlag = false;
		if (Input.GetKeyUp(movUp))
			movUpFlag = false;
		if (Input.GetKeyUp(movDown))
			movDownFlag = false;

		cam.movingX = 0;
		cam.movingY = 0;
		if (movLeftFlag)
			cam.movingX--;
		if (movRightFlag)
			cam.movingX++;
		if (movUpFlag)
			cam.movingY++;
		if (movDownFlag)
			cam.movingY--;		
		
    }
}
