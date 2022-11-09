using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * @class SaveStability
 * @brief Saves new Stability score to file for progression
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public static class SaveStability
{
    public static void SaveStabilityData(StabilityScoreType stability, string filename)
    {

        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, stability);
            stream.Close();
            Debug.Log("SaveMetrics: Successfully saved attempt as : " + filename);
        }
    }
}
