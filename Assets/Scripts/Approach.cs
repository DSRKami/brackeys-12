using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Approach
{ 
    public static IEnumerator LerpValues(float startValue, float endValue, float duration, System.Action<float> onValueChanged)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // Interpolate between startValue and endValue based on time elapsed
            float newValue = Mathf.Lerp(startValue, endValue, timeElapsed / duration);

            // Call the delegate to apply the interpolated value (e.g., update position, rotation, or scale)
            onValueChanged?.Invoke(newValue);

            // Increase time elapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final value is set at the end of the lerp
        onValueChanged?.Invoke(endValue);
    }
}
