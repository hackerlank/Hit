﻿using System;

namespace SRDebugger.Services
{

	public interface IDebugPanelService
	{

		event Action<IDebugPanelService, bool> VisibilityChanged;

		/// <summary>
		/// Is the debug panel currently loaded into the scene
		/// </summary>
		bool IsLoaded { get; }

		/// <summary>
		/// Get or set whether the debug pane should be visible
		/// </summary>
		bool IsVisible { get; set; }

		/// <summary>
		/// Force the debug panel to unload from the scene
		/// </summary>
		void Unload();

		/// <summary>
		/// Open the given tab
		/// </summary>
		/// <param name="tab"></param>
		void OpenTab(DefaultTabs tab);

		/// <summary>
		/// Currently active tab (if available in DefaultTabs, otherwise null)
		/// </summary>
		DefaultTabs? ActiveTab { get; }

	}

}