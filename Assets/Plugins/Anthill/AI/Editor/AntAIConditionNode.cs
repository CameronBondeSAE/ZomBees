namespace Anthill.AI
{
	using UnityEngine;
	using UnityEditor;

	using Anthill.Utils;

	/// <summary>
	/// This is implementation of the Conditions list in the AIWorkbench.
	/// </summary>
	public class AntAIConditionNode : AntAIBaseNode
	{
		#region Variables

		private AntAIScenario _scenario;
		private const float LINE_HEIGHT = 20.0f;

		#endregion
		#region Getters / Setters

		public AntAIScenario Scenario
		{
			get => _scenario;
			set
			{
				_scenario = value;
				rect.position = _scenario.conditions.position;
			}
		}

		#endregion
		#region Public Methods

		public AntAIConditionNode(Vector2 aPosition, float aWidth, float aHeight,
			GUIStyle aDefaultStyle, GUIStyle aSelectedStyle) : base(aPosition, aWidth, aHeight, aDefaultStyle, aSelectedStyle)
		{
			// ..
		}

		public override void Drag(Vector2 aDelta)
		{
			var oldPos = rect.position;
			base.Drag(aDelta);
			if (!AntMath.Equal(oldPos.x, rect.position.x) || 
				!AntMath.Equal(oldPos.y, rect.position.y))
			{
				_scenario.conditions.position = rect.position;
				EditorUtility.SetDirty(_scenario);
			}
		}

		public override void Draw()
		{
			if (_scenario == null)
			{
				return;
			}

			rect.height = (_scenario.conditions.list.Length > 0)
				? LINE_HEIGHT * _scenario.conditions.list.Length
				: LINE_HEIGHT;
#if UNITY_2019_3_OR_NEWER
			rect.height += 54.0f;
#else
			rect.height += 52.0f;
#endif
			GUI.Box(rect, "", currentStyle);
			
			// Title.
			GUI.Label(new Rect(rect.x + 12.0f, rect.y + 12.0f, rect.y + 12.0f, rect.width - 24.0f), title, _titleStyle);

			content.x = rect.x + 7.0f;
			content.y = rect.y + 30.0f;
			content.width = rect.width - 14.0f;
			content.height = rect.height - 50.0f;
			GUI.Box(content, "", _bodyStyle);

			var r = new Rect(rect.x + rect.width - 25.0f, rect.y + 11.0f, 20.0f, 44.0f);
			EditorGUI.BeginChangeCheck();
			GUILayout.BeginArea(r);
			if (GUILayout.Button("", "OL Plus", GUILayout.MaxWidth(16.0f), GUILayout.MaxHeight(16.0f)))
			{
				AntArray.Add(ref _scenario.conditions.list, new AntAIScenarioConditionItem
				{
					id = _scenario.conditions.list.Length,
					name = "<Unnamed>"
				});
			}
			GUILayout.EndArea();

			content.y += 1.0f;

			content.height = (_scenario.conditions.list.Length > 0)
				? LINE_HEIGHT * _scenario.conditions.list.Length
				: LINE_HEIGHT;

			GUILayout.BeginArea(content);
			{
				EditorGUIUtility.labelWidth = 80.0f;
				if (_scenario.conditions.list.Length > 0)
				{
					int delIndex = -1;
					GUILayout.BeginVertical();
					{
						for (int i = 0, n = _scenario.conditions.list.Length; i < n; i++)
						{
#if UNITY_2019_3_OR_NEWER
							GUILayout.BeginHorizontal();
#else
							GUILayout.BeginHorizontal("Icon.ClipSelected");
#endif
							{
								GUILayout.Space(4.0f);
								_scenario.conditions.list[i].name = EditorGUILayout.TextField(_scenario.conditions.list[i].name);
								if (GUILayout.Button("", "OL Minus", GUILayout.MaxWidth(18.0f), GUILayout.MinHeight(20.0f)))
								{
									delIndex = i;
								}
							}
							GUILayout.EndHorizontal();
						}
					}
					GUILayout.EndVertical();

					if (delIndex > -1)
					{
						UpdateConditionIndexes(delIndex);
						UpdateActionIndexes(delIndex);
						UpdateGoalIndexes(delIndex);
						UpdateWorldStates(delIndex);

						AntArray.RemoveAt(ref _scenario.conditions.list, delIndex);
					}
				}
				else
				{
					GUILayout.Label("<No Coditions>", EditorStyles.centeredGreyMiniLabel);
				}
			}
			GUILayout.EndArea();
			
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(_scenario);
			}
		}

		#endregion
		#region Private Methods
		
		private void UpdateConditionIndexes(int aDelIndex)
		{
			for (int i = _scenario.conditions.list.Length - 1; i >= 0; i--)
			{
				if (_scenario.conditions.list[i].id > aDelIndex)
				{
					_scenario.conditions.list[i].id -= 1;
				}
			}
		}

		private void UpdateActionIndexes(int aDelIndex)
		{
			for (int i = 0, n = _scenario.actions.Length; i < n; i++)
			{
				var action = _scenario.actions[i];

				// Update pre conditions for actions.
				for (int j = action.pre.Length - 1; j >= 0; j--)
				{
					if (action.pre[j].id == aDelIndex)
					{
						AntArray.RemoveAt(ref action.pre, j);
					}
					else
					{
						if (action.pre[j].id > aDelIndex)
						{
							action.pre[j].id -= 1;
						}
					}
				}

				// Update post conditions for actions.
				for (int j = action.post.Length - 1; j >= 0; j--)
				{
					if (action.post[j].id == aDelIndex)
					{
						AntArray.RemoveAt(ref action.post, j);
					}
					else
					{
						if (action.post[j].id > aDelIndex)
						{
							action.post[j].id -= 1;
						}
					}
				}
			}
		}

		private void UpdateGoalIndexes(int aDelIndex)
		{
			for (int i = 0, n = _scenario.goals.Length; i < n; i++)
			{
				var goal = _scenario.goals[i];

				// Update goal conditions.
				for (int j = goal.conditions.Length - 1; j >= 0; j--)
				{
					if (goal.conditions[j].id == aDelIndex)
					{
						AntArray.RemoveAt(ref goal.conditions, j);
					}
					else
					{
						if (goal.conditions[j].id > aDelIndex)
						{
							goal.conditions[j].id -= 1;
						}
					}
				}
			}
		}

		private void UpdateWorldStates(int aDelIndex)
		{
			for (int i = 0, n = _scenario.worldStates.Length; i < n; i++)
			{
				var state = _scenario.worldStates[i];

				// Update world state conditions.
				for (int j = state.list.Length - 1; j >= 0; j--)
				{
					if (state.list[j].id == aDelIndex)
					{
						AntArray.RemoveAt(ref state.list, j);
					}
					else
					{
						if (state.list[j].id > aDelIndex)
						{
							state.list[j].id -= 1;
						}
					}
				}
			}
		}
		
		#endregion
	}
}