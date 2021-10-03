using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public float horizontal;

    private float startTouch;
    private float swipeDelta;

    private void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            swipeDelta = Input.mousePosition.x - startTouch;
            startTouch = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipeDelta = 0f;
        }

        var current = horizontal;

        var target = (Mathf.Lerp(0, 1, Mathf.Abs(swipeDelta) / Screen.width)) * Mathf.Sign(swipeDelta);

        horizontal = Vector2.MoveTowards(new Vector2(current, 0), new Vector2(target, 0), Time.deltaTime).x;
#else

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                swipeDelta = touch.position.x - startTouch;
                startTouch = touch.position.x;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                swipeDelta = 0f;
            }
        }
        var current = horizontal;

        var target = (Mathf.Lerp(0, 1, Mathf.Abs(swipeDelta) / Screen.width)) * Mathf.Sign(swipeDelta);

        horizontal = Vector2.MoveTowards(new Vector2(current, 0), new Vector2(target, 0), Time.deltaTime).x;


#endif


    }
}
