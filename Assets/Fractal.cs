using System.Collections;
using UnityEngine;

public class Fractal : MonoBehaviour { // extending to MonoBehaviour to make it work in Unity


    private static Vector3[] childDirections = { 
            Vector3.up, 
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back
    }; // Vector3 is object. Later on, for random motion, we call a random vector3 from childDirections. Not including down because that can hide the object



    private static Quaternion[] childOrientations = {
            Quaternion.identity, // this is unit value for a Quaternion. This can be done with Vector3, just for learning purpose, Quaternion is used
            Quaternion.Euler(0f, 0f, -90f), // -90 degree negative, z axis
            Quaternion.Euler(0f, 0f, 90f), // 90 degree positive, z axis
            Quaternion.Euler(90f, 0f, 0f), // 90 degree positive, x axis
            Quaternion.Euler(-90f, 0f, 0f), // -90 degree negative, x axis
    };


    // VECTOR3 IS DIRECTION, QUATERNION IS ORIENTATION OF CHILD FRACTAL



    public Mesh[] meshes; // letting the user pick their shapes (cubes, spheres) in Unity
    public Material material; // material of fractal
    public int maxDepth; // how many children of fractal
    public float childScale; // size of fractal child
    public float spawnProbability; 
    public float maxRotationSpeed; // max rotation speed for fractal children
    public float maxTwist;

    private float rotationSpeed;
    private int depth; 
    private Material[,] materials; // 2D array of Material


    private void InitializeMaterials() {

        materials = new Material[maxDepth + 1, 2]; // intializing the array
        for (int i = 0; i <= maxDepth; i++) {
            float t = i / (maxDepth - 1f);
            t *= t; // CONTROLS THE CHANGE OF COLOR
            materials[i, 0] = new Material(material); 
            materials[i, 0].color = Color.Lerp(Color.white, Color.magenta, t); // picks the color between white and blue based on t's change
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.yellow;
            // ================================================================EVERYTHING ABOVE HERE IS TO ASSIGN COLOR TO FRACTAL OBJECTS

        }
        materials[maxDepth, 0].color = Color.yellow;
        materials[maxDepth, 1].color = Color.yellow;

		// THE ABOVE COLORS ARE YELLOW FOR THE STARRYNIGHTS PROJECT WHERE THE STARS NEEDED TO BE A CONSTANT YELLOW

    }

    private void Start() {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed); // letting max rotation speeds be between negative and positive values
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f); // watch out for uppercase R in Rotate; its an object. 
        if (materials == null)
        {
            InitializeMaterials();
        }
        gameObject.AddComponent<MeshFilter>().mesh =
            meshes[Random.Range(0, meshes.Length)]; // random mesh given to the gameObject blueprint. Uppercase GameObject is Unity object. Lowercase gameObject is the current object
        gameObject.AddComponent<MeshRenderer>().material =
            materials[depth, Random.Range(0, 2)]; // MeshRenderer is Material in Unity
        // gameObject.AddComponent<Transform>.Rotation = transform.Rotate;
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren()); // until the maxDepth is reached, children need to be produced. STARTCOUROUNTINE BELOW USES IENUMERATOR TO START MORE CHILDREN
        }
    }

    private IEnumerator CreateChildren() {
        for (int i = 0; i < childDirections.Length; i++)
        {
            if (Random.value < spawnProbability)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.2f)); // waiting for random interval of seconds
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
            }
        }
    }

    private void Initialize(Fractal parent, int childIndex) { // allowing user to chose level of children
        meshes = parent.meshes;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        spawnProbability = parent.spawnProbability;
        maxRotationSpeed = parent.maxRotationSpeed;
        maxTwist = parent.maxTwist;
        transform.parent = parent.transform; // transform.parent selects the object to be transformed (parent) which is assigned the transformation status of parent by doing parent.transform
        transform.localScale = Vector3.one /*Vector3.one is unit value */ * childScale; // identity Vector converted childScale to a vector
        transform.localPosition = childDirections[childIndex] * (2f + 40f * childScale);
        transform.localRotation = childOrientations[childIndex];
    }

    private void Update() {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}


