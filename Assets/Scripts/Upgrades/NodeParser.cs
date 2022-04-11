using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeParser : MonoBehaviour
{

    [SerializeField] private UpgradeTreeGraph _graph;
    [SerializeField] private CharacterStats _characterStats;

    public HashSet<PlayerUpgrade> GetAllAvailableUpgrades()
    {
        HashSet<PlayerUpgrade> upgrades = new HashSet<PlayerUpgrade>();
        foreach (var upgrade in _characterStats.Upgrades)
        {
            foreach (var nextUpgrade in GetNextUpgrades(upgrade))
            {
                if (_characterStats.Upgrades.Contains(nextUpgrade))
                    continue;
                var prerequisiteUpgrades = GetPreviousUpgrades(nextUpgrade);
                if (prerequisiteUpgrades.All(t => _characterStats.Upgrades.Contains(t)))
                {
                    upgrades.Add(nextUpgrade);
                }
            }
        }
        foreach (var upgrade in GetStartingUpgrades())
        {
            if (!_characterStats.Upgrades.Contains(upgrade))
            {
                upgrades.Add(upgrade);
            }
        }
        return upgrades;
    }

    private BaseNode GetNode(PlayerUpgrade upgrade)
    {
        return _graph.nodes.Cast<BaseNode>().FirstOrDefault(t => t.GetUpgrade() == upgrade);
    }

    private List<PlayerUpgrade> GetStartingUpgrades()
    {
        var startNode = _graph.StartingNode;
        var upgrades = AddAdjacentUpgradesToList(startNode);
        return upgrades;
    }

    private List<PlayerUpgrade> GetNextUpgrades(PlayerUpgrade upgrade)
    {
        var node = GetNode(upgrade);
        var upgrades = AddAdjacentUpgradesToList(node);
        return upgrades;
    }

    private List<PlayerUpgrade> GetPreviousUpgrades(PlayerUpgrade upgrade)
    {
        var upgrades = new List<PlayerUpgrade>();
        var node = GetNode(upgrade);
        foreach (var input in node.Inputs)
        {
            foreach (var nodePort in input.GetConnections())
            {
                var adjacentNode = nodePort.node as BaseNode;
                if (adjacentNode != null && adjacentNode.GetUpgrade() != null)
                {
                    upgrades.Add(adjacentNode.GetUpgrade());
                }
            }
        }
        return upgrades;
    }

    private List<PlayerUpgrade> AddAdjacentUpgradesToList(BaseNode node)
    {
        var upgrades = new List<PlayerUpgrade>();
        foreach (var output in node.Outputs)
        {
            foreach (var nodePort in output.GetConnections())
            {
                var adjacentNode = nodePort.node as BaseNode;
                if (adjacentNode != null && adjacentNode.GetUpgrade() != null)
                {
                    upgrades.Add(adjacentNode.GetUpgrade());
                }
            }
            
        }
        return upgrades;
    }
}
