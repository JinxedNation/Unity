using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * @class LoadStability
 * @brief Loads the previous saved stability score and timestamp when saved
 *          Used to calculate the decay since the stability score last updated
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class LoadStability : MonoBehaviour
{
    public static StabilityScoreType loadStabilityScore(string filename)
    {
        IFormatter formatter = new BinaryFormatter();

        try
        {
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return (StabilityScoreType)formatter.Deserialize(stream);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("LoadStability: Failed to open filename : " + filename);

        }
        return null;
    }
}
