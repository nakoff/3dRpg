using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class Owner 
    {
        public ENTITY_TYPE Type { get; }
        public uint Id { get; }
        public Owner (ENTITY_TYPE type, uint id) 
        {
            Type = type;
            Id = id;
        }
    }

    public class EntityPref
    {
        public Vector3 Pos;
        public Owner owner;

        public EntityPref(Vector3 pos, Owner owner=null)
        {
            Pos = pos;
            this.owner = owner;
        }
    }

    public static class EntityFactory
    {
        private static Dictionary<ENTITY_TYPE, System.Action<EntityPref>> _enitites = new Dictionary<ENTITY_TYPE, System.Action<EntityPref>>
        {
            [ENTITY_TYPE.PLAYER] = pref => 
            {
                var view = EntityManager.CreateView<Player.IPlayerView>("Entities/Player");   
                view.Position = pref.Pos;
            },

            [ENTITY_TYPE.SPELL_FIREBALL_BIG] = pref =>
            {
                var view = EntityManager.CreateView<Spells.IFireballView>("Spells/IceBall");
                // view.owner = pref.owner;
                view.Position = pref.Pos;
            }
        };


        public static void CreatePlayer(Vector3 pos)
        {
            _enitites[ENTITY_TYPE.PLAYER](new EntityPref(pos));
        }

        public static void CreateFireball(Vector3 pos, ENTITY_TYPE ownerType, uint ownerId) 
        {
            _enitites[ENTITY_TYPE.SPELL_FIREBALL_BIG](new EntityPref(pos, new Owner(ownerType, ownerId)));
        }
    }
}