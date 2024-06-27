using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FlockingBaseComponent : MonoBehaviour
{
    public Vector3 FlockingVector;
    public VisibilityManager visManager;
    public string HerdTag;
    public float CohesionFactor = 1.0f;
    public float SeparationFactor = 1.0f;
    public float AlignmentFactor = 1.0f;

    void Start()
    {
        FlockingVector = Vector3.zero;
    }

    void Update()
    {
        var visibleEntities = visManager.getVisibleObjectsByTag(HerdTag);
        //Debug.Log("Visible entities: " + visibleEntities.Count);
        Vector3 centroid = getCentroidOfEntities(visibleEntities);
        Debug.DrawLine(centroid, centroid + Vector3.up * 10);
        Vector3 cohesionVector = (centroid - gameObject.transform.position);
        Vector3 separationVector = getSeperationCoefficient(visibleEntities);
        Vector3 alignmentVector = getAlignmentCoefficient(visibleEntities);

        Vector3 result = CohesionFactor * cohesionVector + SeparationFactor * separationVector +
                         AlignmentFactor * alignmentVector;
        FlockingVector = result;
    }

    private Vector3 getCentroidOfEntities(List<GameObject> objs)
    {
        Vector3 centroid = Vector3.zero;
        foreach (var obj in objs)
        {
            centroid += obj.transform.position;
        }

        centroid /= objs.Count;
        return centroid;
    }

    private Vector3 getSeperationCoefficient(List<GameObject> objs)
    {
        Vector3 pushFactor = Vector3.zero;
        foreach (var obj in objs)
        {
            // get differences between visible entities and current object, and average them
            Vector3 diff = gameObject.transform.position - obj.transform.position;
            pushFactor += diff;
        }

        return pushFactor / objs.Count;
    }

    private Vector3 getAlignmentCoefficient(List<GameObject> objs)
    {
        Vector3 alignmentFactor = Vector3.zero;
        foreach (var obj in objs)
        {
            // get differences between visible entities and current object, and average them
            Vector3 alignment = obj.transform.forward;
            alignmentFactor += alignment;
        }

        return alignmentFactor / objs.Count();
    }

}