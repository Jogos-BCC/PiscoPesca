using System.Collections.Generic;
using UnityEngine;

public class IndexManager : MonoBehaviour
{
    public static IndexManager Instance { get; private set; }
    public List<FishItem> allFishItems = new List<FishItem>();

    private HashSet<string> caughtFishNames = new HashSet<string>();
    private const string SAVE_KEY = "CaughtFish";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProgress();
    }

    public void RegisterCatch(FishItem fish)
    {
        if (fish == null)
        {
            return;
        }
        bool isNew = caughtFishNames.Add(fish.fishName);
        SaveProgress();
    }

    public bool IsCaught(FishItem fish)
    {
        return fish != null && caughtFishNames.Contains(fish.fishName);
    }

    public int GetCaughtCount() => caughtFishNames.Count;
    public int GetTotalCount() => allFishItems.Count;

    void SaveProgress()
    {
        string data = string.Join(",", caughtFishNames);
        PlayerPrefs.SetString(SAVE_KEY, data);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        string data = PlayerPrefs.GetString(SAVE_KEY, "");
        if (string.IsNullOrEmpty(data)) return;
        foreach (string name in data.Split(','))
            if (!string.IsNullOrEmpty(name))
                caughtFishNames.Add(name);
    }

    [ContextMenu("Limpar Progresso (Debug)")]
    public void ClearProgress()
    {
        caughtFishNames.Clear();
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("[Codex] Progresso limpo!");
    }

    [ContextMenu("Registrar Todos (Debug)")]
    public void RegisterAllForDebug()
    {
        foreach (var fish in allFishItems)
            RegisterCatch(fish);
    }
}
