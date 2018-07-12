using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POC;

namespace TodoPOC
{
    public class TodoPOCItem : POCComponent
    {
        public class Data
        {
            public string Description { get; set; }

            public bool IsCheck { get; set; }

            public int ExtraBit { get; set; }
        }

        public Data DTO { get; set; }

        /// <summary>
        /// (親方->子供)データあるけど、子供まだない
        /// </summary>
        public override void Initialize()
        {
            Debug.Log($"__Item initialize__ have data {DTO} have parent {Parent}");
        }

        /// <summary>
        /// (子供->親方)データはある、子供もある
        /// </summary>
        public override void Mounted()
        {
            Debug.Log($"__Item mounted__ have data {DTO} have parent {Parent}");
        }

        public void SetCheckMark(bool input)
        {
            DTO.IsCheck = input;
        }
    }
}

