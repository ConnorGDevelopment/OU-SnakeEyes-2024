using UnityEngine;

public class Glasses : MonoBehaviour
{
    public GameObject glasses;
    public GameObject socket;
    public GameObject raveWorldObjects;
    public GameObject stage;
    private bool glassesCloseToSocket = false;
    public float detectionRange = 1f; // Adjust this value as needed
    public float activationDelay = 0.2f; // Adjust this value as needed
    public bool turnOn;
    // Start is called before the first frame update
    void Start()
    {
        SetRaveWorldObjectsActive(false);
        SetStageActive(false);
        turnOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the distance between glasses and socket is within the detection range
        //if (Vector3.Distance(glasses.transform.position, socket.transform.position) <= detectionRange)
        if(turnOn)
        {
            glassesCloseToSocket = true;
        }
        else
        {
            glassesCloseToSocket = false;
        }

        // Check if the glasses are close to the socket
        if (glassesCloseToSocket)
        {
            // Delay the activation of rave world objects after teleportation
            Invoke("ActivateRaveWorldObjects", activationDelay);
            glasses.transform.position = socket.transform.position;
            glasses.GetComponent<MeshRenderer>().enabled = false;
            glasses.transform.Find("Attach Left").gameObject.GetComponent<BoxCollider>().enabled = false;
            glasses.transform.Find("DISABLE").gameObject.SetActive(false);
            glasses.transform.Find("BOX GRAB").gameObject.SetActive(true);
            glasses.GetComponent<Rigidbody>().useGravity = false;
            glasses.GetComponent<Rigidbody>().freezeRotation = true;
        }
        else
        {
            SetRaveWorldObjectsActive(false);
            SetStageActive(false); 
            ExitRaveWorld();
            glasses.GetComponent<MeshRenderer>().enabled = true;
            glasses.transform.Find("DISABLE").gameObject.SetActive(true);
            glasses.transform.Find("Attach Left").gameObject.GetComponent<BoxCollider>().enabled = true;
            glasses.transform.Find("BOX GRAB").gameObject.SetActive(false);
            glasses.GetComponent<Rigidbody>().useGravity = true;
            glasses.GetComponent<Rigidbody>().freezeRotation = false;
        }
    }

    public void SetRaveWorldObjectsActive(bool active)
    {
        foreach (Transform child in raveWorldObjects.transform)
        {
            child.gameObject.SetActive(active);
        }
    }


    public void SetStageActive(bool active)
    {
        foreach (Transform child in stage.transform)
        {
            child.gameObject.SetActive(active);
        }
    }


        void ActivateRaveWorldObjects()
    {
        // Activate the rave world objects
        SetRaveWorldObjectsActive(true);
        SetStageActive(true);
        EnterRaveWorld();
    }

    void EnterRaveWorld()
    {
        // Add code here for entering the rave world if needed
    }

    void ExitRaveWorld()
    {
        // Add code here for exiting the rave world if needed
    }
}
