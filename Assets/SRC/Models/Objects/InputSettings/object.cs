
namespace Models
{
    public class InputSettingsObject: IObject
    {
        public event System.Action<int> update = delegate{};
        public void EmitEvent(int change) 
        {
            update(change); 
        }

        public InputSettingsObject(int type, int parentType, uint parentId) 
        {
            Type = type;
            Id = ObjectManager.IdIncrement;
            ParentType = parentType;
            ParentId = parentId;
        }

        public int Type { get; private set; }
        public uint Id { get; private set; }
        public int ParentType { get; private set; }
        public uint ParentId { get; private set; }

        public int mouseSens = 100;
    }
}