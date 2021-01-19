
namespace Models
{
    public class CharacterObject: IObject
    {
        public event System.Action<int> update = delegate{};
        public void EmitEvent(int change) 
        {
            update(change); 
        }

        public CharacterObject(int type, int parentType, uint parentId)
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

        public float posX;
        public float posY;
        public float posZ;

        public float moveX;
        public float moveY;
        public float moveZ;

        public float rotX;
        public float rotY;

        public int health = 100;
        public int moveSpeed = 100;
        public int curState;

    }
}