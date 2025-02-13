using System.Collections.Generic;
using System.Linq;
using MooshieGames.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace MooshieGames.Common.UI
{
	public class TabGroup : VisualElement
	{
		private List<VisualElement> _tabs = new();
		private VisualElement _selectionContainer;
		private VisualElement _tabContainer;

		private int _currentTabIndex;

		public TabGroup()
		{
			_selectionContainer = this.CreateChild("selection-container");
			_tabContainer = this.CreateChild("tab-container");
		}

		public TabGroup AddTab(VisualElement tab, string tabName)
		{
			_tabContainer.Add(tab);
			_tabs.Add(tab);
			tab.AddToClassList("tab");
			var index = _tabs.Count - 1;
			CreateTabButton(index, tabName);
			tab.SetVisible(index == _currentTabIndex);

			return this;
		}

		private void CreateTabButton(int index, string tabName)
		{
			var btn = _selectionContainer.CreateChild<Button>("tab-button", $"tab-button-{index}").SetText(tabName);
			btn.clicked += () => HandleButtonClicked(index);

			btn.CreateChild("overlay");

			if (index != _currentTabIndex) return;

			btn.AddToClassList("active");
			btn.AddToClassList("active-" + index);
		}

		private void HandleButtonClicked(int index)
		{
			if (index == _currentTabIndex) return;

			var children = _selectionContainer.Children().ToArray();

			children[_currentTabIndex].RemoveFromClassList("active");
			children[_currentTabIndex].RemoveFromClassList("active-" + _currentTabIndex);
			children[index].AddToClassList("active");
			children[index].AddToClassList("active-" + index);

			_currentTabIndex = index;

			for (var i = 0; i < _tabs.Count; i++)
			{
				_tabs[i].SetVisible(_currentTabIndex == i);
			}
		}
	}
}