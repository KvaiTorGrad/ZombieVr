//using Unity.Burst;
////using UnityEngine.Jobs;
//using Unity.Jobs;
//using Unity.Collections;
//using UnityEngine;

////[BurstCompile]
////public struct JobMoveEnemy : IJobParallelForTransform
////{
////    public NativeArray<float> speed;
////    public float deltaTime;
////    public Vector3 player;

////    public void Execute(int index, TransformAccess transform)
////    {
////      transform.position = (new Vector3(player.x, 0, player.z) - transform.position).normalized;
////    }
////}

//[BurstCompile]
//public struct JobMoveEnemy : IJobParallelFor
//{
//    public NativeArray<Vector3> enemyPosition;
//    public NativeArray<float> distance;
//    public Vector3 player;

//    public void Execute(int index)
//    {
//        distance[index] = player.x * enemyPosition[index].x + player.y * enemyPosition[index].y + player.z * enemyPosition[index].z;
//    }
//}
