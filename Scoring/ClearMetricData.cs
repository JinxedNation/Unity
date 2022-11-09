using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************
 * @class ClearMetricData
 * @brief Calls the metric index singleton to clear all metrics data
 *
 * @author William Halling
 * @version 01
 * @date 20/05/2022
 *************************/
public class ClearMetricData : MonoBehaviour
{
        /**********************************************************************************************************
         * Calls the metricindexing singleton to delete all scene attempts and clear the indexing dictionary
         * 
         **********************************************************************************************************/
    
    public void ClearMetrics()
    {
        MetricIndexing.Instance.ClearMetrics();
        StabilityManager.Instance.ClearStability();
    }
}
