namespace Anthill.AI
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;

	using UnityEngine;
	using UnityEditor;

	using Anthill.Utils;

	/// <summary>
	/// Main class of the AIWorkbench.
	/// </summary>
	public class AntAIWorkbench : EditorWindow
	{
		private struct LabelData
		{
			public string caption;
			public Rect rect;
			public GUIStyle style;
		}

		private struct Connection
		{
			public Vector2 from;
			public Vector2 to;
			public Vector2 center;
			public Vector2 arrowA;
			public Vector2 arrowB;
			public AntAIBaseNode fromNode;
			public AntAIBaseNode toNode;
		}

		#region Variables

		// ● ○
		internal const string _TRUE = "1"; //"▮";
		internal const string _FALSE = "0"; //"●";

		private List<AntAIBaseNode> _nodes;

		private GUIStyle _titleStyle;
		private GUIStyle _labelStyle;
		private GUIStyle _smallLabelStyle;
		private GUIStyle _nodeStyle;
		private GUIStyle _activeNodeStyle;
		private GUIStyle _actionStyle;
		private GUIStyle _activeActionStyle;
		private GUIStyle _failStyle;
		private GUIStyle _activeFailStyle;
		private GUIStyle _successStyle;
		private GUIStyle _activeSuccessStyle;

		private Vector2 _offset;
		private Vector2 _drag;
		private Vector2 _totalDrag;
		// private Vector2 _mousePosition;
		private bool _alignToGrid;

		private AntAIScenario _current;
		private AntAIScenario[] _scenarios;
		private List<Connection> _connections;
		private List<LabelData> _labels;

		private static Texture2D _aaLineTexture;
		private static Texture2D _lineTexture;
		private static Material _blitMaterial;
		private static Material _blendMaterial;
		private static Rect _lineRect = new Rect(0, 0, 1, 1);
		private static Matrix4x4 _matrixBackup;

		#endregion
		#region Getters / Setters

		public bool IsAlignToGrid
		{
			get => _alignToGrid;
		}

		public Vector2 Offset
		{
			get => _offset;
		}

		#endregion
		#region Initialize Window

		// [MenuItem("Tools/Anthill/AI Workbench")]
		public static AntAIWorkbench ShowWindow()
		{
			var window = (AntAIWorkbench) EditorWindow.GetWindow(typeof(AntAIWorkbench), false, "AI Workbench");
			window.autoRepaintOnSceneChange = true;
			return window;
		}

		public static void OpenScenario(string aName)
		{
			ShowWindow().SelectPresetHandler(aName);
		}

		public static T[] GetAllInstances<T>() where T : ScriptableObject
		{
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
			T[] a = new T[guids.Length];
			for(int i = 0, n = guids.Length; i < n; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[i]);
				a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
			}

			return a;
		}

		internal static string BoolToStr(bool aValue)
		{
			return (aValue) ? _TRUE : _FALSE;
		}

		#endregion
		#region Unit Calls

		private void OnEnable()
		{
			_titleStyle = new GUIStyle();
			_titleStyle.fontSize = 22;
			_titleStyle.normal.textColor = Color.gray;

			_labelStyle = new GUIStyle();
			_labelStyle.fontSize = 14;
			_labelStyle.normal.textColor = Color.gray;

			_smallLabelStyle = new GUIStyle();
			_smallLabelStyle.normal.textColor = Color.gray;

			_nodeStyle = CreateNodeStyle("node0.png");
			_activeNodeStyle = CreateNodeStyle("node0 on.png");
			_actionStyle = CreateNodeStyle("node5.png");
			_activeActionStyle = CreateNodeStyle("node5 on.png");
			_successStyle = CreateNodeStyle("node3.png");
			_activeSuccessStyle = CreateNodeStyle("node3 on.png");
			_failStyle = CreateNodeStyle("node6.png");
			_activeFailStyle = CreateNodeStyle("node6 on.png");
			_nodes = new List<AntAIBaseNode>();
			_connections = new List<Connection>();
			_labels = new List<LabelData>();
			_scenarios = GetAllInstances<AntAIScenario>();
		}

		private void OnInspectorUpdate()
		{
			if (Selection.objects.Length == 1 && Selection.objects[0] is AntAIScenario)
			{
				var tmp = (AntAIScenario) Selection.objects[0];
				if (tmp == null)
				{
					Repaint();
				}
				else if (!System.Object.ReferenceEquals(tmp, _current))
				{
					Repaint();
				}
			}
		}

		private void OnGUI()
		{
			Handles.DrawSolidRectangleWithOutline(
				new Rect(0.0f, 0.0f, position.width, position.height), 
				new Color(0.184f, 0.184f, 0.184f), 
				new Color(0.184f, 0.184f, 0.184f)
			);

			DrawGrid(20, Color.gray, 0.1f);
			DrawGrid(100, Color.gray, 0.1f);
			
			if (_current != null)
			{
				DrawLabels();
				DrawLinks();
				DrawNodes(_nodes);
				ProcessEvents(Event.current);
			}
			else
			{
				if (Event.current.type == EventType.Repaint)
				{
					GUI.Label(
						new Rect(10.0f, 10.0f, 200.0f, 50.0f), 
						"AI scenario not selected.",
						_titleStyle
					);

					GUI.Label(
						new Rect(20.0f, 50.0f, 200.0f, 50.0f),
						"Create new scenario:",
						_labelStyle
					);

					GUI.Label(
						new Rect(30.0f, 80.0f, 200.0f, 50.0f),
						"1. Open context menu in the Project window.",
						_smallLabelStyle
					);

					GUI.Label(
						new Rect(30.0f, 100.0f, 200.0f, 50.0f),
						"2. Select `Create > Anthill > AI Scenario`.",
						_smallLabelStyle
					);

					GUI.Label(
						new Rect(30.0f, 120.0f, 200.0f, 50.0f),
						"3. Enter the name and setup new AI behaviour.",
						_smallLabelStyle
					);
				}
			}

			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
			{
				string currentName = (_current != null) ? _current.name : "Open";
				if (GUILayout.Button(currentName, EditorStyles.toolbarDropDown))
				{
					_scenarios = GetAllInstances<AntAIScenario>();
					var menu = new GenericMenu();
					if (_scenarios != null && _scenarios.Length > 0)
					{
						for (int i = 0, n = _scenarios.Length; i < n; i++)
						{
							bool isSelected = (_current != null && _current.name.Equals(_scenarios[i].name));
							menu.AddItem(new GUIContent(_scenarios[i].name), isSelected, SelectPresetHandler, _scenarios[i].name);
						}
					}
					else
					{
						menu.AddDisabledItem(new GUIContent("<No AI Scenarios>"));
					}

					menu.DropDown(new Rect(0.0f, 12.0f, 0.0f, 0.0f));
				}

				if (_current != null)
				{
					if (GUILayout.Button("Conditions to Enum", EditorStyles.toolbarButton))
					{
						CopyConditionsAsEnum();
					}
				}

				GUILayout.FlexibleSpace();
			}
			EditorGUILayout.EndHorizontal();
		}

		#endregion
		#region Private Methods

		private void DrawLabels()
		{
			LabelData item;
			Rect rect = new Rect();
			for (int i = 0, n = _labels.Count; i < n; i++)
			{
				item = _labels[i];
				rect.x = item.rect.x + _totalDrag.x;
				rect.y = item.rect.y + _totalDrag.y;
				rect.width = item.rect.width;
				rect.height = item.rect.height;
				GUI.Label(rect, item.caption, item.style);
			}
		}

		private void DrawLinks()
		{
			AntAIBaseNode node;
			AntAIBaseNode toNode;
			Vector2 outPosition;
			Vector2 inPosition;

			_connections.Clear();
			for (int i = 0, n = _nodes.Count; i < n; i++)
			{
				node = _nodes[i];
				for (int j = 0, nj = node.links.Count; j < nj; j++)
				{
					toNode = node.links[j];
					outPosition = node.GetOutputPoint(toNode);
					inPosition = toNode.GetInputPoint(node);

					float dist = AntMath.Distance(outPosition, inPosition);
					float ang = AntMath.AngleRad(outPosition, inPosition);
					var pos = new Vector2(
						outPosition.x + dist * 0.5f * Mathf.Cos(ang),
						outPosition.y + dist * 0.5f * Mathf.Sin(ang));
					var posA = new Vector2(
						pos.x - 10f * Mathf.Cos(ang + 35.0f * Mathf.Deg2Rad),
						pos.y - 10f * Mathf.Sin(ang + 35.0f * Mathf.Deg2Rad));
					var posB = new Vector2(
						pos.x - 10f * Mathf.Cos(ang - 35.0f * Mathf.Deg2Rad),
						pos.y - 10f * Mathf.Sin(ang - 35.0f * Mathf.Deg2Rad));

					_connections.Add(
						new Connection 
						{
							from = outPosition,
							to = inPosition,
							center = pos,
							arrowA = posA,
							arrowB = posB,
							fromNode = node,
							toNode = toNode
						}
					);
				}
			}

			Connection con;
			Color c = Color.gray;
			for (int i = 0, n = _connections.Count; i < n; i++)
			{
				con = _connections[i];
				DrawSolidLine(con.from, con.to, c);
				DrawSolidLine(con.center, con.arrowA, c);
				DrawSolidLine(con.center, con.arrowB, c);
			}
		}

		private GUIStyle CreateNodeStyle(string aTextureName)
		{
			var style = new GUIStyle();
			style.normal.background = (EditorGUIUtility.isProSkin)
				? (Texture2D) EditorGUIUtility.Load($"builtin skins/darkskin/images/{aTextureName}")
				: (Texture2D) EditorGUIUtility.Load($"builtin skins/lightskin/images/{aTextureName}");
			style.border = new RectOffset(12, 12, 12, 12);
			style.richText = true;
			style.fontStyle = FontStyle.Bold;
			style.padding = new RectOffset(12, 0, 10, 0);
			style.normal.textColor = new Color(0.639f, 0.65f, 0.678f);
			return style;
		}

		private void SetCurrentScenario(AntAIScenario aScenario)
		{
			_current = aScenario;
			ClearNodes();
			_labels.Clear();
			if (_current == null) return;
			
			_nodes.Add(CreateConditionNode(_current));

			// Add All Actions.
			for (int i = 0, n = _current.actions.Length; i < n; i++)
			{
				_nodes.Add(CreateActionNode(_current, _current.actions[i]));
			}

			// Add All Goals.
			for (int i = 0, n = _current.goals.Length; i < n; i++)
			{
				_nodes.Add(CreateGoalNode(_current, _current.goals[i]));
			}

			// Add All World States.
			for (int i = 0, n = _current.worldStates.Length; i < n; i++)
			{
				_nodes.Add(CreateWorldStateNode(_current, _current.worldStates[i]));
			}
		}

		private void ClearNodes()
		{
			for (int i = 0, n = _nodes.Count; i < n; i++)
			{
				if (_nodes[i] is AntAIActionNode)
				{
					(_nodes[i] as AntAIActionNode).EventDelete -= DeleteActionHandler;
				}
				else if (_nodes[i] is AntAIGoalNode)
				{
					(_nodes[i] as AntAIGoalNode).EventDelete -= DeleteGoalHandler;	
				}
			}

			_nodes.Clear();
		}

		private void AddLabel(float aX, float aY, string aCaption, GUIStyle aStyle)
		{
			_labels.Add(new LabelData
			{
				caption = aCaption,
				rect = new Rect(aX, aY, 500.0f, 50.0f),
				style = aStyle
			});
		}

		private AntAIWorldStateNode CreateWorldStateNode(AntAIScenario aScenario, AntAIWorldState aWorldState)
		{
			var node = new AntAIWorldStateNode(aWorldState.position, 200.0f, 300.0f, _actionStyle, _activeActionStyle);
			node.EventDelete += DeleteWorldStateHandler;
			node.EventBuildPlan += BuildPlanHandler;
			node.EventClearPlan += ClearPlanHandler;
			node.title = "WORLD STATE";
			node.Scenario = aScenario;
			node.WorldState = aWorldState;
			return node;
		}

		private AntAIGoalNode CreateGoalNode(AntAIScenario aScenario, AntAIScenarioGoal aGoal)
		{
			var node = new AntAIGoalNode(aGoal.position, 200.0f, 300.0f, _nodeStyle, _activeNodeStyle);
			node.EventDelete += DeleteGoalHandler;
			node.title = "★ {0}";
			node.Scenario = aScenario;
			node.Goal = aGoal;
			return node;
		}

		private AntAIActionNode CreateActionNode(AntAIScenario aScenario, AntAIScenarioAction aAction)
		{
			var node = new AntAIActionNode(aAction.position, 200.0f, 300.0f, _nodeStyle, _activeNodeStyle);
			node.EventDelete += DeleteActionHandler;
			node.title = "▶ {0}";
			node.Scenario = aScenario;
			node.Action = aAction;
			return node;
		}

		private AntAIConditionNode CreateConditionNode(AntAIScenario aScenario)
		{
			var node = new AntAIConditionNode(aScenario.conditions.position, 200.0f, 300.0f, _nodeStyle, _activeNodeStyle);
			node.title = "CONDITIONS";
			node.Scenario = aScenario;
			return node;
		}

		private void DrawNodes(List<AntAIBaseNode> aNodes)
		{
			for (int i = 0, n = aNodes.Count; i < n; i++)
			{
				aNodes[i].Draw();
			}
		}

		private void ProcessEvents(Event aEvent)
		{
			_drag = Vector2.zero;

			for (int i = _nodes.Count - 1; i >= 0; i--)
			{
				if (_nodes[i].ProcessEvents(aEvent, this))
				{
					return;
				}
			}

			switch (aEvent.type)
			{
				case EventType.MouseDown :
					if (aEvent.button == 1 && _current != null)
					{
						var menu = new GenericMenu();
						menu.AddItem(new GUIContent("▶ Add Action"), false, AddActionHandler);
						menu.AddItem(new GUIContent("★ Add Goal"), false, AddGoalHandler);
						menu.AddItem(new GUIContent("Add World State"), false, AddWorldStateHandler);
						menu.AddSeparator("");
						menu.AddItem(new GUIContent("Align To Grid"), _alignToGrid, AlignToGridHandler);
						// _mousePosition = aEvent.mousePosition;
						menu.ShowAsContext();
					}
					break;

				case EventType.MouseDrag :
					if (aEvent.button == 0)
					{
						OnDrag(aEvent.delta);
					}
					break;
			}
		}

		private void OnDrag(Vector2 aDelta)
		{
			_totalDrag += aDelta;
			_drag = aDelta;

			for (int i = 0, n = _nodes.Count; i < n; i++)
			{
				_nodes[i].Drag(aDelta);
			}

			GUI.changed = true;
		}

		private void DrawGrid(float aCellSize, Color aColor, float aOpacity)
		{
			int cols = Mathf.CeilToInt(position.width / aCellSize);
			int rows = Mathf.CeilToInt(position.height / aCellSize);

			Handles.BeginGUI();
			Color c = Handles.color;
			Handles.color = new Color(aColor.r, aColor.g, aColor.b, aOpacity);

			_offset += _drag * 0.5f;
			Vector3 newOffset = new Vector3(_offset.x % aCellSize, _offset.y % aCellSize, 0.0f);

			for (int i = 0; i < cols; i++)
			{
				Handles.DrawLine(
					new Vector3(aCellSize * i, -aCellSize, 0.0f) + newOffset,
					new Vector3(aCellSize * i, position.height, 0.0f) + newOffset
				);
			}

			for (int i = 0; i < rows; i++)
			{
				Handles.DrawLine(
					new Vector3(-aCellSize, aCellSize * i, 0.0f) + newOffset,
					new Vector3(position.width, aCellSize * i, 0.0f) + newOffset
				);
			}

			Handles.color = c;
			Handles.EndGUI();
		}

		private void ClearPlan()
		{
			for (int i = _nodes.Count - 1; i >= 0; i--)
			{
				if (_nodes[i] is AntAIActionStateNode)
				{
					_nodes[i].links.Clear();
					_nodes.RemoveAt(i);
				}
				else if (_nodes[i] is AntAIWorldStateNode)
				{
					_nodes[i].links.Clear();
				}
			}
		}

		#endregion
		#region Event Handlers

		public void SelectPresetHandler(object aPresetName)
		{
			AntAIScenario selectedPreset = System.Array.Find(_scenarios, x => x.name.Equals(aPresetName.ToString()));
			_current = selectedPreset;
			ClearNodes();

			SetCurrentScenario(selectedPreset);

			// Old selection in the project window.
			// ------------------------------------
			// if (Selection.objects.Length == 1 && Selection.objects[0] is AntAIScenario)
			// {
			// 	var tmp = (AntAIScenario) Selection.objects[0];
			// 	if (tmp == null)
			// 	{
			// 		SetCurrentScenario(null);
			// 	}
			// 	else if (!System.Object.ReferenceEquals(tmp, _current))
			// 	{
			// 		SetCurrentScenario(tmp);
			// 	}
			// }
			// else
			// {
			// 	SetCurrentScenario(null);
			// }
		}

		private void AddActionHandler()
		{
			AntArray.Add(ref _current.actions, new AntAIScenarioAction());
			_nodes.Add(CreateActionNode(_current, _current.actions[_current.actions.Length - 1]));
			EditorUtility.SetDirty(_current);
		}

		private void AddGoalHandler()
		{
			AntArray.Add(ref _current.goals, new AntAIScenarioGoal());
			_nodes.Add(CreateGoalNode(_current, _current.goals[_current.goals.Length - 1]));
			EditorUtility.SetDirty(_current);
		}

		private void AddWorldStateHandler()
		{
			AntArray.Add(ref _current.worldStates, new AntAIWorldState());
			_nodes.Add(CreateWorldStateNode(_current, _current.worldStates[_current.worldStates.Length - 1]));
			EditorUtility.SetDirty(_current);
		}

		private void DeleteWorldStateHandler(AntAIWorldStateNode aNode)
		{
			ClearPlan();

			int index = Array.IndexOf(_current.worldStates, aNode.WorldState);
			if (index >= 0 && index < _current.worldStates.Length)
			{
				AntArray.RemoveAt(ref _current.worldStates, index);
			}

			aNode.EventDelete -= DeleteWorldStateHandler;
			aNode.EventBuildPlan -= BuildPlanHandler;
			_nodes.Remove(aNode);
			EditorUtility.SetDirty(_current);
		}

		private void DeleteGoalHandler(AntAIGoalNode aNode)
		{
			int index = Array.IndexOf(_current.goals, aNode.Goal);
			if (index >= 0 && index < _current.goals.Length)
			{
				AntArray.RemoveAt(ref _current.goals, index);
			}

			aNode.EventDelete -= DeleteGoalHandler;
			_nodes.Remove(aNode);
			EditorUtility.SetDirty(_current);
		}

		private void DeleteActionHandler(AntAIActionNode aNode)
		{
			int index = Array.IndexOf(_current.actions, aNode.Action);
			if (index >= 0 && index < _current.actions.Length)
			{
				AntArray.RemoveAt(ref _current.actions, index);
			}

			aNode.EventDelete -= DeleteActionHandler;
			_nodes.Remove(aNode);
			EditorUtility.SetDirty(_current);
		}

		private void ClearPlanHandler(AntAIWorldStateNode aNode)
		{
			ClearPlan();
		}

		private void BuildPlanHandler(AntAIWorldStateNode aNode, AntAIPlan aPlan, AntAIPlanner aPlanner)
		{
			ClearPlan();

			AntAIAction action;
			AntAICondition curConditions = aPlanner.DebugConditions;
			AntAICondition preConditions;
			AntAIActionStateNode node;
			Vector2 pos = aNode.Position;

			aNode.links.Clear();
			AntAIBaseNode prevNode = aNode;
			for (int i = 0, n = aPlan.Count; i < n; i++)
			{
				action = aPlanner.GetAction(aPlan[i]);
				preConditions = curConditions.Clone();
				curConditions.Act(action.post);
				// DescribePlan(action.name, aPlanner, curConditions, preConditions);

				pos.x += 220.0f;
				if (i + 1 == aPlan.Count)
				{
					node = (aPlan.isSuccess)
						? new AntAIActionStateNode(pos, 200.0f, 300.0f, _successStyle, _activeSuccessStyle)
						: new AntAIActionStateNode(pos, 200.0f, 300.0f, _failStyle, _activeFailStyle);
				}
				else
				{
					node = new AntAIActionStateNode(pos, 200.0f, 300.0f, _nodeStyle, _activeNodeStyle);
				}

				node.BindData(string.Concat((i + 1).ToString(), ". ", action.name), aPlanner, curConditions, preConditions);
				_nodes.Add(node);

				prevNode.links.Add(_nodes[_nodes.Count - 1]);
				prevNode = _nodes[_nodes.Count - 1];
			}
		}

		private void AlignToGridHandler()
		{
			_alignToGrid = !_alignToGrid;
		}

		private void DrawSolidLine(Vector2 aPointA, Vector2 aPointB, Color aColor,
			float aWidth = 2.0f, bool aAntialias = true)
		{
			if (_lineTexture == null)
			{
				InitializeSolid();
			}

			// Note that theta = atan2(dy, dx) is the angle we want to rotate by, but instead
        	// of calculating the angle we just use the sine (dy/len) and cosine (dx/len).
			float dx = aPointB.x - aPointA.x;
			float dy = aPointB.y - aPointA.y;
			float len = Mathf.Sqrt(dx * dx + dy * dy);

			// Early out on tiny lines to avoid divide by zero.
        	// Plus what's the point of drawing a line 1/1000th of a pixel long??
			if (len < 0.001f)
			{
				return;
			}

			// Pick texture and material (and tweak width) based on anti-alias setting.
			Texture2D tex;
			Material mat;
			if (aAntialias)
			{
				// Multiplying by three is fine for anti-aliasing width-1 lines, but make a wide "fringe"
            	// for thicker lines, which may or may not be desirable.
				aWidth *= 3.0f;
				tex = _aaLineTexture;
				mat = _blendMaterial;
			}
			else
			{
				tex = _lineTexture;
				mat = _blitMaterial;
			}

			float wdx = aWidth * dy / len;
			float wdy = aWidth * dx / len;

			var m = Matrix4x4.identity;
			m.m00 = dx;
			m.m01 = -wdx;
			m.m03 = aPointA.x + 0.5f * wdx;
			m.m10 = dy;
			m.m11 = wdy;
			m.m13 = aPointA.y - 0.5f * wdy;

			// Use GL matrix and Graphics.DrawTexture rather than GUI.matrix and GUI.DrawTexture,
        	// for better performance. (Setting GUI.matrix is slow, and GUI.DrawTexture is just a
        	// wrapper on Graphics.DrawTexture.)
			GL.PushMatrix();
			GL.MultMatrix(m);
			Graphics.DrawTexture(_lineRect, tex, _lineRect, 0, 0, 0, 0, aColor, mat);
			GL.PopMatrix();
		}

		private void InitializeSolid()
		{
			if (_lineTexture == null)
			{
				_lineTexture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
				_lineTexture.SetPixel(0, 1, Color.white);
				_lineTexture.Apply();
			}

			if (_aaLineTexture == null)
			{
				_aaLineTexture = new Texture2D(1, 3, TextureFormat.ARGB32, true);
				_aaLineTexture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.0f));
				_aaLineTexture.SetPixel(0, 1, Color.white);
				_aaLineTexture.SetPixel(0, 2, new Color(1.0f, 1.0f, 1.0f, 0.0f));
				_aaLineTexture.Apply();
			}

			// GUI.blitMaterial and GUI.blendMaterial are used internally by GUI.DrawTexture,
        	// depending on the alphaBlend parameter. Use reflection to "borrow" these references.
			_blitMaterial = (Material) typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
			_blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
		}

		private void CopyConditionsAsEnum()
		{
			var str = string.Format("public enum {0}", (_current != null) ? _current.name : "AICondition");
			str = string.Concat(str, "\n{\n");
			string conditionName = string.Empty;
			for (int i = 0, n = _current.conditions.list.Length; i < n; i++)
			{
				conditionName = RemoveSpacesFromString(_current.conditions.list[i].name);
				str = string.Concat(str, $"\t{conditionName} = {_current.conditions.list[i].id}");
				str = (i + 1 == _current.conditions.list.Length)
					? string.Concat(str, "\n}")
					: string.Concat(str, ",\n");
			}

			var te = new TextEditor();
			te.text = str;
			te.SelectAll();
			te.Copy();

			EditorUtility.DisplayDialog("Copied!", "All conditions copied to the clipboard as Enum.", "Ok");
		}

		// todo: make this method as string extension.
		private string RemoveSpacesFromString(string aValue)
		{
			char[] characters = aValue.ToCharArray();
			List<char> nonBlankChars = new List<char>();

			char blank = ' ';
			int numChars = characters.Length;
			for (int i = 0; i < numChars; i++)
			{
				if (characters[i] != blank)
				{
					nonBlankChars.Add(characters[i]);
				}
			}

			return new string(nonBlankChars.ToArray());
		}

		#endregion
	}
}