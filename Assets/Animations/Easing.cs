using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Easing {
    public static float EaseOutExp(float duration, float time, float start, float end, float exponent) {
        float rangeY = Mathf.Abs(end - start);
        float normalizedTime = Mathf.Clamp(time / duration, 0, 1);

        if (end < start) {
            return rangeY * Mathf.Pow(1 - normalizedTime, exponent) + end;
        } else {
            return -1 * rangeY * Mathf.Pow(1 - normalizedTime, exponent) + rangeY + start;
        }
    }

    public static float EaseInExp(float duration, float time, float start, float end, float exponent) {
        float rangeY = Mathf.Abs(end - start);
        float normalizedTime = Mathf.Clamp(time / duration, 0, 1);

        if (end < start) {
            return Mathf.Pow(-1, exponent + 1) * rangeY * Mathf.Pow(-1 * normalizedTime, exponent) + rangeY + end;
        }
        else {
            return rangeY * Mathf.Pow(normalizedTime, exponent) + start;
        }
    }

    public static float EaseInOutExp(float duration, float time, float start, float end, float exponent) {
        float rangeY = Mathf.Abs(end - start);
        float normalizedTime = Mathf.Clamp(time / duration, 0, 1);

        if (end < start) {
            if (normalizedTime < 0.5f) {
                return -1 * rangeY * (0.5f / Mathf.Pow(0.5f, exponent) * Mathf.Pow(normalizedTime, exponent)) + rangeY + end;
            } else {
                return rangeY * (-1 + Mathf.Pow(-2 * normalizedTime + 2, exponent) / 2) + rangeY + end;
            }
        } else {
            if (normalizedTime < 0.5f) {
                return rangeY * (0.5f / Mathf.Pow(0.5f, exponent) * Mathf.Pow(normalizedTime, exponent)) + start;
            } else {
                return rangeY * (1 - Mathf.Pow(-2 * normalizedTime + 2, exponent) / 2) + start;
            }
        }
    }

    public static float Circle(float duration, float time, int quadrant, float start, float end) {
        float radius = Mathf.Abs(start - end);
        float offsetY = Mathf.Min(start, end);
        float normalizedTime = Mathf.Clamp(time / duration, 0, 1);

        switch (quadrant) {
            case 1:
                return radius * (Mathf.Sqrt(1 - Mathf.Pow(normalizedTime, 2))) + offsetY;
            case 2:
                return radius * (Mathf.Sqrt(1 - Mathf.Pow(normalizedTime - 1, 2))) + offsetY;
            case 3:
                return radius * (1 - Mathf.Sqrt(1 - Mathf.Pow(normalizedTime - 1, 2))) + offsetY;
            case 4:
                return radius * (1 - Mathf.Sqrt(1 - Mathf.Pow(normalizedTime, 2))) + offsetY;
            default:
                return 0;
        }
    }

    // Failed implementation of a cubic bezier curve
    // Bezier curves are parametric functions, so you cannot simply calculate a numeric output based on time
    // The x and y values need to be calculated separately before an output can be produced
    // Maxime Heckel has a beautiful article on cubic bezier curves:
    // https://blog.maximeheckel.com/posts/cubic-bezier-from-math-to-motion/
    public static float CubicBezier(Vector2 point1, Vector2 point2, float duration, float time, float start, float end) {
        Vector2 point0 = new Vector2(0 ,0);
        Vector2 point3 = new Vector2(1, 1);
        float offsetY;
        float rangeY = Mathf.Abs(end - start);
        float normalizedTime = Mathf.Clamp(time / duration, 0, 1);

        if (end < start) {
            point0 = new Vector2(0, 1);
            point3 = new Vector2(1, 0);
            offsetY = end;
        }
        else {
            point0 = new Vector2(0, 0);
            point3 = new Vector2(1, 1);
            offsetY = start;
        }

        return
            rangeY * (
            Mathf.Pow(1 - normalizedTime, 3) * point0.y +
            3 * Mathf.Pow(1 - normalizedTime, 2) * normalizedTime * point1.y +
            3 * (1 - normalizedTime) * Mathf.Pow(normalizedTime, 2) * point2.y +
            Mathf.Pow(normalizedTime, 3) * point3.y
            ) + offsetY;
    }
}
