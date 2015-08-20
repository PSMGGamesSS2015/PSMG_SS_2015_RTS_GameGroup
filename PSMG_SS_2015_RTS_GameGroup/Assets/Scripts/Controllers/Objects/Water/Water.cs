using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects.Water
{
    public class Water : MonoBehaviour
    {
        //Our renderer that'll make the top of the water visible
        private LineRenderer body;

        //Our physics arrays
        private float[] xpositions;
        private float[] ypositions;
        private float[] velocities;
        private float[] accelerations;

        //Our meshes and colliders
        private GameObject[] meshobjects;
        private GameObject[] colliders;
        private Mesh[] meshes;

        // To be assigned in the editor
        public GameObject SplashPrefab;
        public Material Mat;
        public GameObject Watermesh;

        //All our constants
        private const float Springconstant = 0.02f;
        private const float Damping = 0.04f;
        private const float Spread = 0.05f;
        private const float Z = -1f;

        //The properties of our water
        private float baseheight;
        // ReSharper disable once NotAccessedField.Local
        private float left;
        private float bottom;

        public void Splash(float xpos, Collider2D collider, float velocity)
        {
            //If the position is within the bounds of the water:
            if (IsPositionWithinWater(xpos)) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;
            //Offset the x position to be the distance from the left side
            xpos -= xpositions[0];

            //Find which spring we're touching
            var index = Mathf.RoundToInt((xpositions.Length - 1)*(xpos/(xpositions[xpositions.Length - 1] - xpositions[0])));

            //Add the velocity of the falling object to the spring
            velocities[index] += velocity;

            //Set the lifetime of the particle system.
            var lifetime = 0.93f + Mathf.Abs(velocity)*0.07f;

            //Set the splash to be between two values in Shuriken by setting it twice.
            var particleSystem = SplashPrefab.GetComponent<ParticleSystem>();
            particleSystem.startSpeed = 8 + 2*Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            particleSystem.startSpeed = 9 + 2*Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            particleSystem.startLifetime = lifetime;
            particleSystem.GetComponent<Renderer>().sortingLayerName = SortingLayerReferences.MiddleForeground;

            //Set the correct position of the particle system.
            //var position = new Vector3(xpositions[index], ypositions[index] - 0.1f, 5);

            var position = new Vector3(xpositions[index], collider.transform.position.y, 5);

            ////This line aims the splash towards the middle. Only use for small bodies of water:
            //var rotation =
            //    Quaternion.LookRotation(
            //        new Vector3(xpositions[Mathf.FloorToInt(xpositions.Length/2)], baseheight + 8, 5) - position);

            //Create the splash and tell it to destroy itself.
            var splash = (GameObject)Instantiate(SplashPrefab, position, Quaternion.identity);

            Destroy(splash, lifetime + 0.3f);
        }

        private bool IsPositionWithinWater(float xpos)
        {
            return !(xpos >= xpositions[0]) || !(xpos <= xpositions[xpositions.Length - 1]);
        }

        public void SpawnWater(float left, float width, float top, float bottom)
        {

            //Calculating the number of edges and nodes we have
            var edgecount = Mathf.RoundToInt(width)*5;
            var nodecount = edgecount + 1;

            //Add our line renderer and set it up:
            body = gameObject.AddComponent<LineRenderer>();
            body.material = Mat;
            body.material.renderQueue = 1000;
            body.SetVertexCount(nodecount);
            body.SetWidth(0.1f, 0.1f);

            //Declare our physics arrays
            xpositions = new float[nodecount];
            ypositions = new float[nodecount];
            velocities = new float[nodecount];
            accelerations = new float[nodecount];

            //Declare our mesh arrays
            meshobjects = new GameObject[edgecount];
            meshes = new Mesh[edgecount];
            colliders = new GameObject[edgecount];

            //Set our variables
            baseheight = top;
            this.bottom = bottom;
            this.left = left;

            //For each node, set the line renderer and our physics arrays
            for (var i = 0; i < nodecount; i++)
            {
                ypositions[i] = top;
                xpositions[i] = left + width*i/edgecount;
                body.SetPosition(i, new Vector3(xpositions[i], top, Z));
                accelerations[i] = 0;
                velocities[i] = 0;
            }

            //Setting the meshes now:
            for (var i = 0; i < edgecount; i++)
            {
                //Make the mesh
                meshes[i] = new Mesh();

                //Create the corners of the mesh
                var vertices = new Vector3[4];
                vertices[0] = new Vector3(xpositions[i], ypositions[i], Z);
                vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], Z);
                vertices[2] = new Vector3(xpositions[i], bottom, Z);
                vertices[3] = new Vector3(xpositions[i + 1], bottom, Z);

                //Set the UVs of the texture
                var uVs = new Vector2[4];
                uVs[0] = new Vector2(0, 1);
                uVs[1] = new Vector2(1, 1);
                uVs[2] = new Vector2(0, 0);
                uVs[3] = new Vector2(1, 0);

                //Set where the triangles should be.
                var tris = new[] {0, 1, 3, 3, 2, 0};

                //Add all this data to the mesh.
                meshes[i].vertices = vertices;
                meshes[i].uv = uVs;
                meshes[i].triangles = tris;

                //Create a holder for the mesh, set it to be the manager's child
                meshobjects[i] = (GameObject) Instantiate(Watermesh, Vector3.zero, Quaternion.identity);
                meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
                meshobjects[i].transform.parent = transform;

                //Create our colliders, set them be our child
                colliders[i] = new GameObject();
                colliders[i].name = "Trigger";
                colliders[i].AddComponent<BoxCollider2D>();
                colliders[i].transform.parent = transform;

                //Set the position and scale to the correct dimensions
                colliders[i].transform.position = new Vector3(left + width*(i + 0.5f)/edgecount, top - 0.5f, 0);
                colliders[i].transform.localScale = new Vector3(width/edgecount, 1, 1);

                //Add a WaterDetector and make sure they're triggers
                colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
                colliders[i].AddComponent<WaterDetector>();

                WaterHasBeenSpawned = true;
            }
        }

        //Same as the code from in the meshes before, set the new mesh positions
        private void UpdateMeshes()
        {
            for (var i = 0; i < meshes.Length; i++)
            {
                var vertices = new Vector3[4];
                vertices[0] = new Vector3(xpositions[i], ypositions[i], Z);
                vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], Z);
                vertices[2] = new Vector3(xpositions[i], bottom, Z);
                vertices[3] = new Vector3(xpositions[i + 1], bottom, Z);

                meshes[i].vertices = vertices;
            }
        }

        public void FixedUpdate()
        {
            if (!WaterHasBeenSpawned) return;

            HandlePhysicsOfSprings();

            MakePassesForFluidity();

            UpdateMeshes();
        }

        private void MakePassesForFluidity()
        {
            //Now we store the difference in heights:
            var leftDeltas = new float[xpositions.Length];
            var rightDeltas = new float[xpositions.Length];

            //We make 8 small passes for fluidity:
            for (var j = 0; j < 8; j++)
            {
                for (var i = 0; i < xpositions.Length; i++)
                {
                    //We check the heights of the nearby nodes, adjust velocities accordingly, record the height differences
                    if (i > 0)
                    {
                        leftDeltas[i] = Spread*(ypositions[i] - ypositions[i - 1]);
                        velocities[i - 1] += leftDeltas[i];
                    }
                    if (i >= xpositions.Length - 1) continue;

                    rightDeltas[i] = Spread*(ypositions[i] - ypositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }

                //Now we apply a difference in position
                for (var i = 0; i < xpositions.Length; i++)
                {
                    if (i > 0)
                        ypositions[i - 1] += leftDeltas[i];
                    if (i < xpositions.Length - 1)
                        ypositions[i + 1] += rightDeltas[i];
                }
            }
        }

        private void HandlePhysicsOfSprings()
        {
            for (var i = 0; i < xpositions.Length; i++)
            {
                var force = Springconstant*(ypositions[i] - baseheight) + velocities[i]*Damping;
                accelerations[i] = -force;
                ypositions[i] += velocities[i];
                velocities[i] += accelerations[i];
                body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], Z));
            }
        }

        public bool WaterHasBeenSpawned { get; set; }
    }
}