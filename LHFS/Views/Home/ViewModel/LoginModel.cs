﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LHFS.Views.Home.ViewModel {
	public class LoginModel {
		public string Username { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}