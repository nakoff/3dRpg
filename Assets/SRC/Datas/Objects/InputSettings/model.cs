
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Datas
{
    public class InputSettingsModel:BaseModel<InputSettingsModel, InputSettingsObject>
    {
        public static OBJECT_TYPE Type = OBJECT_TYPE.SETTINGS;
        public static InputSettingsModel Create() => Create(Type, 0, 0); 
        public static InputSettingsModel Get() => GetAll(Type).FirstOrDefault(); 


        public enum CHANGE { ANY }
        public uint Id => obj.Id;

        public InputSettingsModel(InputSettingsObject obj):base(obj){}

        public int MouseSens
        {
            get => obj.mouseSens;
            set
            {
                obj.mouseSens = value;
                obj.EmitEvent((int)CHANGE.ANY);
            }
        }

    }
}