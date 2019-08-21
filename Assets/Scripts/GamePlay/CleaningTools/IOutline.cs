using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOutline {

    string GetName();

    Transform GetTransform();

    void InitOutline();

    void EnableOutlineColor();

    void DisableOutlineColor();
}
