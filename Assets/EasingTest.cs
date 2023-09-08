using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class EasingTest : MonoBehaviour
{
	public LineRenderer LineRenderer;
	public Easing.EasingType EasingType;
	public Vector3[] Positions = new Vector3[100];
	public Transform SpriteTransform;
	public Button BtnTemplate;
	public RectTransform BtnContainer;


	public void Start()
	{
		int c = BtnContainer.childCount;
		for (int i = c - 1; i >= 1; i--)
		{
			if (Application.isPlaying)
			{
				Destroy(BtnContainer.GetChild(i).gameObject);
			}
			else
			{
				DestroyImmediate(BtnContainer.GetChild(i).gameObject);
			}

		}

		for (int i = 0; i < 31; i++)
		{
			var btn = Instantiate(BtnTemplate, BtnContainer);
			Easing.EasingType t = (Easing.EasingType)i;
			btn.onClick.AddListener(() => OnValueChanged((int)t));
			btn.GetComponentInChildren<Text>().text = t.ToString();
			btn.gameObject.SetActive(true);
			if (i == 1)
			{
				btn.Select();
				EasingType = (Easing.EasingType)i;
			}
		}

	}

	private void OnValueChanged(int value)
	{
		EasingType = (Easing.EasingType)value;
	}

	// Update is called once per frame
	void Update()
	{
		if (LineRenderer.positionCount != Positions.Length)
			LineRenderer.positionCount = Positions.Length;

		for (int i = 0; i < Positions.Length; i++)
		{
			float t = i / (float)Positions.Length;
			Positions[i] = new Vector3(Mathf.Lerp(-10, 10, t), Mathf.LerpUnclamped(-10, 10, Easing.Ease(t, EasingType)), 0);
		}
		LineRenderer.SetPositions(Positions);


		{
			Vector3 pos = SpriteTransform.localPosition;
			float t = ( (Time.timeSinceLevelLoad * 0.25f) % 1);
			pos.y = Mathf.LerpUnclamped(-10, 10, Easing.Ease(t, EasingType));
			SpriteTransform.localPosition = pos;
		}
	}
}
