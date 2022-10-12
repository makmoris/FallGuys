using UnityEngine;

public class AttackPointer : MonoBehaviour // вешается на все объекты, в которые можно стрелять
{

    private void Awake()
    {
        if (gameObject.layer != 7) gameObject.layer = 7; // 7 - AttackPointer layer
    }
}
