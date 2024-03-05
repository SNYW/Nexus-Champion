using UnityEngine;
using UnityEngine.AI;

public static class MouseManager
{
   public static Vector3 GetDirectionToMouse(Vector3 rootPosition, LayerMask layerMask)
   {
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         // Check if the ray hits something
         if (!Physics.Raycast(ray, out var hit, int.MaxValue,layerMask)) return Vector3.forward;
        
         // Check if the hit point is on the NavMesh
         if (!NavMesh.SamplePosition(hit.point, out var navMeshHit, 1.0f, NavMesh.AllAreas)) return Vector3.forward;

         // Calculate the direction the agent will be traveling
         Vector3 direction = navMeshHit.position - rootPosition;
         direction.y = 0f; // Keep the direction horizontal

         // Set the agent's forward direction to the calculated direction
         if (direction != Vector3.zero)
         {
           return direction.normalized;
         }

         return Vector3.forward;
    }
   
   public static bool GetMousePositionOnNavmesh(LayerMask layerMask, Vector3 startPos, out Vector3 pos)
   {
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       pos = startPos;
       // Check if the ray hits something
       if (!Physics.Raycast(ray, out var hit, int.MaxValue, layerMask)) return false;
        
       // Check if the hit point is on the NavMesh
       if (!NavMesh.SamplePosition(hit.point, out var navMeshHit, 1.0f, NavMesh.AllAreas)) return false;

       pos = navMeshHit.position;
       return true;
   }
   
}
