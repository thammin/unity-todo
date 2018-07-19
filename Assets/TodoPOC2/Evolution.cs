using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POC;
using UniRx;
using System.Linq;

public class Evolution : POCComponent
{

    #region ここら辺は全部APIに入るもの
    public class Girl
    {
        public ReactiveProperty<string> Name { get; set; }
        public ReactiveProperty<int> ProgressCurrent { get; set; }
        public ReactiveProperty<int> ProgressTotal { get; set; }
        public ReactiveCollection<Story> Stories { get; set; }
        public ReactiveProperty<bool> IsSelected { get; set; }
    }
    public class Story
    {
        public ReactiveProperty<string> Title { get; set; }
        public ReactiveProperty<int> NeededLove { get; set; }
        public ReactiveProperty<bool> IsNew { get; set; }
        public ReactiveProperty<bool> IsLock { get; set; }
    }
    #endregion

    public class Data
    {
        public ReactiveProperty<int> AP { get; set; }
        public ReactiveProperty<int> Diamond { get; set; }
        public ReactiveProperty<int> Love { get; set; }
        public ReactiveProperty<int> Money { get; set; }
        public ReactiveCollection<Girl> Girls { get; set; }

        // TODO computed 系
        public ReactiveProperty<string> SelectedGirlName { get; set; }
        public ReactiveProperty<string> SelectedGirlProgress { get; set; }
        public ReactiveCollection<Story> SelectedGirlStories { get; set; }
    }

    public Data DTO { get; set; }

    public override void Initialize()
    {
        // TODO データ依存アルゴリズムできたらなくなる
        DTO = new Data()
        {
            AP = new ReactiveProperty<int>(6789),
            Diamond = new ReactiveProperty<int>(6666),
            Love = new ReactiveProperty<int>(7777),
            Money = new ReactiveProperty<int>(9999),
            Girls = new ReactiveCollection<Girl>(
                Enumerable
                    .Range(1, 20)
                    .Select(i => new Girl()
                    {
                        Name = new ReactiveProperty<string>($"ガール {i}"),
                        ProgressCurrent = new ReactiveProperty<int>(i),
                        ProgressTotal = new ReactiveProperty<int>(30),
                        Stories = new ReactiveCollection<Story>(
                            Enumerable
                                .Range(1, 10)
                                .Select(n => new Story()
                                {
                                    Title = new ReactiveProperty<string>($"タイトル {i}-{n}"),
                                    NeededLove = new ReactiveProperty<int>(n * 5),
                                    IsNew = new ReactiveProperty<bool>(n == 4),
                                    IsLock = new ReactiveProperty<bool>(n > 4)
                                })
                                .ToList()

                        ),
                        IsSelected = new ReactiveProperty<bool>(i == 1)
                    })
                    .ToList()
            )
        };
        DTO.SelectedGirlName = new ReactiveProperty<string>(DTO.Girls[0].Name.Value);
        DTO.SelectedGirlProgress = new ReactiveProperty<string>($"{DTO.Girls[0].ProgressCurrent}/{DTO.Girls[0].ProgressTotal}");
        DTO.SelectedGirlStories = new ReactiveCollection<Story>(DTO.Girls[0].Stories.Reverse());
    }

    public void SetSelectedGirlIndex(EvolutionGirl girl)
    {
        var index = DTO.Girls.IndexOf(girl.DTO);

        // TODO computed みたいの機能あればなくなる
        DTO.SelectedGirlName.Value = girl.DTO.Name.Value;
        DTO.SelectedGirlProgress.Value = $"{girl.DTO.ProgressCurrent}/{girl.DTO.ProgressTotal}";
        DTO.SelectedGirlStories = new ReactiveCollection<Story>(girl.DTO.Stories.Reverse());
        for (var i = 0; i < DTO.Girls.Count; i++)
        {
            DTO.Girls[i].IsSelected.Value = index == i;
        }
    }
}
