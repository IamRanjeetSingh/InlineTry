using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTry
{
	public class Chainable
	{
		protected readonly List<Type> exceptionsToCatch;

		internal Chainable()
		{
			exceptionsToCatch = new List<Type>();
		}

		protected bool IsCatchedException(Type exceptionType)
		{
			return exceptionsToCatch.Any(exceptionToCatch => exceptionToCatch.IsAssignableFrom(exceptionType));
		}
	}
}
