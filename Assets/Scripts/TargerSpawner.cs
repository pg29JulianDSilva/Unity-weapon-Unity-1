using System.Collections;
using System.Drawing;

//using Unity.Mathematics;
using UnityEngine;

public class TargerSpawner : MonoBehaviour
{
    //Spawn enemies primitive
    [Header("Spawn Area")]
    public Collider spawnArea;

    [Header("Swapwn Objects")]
    public GameObject cubePrefab;

    //Here the [Min(0f)] creates a base for the value acepted
    [Header("Spawn Properties")]
    [Min(0f)] public float minDelay = 0.5f;
    [Min(0f)] public float maxDelay = 2.0f;

    public bool randomRotation = true;
    public Transform parentForSpawned;

    const int MaxAmountOfTries = 3;

    //For checking for the collider to exsist
    private void Awake()
    {
        if (!spawnArea) spawnArea = gameObject.GetComponent<Collider>();
        if (spawnArea && !spawnArea.isTrigger)
        {
            Debug.LogWarning("Spawn area should be a trigger.");
        }
    }

    private void OnEnable()
    {
        //This will start the co routine. This one should stop when disenable, but it can be killed with the other routine.
        StartCoroutine(SpawnLoop());
        //StopCoroutine();
    }

    //This one works as a sleep once a trigger is activated, waiting the time in the yield
    IEnumerator SpawnLoop()
    {
        while (enabled) 
        {
            if (cubePrefab && spawnArea)
            {
                if (TryGetRandomPoint(spawnArea, out Vector3 position))
                {
                    //This is for random rotation of the identity (instance)
                    Quaternion rot = randomRotation ? Random.rotation : Quaternion.identity;
                    Transform parent = parentForSpawned ? parentForSpawned : transform;
                    GameObject go = Instantiate(cubePrefab, position, rot, parent);
                }
            }
            //yield return null; will be really useful because it waits until the next frame
            //yield return new WaitForSeconds(4);

            //This is a rando wait
            float wait = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(wait);
        }
    } 

    //Static means that is shared between all the instances from that object // out is really similar to pointers, when a value pass the memory space of the vector but getting the value
    static bool TryGetRandomPoint(Collider objectColider, out Vector3 position) 
    {
        Bounds bounds = objectColider.bounds;

        for (int i = 0; i < MaxAmountOfTries; ++i)
        {
            //This is to get random values. This one is exclusive (0 , 3  can give 0,1,2) except for float that is inclusive.
            Vector3 point = new Vector3
                (
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    Random.Range(bounds.min.z, bounds.max.z)
                );

            if (objectColider.ClosestPoint(point) == point)
            {
                position = point;
                return true;
            }
        }
        position = Vector3.zero;
        return false;
    }

}
