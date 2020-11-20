namespace DevTools
{
	using UnityEngine;

	public class TransformResetterMonoFacade : MonoBehaviour, ITransformResetter
	{
		private TransformResetter resetter;

		public TransformResetter Resetter
		{
			get
			{
				if (resetter == null)
				{
					resetter = new TransformResetter(transform);
				}

				return resetter;
			}
		}

		public void ResamplingOrigin()
		{
			Resetter.ResamplingOrigin();
		}

		public void ResamplingOrigin(Transform transform)
		{
			Resetter.ResamplingOrigin(transform);
		}

		public void ResetAll(Space space = Space.World)
		{
			Resetter.ResetAll(space);
		}

		public void ResetPosition(Space space = Space.World)
		{
			Resetter.ResetPosition(space);
		}

		public void ResetRotation(Space space = Space.World)
		{
			Resetter.ResetRotation(space);
		}

		public void ResetScale()
		{
			Resetter.ResetScale();
		}

		private void Awake()
		{
			if (resetter == null)
			{
				resetter = new TransformResetter(transform);
			}
		}
	}
}

#if UNITY_EDITOR

namespace DevTools.Editor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(TransformResetterMonoFacade))]
	public class TransformResetterMonoFacadeInspector : Editor
	{
		private TransformResetterMonoFacade _target;

		public override void OnInspectorGUI()
		{
			_target = target as TransformResetterMonoFacade;

			DrawReset("Original Position", _target.Resetter.OriginalPosition, () => _target.ResetPosition());
			DrawReset("Original Local Position", _target.Resetter.OriginalLocalPosition, () => _target.ResetPosition(Space.Self));
			DrawReset("Original Rotation", _target.Resetter.OriginalRotation.eulerAngles, () => _target.ResetRotation());
			DrawReset("Original Local Rotation", _target.Resetter.OriginalLocalRotation.eulerAngles, () => _target.ResetRotation(Space.Self));
			DrawReset("Original Local Scale", _target.Resetter.OriginalLocalScale, () => _target.ResetScale());

			if (GUILayout.Button("Resampling Original Transform"))
			{
				_target.ResamplingOrigin();
			}
		}

		private void DrawReset(string label, Vector3 value, Action action)
		{
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Reset"))
				{
					action?.Invoke();
				}
				EditorGUILayout.Vector3Field(label, value);
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}

#endif