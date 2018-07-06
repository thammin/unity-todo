using MarkLight;
using MarkLight.Views.UI;

namespace TodoMarkLight
{
    public class TodoMarkLightItemModel
    {
        public string Description;
    }

    public class TodoMarkLightItem : UIView
    {
        public _bool IsCheck;

        public _bool IsShowCheckMark;

        public void ToggleCheckMark()
        {
            IsShowCheckMark.Value = IsCheck.Value;
        }
    }
}
