using UnityEngine;

public class Sun : MonoBehaviour
{
    public static Sun instance;

    enum Method
    {
        VERLET,
        RUNGEKUTTA,
        ANALYTICAL
    }


    [SerializeField] float addedSpeed;
    [SerializeField] public float deltaTime;

    [SerializeField] Method typeOfMovement;

    [SerializeField] public Orbit[] allOrbits;

    public Vector3 position;


    [SerializeField] public GameObject focus;
    
    void Awake()
    {
        instance = this;
        position = transform.position;
    }


    private void Update()
    {
        switch (typeOfMovement)
        {
            case Method.VERLET:
                foreach (Orbit orb in allOrbits)
                {
                    orb.MoveVerlet(addedSpeed);
                }
                break;



            case Method.RUNGEKUTTA:
                break;




            case Method.ANALYTICAL:

                foreach (Orbit orb in allOrbits)
                {
                    orb.MoveAnalytical(addedSpeed);
                }

                break;


                
            default:
                break;
        }

        MoveCamera();
    }


    private void MoveCamera()
    {
        if (Input.GetKey(KeyCode.D)) {

            Camera c = Camera.main;
            Vector3 dir = c.transform.right / 10;
            c.transform.position += dir;


            c.transform.LookAt(focus.transform);

        }
        else if (Input.GetKey(KeyCode.A)) {

            Camera c = Camera.main;
            Vector3 dir = c.transform.right / 10;
            c.transform.position -= dir;

            c.transform.LookAt(focus.transform);
        }
        else if (Input.GetKey(KeyCode.W)) {

            Camera c = Camera.main;
            Vector3 dir = c.transform.forward / 10;
            c.transform.position += dir;
            
            c.transform.LookAt(focus.transform);
        }
        else if (Input.GetKey(KeyCode.S)) {
            
            Camera c = Camera.main;
            Vector3 dir = c.transform.forward / 10;
            c.transform.position -= dir;
        
            c.transform.LookAt(focus.transform);
        }

        else if (Input.GetKey(KeyCode.Space))
        {

            Camera c = Camera.main;
            Vector3 dir = c.transform.up / 10;
            c.transform.position += dir;

            c.transform.LookAt(focus.transform);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera c = Camera.main;
            Vector3 dir = c.transform.up / 10;
            c.transform.position -= dir;

            c.transform.LookAt(focus.transform);
        }

        else if (Input.GetKey(KeyCode.M))
        {
            Vector3 dir = Camera.main.transform.forward;

            focus.transform.position = new Vector3(
                focus.transform.position.x + dir.x,
                focus.transform.position.y,
                focus.transform.position.z + dir.z);

            Camera.main.transform.LookAt(focus.transform);
        }
        else if (Input.GetKey(KeyCode.N))
        {
            Vector3 dir = Camera.main.transform.forward;

            focus.transform.position = new Vector3(
                focus.transform.position.x - dir.x,
                focus.transform.position.y,
                focus.transform.position.z - dir.z);

            Camera.main.transform.LookAt(focus.transform);
        }

    }
}
