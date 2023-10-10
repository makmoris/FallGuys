using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRaceBackMovingWall : MonoBehaviour
{
    private Vector3 finishPos;

    private float speed = 5f;

    private bool dump;

    private void Awake()
    {
        finishPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -2.25f);
    }

    private void Update()
    {
        if (dump)
        {
            var step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, finishPos, step);

            if (Vector3.Distance(transform.localPosition, finishPos) < 0.001f)
            {
                dump = false;
            }
        }
    }

    public void Dumping(float timeBeforDumping)
    {
        StartCoroutine(WaitAndDump(timeBeforDumping));
    }

    IEnumerator WaitAndDump(float timeBeforDumping)
    {
        yield return new WaitForSeconds(timeBeforDumping);

        dump = true;
    }
}
