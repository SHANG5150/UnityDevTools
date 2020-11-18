using UnityEngine;

namespace DevTools
{
	public class TransformResetter : ITransformResetter
	{
		public struct OriginalTransform
		{
			public Vector3 Position;
			public Vector3 LocalPosition;
			public Vector3 LocalScale;
			public Quaternion Rotation;
			public Quaternion LocalRotation;
		}

		public OriginalTransform Origin;

		private readonly Transform transform;

		public Vector3 OriginalPosition => Origin.Position;
		public Vector3 OriginalLocalPosition => Origin.LocalPosition;
		public Vector3 OriginalLocalScale => Origin.LocalScale;
		public Quaternion OriginalRotation => Origin.Rotation;
		public Quaternion OriginalLocalRotation => Origin.LocalRotation;

		public TransformResetter(Transform transform)
		{
			this.transform = transform;

			ResamplingOrigin(transform);
		}

		public void ResamplingOrigin()
		{
			ResamplingOrigin(transform);
		}

		public void ResamplingOrigin(Transform transform)
		{
			Origin.Position = transform.position;
			Origin.LocalPosition = transform.localPosition;
			Origin.LocalScale = transform.localScale;
			Origin.Rotation = transform.rotation;
			Origin.LocalRotation = transform.localRotation;
		}

		public void ResetAll(Space space)
		{
			ResetPosition(space);
			ResetScale();
			ResetRotation(space);
		}

		public void ResetPosition(Space space)
		{
			transform.position = space == Space.World ? Origin.Position : Origin.LocalPosition;
		}

		public void ResetScale()
		{
			transform.localScale = Origin.LocalScale;
		}

		public void ResetRotation(Space space)
		{
			transform.rotation = space == Space.World ? Origin.Rotation : Origin.LocalRotation;
		}
	}
}