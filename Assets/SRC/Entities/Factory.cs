using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class TypeId 
    {
        public ENTITY_TYPE Type { get; }
        public uint Id { get; }
        public TypeId (ENTITY_TYPE type, uint id) 
        {
            Type = type;
            Id = id;
        }
    }

    public class EntityPref
    {
        public Vector3 Pos;

        public EntityPref(){}
        public EntityPref(Vector3 pos)
        {
            Pos = pos;
        }
    }

    public static class EntityFactory
    {
        private static Dictionary<ENTITY_TYPE, System.Func<EntityPref,TypeId>> _enitites = new Dictionary<ENTITY_TYPE, System.Func<EntityPref, TypeId>>
        {
            [ENTITY_TYPE.PLAYER] = pref => 
            {
                var view = EntityManager.CreateView<Player.IPlayerView>("Entities/Player");   
                view.Position = pref.Pos;
                return new TypeId(view.EntityType, view.EntityId);
            },

            [ENTITY_TYPE.SPELL_FIREBALL_BIG] = pref =>
            {
                var view = EntityManager.CreateView<Spells.IFireballView>("Spells/IceBall");
                return new TypeId(view.EntityType, view.EntityId);
            }
        };


        public static TypeId CreatePlayer(Vector3 pos)
        {
            return _enitites[ENTITY_TYPE.PLAYER](new EntityPref(pos));
        }

        public static TypeId CreateFireball()
        {
            return _enitites[ENTITY_TYPE.SPELL_FIREBALL_BIG](new EntityPref());
        }
    }
}