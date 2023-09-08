using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteAlways]
public class EasingTest : MonoBehaviour
{
	public LineRenderer LineRenderer;
	public Easing.EasingType EasingType;
	public Vector3[] Positions = new Vector3[100];
	public Transform SpriteTransform;

	public Dropdown Dropdown;
	List<string> Options = new List<string>();

	public void Start()
	{
		Options.Clear();
		for (int i = 0; i < 31; i++)
			Options.Add(((Easing.EasingType)i).ToString());
		
		Dropdown.ClearOptions();
		Dropdown.AddOptions(Options);
		Dropdown.onValueChanged.RemoveAllListeners();
		Dropdown.onValueChanged.AddListener(OnValueChanged);
		Dropdown.value = 1;
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
