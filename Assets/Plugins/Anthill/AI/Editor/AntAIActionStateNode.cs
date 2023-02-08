namespace Anthill.AI
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	/// <summary>
	/// This class implements a card of the plan that can be builded from the world state card
	/// in the AIWorkbench.
	/// </summary>
	public class AntAIActionStateNode : AntAIBaseNode
	{
		#region Variables

		private struct Item
		{
			public string name;
			public bool value;
			public bool isChanged;
		}

		private const float LINE_HEIGHT = 20.0f;
		private List<Item> _items;
		private GUIStyle _badgeStyle;
		private GUIStyle _labelStyle;
		private GUIStyle _boldLabelStyle;

		#endregion
		#region Public Methods

		public AntAIActionStateNode(Vector2 aPosition, float aWidth, float aHeight,
			GUIStyle aDefaultStyle, GUIStyle aSelectedStyle) : base(aPosition, aWidth, aHeight, aDefaultStyle, aSelectedStyle)
		{
			_badgeStyle = new GUIStyle("CN CountBadge");
 			_badgeStyle.normal.textColor = Color.white;
			_badgeStyle.active.textColor = Color.white;
			_badgeStyle.focused.textColor = Color.white;
			_badgeStyle.hover.textColor = Color.white;

			_labelStyle = new GUIStyle(EditorStyles.label);
			var m = _labelStyle.margin;
			m.top = 0;
			_labelStyle.margin = m;

			_boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
			m = _boldLabelStyle.margin;
			m.top = 0;
			_boldLabelStyle.margin = m;
		}

		public void BindData(string aTitle, AntAIPlanner aPlanner, AntAICondition aCur, AntAICondition aPre)
		{
			title = string.Concat("▶ ", aTitle);
			
			_items = new List<Item>();
			bool v;
			for (int i = 0, n = AntAIPlanner.MAX_ATOMS; i < n; i++)
			{
				if (aCur.GetMask(i))
				{
					v = aCur.GetValue(i);
					_items.Add(new Item
					{
						name = aPlanner.atoms[i],
						value = v,
						isChanged = (v != aPre.GetValue(i))
					});
				}
			}
		}

		public override void Draw()
		{
			rect.height = (_items.Count > 0)
				? LINE_HEIGHT * _items.Count
				: LINE_HEIGHT;
			rect.height += 52.0f;
			GUI.Box(rect, "", currentStyle);
			
			// Title
			GUI.Label(new Rect(rect.x + 12.0f, rect.y + 12.0f, rect.y + 12.0f, rect.width - 24.0f), title, _titleStyle);

			content.x = rect.x + 7.0f;
			content.y = rect.y + 30.0f;
			content.width = rect.width - 14.0f;
			content.height = rect.height - 50.0f;
			GUI.Box(content, "", _bodyStyle);

#if UNITY_2019_3_OR_NEWER
			content.y += 3.0f;
#else
			content.y += 1.0f;
#endif

			content.height = (_items.Count > 0)
				? LINE_HEIGHT * _items.Count
				: LINE_HEIGHT;

			var c = GUI.color;
			GUILayout.BeginArea(content);
			{
				EditorGUIUtility.labelWidth = 80.0f;
				if (_items.Count > 0)
				{
					GUILayout.BeginVertical();
					{
						for (int i = 0, n = _items.Count; i < n; i++)
						{
#if UNITY_2019_3_OR_NEWER
							GUILayout.BeginHorizontal();
#else
							GUILayout.BeginHorizontal("Icon.ClipSelected");
#endif
							{
								GUILayout.Space(4.0f);
								GUI.color = c * ((_items[i].value) 
									? new Color(0.5f, 1.0f, 0.5f) // green
									: new Color(1.0f, 0.5f, 0.5f) // red
								);
								
								GUILayout.Button(AntAIWorkbench.BoolToStr(_items[i].value), _badgeStyle, GUILayout.MaxWidth(20.0f), GUILayout.MaxHeight(20.0f));
								GUI.color = c;
								
								if (_items[i].isChanged)
								{

#if UNITY_2019_3_OR_NEWER
									GUILayout.Label(string.Concat("▶ ", _items[i].name), _boldLabelStyle);
#else
									GUILayout.Label(string.Concat("▶ ", _items[i].name));
#endif
									GUILayout.FlexibleSpace();
									GUILayout.BeginVertical();
									{
										GUILayout.Space(2.0f);
										GUILayout.Label((_items[i].value) ? "YES" : "NO", GUI.skin.FindStyle("AssetLabel"));
									}
									GUILayout.EndVertical();
								}
								else
								{
#if UNITY_2019_3_OR_NEWER
									GUILayout.Label(_items[i].name, _labelStyle);
#else
									GUILayout.Label(_items[i].name);
#endif
								}
							}
							GUILayout.EndHorizontal();
						}
					}
					GUILayout.EndVertical();
				}
				else
				{
					GUILayout.Label("<No Coditions>", EditorStyles.centeredGreyMiniLabel);
				}
			}
			GUILayout.EndArea();
		}

		#endregion
	}
}