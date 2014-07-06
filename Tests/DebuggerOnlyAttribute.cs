﻿using System.Diagnostics;
using Xunit;

namespace Tests
{
	public class DebuggerOnlyAttribute : FactAttribute
	{
		public override string Skip
		{
			get
			{
				return Debugger.IsAttached
					? base.Skip
					: "Only running in the debugger.";
			}
			set
			{
				base.Skip = value;
			}
		}
	}
}