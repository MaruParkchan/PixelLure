using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChainUISystem))] 
public class DataButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ChainUISystem chainUISystem = (ChainUISystem)target;

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
        //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

       if (GUILayout.Button("데이터 초기화", GUILayout.Width(120), GUILayout.Height(30))) 
        {
            chainUISystem.AllResetChainData();
        }
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝
    }
}