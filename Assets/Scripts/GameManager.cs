using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
	public playerIdentity cState = playerIdentity.patient;
	private float nextSwitchTimer = 0;
	[SerializeField] private int totalSwitches = 0;
	[SerializeField] private int switchesBeforeVictory = 4;
	[SerializeField] private float switchDelay = 60;
	[SerializeField] private UserInput userInput = null;
	private PlayerController patient;
	private PlayerController researcher;
	[SerializeField] private GameObject patientHUD = null;
	[SerializeField] private AudioSource mainAs = null;
	[SerializeField] [TextArea] private string patientIntroductoryString = "patient default substring";
	[SerializeField] [TextArea] private string researcherIntroductoryString = "researcher default substring";
	[SerializeField] [TextArea] private string patientReturnString = "patient return default substring";
	[SerializeField] private HintManager hintManager = null;
	[SerializeField] private AudioClip patientAudioClip = null;
	[SerializeField] private AudioClip researcherAudioClip = null;
	[SerializeField] private GameStateManager gameState = null;
	[SerializeField] private GameObject researcherHUD = null;
	private Camera patientCamera;
	private Camera researcherCamera;
	private Cauldron cauldron;
	private ResearchDesk rDesk;
	[SerializeField] private Color patientColor = Color.magenta;
	[SerializeField] private Color researcherColor = Color.blue;
	[SerializeField] private Image fadeScreen = null;
	[SerializeField] private Text timerText = null;

	public enum playerIdentity
	{
		researcher,
		doctor,
		patient
	}

	public void Start()
	{
		patient = GameObject.FindGameObjectWithTag("Patient").GetComponent<PlayerController>();
		researcher = GameObject.FindGameObjectWithTag("Researcher").GetComponent<PlayerController>();
		patientCamera = GameObject.FindGameObjectWithTag("PatientCamera").GetComponent<Camera>();
		researcherCamera = GameObject.FindGameObjectWithTag("ResearcherCamera").GetComponent<Camera>();
		cauldron = GameObject.FindGameObjectWithTag("Cauldron").GetComponent<Cauldron>();
		rDesk = GameObject.FindGameObjectWithTag("ResearchDesk").GetComponent<ResearchDesk>();
		nextSwitchTimer += switchDelay;
		hintManager.displayHint(patientIntroductoryString);
	}
	public void Update()
	{
		nextSwitchTimer -= Time.deltaTime;
		if (totalSwitches >= switchesBeforeVictory)
		{
			gameState.Victory();
		}
		if (nextSwitchTimer <= 0)
		{
			switchToNextIdentity();
		}
		timerText.text = Mathf.FloorToInt(nextSwitchTimer).ToString();
	}
	public void switchToNextIdentity()
	{
		nextSwitchTimer = switchDelay;
		StartCoroutine(UIExtensions.ImageFadeInAndOutRoutine(fadeScreen, 0.5f));
		totalSwitches++;
		if (cState == playerIdentity.researcher)
		{
			cState = playerIdentity.patient;
			Invoke("switchToPatient", 0.5f);
		}
		else if (cState == playerIdentity.patient)
		{
			cState = playerIdentity.researcher;
			Invoke("switchToResearcher", 0.5f);
		}
	}
	private void switchToPatient()
	{
			GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in ennemies)
			{
				enemy.GetComponent<SeekerAI>().UnFreeze();
			}
			userInput.setCharacter(patient, patientCamera.gameObject.GetComponent<CameraController>());
			researcher.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			patientCamera.enabled = true;
			researcherCamera.enabled = false;
			patientCamera.gameObject.GetComponent<AudioListener>().enabled = true;
			researcherCamera.gameObject.GetComponent<AudioListener>().enabled = false;
			mainAs.Stop();
			mainAs.clip = patientAudioClip;
			mainAs.Play();
		patient.GetComponent<Patient>().UnFreeze();
		patient.GetComponent<Damageable>().currentHealth = patient.GetComponent<Damageable>().maxHealth;
		patient.gameObject.GetComponent<Patient>().nPills = cauldron.completedPills * 2;
			cauldron.completedPills = 0;
		hintManager.displayHint(patientReturnString);
		patientReturnString = "";
		researcherHUD.SetActive(false);
			patientHUD.SetActive(true);
			RenderSettings.fogColor = patientColor;
		}

	private void switchToResearcher()
	{
			GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in ennemies)
			{
				enemy.GetComponent<SeekerAI>().Freeze();
			}
			userInput.setCharacter(researcher, researcherCamera.gameObject.GetComponent<CameraController>());
			patient.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			patientCamera.enabled = false;
			patientCamera.gameObject.GetComponent<AudioListener>().enabled = false;
			mainAs.Stop();
			mainAs.clip = researcherAudioClip;
			mainAs.Play();
		patient.GetComponent<Patient>().Freeze();
			researcherCamera.gameObject.GetComponent<AudioListener>().enabled = true;
			researcherCamera.enabled = true;
			cauldron.nClues = rDesk.nClues;
			cauldron.refreshCauldron();
			rDesk.nClues = 0;
		rDesk.researchToNextPill = 100;
			researcherHUD.SetActive(true);
			patientHUD.SetActive(false);
			hintManager.displayHint(researcherIntroductoryString);
		researcherIntroductoryString = "";
			RenderSettings.fogColor = researcherColor;
	}
}
