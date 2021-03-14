using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{

    // X, Y座標の移動可能範囲
    [SerializeField]
    GameObject _player;

    [Serializable]
    public class Bounds
    {
        public float xMin, xMax, yMin, yMax;
    }

    [SerializeField] Bounds bounds;

    // 補間の強さ（0f～1f） 。0なら追従しない。1なら遅れなしに追従する。
    [SerializeField, Range(0f, 1f)] private float followStrength;

    Vector3 targetPos;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        targetPos = Input.mousePosition;

        // X, Y座標の範囲を制限する
        targetPos.y = Mathf.Clamp(targetPos.y, bounds.yMin, bounds.yMax);
        targetPos.z = 0f;

        // このスクリプトがアタッチされたゲームオブジェクトを、マウス位置に線形補間で追従させる
        _player.transform.position = Vector3.Lerp(transform.position, targetPos, followStrength);
    }
}
