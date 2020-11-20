using UnityEngine;

namespace DevTools
{
	public interface ITransformResetter
	{
		void ResamplingOrigin();

		void ResamplingOrigin(Transform transform);

		void ResetAll(Space space);

		void ResetPosition(Space space);

		void ResetRotation(Space space);

		void ResetScale();
	}
}