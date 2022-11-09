using System.Collections.Generic;
using System.Linq;

/**
 * @class Collected
 * @brief Singleton Class to contain the players collected clickable items, used in the beach clean up and recycling scenes.
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class Collected
{
    /**
     * The singleton instance
     */
    private static Collected _instance;

    /**
     * Stops multiple threads accessing the singleton at the same time
     */
    private static readonly object ThreadLock = new object();

    /**
     * The collected items for the scene
     */
    private List<Collectable> _collected = new List<Collectable>();

    /**
     * If the player has collected any non natural items yet.
     */
    private bool _hasWaste;

    /**
     * Private constructor
     */
    private Collected()
    {
    }

    /**
     * Returns the Collected instance and creates it if required.
     */
    public static Collected Instance
    {
        get
        {
            lock (ThreadLock)
            {
                if (_instance != null) return _instance;

                _instance = new Collected();

                return _instance;
            }
        }
    }

    /**
     * Add a clickable item to the list
     */
    public void Add(Collectable item)
    {
        _collected.Add(item);
        item.gameObject.SetActive(false);
        if (!_hasWaste && item.GetItemType() != Collectable.ItemType.Natural)
        {
            _hasWaste = true;
        }
    }

    /**
     * Clears all collected items.
     */
    public void Clear()
    {
        _collected.Clear();
        _hasWaste = false;
    }

    /**
     * Return the list of collected items
     */
    public List<Collectable> GetCollected()
    {
        return _collected;
    }

    /**
     * Set the list of collected items
     */
    public void SetCollected(List<Collectable> collected)
    {
        _collected = collected;
    }

    public Collectable GetLast()
    {
        return _collected.Count > 0 ? _collected.Last() : null;
    }

    public void Remove(Collectable item)
    {
        _collected.Remove(item);
        _hasWaste = false;
        foreach (var collectable in _collected)
        {
            if (collectable.GetItemType() != Collectable.ItemType.Natural)
            {
                _hasWaste = true;
                break;
            }
        }
    }

    public bool HasWaste()
    {
        return _hasWaste;
    }
}