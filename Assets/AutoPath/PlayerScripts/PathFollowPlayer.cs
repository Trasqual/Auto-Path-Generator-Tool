using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowPlayer : MonoBehaviour
{
    TouchInput input;

    private void Start()
    {
        input = GetComponent<TouchInput>();
    }

    private void Update()
    {
        transform.localPosition += new Vector3(input.horizontal * 2 * Time.deltaTime, 0, 0);
        var pos = transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -3, 3);
        transform.localPosition = pos;
    }
}
