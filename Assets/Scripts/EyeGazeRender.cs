using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class EyeGazeRender : MonoBehaviour 
{
	// Singleton instance
	public static EyeGazeRender Instance;

	// public variables
	public Transform cameraEyeAnchor;
	public GameObject mainSelection;
	public GameObject Crosshair;

	// private variables
	bool isActive;
	Renderer[] ra;

	// Use this for initialization
	void Awake () 
	{
		Instance = this;

		ra = mainSelection.GetComponentsInChildren<Renderer>();
	}

	void OnEnable ()
	{
		isActive = false;

		ResetColor ();
		Invoke ("DelayActivate", 1.0f);
	}

	public void ResetColor ()
	{
		foreach (Renderer r in ra)
		{
			r.gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
	}

	void DelayActivate ()
	{
		isActive = true;
	}

	// Update is called once per frame
	void Update () 
	{
		CheckCollisions ();	
	}

	void CheckCollisions ()
	{
		Ray ray = new Ray (cameraEyeAnchor.position, -cameraEyeAnchor.forward);
		RaycastHit hit = new RaycastHit ();

		for (int i = 0; i < ra.Length; i++) 
		{
			if (ra[i].gameObject.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
			{
				if (isActive)
				{
					if (Crosshair.activeSelf)
					{
						EyeGazeSelection.Instance.SetColorLerp (ra[i].gameObject);	
					}
					else
					{
						EyeGazeSelection.Instance.ResetColorLerp (ra[i].gameObject);	
					}
				}
			}
			else
			{
				EyeGazeSelection.Instance.ResetColorLerp (ra[i].gameObject);
			}
		}
	}
}
