using System;
using System.Collections.Generic;
using System.Text;

namespace MrE.Repository.Abstractions
{ /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
    public interface IDataAuditor
    {
        void Audit(DataStoreContext context);
    }
}
