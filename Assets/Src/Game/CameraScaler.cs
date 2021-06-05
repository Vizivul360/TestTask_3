using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public Camera target;

    public Vector3 paddingOrhtograhic, paddingPerspective;

    private Vector3 corner
    {
        get { return target.ViewportToWorldPoint(Vector3.one); }
    }

    public void SetScale(Vector2 mapSize)
    {
        var s = mapSize / 2;
        var to = new Vector3(s.x, 0, s.y);

        setPerspective(to + paddingPerspective);
        setOrthographic(to + paddingOrhtograhic);
    }

    private void setOrthographic(Vector3 to)
    {
        target.orthographicSize *= Mathf.Max(to.x / corner.x, to.z / corner.z);
    }

    private void setPerspective(Vector3 to)
    {
        var dy = target.transform.position.y;

        var dx = to.x;
        var dz = to.z;

        var a0 = Mathf.Atan2(dx / target.aspect, dy);
        var a1 = Mathf.Atan2(dz, dy);

        target.fieldOfView = 2 * Mathf.Max(a0, a1) * Mathf.Rad2Deg;
    }
}
