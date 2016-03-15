using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent, ExecuteInEditMode]
public class UIController : MonoBehaviour
{
	[SerializeField]
	private UIManager.UIControllerID _controllerId = UIManager.UIControllerID.None;
	public UIManager.UIControllerID ID
	{
		get { return _controllerId; }
		set { }
	}
	protected GameObject Components;

	public virtual void Init()
	{
		Components = gameObject;
	}

	protected virtual bool IsActive()
	{
		return (this.enabled && Components.activeInHierarchy);
	}

	public virtual void Show()
	{
		if (this.IsActive()) return;
		Components.SetActive(true);
	}

	public virtual void Hide()
	{
		if (!this.IsActive()) return;
		Components.SetActive(false);
	}
}
