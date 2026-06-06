using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;

    [SerializeField] private Rigidbody floater;
    [SerializeField] private FishSelectorComponent fishSelector;
    [SerializeField] private float fishBiteStrength = 1f;
    [SerializeField] private float reelTimeLimit = 0.75f;
    
    public bool isFishing { get; private set; }
    private Timer fishBiteTimer = new();
    private Timer reelTimer = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            reelTimer.timeSeconds = reelTimeLimit;
        }
    }

    public void FixedUpdate()
    {
        if (!isFishing)
            return;

        // When the fish bites the reel, start the "reeling time" for the player
        // And apply force to the floater
        if (fishBiteTimer.running && fishBiteTimer.IsDone())
        {
            reelTimer.Restart();
            fishBiteTimer.Stop();
            floater.AddForce(Vector3.down * fishBiteStrength, ForceMode.Impulse);
            Debug.Log("Fish bite!");
        }
        
        // If player did not press the button in the reel time, he failed!
        // So restart fishing
        if (reelTimer.running && reelTimer.IsDone())
        {
            Debug.Log("Failed to catch fish!");
            reelTimer.Stop();
            StartFishing();
        }
    }

    public void StartFishing()
    {
        isFishing = true;
        fishBiteTimer.timeSeconds = Random.Range(5f, 10f);
        fishBiteTimer.Restart();
    }

    public void StopFishing()
    {
        if (isFishing && reelTimer.running && !reelTimer.IsDone())
        {
            var fish = fishSelector.GetRandomFish();
            Debug.Log($"Caught a {fish.fishName}!");
            reelTimer.Stop();
        }
        isFishing = false;   
        fishBiteTimer.Stop();
    }
}
