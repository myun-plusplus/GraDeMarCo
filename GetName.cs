using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GrainDetector
{
    public static class GetName
    {
        public static string Of<MemberType>(Expression<Func<MemberType>> expression)
        {
            return (expression.Body as MemberExpression).Member.Name;
        }
    }
}
