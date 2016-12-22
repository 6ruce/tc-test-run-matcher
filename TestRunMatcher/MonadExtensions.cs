using System;
using LanguageExt;

namespace TestRunMatcher
{
	public static class MonadExtensions
	{
		public static Either<L, R> LiftM2<L, R1, R2, R>(this Either<L, R1> self, Either<L, R2> other, Func<R1, R2, Either<L, R>>  bind) {
			return self.Match(
				Right: val1 => other.Match(
					Right: val2 => bind(val1, val2),
					Left: Prelude.Left<L, R>),
				Left: Prelude.Left<L, R>);
		}

		public static Lst<U> Apply<T, U>(this Lst<T> self, Func<Lst<T>, Lst<U>> applyer) {
			return applyer(self);
		}
	}
}
