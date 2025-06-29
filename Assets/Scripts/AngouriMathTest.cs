using AngouriMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngouriMathTest : MonoBehaviour
{
    private void Start()
    {
        var expr = "sin(x^2)";

        Entity formula = expr;

        Entity derivative = formula.Differentiate("x").Simplify();

        Debug.Log($"f(x) = {formula}");
        Debug.Log($"f'(x) = {derivative}");

        var result = derivative.Substitute("x", Mathf.PI).EvalNumerical();
        Debug.Log($"f'(PI) = {result}");
    }
}
