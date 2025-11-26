using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;

public class SceneManager : MonoBehaviour
{
    public GameObject capsule;
    public GameObject capsuleTracker;
    public GameObject cube;
    public GameObject cubeTracker;
    public GameObject sphere;
    public GameObject sphereTracker;
    public GameObject LineAndText;

    public TextMeshProUGUI distanceText;

    TextMeshProUGUI distanceTextBis;
    GameObject textObject;
    public Canvas canvas;

    public TMP_FontAsset policeText;

    GameObject lineObject;
    LineRenderer line;
    public Material lineMaterial;

    float dist = 0.0f;
    Vector3 middlePoint;

    float perimetre = 0.0f;
    float aire = 0.0f;

    public TextMeshProUGUI perimetreText;
    public TextMeshProUGUI areaText;

    // Nouvelle variable pour garder en mémoire le texte flottant de l'aire
    private TextMeshProUGUI floatingAreaText;

    void Update()
    {
        var trackableCapsule = capsuleTracker.GetComponent<ObserverBehaviour>();
        var statusCapsule = trackableCapsule.TargetStatus.Status;

        var trackableCube = cubeTracker.GetComponent<ObserverBehaviour>();
        var statusCube = trackableCube.TargetStatus.Status;

        var trackableSphere = sphereTracker.GetComponent<ObserverBehaviour>();
        var statusSphere = trackableSphere.TargetStatus.Status;

        if (statusCube == Status.TRACKED && statusSphere == Status.TRACKED && statusCapsule == Status.TRACKED)
        {
            LineAndText.SetActive(true);

            // --- Calcul des côtés ---
            var distLineSphere = traceLine(cube, sphere, "LineSphereCube", "lineSphereCubeText");
            var distLineCapsule = traceLine(capsule, sphere, "LineSphereCapsule", "lineSphereCapsuleText");
            var distLineCube = traceLine(capsule, cube, "Line", "LineText");

            // --- Calcul du Périmètre ---
            perimetre = distLineCapsule + distLineCube + distLineSphere;

            if (perimetreText != null)
                perimetreText.text = "Périmètre = " + perimetre.ToString("F2") + " cm";

            // --- Calcul de l'Aire (Héron) ---
            float s = perimetre / 2.0f;
            aire = Mathf.Sqrt(s * (s - distLineSphere) * (s - distLineCapsule) * (s - distLineCube));

            // Affichage dans le text UI fixe (si existant)
            if (areaText != null)
                areaText.text = "Aire = " + aire.ToString("F2") + " cm²";

            // --- NOUVEAU : Affichage Flottant au centre ---
            // 1. On trouve le centre du triangle (moyenne des 3 positions)
            Vector3 centerPosition = (cube.transform.position + capsule.transform.position + sphere.transform.position) / 3f;

            // 2. On appelle la fonction pour afficher le texte à cet endroit
            UpdateFloatingAreaText(aire, centerPosition);
        }
        else
        {
            LineAndText.SetActive(false);

            // Si on perd le tracking, on cache aussi le texte de l'aire
            if (floatingAreaText != null)
                floatingAreaText.gameObject.SetActive(false);
        }
    }

    // Nouvelle fonction pour gérer le texte de l'aire au milieu
    void UpdateFloatingAreaText(float areaValue, Vector3 worldPosition)
    {
        string objectName = "FloatingAreaLabel";

        // Si le texte n'existe pas encore, on le crée (comme dans traceLine)
        if (floatingAreaText == null)
        {
            // On vérifie s'il existe déjà dans la scène pour éviter les doublons
            GameObject existingObj = GameObject.Find(objectName);

            if (existingObj == null)
            {
                GameObject newObj = new GameObject(objectName);
                newObj.transform.parent = canvas.transform;

                floatingAreaText = newObj.AddComponent<TextMeshProUGUI>();
                floatingAreaText.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 200);

                if (policeText != null)
                    floatingAreaText.font = policeText;

                floatingAreaText.fontSize = 50; // Un peu plus gros
                floatingAreaText.color = Color.blue; // En bleu pour différencier du rouge
                floatingAreaText.alignment = TextAlignmentOptions.Center;
                floatingAreaText.fontStyle = FontStyles.Bold;
            }
            else
            {
                floatingAreaText = existingObj.GetComponent<TextMeshProUGUI>();
            }
        }

        // On s'assure qu'il est activé
        floatingAreaText.gameObject.SetActive(true);

        // Mise à jour du texte
        floatingAreaText.text = "Aire\n" + areaValue.ToString("F2") + " cm²";

        // Mise à jour de la position écran
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        floatingAreaText.transform.position = screenPos;
    }

    float traceLine(GameObject firstPoint, GameObject endPoint, string LineName, string LineNameText)
    {
        dist = Vector3.Distance(firstPoint.transform.position, endPoint.transform.position);

        if (GameObject.Find(LineName) == null)
        {
            lineObject = new GameObject(LineName);
            lineObject.transform.parent = LineAndText.transform;
            line = lineObject.AddComponent<LineRenderer>();
            line.widthMultiplier = 0.01f;
            line.material = lineMaterial;
        }

        if (GameObject.Find(LineNameText) == null)
        {
            textObject = new GameObject(LineNameText);
            textObject.transform.parent = canvas.transform;

            distanceTextBis = textObject.AddComponent<TextMeshProUGUI>();
            distanceTextBis.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);

            if (policeText != null)
            {
                distanceTextBis.font = policeText;
            }

            distanceTextBis.fontSize = 40;
            distanceTextBis.color = Color.red;

            distanceTextBis.alignment = TextAlignmentOptions.Center;
        }

        line.enabled = true;

        line = GameObject.Find(LineName).GetComponent<LineRenderer>();
        line.SetPosition(0, firstPoint.transform.position);
        line.SetPosition(1, endPoint.transform.position);

        distanceTextBis = GameObject.Find(LineNameText).GetComponent<TextMeshProUGUI>();
        distanceTextBis.text = dist.ToString("F2") + " cm";

        middlePoint = (firstPoint.transform.position + endPoint.transform.position) / 2;
        Vector3 posToStick = Camera.main.WorldToScreenPoint(middlePoint);
        distanceTextBis.transform.position = posToStick;

        return dist;
    }
}