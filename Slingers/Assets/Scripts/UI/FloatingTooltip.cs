using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingTooltip : MonoBehaviour
{
	private GameObject tooltipTextObject;
	[HideInInspector] public Text tooltipText;
	private BoxCollider2D triggerArea;

	// Use this for initialization
	void Start()
	{
		tooltipText = transform.GetChild(0).GetComponentInChildren<Text>();
		if (tooltipText != null)
		{
			tooltipTextObject = tooltipText.gameObject;
			tooltipTextObject.SetActive(false);
		}
		else
		{
			Debug.LogWarning("No tooltip text object on " + transform.root.name);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			if (tooltipTextObject != null) tooltipTextObject.SetActive(true);
		}
	}

	void OnTriggerExit2D()
	{
		if (tooltipTextObject != null) tooltipTextObject.SetActive(false);
	}
}
