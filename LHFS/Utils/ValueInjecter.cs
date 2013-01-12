using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.ValueInjecter;

namespace LHFS.Utils
{
	public class NotNullValueInjection : LoopValueInjection 
    {
		protected override bool AllowSetValue(object value) {
			return value != null;
		}
    }
}
