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
		public float doubTime = 0.1f;
		public UnityEvent stEV;
		public UnityEvent whileEV;
		public UnityEvent endEV;
		public UnityEvent doubEV;

		private float fixedTimer;
		private float updTimer;

		private void FixedUpdate()
		{
			fixedTimer += Time.fixedDeltaTime;
		}
		private void Update()
		{
			updTimer += Time.deltaTime;
			if (state) whileEV.Invoke();
		}
		void ResetTimers()
		{
			fixedTimer = 0;
			updTimer = 0;
		}
		public void OnPointerDown(PointerEventData data)
		{
			stEV.Invoke();
			state = true;
			if(fixedTimer < doubTime || updTimer < doubTime)
			{
				doubEV.Invoke();
				ResetTimers();
			}
			else
			{
				ResetTimers();
			}
		}
		public void OnPointerUp(PointerEventData data)
		{
			endEV.Invoke();
			state = false;
		}
	}
}