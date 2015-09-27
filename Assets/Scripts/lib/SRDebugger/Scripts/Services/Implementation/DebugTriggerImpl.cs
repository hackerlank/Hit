﻿using System;
using SRDebugger.Internal;
using SRDebugger.UI.Other;
using SRF;
using SRF.Service;
using UnityEngine;

namespace SRDebugger.Services.Implementation
{

	[Service(typeof(IDebugTriggerService))]
	public class DebugTriggerImpl : SRServiceBase<IDebugTriggerService>, IDebugTriggerService
	{

		public bool IsEnabled
		{
			get { return _trigger != null && _trigger.CachedGameObject.activeSelf; }
			set
			{

				// Create trigger if it does not yet exist
				if (value && _trigger == null) {

					CreateTrigger();

				}

				if (_trigger != null)
					_trigger.CachedGameObject.SetActive(value);

			}
		}

		public Settings.TriggerPositions Position
		{
			get { return _position; }
			set
			{

				if (_trigger != null) {
					SetTriggerPosition(_trigger.TriggerTransform, value);
				}

				_position = value;

			}
		}

		private Settings.TriggerPositions _position;
		private TriggerRoot _trigger;

		protected override void Awake()
		{

			base.Awake();
			DontDestroyOnLoad(CachedGameObject);

			CachedTransform.SetParent(Hierarchy.Get("SRDebugger"), true);

			name = "Trigger";

		}

		private void CreateTrigger()
		{

			var prefab = Resources.Load<TriggerRoot>(SRDebugPaths.TriggerPrefabPath);

			if (prefab == null) {
				Debug.LogError("[SRDebugger] Error loading trigger prefab");
				return;
			}

			_trigger = SRInstantiate.Instantiate(prefab);
			_trigger.CachedTransform.SetParent(CachedTransform, true);

			SetTriggerPosition(_trigger.TriggerTransform, _position);

			switch (Settings.Instance.TriggerBehaviour) {
					
				case Settings.TriggerBehaviours.TripleTap: {

					_trigger.TripleTapButton.onClick.AddListener(OnTriggerButtonClick);
					_trigger.TapHoldButton.gameObject.SetActive(false);

					break;
				}

				case Settings.TriggerBehaviours.TapAndHold: {

					_trigger.TapHoldButton.onLongPress.AddListener(OnTriggerButtonClick);
					_trigger.TripleTapButton.gameObject.SetActive(false);

					break;

				}

				default:
					throw new Exception("Unhandled TriggerBehaviour");

			}


			SRDebuggerUtil.EnsureEventSystemExists();

		}

		private void OnLevelWasLoaded(int level)
		{

			Internal.SRDebuggerUtil.EnsureEventSystemExists();

		}

		private void OnTriggerButtonClick()
		{

			SRDebug.Instance.ShowDebugPanel();

		}

		private static void SetTriggerPosition(RectTransform t, Settings.TriggerPositions position)
		{

			var pivotX = 0f;
			var pivotY = 0f;

			var posX = 0f;
			var posY = 0f;

			if (position == Settings.TriggerPositions.TopLeft || position == Settings.TriggerPositions.TopRight) {
				pivotY = 1f;
				posY = 1f;
			} else if (position == Settings.TriggerPositions.BottomLeft || position == Settings.TriggerPositions.BottomRight) {
				pivotY = 0f;
				posY = 0f;
			}

			if (position == Settings.TriggerPositions.TopLeft || position == Settings.TriggerPositions.BottomLeft) {
				pivotX = 0f;
				posX = 0f;
			} else if (position == Settings.TriggerPositions.TopRight || position == Settings.TriggerPositions.BottomRight) {
				pivotX = 1f;
				posX = 1f;
			}

			t.pivot = new Vector2(pivotX, pivotY);
			t.anchorMax = t.anchorMin = new Vector2(posX, posY);

		}

	}

}