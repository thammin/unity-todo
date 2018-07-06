using MarkLight;

namespace TodoMarkLight
{
    public class TodoMarkLight : View
    {
        public _string Input;

        public ObservableList<TodoMarkLightItemModel> Items;

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // 本来はWorkからデータをモデルを作るけど割愛する
            Items = new ObservableList<TodoMarkLightItemModel>();
            for (var i = 0; i < 5; i++)
            {
                Items.Add(new TodoMarkLightItemModel
                {
                    Description = $"item {i}"
                });
            }
        }

        public void AddItem()
        {
            Items.Add(new TodoMarkLightItemModel
            {
                Description = Input.Value
            });
            Input.Value = "";
        }
    }
}
