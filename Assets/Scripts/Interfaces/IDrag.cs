using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrag
{
    void Down();
    void Drag(Vector2 pos);
    void Break();
}
