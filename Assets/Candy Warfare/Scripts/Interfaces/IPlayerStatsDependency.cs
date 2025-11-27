using System.Xml.Serialization;
using UnityEngine;

public interface IPlayerStatsDependency
{
    void UpdateStats(PlayerStatsManager statsManager);
}
