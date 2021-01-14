
namespace Datas
{
    public class AnimStateObject: IObject
    {
        public event System.Action<int> update = delegate{};
        public void EmitEvent(int change) 
        {
            update(change); 
        }

        public AnimStateObject(int type, int parentType, uint parentId) 
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

        public int curAnim;
        public int prevAnim;
        public int action;
        public bool isPlaying;
    }
}