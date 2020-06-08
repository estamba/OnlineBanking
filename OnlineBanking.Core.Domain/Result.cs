using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Core.Domain
{
    public class Result<T>
    {
       public T Data { get; set; }
      public  bool IsSuccessFul { get; set; }
    }
}
