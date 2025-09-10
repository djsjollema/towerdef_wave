using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static int Alive = 0;

    public static void ResetCount() => Alive = 0;
}
