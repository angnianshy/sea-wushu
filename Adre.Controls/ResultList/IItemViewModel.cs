using System;

namespace Adre.Controls.ResultList
{
    public interface IItemViewModel : IResult
    {
        StartList.IItemViewModel StartListItem { get; set; }
        

    }
}
