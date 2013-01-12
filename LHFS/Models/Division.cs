﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	[DisplayColumn("Name")]
	public class Division {
		[KeyAttribute]
		public int ID { get; set; }
		public string Name { get; set; }
	}
}