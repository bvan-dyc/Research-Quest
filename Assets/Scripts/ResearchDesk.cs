using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResearchDesk : MonoBehaviour
{
	[SerializeField] private Slider researchSlider = null;
	[SerializeField] private float researchSpeed = 3;
	public float researchToNextPill = 100;
	[SerializeField] private ParticleSystem researchParticles = null;
	[SerializeField] private HintManager hintManager = null;
	[SerializeField] [TextArea] private string researchHintString = "helper default string";
	private bool initialActivation = false;
	[SerializeField] private float successMult = 1.5f;
	private float nextDiscoverTime;
	private bool isResearching = false;
	public int nClues = 0;
	private float totalResearch = 0;
	private void Update()
	{
		if (isResearching)
		{
			totalResearch += researchSpeed * Time.deltaTime;
			if (totalResearch >= researchToNextPill)
			{
				nClues++;
				totalResearch = 0;
				researchToNextPill *= successMult;
				researchSlider.maxValue = researchToNextPill;
			}
			researchSlider.value = totalResearch;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Patient")
		{
			isResearching = true;
			researchParticles.Play();
			if (initialActivation == false)
			{
				hintManager.displayHint(researchHintString);
				initialActivation = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Patient")
		{
			isResearching = false;
			researchParticles.Stop();
		}
	}
}
