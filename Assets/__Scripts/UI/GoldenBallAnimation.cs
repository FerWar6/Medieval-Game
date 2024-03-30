using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBallAnimation : MonoBehaviour
{
    Animator anim;
    Vector3 endpos;
    LineRenderer line;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        SetLineRendererPositions(transform.position, endpos);
    }
    void SetLineRendererPositions(Vector3 startPosition, Vector3 endPosition)
    {
        line.positionCount = 2;
        line.SetPosition(0, startPosition);
        line.SetPosition(1, endPosition);
    }
    public void SetEndPosition(Vector3 pos)
    {
        endpos = pos;
        anim.SetTrigger("Attack");
    }
}
