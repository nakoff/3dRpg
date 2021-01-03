
namespace Datas
{
    public class CharacterObject: IObject
    {
        public event System.Action<int> update;
        public void EmitEvent(int change) 
        {
            update(change); 
        }

        public CharacterObject(int type, int parentType, uint parentId)
        {
            Type = type;
            Id = DataManager.IdIncrement;
            ParentType = parentType;
            ParentId = parentId;
        }

        public int Type { get; private set; }
        public uint Id { get; private set; }
        public int ParentType { get; private set; }
        public uint ParentId { get; private set; }

        public float posX;
        public float posY;
        public float posZ;
        public int health;
        public int moveSpeed;
        public int moving;

    }
}