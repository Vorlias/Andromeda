using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System.Experimental
{
    public static class GameSceneExtension
    {
        public static void SetGroupActive(this StateManager s, GameScene group)
        {
            s.StatesByPriority.Where(state => state.GetGroup() == group).ForEach(state => state.IsActive = true);
        }

        public static void SetGroupInactive(this StateManager s, GameScene group)
        {
            s.StatesByPriority.Where(state => state.GetGroup() == group).ForEach(state => state.IsActive = false);
        }

        public static string GetGroupName(this GameState c)
        {
            return GameScene.GetGroup(c).Name;
        }

        public static GameScene GetGroup(this GameState c)
        {
            return GameScene.GetGroup(c);
        }

        public static GameScene AddGrouped<T>(this StateManager s, string name, string groupName, GameStatePriority priority = GameStatePriority.Normal) where T : GameState, new()
        {
            s.Add<T>(name, priority);
            var res = s[name];
            res.AddToGroup(groupName);
            return res.GetGroup();
        }

        public static void AddToGroup(this GameState c, string groupName)
        {
            if (GameScene.HasGroup(groupName))
                GameScene.GetGroup(groupName).AddState(c);
            else
                GameScene.CreateGroup(groupName).AddState(c);
        }
    }

    public class GameScene
    {
        static Dictionary<string, GameScene> groups = new Dictionary<string, GameScene>();
        List<GameState> members;

        public GameScene()
        {
            members = new List<GameState>();
        }

        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public static GameScene CreateGroup(string name)
        {
            GameScene group = new GameScene();
            group.name = name;

            groups[name] = group;
            return group;
        }

        public static bool HasGroup(string name)
        {
            return groups.ContainsKey(name);
        }

        public static GameScene GetGroup(string name)
        {
            return groups[name];
        }

        public static GameScene GetGroup(GameState state)
        {
            return groups.Select(keyPair => keyPair.Value).Where(group => group.members.Contains(state)).First();
        }

        public void AddState(GameState c)
        {
            members.Add(c);
        }
    }
}
