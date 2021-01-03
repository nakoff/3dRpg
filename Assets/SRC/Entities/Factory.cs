using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class EntityPref
    {
        public Vector3 Pos;

        public EntityPref(Vector3 pos)
        {
            Pos = pos;
        }
    }

    public static class EntityFactory
    {
        private static Dictionary<ENTITY_TYPE, System.Action<EntityPref>> _enitites = new Dictionary<ENTITY_TYPE, System.Action<EntityPref>>
        {
            [ENTITY_TYPE.PLAYER] = pref => 
            {
                var view = EntityManager.CreateView<IPlayerView>("Prefabs/Player");   
                Logger.Print(view.ToString());
                var pres = new Player(view); 
            }
        };


        public static void CreatePlayer(Vector3 pos)
        {
            _enitites[ENTITY_TYPE.PLAYER](new EntityPref(pos));
        }
    }
}