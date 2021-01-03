using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class EntityPref
    {
        public string View;
        public Vector3 Pos;

        public EntityPref(string view, Vector3 pos)
        {
            View = view;
            Pos = pos;
        }
    }

    public static class EntityFactory
    {
        private static Dictionary<ENTITY_TYPE, System.Action<EntityPref>> _enitites = new Dictionary<ENTITY_TYPE, System.Action<EntityPref>>
        {
            [ENTITY_TYPE.PLAYER] = pref => { var pres = new Player(pref); }
        };


        public static void CreatePlayer(Vector3 pos)
        {
            _enitites[ENTITY_TYPE.PLAYER](new EntityPref("res://Scenes/Entities/Player.tscn", pos));
        }
    }
}