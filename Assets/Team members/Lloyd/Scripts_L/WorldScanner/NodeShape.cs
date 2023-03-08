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

    public void OnEnable()
    {
        cube = gameObject.AddComponent<Cuboid>();
        cube.transform.position = transform.position;
        cube.transform.rotation = Quaternion.identity;
        cube.Color = new Color(color.r, color.g, color.b, 1f);
        cube.Size = new Vector3(cubeSize, cubeSize, cubeSize);
    }

    private void Update()
    {
        DrawShapes(cam);
    }


    [Button]
    public void ChangeColour(Color newColor)
    {
        color = newColor;
    }

    public override void DrawShapes(Camera cam)
    {
        using (Draw.Command(cam))
        {
            Draw.Cuboid(transform.position, Quaternion.identity, new Vector3(cubeSize, cubeSize, cubeSize), color);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
}

