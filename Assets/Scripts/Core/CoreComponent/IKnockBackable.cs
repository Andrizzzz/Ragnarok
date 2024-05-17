using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance.CoreSystem
{
    public interface IKnockBackable
    {
        void KnockBack(Vector2 angle, float strength, int direction);
    }
}