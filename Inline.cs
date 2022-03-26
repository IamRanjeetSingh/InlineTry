using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlineTry
{
	public static class Inline
	{
		public static ChainableResult<TResult> Try<TResult>(Func<TResult> func)
		{
			return new ChainableResult<TResult>(func);
		}

		public static AsyncChainableResult<TResult> TryAsync<TResult>(Func<Task<TResult>> func)
		{
			return new AsyncChainableResult<TResult>(func);
		}

		public static ChainableAction Try(Action action)
		{
			return new ChainableAction(action);
		}
	}
}
