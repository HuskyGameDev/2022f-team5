using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monstrous.AI;

public class SwingAttackScript : MonoBehaviour
{
    public float SwingAtkDamage = 70f;
    public float SwingAtkAS = 2.5f;
    private float timing = 2.5f;
    public Vector2 size;

    public Transform swingOrigin;
    public Animator animator;

    public 
        
    // Start is called before the first frame update
    void Start()
    {
                
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
        //DrawWireCapsule(pos, Quaternion.Euler(0, 0, 90), size.x, size.y);
    }

    /*
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
    */

    private void SwingCollide()
    {
        foreach(Collider2D other in Physics2D.OverlapCapsuleAll(swingOrigin.position, size, CapsuleDirection2D.Horizontal, 0f))
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(other);
                other.gameObject.GetComponent<EnemyBase>().dealDamage(SwingAtkDamage);
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                Vector2 difference = other.transform.position - transform.position;
                float xmodify = 1f;
                float ymodify = 1f;
                if(difference.x < 0)
                {
                    xmodify = -1f;
                }
                if(difference.y < 1)
                {
                    ymodify = -1f;
                }
                Vector2 knockback = new Vector2(xmodify * 20 * (otherRB.mass / 10), ymodify * 20 * (otherRB.mass / 10));
                otherRB.AddForce(knockback, ForceMode2D.Impulse);
            }
        }
    }
}
