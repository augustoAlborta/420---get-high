using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_animacion : MonoBehaviour
{
    public cazadordepersonas caza;
    public void active()
    {
        caza.Shoot();
    }
}
