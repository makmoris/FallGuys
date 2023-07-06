using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class PostRacePlaceController : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private GameObject gameRaceCamera;
    [SerializeField] private GameObject postRaceCamera;

    [Header("Canvases")]
    [SerializeField] private GameObject gameRaceCanvas;
    [SerializeField] private GameObject postRaceCanvas;

    private PostRaceCanvasUIController postRaceCanvasUIController;

    [Header("Places")]
    [SerializeField] private GameObject placesGO;
    [Space]
    [SerializeField] private float minTimeBeforDump = 1f;
    [SerializeField] private float maxTimeBeforDump = 2f;
    [SerializeField] private float timeBeforeLoadNextLevel = 4f;

    private List<PostRacePlace> postRacePlacesList = new List<PostRacePlace>();

    private void Awake()
    {
        if (postRaceCamera.activeSelf) postRaceCamera.SetActive(false);
        if (postRaceCanvas.activeSelf) postRaceCanvas.SetActive(false);

        postRaceCanvasUIController = postRaceCanvas.GetComponent<PostRaceCanvasUIController>();

        FillPostRacePlacesList();
    }

    public void StartPostRacing(List<WheelVehicle> winnersList, List<WheelVehicle> losersList, int numberOfWinners, bool currentPlayerIsWinner)
    {
        //размещаем тачки
        foreach (var winDriver in winnersList)
        {
            if(postRacePlacesList.Count > 0)
            {
                winDriver.gameObject.SetActive(true);

                PostRacePlace postRacePlace = postRacePlacesList[0];

                Transform placeTransform = postRacePlace.GetPlaceTransform();

                winDriver.transform.SetParent(null);

                winDriver.transform.position = placeTransform.position;
                winDriver.transform.rotation = placeTransform.rotation;

                winDriver.Handbrake = true;

                winDriver.Throttle = 0f;
                winDriver.Steering = 0f;

                winDriver.GetComponent<Rigidbody>().velocity = Vector3.zero;

                postRacePlacesList.Remove(postRacePlace);
            }
        }

        List<string> losersNames = new List<string>();

        foreach (var loserDriver in losersList)
        {
            if (postRacePlacesList.Count > 0)
            {
                loserDriver.gameObject.SetActive(true);

                PlayerName playerName = loserDriver.gameObject.GetComponent<PlayerName>();
                if (playerName != null) losersNames.Add(playerName.Name);
                else throw new System.Exception("GameObject have not PlayerName component");

                PostRacePlace postRacePlace = postRacePlacesList[0];

                Transform placeTransform = postRacePlace.GetPlaceTransform();

                loserDriver.transform.SetParent(null);

                loserDriver.transform.position = placeTransform.position;
                loserDriver.transform.rotation = placeTransform.rotation;

                loserDriver.Handbrake = false;

                loserDriver.Throttle = 0f;
                loserDriver.Steering = 0f;

                loserDriver.GetComponent<Rigidbody>().velocity = Vector3.zero;

                float timeBeforDump = Random.Range(minTimeBeforDump, maxTimeBeforDump);

                postRacePlace.DumpThisCar(timeBeforDump);

                postRacePlacesList.Remove(postRacePlace);
            }
        }

        postRaceCanvasUIController.SetNumberOfWinnersText(numberOfWinners);

        postRaceCamera.SetActive(true);
        gameRaceCamera.SetActive(false);

        postRaceCanvas.SetActive(true);
        gameRaceCanvas.SetActive(false);

        if (currentPlayerIsWinner)
        {
            postRaceCanvasUIController.ShowPlayerWinWindow(losersNames, timeBeforeLoadNextLevel);
        }
        else
        {
            postRaceCanvasUIController.ShowPlayerLoseWindow();
        }
    }

    private void FillPostRacePlacesList()
    {
        for(int i = 0; i < placesGO.transform.childCount; i++)
        {
            postRacePlacesList.Add(placesGO.transform.GetChild(i).GetComponent<PostRacePlace>());
        }

        System.Random rand = new();

        for (int i = postRacePlacesList.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            var tmp = postRacePlacesList[j];
            postRacePlacesList[j] = postRacePlacesList[i];
            postRacePlacesList[i] = tmp;
        }
    }
}
