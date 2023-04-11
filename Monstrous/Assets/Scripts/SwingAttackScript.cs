using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SwingAttackScript : MonoBehaviour
{
    public float SwingAtkDamage = 60f;
    public float SwingAtkAS = 5.0f;
    private float timing = 2.5f;
    public Vector2 size;

    public Transform swingOrigin;
    public Animator animator;
        
    // Start is called before the first frame update
    void Start()
    {
        //collecting the Swing upgrade will enable the attack
        //will also add/enable accociated upgrades

        
    }

    void Update()
    {
        timing += Time.deltaTime;
        if (timing > SwingAtkAS)
        {
            timing = 0.0f;
            Swing();
        }
    }

    public void Swing()
    {
        animator.SetTrigger("Swing");
        Debug.Log("Swung");
        SwingCollide();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = swingOrigin == null ? Vector3.zero : swingOrigin.position;
        DrawWireCapsule(pos, Quaternion.Euler(0, 0, 90), 1f, 4f);
    }

    private static void DrawWireCapsule(Vector3 _pos, Quaternion _rot, float _radius, float _height, Color _color = default(Color))
    {
        if (_color != default(Color))
            Handles.color = _color;
        Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);
        using (new Handles.DrawingScope(angleMatrix))
        {
            var pointOffset = (_height - (_radius * 2)) / 2;

            //draw sideways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, _radius);
            Handles.DrawLine(new Vector3(0, pointOffset, -_radius), new Vector3(0, -pointOffset, -_radius));
            Handles.DrawLine(new Vector3(0, pointOffset, _radius), new Vector3(0, -pointOffset, _radius));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, _radius);
            //draw frontways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, _radius);
            Handles.DrawLine(new Vector3(-_radius, pointOffset, 0), new Vector3(-_radius, -pointOffset, 0));
            Handles.DrawLine(new Vector3(_radius, pointOffset, 0), new Vector3(_radius, -pointOffset, 0));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, _radius);
            //draw center
            Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, _radius);
            Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, _radius);

        }
    }

    private void SwingCollide()
    {
        foreach(Collider2D other in Physics2D.OverlapCapsuleAll(swingOrigin.position, size, CapsuleDirection2D.Horizontal, 0f))
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(other);
                //other.gameObject.GetComponent<EnemyBase>.dealDamage(SwingAtkDamage);
            }
        }
    }
}
