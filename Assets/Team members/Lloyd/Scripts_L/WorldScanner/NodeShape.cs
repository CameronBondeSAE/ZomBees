/*
using System;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteAlways]
public class NodeShape : ImmediateModeShapeDrawer
{
    public float cubeSize;
    public Color color;

    public Camera cam;
    
    private Cuboid cube;

    public bool blocked;

    public void CubeTime()
    {
        cube = gameObject.AddComponent<Cuboid>();
        if (cube)
        {
            cube.transform.position = transform.position;
            cube.transform.rotation = Quaternion.identity;
            cube.Color = new Color(color.r, color.g, color.b, 1f);
            cube.Size = new Vector3(cubeSize, cubeSize, cubeSize);
        }
    }

    private void Update()
    {
        DrawShapes(cam);

        //cube.Color = color;
    }

    public void ChangeColour(WorldNode x, bool _blocked)
    {
        blocked = _blocked;
            color = blocked ? Color.red : Color.green;
            CubeTime();
    }

    public override void DrawShapes(Camera cam)
    {
        using (Draw.Command(cam))
        {
            Draw.Cuboid(transform.position, Quaternion.identity, new Vector3(cubeSize, cubeSize, cubeSize), color);
        }
    }
}
*/

