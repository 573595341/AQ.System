using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    public class BaseRequest
    {

    }

    public class BaseRequest<TModel> where TModel : class
    {
        public TModel Data { get; set; }
    }
}
