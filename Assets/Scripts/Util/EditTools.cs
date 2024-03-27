using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using NavMeshPlus.Components; 


/*public class ColliderSeparatorEditor : MonoBehaviour
{
    [MenuItem("Tools/Separate Colliders and Add NavMeshModifier")]
    static void SeparateCollidersAndAddNavMeshModifier()
    {
        foreach (GameObject prefab in Selection.gameObjects)
        {
            GameObject cliff = new GameObject("Cliff");
            cliff.transform.SetParent(prefab.transform);
            Transform cliffTransform = prefab.transform.Find("Cliff");
            if (cliffTransform != null)
            {
                List<BoxCollider2D> colliders = new List<BoxCollider2D>(cliffTransform.GetComponentsInChildren<BoxCollider2D>());

                foreach (BoxCollider2D collider in colliders)
                {
                    GameObject newColliderHolder = new GameObject("holder");

                    newColliderHolder.transform.SetParent(cliff.transform);

                    newColliderHolder.transform.localPosition = collider.transform.localPosition;
                    newColliderHolder.transform.localRotation = collider.transform.localRotation;
                    newColliderHolder.transform.localScale = collider.transform.localScale;

                    NavMeshModifier modifier = newColliderHolder.AddComponent<NavMeshModifier>();

                    modifier.overrideArea = true;
                    modifier.area = 1;
                    BoxCollider2D b = newColliderHolder.AddComponent<BoxCollider2D>();
                    b.size = collider.size;
                    b.transform.position = collider.transform.position;
                    b.edgeRadius = collider.edgeRadius;
                    b.offset = collider.offset;
                }
            }
            cliff.transform.localScale = new Vector3(1, 1, 1);
            cliffTransform.gameObject.SetActive(false);
        }
    }
}
*/