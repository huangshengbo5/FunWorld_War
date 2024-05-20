using UnityEngine;
using Random = System.Random;

public class Town_City : MonoBehaviour
{
    public GameObject ObjSolider;

    public void CreateSolider()
    {
        var solider = (GameObject)Instantiate(ObjSolider);
        var soliderTans = solider.GetComponent<Transform>();
        soliderTans.position = GetSoliderPosition();
        soliderTans.localScale = Vector3.one;
        soliderTans.rotation = Quaternion.identity;
        var soliderCom = solider.GetComponent<SoliderPlayer>();
        soliderCom.MoveToTarget();
    }

    Vector3 GetSoliderPosition()
    {
        var selfPosition = this.gameObject.transform.position;
        var random = new Random();
        var posx = random.Next((int)selfPosition.x,(int)selfPosition.x+5);
        var posy = random.Next((int)selfPosition.y,(int)selfPosition.y+5);
        var posz = random.Next((int)selfPosition.z,(int)selfPosition.z+5);
        return new Vector3(posx,posy,posz);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateSolider();
        }
    }
}
