
namespace Entities
{

    public interface IAnimatedView
    {
        enum CHANGED { ANIMATION_FINISHED, }
        void Subscribe(System.Action<CHANGED> listener);
        void ChangeAnimation(string animName);
    }
}