using System;
using Models;
using UnityEngine;

namespace Entities.Freezer
{

    public class InteractSystem
    {

        private FreezerPres _freezer;
        private InteractStateModel _interactState;
        private CharacterModel _targetCharacter;

        public InteractSystem(FreezerPres entity, InteractStateObject interactObj)
        {
            _freezer = entity;
            _interactState = new InteractStateModel(interactObj);
            _interactState.Subscribe(OnInteractControllerChanged);
        }

        private void OnInteractControllerChanged(int change)
        {
            switch ((InteractStateModel.CHANGE)change)
            {
                case InteractStateModel.CHANGE.COLLIDE:
                    CollidedEntities(_interactState.Target);
                    break;
            }
        }

        private void CollidedEntities(InteractStateModel.TargetEntity target)
        {
            Logger.Print("Freezer interact with: "+target.Type.ToString());
            _targetCharacter = CharacterModel.GetByParent(target.Type, target.Id);
        }

        public void OnUpdate(float dt) 
        {
            if (_targetCharacter == null)
                return;
            _targetCharacter.Moving = Vector3.zero;
        }

        public void OnDelete() 
        {
            _interactState.UnSubscribes();
        }
    }
}