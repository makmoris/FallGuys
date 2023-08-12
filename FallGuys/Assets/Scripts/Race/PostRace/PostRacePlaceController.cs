using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;

public class PostRacePlaceController : PostLevelPlaceController
{
    //[Header("-----")]

    //[Header("UI")]
    //[SerializeField] private List<GameObject> uiElementsToDisable;

    //private PostRaceCanvasUIController postRaceCanvasUIController;

    //[Header("Places")]
    //[SerializeField] private GameObject placesGO;
    //[Space]
    //[SerializeField] private float minTimeBeforDump = 1f;
    //[SerializeField] private float maxTimeBeforDump = 2f;
    //[SerializeField] private float timeBeforeLoadNextLevel = 4f;

    //private List<PostPlace> postRacePlacesList = new List<PostPlace>();

    //private void Awake()
    //{
    //    if (postCameraGO.activeSelf) postCameraGO.SetActive(false);
    //    if (postUIGO.activeSelf) postUIGO.SetActive(false);

    //    postRaceCanvasUIController = postUIGO.GetComponent<PostRaceCanvasUIController>();

    //    //FillPostRacePlacesList();
    //}

    //private void Start()
    //{
    //    postRaceCanvasUIController = postLevelUIController as PostRaceCanvasUIController;
    //}

    public void StartPostRacing(List<WheelVehicle> winnersList, List<WheelVehicle> losersList, int numberOfWinners)
    {
        //размещаем тачки
        //foreach (var winDriver in winnersList)
        //{
        //    if(postRacePlacesList.Count > 0)
        //    {
        //        winDriver.gameObject.SetActive(true);

        //        PostPlace postRacePlace = postRacePlacesList[0];

        //        Transform placeTransform = postRacePlace.GetPlaceTransform();

        //        winDriver.transform.SetParent(null);

        //        winDriver.transform.position = placeTransform.position;
        //        winDriver.transform.rotation = placeTransform.rotation;

        //        winDriver.Handbrake = true;

        //        winDriver.Throttle = 0f;
        //        winDriver.Steering = 0f;

        //        winDriver.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //        postRacePlacesList.Remove(postRacePlace);
        //    }
        //}

        //foreach (var loserDriver in losersList)
        //{
        //    if (postRacePlacesList.Count > 0)
        //    {
        //        loserDriver.gameObject.SetActive(true);

        //        PostPlace postRacePlace = postRacePlacesList[0];

        //        Transform placeTransform = postRacePlace.GetPlaceTransform();

        //        loserDriver.transform.SetParent(null);

        //        loserDriver.transform.position = placeTransform.position;
        //        loserDriver.transform.rotation = placeTransform.rotation;

        //        loserDriver.Handbrake = false;

        //        loserDriver.Throttle = 0f;
        //        loserDriver.Steering = 0f;

        //        loserDriver.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //        float timeBeforDump = Random.Range(minTimeBeforDump, maxTimeBeforDump);

        //        postRacePlace.DumpThisCar(timeBeforDump);

        //        postRacePlacesList.Remove(postRacePlace);
        //    }
        //}

        if(numberOfWinners == 1)
        {
            // сразу показываем окно победителя
            GameObject winner = winnersList[0].gameObject;
            winner.GetComponent<Rigidbody>().isKinematic = true;

            //ShowWinnerWindow(winner);
        }
        else // если нет, значит игра продолжается
        {
            //postRaceCanvasUIController.SetNumberOfWinnersText(numberOfWinners);

            postCameraGO.SetActive(true);
            gameCameraGO.SetActive(false);

            //postUIGO.SetActive(true);
            //foreach (var element in uiElementsToDisable)
            //{
            //    element.SetActive(false);
            //}

            //StartCoroutine(WaitAndLoadNextLevel());

            //LoadNextGameStage();
        }
    }

    //private void FillPostRacePlacesList()
    //{
    //    for(int i = 0; i < placesGO.transform.childCount; i++)
    //    {
    //        postRacePlacesList.Add(placesGO.transform.GetChild(i).GetComponent<PostPlace>());
    //    }

    //    System.Random rand = new();

    //    for (int i = postRacePlacesList.Count - 1; i >= 1; i--)
    //    {
    //        int j = rand.Next(i + 1);

    //        var tmp = postRacePlacesList[j];
    //        postRacePlacesList[j] = postRacePlacesList[i];
    //        postRacePlacesList[i] = tmp;
    //    }
    //}

    //IEnumerator WaitAndLoadNextLevel()
    //{
    //    yield return new WaitForSeconds(timeBeforeLoadNextLevel);

    //    bool itWasNotTheLastGameStage = _gameManager.StartGameStage();

    //    if (!itWasNotTheLastGameStage)
    //    {
    //        // показываем окно с победителем
    //        Debug.Log("Show Winner Window");
    //    }
    //}
}
