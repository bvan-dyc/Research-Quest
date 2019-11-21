using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
	[SerializeField] private Text mainText = null;
	[SerializeField] private float reloadDelay = 5f;
	[SerializeField] private string lossString = "KEEP FIGHTING!";
	[SerializeField] private string victoryString = "PATHOLOGY DEFEATED!";
	[SerializeField] private UserInput inputManager = null;
	[SerializeField] private Image fadeScreen = null;
	[SerializeField] private Image victoryScreen = null;
	[SerializeField] private float fadeDuration = 1f;
	[SerializeField] private GameObject compareButton = null;
	private string currentText;
	[SerializeField] private string compareLink = null;
	public void LoadCompare()
	{
		Application.OpenURL(compareLink);
	}
	private void Start()
	{
		StartCoroutine(UIExtensions.ImageFadeOutRoutine(fadeScreen, fadeDuration * 2));
	}
	public void GameOver()
	{
		inputManager.canControl = false;
		StartCoroutine(ReloadRoutine(reloadDelay));
		StartCoroutine(UIExtensions.TypeTextRoutine(lossString, mainText, 0.2f));
		StartCoroutine(UIExtensions.ImageFadeInRoutine(fadeScreen, fadeDuration));
	}
	public void Victory()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		inputManager.canControl = false;
		StartCoroutine(UIExtensions.ImageFadeInRoutine(fadeScreen, fadeDuration));
		StartCoroutine(CompareRoutine(fadeDuration + 0.2f));
	}
	IEnumerator ReloadRoutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	IEnumerator CompareRoutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		compareButton.SetActive(true);
		StartCoroutine(UIExtensions.ImageFadeInRoutine(victoryScreen, fadeDuration));
	}
}
