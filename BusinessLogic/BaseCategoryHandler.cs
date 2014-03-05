using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;

namespace BusinessLogic
{
    public abstract class BaseCategoryHandler
    {
        public abstract List<BaseCategory> LoadAll();

        public abstract bool Save(BaseCategory categoryToSave);
    }
}
