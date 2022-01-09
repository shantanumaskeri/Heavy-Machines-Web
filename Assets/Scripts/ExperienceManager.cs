using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ExperienceManager : MonoBehaviour 
{
	// Singleton instance
	public static ExperienceManager Instance;

	// public variables
	public bool isAnimating;
	public bool isRotating;
	public bool isInside;
	public bool isGeared;
	public GameObject interiorPosition;
	public GameObject exteriorHotspot;
	public GameObject interiorHotspot;
	public GameObject trackHotspot;
	public GameObject armHotspot;
	public GameObject gearHotspot;
	public GameObject lcmeHotspot;
	public GameObject lcmiHotspot;
	public GameObject machine360Hotspot;
	public GameObject leadingOutputHotspot;
	public GameObject exteriorHotspotName;
	public GameObject interiorHotspotName;
	public GameObject trackHotspotName;
	public GameObject armHotspotName;
	public GameObject gearHotspotName;
	public GameObject lcmeHotspotName;
	public GameObject lcmiHotspotName;
	public GameObject machine360HotspotName;
	public GameObject leadingOutputHotspotName;
	public GameObject armInfo;
	public GameObject lcmeInfo;
	public GameObject trackInfo;
	public GameObject lcmiInfo;
	public GameObject leadingOutputInfo;
	public GameObject excavatorContainer;
	public GameObject secondSelection;
	public GameObject mainExperience;
	public Transform ovrPlayerController;
	public Transform cameraEyeAnchor;

	// private variables
	bool isActive = true;
	Vector3 initialPosition;

	// Use this for initialization
	void Start () 
	{
		Instance = this;

		isAnimating = false;
		isRotating = false;
		isInside = false;
		isGeared = false;

		exteriorHotspot.SetActive (false);
		gearHotspot.SetActive (false);
		lcmiHotspot.SetActive (false);
		leadingOutputHotspot.SetActive (false);
		exteriorHotspotName.SetActive (false);
		gearHotspotName.SetActive (false);
		lcmiHotspotName.SetActive (false);
		leadingOutputHotspotName.SetActive (false);

		initialPosition = ovrPlayerController.position;
	}

	void OnEnable ()
	{
		isActive = true;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ExecuteAction (GameObject go)
	{
		switch (go.name)
		{
		case "Hotspot Arm":
		case "Hotspot Arm Name":
		case "Information Arm":
			SwitchVisibility (armInfo);
			break;

		case "Hotspot LCME":
		case "Hotspot LCME Name":
		case "Information LCME":
			SwitchVisibility (lcmeInfo);

			trackInfo.SetActive (false);
			break;

		case "Hotspot LCMI":
		case "Hotspot LCMI Name":
		case "Information LCMI":
			SwitchVisibility (lcmiInfo);

			leadingOutputInfo.SetActive (false);
			break;

		case "Hotspot Track":
		case "Hotspot Track Name":
		case "Information Track":
			SwitchVisibility (trackInfo);

			lcmeInfo.SetActive (false);
			break;

		case "Hotspot Leading Output":
		case "Hotspot Leading Output Name":
		case "Information Leading Output":
			SwitchVisibility (leadingOutputInfo);

			lcmiInfo.SetActive (false);
			break;

		case "Hotspot 360":
		case "Hotspot 360 Base":
		case "Hotspot 360 Name":
			isRotating = true;

			lcmiInfo.SetActive (false);
			lcmeInfo.SetActive (false);
			armInfo.SetActive (false);
			trackInfo.SetActive (false);
			trackHotspot.SetActive (false);
			lcmeHotspot.SetActive (false);
			armHotspot.SetActive (false);
			interiorHotspot.SetActive (false);
			trackHotspotName.SetActive (false);
			lcmeHotspotName.SetActive (false);
			armHotspotName.SetActive (false);
			interiorHotspotName.SetActive (false);
			break;

		case "Hotspot Interior":
		case "Hotspot Interior Name":
			ShiftToInterior ();
			break;
	
		case "Hotspot Exterior":
		case "Hotspot Exterior Name":
			ShiftToExterior ();
			break;

		case "Hotspot Gear":
		case "Hotspot Gear Name":
			if (isInside)
			{
				isGeared = true;

				gearHotspot.SetActive (false);
				gearHotspotName.SetActive (false);
				lcmiInfo.SetActive (false);
				leadingOutputInfo.SetActive (false);
			}
			break;

		case "Menu Back Button":
			if (!isInside)
			{
				InputManager.Instance.ResetExperience ();
				ApplicationManager.Instance.BackToTerrainSelection ();	
			}
			break;
		}
	}

	void SwitchVisibility (GameObject go)
	{
		if (isActive)
		{
			if (!go.activeSelf)
			{
				go.SetActive (true);
			}
			else
			{
				go.SetActive (false);
			}

			isActive = false;

			Invoke ("DelayActivate", 1.0f);
		}
	}

	void DelayActivate ()
	{
		isActive = true;
	}

	void ShiftToInterior ()
	{
		if (!isInside)
		{
			ovrPlayerController.GetComponent<CharacterController>().enabled = false;
			ovrPlayerController.parent = excavatorContainer.transform;
			ovrPlayerController.position = interiorPosition.transform.position;
			ovrPlayerController.localEulerAngles = new Vector3 (0.0f, 264.0f, 0.0f);

			gearHotspot.SetActive (true);
			lcmiHotspot.SetActive (true);
			leadingOutputHotspot.SetActive (true);
			exteriorHotspot.SetActive (true);
			armHotspot.SetActive (false);
			interiorHotspot.SetActive (false);
			trackHotspot.SetActive (false);
			machine360Hotspot.SetActive (false);
			gearHotspotName.SetActive (true);
			lcmiHotspotName.SetActive (true);
			leadingOutputHotspotName.SetActive (true);
			exteriorHotspotName.SetActive (true);
			armHotspotName.SetActive (false);
			interiorHotspotName.SetActive (false);
			trackHotspotName.SetActive (false);
			machine360HotspotName.SetActive (false);
			armInfo.SetActive (false);
			trackInfo.SetActive (false);
			lcmeInfo.SetActive (false);
			lcmiInfo.SetActive (false);
			leadingOutputInfo.SetActive (false);

			isInside = true;

			ApplicationManager.Instance.isMoveAllowed = false;
		}
	}

	void ShiftToExterior ()
	{
		if (isInside)
		{
			ovrPlayerController.GetComponent<CharacterController>().enabled = true;
			ovrPlayerController.parent = null;
			ovrPlayerController.position = initialPosition;
			ovrPlayerController.localEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);

			gearHotspot.SetActive (false);
			lcmiHotspot.SetActive (false);
			leadingOutputHotspot.SetActive (false);
			exteriorHotspot.SetActive (false);
			armHotspot.SetActive (true);
			interiorHotspot.SetActive (true);
			trackHotspot.SetActive (true);
			machine360Hotspot.SetActive (true);
			gearHotspotName.SetActive (false);
			lcmiHotspotName.SetActive (false);
			leadingOutputHotspotName.SetActive (false);
			exteriorHotspotName.SetActive (false);
			armHotspotName.SetActive (true);
			interiorHotspotName.SetActive (true);
			trackHotspotName.SetActive (true);
			machine360HotspotName.SetActive (true);
			armInfo.SetActive (false);
			trackInfo.SetActive (false);
			lcmeInfo.SetActive (false);

			isInside = false;

			ApplicationManager.Instance.isMoveAllowed = true;
		}
	}
}
