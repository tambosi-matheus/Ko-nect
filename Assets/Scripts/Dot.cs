using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public Color myColor { get; private set; }
    private Color targetColor;

    // Start is called before the first frame update
    void Awake()
    {
        var mat = GetComponent<MeshRenderer>();
        myColor = new Color(Random.value, Random.value, Random.value, 1);
        mat.material.SetColor("_BaseColor", myColor);
        mat.material.SetColor("_EmissionColor", myColor);
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        // Generate random seed
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var rand = Random.Range(0.5f, 1.2f);
        var targetScale = new Vector3(rand, rand, rand);
        while ((transform.localScale - targetScale).magnitude > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.1f);
            yield return null;
        }
    }

    public IEnumerator ConnectTo(Dot target, float time)
    {
        if (target == null) yield break;
        // Set line renderer
        LineRenderer line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.1f;
        Gradient gradient = new Gradient();
        targetColor = target != null ? target.myColor : Color.white;
        gradient.SetKeys(
            new GradientColorKey[] 
            { new GradientColorKey(myColor, 0.5f), new GradientColorKey(targetColor, 1.0f) },
            new GradientAlphaKey[] 
            { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) });
        line.colorGradient = gradient;

        // Line animation
        var iterationTime = time / 100;
        for (int t = 0; t <= 100; t++)
        {
            if (target == null) yield break;
            var destination = 
                transform.position + ((target.transform.position - transform.position) * t/100);
            line.SetPositions(new Vector3[] { transform.position, destination });
            yield return new WaitForSecondsRealtime(iterationTime);
        }
        
    }
}
