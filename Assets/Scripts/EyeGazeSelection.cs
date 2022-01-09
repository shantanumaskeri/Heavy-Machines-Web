using UnityEngine;
using System.Collections;

public class EyeGazeSelection : MonoBehaviour 
{
	// Singleton instance
	public static EyeGazeSelection Instance;

	// Use this for initialization
	void Start () 
	{
		Instance = this;
	}

	public void SetColorLerp (GameObject go)
	{
		Material mat = go.GetComponent<Renderer>().material;

		if (mat.color.r > 0.5f || mat.color.g > 0.5f || mat.color.b > 0.5f)
		{
			mat.color = Color.Lerp (mat.color, Color.black, 1.0f * Time.deltaTime);	
		}
			
		CheckLerpAndDoAction (go, mat);
	}

	public void ResetColorLerp (GameObject go)
	{
		Material mat = go.GetComponent<Renderer>().material;
		mat.color = Color.Lerp (mat.color, Color.white, 3.0f * Time.deltaTime);
	}

	void CheckLerpAndDoAction (GameObject go, Material mat)
	{
		if (mat.color.r < 0.5f || mat.color.g < 0.5f || mat.color.b < 0.5f)
		{
			switch (ApplicationManager.Instance.applicationState)
			{
			case "machineSelection":
				ApplicationManager.Instance.MoveToTerrainSelection ();
				break;

			case "terrainSelection":
				switch (go.name)
				{
				case "Selection 1":
				case "Selection 2":
				case "Selection 3":
					ApplicationManager.Instance.StartApplication ();
					break;

				case "Menu Back Button":
					ApplicationManager.Instance.BackToMachineSelection ();
					break;
				}
				break;

			case "experience":
				ExperienceManager.Instance.ExecuteAction (go);
				break;
			}
		}
	}
}
