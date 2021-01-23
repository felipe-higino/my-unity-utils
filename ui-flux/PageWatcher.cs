using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public abstract class PageWatcher<T> : MonoBehaviour where T : PageWatcher<T>
{
	private void Awake()
	{
		canvas = GetComponent<Canvas>();
		HideIt();
		if (StartVisible && activePage == null)
			ShowIt();
	}

	//private void Start()
	//{
	//	if (activePage==null && StartVisible)
	//		ShowIt();
	//}

	[Header("Presets: ")]
	[SerializeField] bool StartVisible = false;

	Canvas canvas = null;

	static T activePage = null;
	public static T ActivePage { get => activePage; }
	T previousPageRef = null;

	public UnityEvent OnThisPageShow;
	public UnityEvent OnThisPageHide;


	void ShowIt()
	{
		activePage = this as T;
		//visual
		gameObject.SetActive(true);
		if (canvas) canvas.enabled = true;
		//event
		OnThisPageShow?.Invoke();
	}

	void HideIt()
	{
		//visual
		gameObject.SetActive(false);
		if (canvas) canvas.enabled = false;
		//event
		OnThisPageHide?.Invoke();
	}

	public void ShowThisPage()
	{
		if (activePage == this)
			return;

		if(activePage != null)
		{
			activePage.HideIt();
			previousPageRef = activePage;
		}
		this.ShowIt();
	}

	public void ShowPreviousPage()
	{
		if (previousPageRef == null)
			return;
		//hide and show
		this.HideIt();
		previousPageRef.ShowIt();
	}
}
