using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace DataAccess
{
    public abstract class BaseCategoryAccess
    {
        public abstract List<BaseCategory> LoadAll();
    }
}
