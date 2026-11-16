
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [Header("Button Animation")]
    public Transform buttonPress;

    public float pressDistance = 0.02f;
    public float animationDuration = 0.1f;

    [Header("Button Event")]
    public UnityEvent onButtonPressed;

    private Vector3 initialPosition;
    private bool isPressed = false;

    private void Start()
    {
        initialPosition = buttonPress.localPosition;
    }

    public void PressButton()
    {
        if (isPressed) return;

        isPressed = true;

        StopAllCoroutines();
        StartCoroutine(PressAnimation());
    }

    private IEnumerator PressAnimation()
    {
        Vector3 pressedPosition =
            initialPosition - Vector3.up * pressDistance;

        yield return MoveButton(initialPosition, pressedPosition);

        onButtonPressed?.Invoke();

        yield return new WaitForSeconds(0.1f);

        yield return MoveButton(pressedPosition, initialPosition);

        isPressed = false;
    }

    private IEnumerator MoveButton(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / animationDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            buttonPress.localPosition =
                Vector3.Lerp(start, end, t);

            yield return null;
        }

        buttonPress.localPosition = end;
    }
}
