using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
	public PlayerController character;
	public CameraController camController;
	public GameManager gameController;
	public bool canControl = true;
    void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    void FixedUpdate()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		//cheatcode
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			gameController.switchToNextIdentity();
		}
		if (Input.GetMouseButtonDown(0))
		{
			character.CharacterAction();
		}
		if (canControl)
		{
			camController.LookAtMouse(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
			character.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
		}
    }

	public void enableControl()
	{
		canControl = true;
	}

	public void setCharacter(PlayerController newCharacter, CameraController newCam)
	{
		character = newCharacter;
		camController = newCam;
	}

	public void disableControl()
	{
		canControl = false;
	}
}
