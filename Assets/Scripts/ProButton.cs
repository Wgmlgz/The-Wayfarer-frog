using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.Events;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ProButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
		public bool state;
		public UnityEvent stEV;
		public UnityEvent whileEV;
		public UnityEvent endEV;
		public void OnPointerDown(PointerEventData data)
		{
			stEV.Invoke();
			state = true;
		}
		public void OnPointerUp(PointerEventData data)
		{
			endEV.Invoke();
			state = false;
		}
		private void Update()
		{
			whileEV.Invoke();
		}
	}
}