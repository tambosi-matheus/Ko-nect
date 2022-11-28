using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private Dot dotPrefab;
    [SerializeField] private Transform dotsParent;
    [SerializeField] private float delay, chanceToConect;
    [SerializeField] private List<Dot> dots = new List<Dot>();
    [SerializeField] private int radius, dotsQuantity;
    [SerializeField] private float lineAnimationDuration;

    public static Manager Instance;


    // Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start() => StartCoroutine("GenerateDots");
    

    private IEnumerator GenerateDots()
    {
        for (int i = 0; i < dotsQuantity; i++)
        {
            var obj = Instantiate(dotPrefab, Random.insideUnitSphere * radius, Quaternion.identity);
            obj.transform.parent = dotsParent;
            var dot = obj.GetComponent<Dot>();
            if (Random.value < chanceToConect)
                ConnectDots(dot);
            dots.Add(dot);
            yield return new WaitForSeconds(delay);
        }
    }

    // Clear Dots list and restart coroutine
    public void RecreateDots()
    {
        dots.ForEach(d => Destroy(d.gameObject));
        dots = new List<Dot>();
        StopCoroutine("GenerateDots");
        StartCoroutine("GenerateDots");
    }

    public void ConnectDots(Dot dot)
    {
        float closestDistance = radius;
        Dot closest = null;

        if (dots.Count < 2) return;
        foreach (Dot d in dots)
        {
            var distance = Vector3.Distance
                (dot.gameObject.transform.position, d.gameObject.transform.position);
            if (distance < closestDistance)
            {
                closest = d;
                closestDistance = distance;
            }
        }

        if (closest == null) return;
        StartCoroutine(dot.ConnectTo(closest, lineAnimationDuration));
    }
}
