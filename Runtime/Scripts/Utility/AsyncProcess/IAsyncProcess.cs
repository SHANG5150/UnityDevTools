using System.Collections;
using UnityEngine;

internal interface IAsyncProcess
{
	Coroutine StartCoroutine(IEnumerator routine);

	Coroutine StartCoroutine_Auto(IEnumerator routine);

	void StopAllCoroutines();

	void StopCoroutine(IEnumerator routine);

	void StopCoroutine(Coroutine routine);
}