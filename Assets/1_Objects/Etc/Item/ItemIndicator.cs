using UnityEngine;

using T = MatrixTransform;

public class ItemIndicator : MonoBehaviour
{
    public Transform itemTransform;

    private Matrix4x4 mat = new();

    void Update()
    {
        T.Identity(ref mat);
        T.Translate(ref mat, itemTransform.position);
        T.Rotate(ref mat, St_CameraController.Inst.GetRotation());
        T.Translate(ref mat, new Vector2(0f, 1.5f));
        T.Scale(ref mat, new Vector2(0.5f, 0.5f));
        T.Dispatch(transform, ref mat);
    }
}
