///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.rettype;

namespace com.espertech.esper.epl.expression.dot.inner
{
    public class InnerEvaluatorScalarUnpackEvent : ExprDotEvalRootChildInnerEval
    {
        private readonly ExprEvaluator _rootEvaluator;
    
        public InnerEvaluatorScalarUnpackEvent(ExprEvaluator rootEvaluator) {
            _rootEvaluator = rootEvaluator;
        }
    
        public object Evaluate(EvaluateParams evaluateParams)
        {
            object target = _rootEvaluator.Evaluate(evaluateParams);
            if (target is EventBean) {
                return ((EventBean) target).Underlying;
            }
            return target;
        }
    
        public ICollection<EventBean> EvaluateGetROCollectionEvents(EvaluateParams evaluateParams) {
            return null;
        }

        public ICollection<object> EvaluateGetROCollectionScalar(EvaluateParams evaluateParams)
        {
            return null;
        }

        public EventType EventTypeCollection
        {
            get { return null; }
        }

        public Type ComponentTypeCollection
        {
            get { return null; }
        }

        public EventBean EvaluateGetEventBean(EvaluateParams evaluateParams) {
            return null;
        }

        public EventType EventTypeSingle
        {
            get { return null; }
        }

        public EPType TypeInfo
        {
            get { return EPTypeHelper.SingleValue(_rootEvaluator.ReturnType); }
        }
    }
}