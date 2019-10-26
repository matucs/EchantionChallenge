using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClientConsummer.Models
{
    public class ResultModel
    {
        public string Request { get; set; }
        public string Response { get; set; }
    }
    public class ListModel
    {
        public ListModel()
        {
            Items = new List<ResultModel>();
        }

        public IList<ResultModel> Items { get; set; }
    }
    public class MessagingModel
    {
        public MessagingModel()
        {
            _ResultModel = new ResultModel();
            _ListModel = new ListModel();
        }

        public MessagingModel(ResultModel resultModel, ListModel listModel)
        {
            _ResultModel = resultModel;
            _ListModel = listModel;
        }

        public ResultModel _ResultModel { get; set; }
        public ListModel _ListModel { get; set; }
        public string[] Text { get; set; }
    }
}
