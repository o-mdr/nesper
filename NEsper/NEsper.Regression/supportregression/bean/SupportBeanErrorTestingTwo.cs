///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace com.espertech.esper.supportregression.bean
{
    public class SupportBeanErrorTestingTwo
    {
        private readonly String value;
        
        public SupportBeanErrorTestingTwo()
        {
            value = "default";
        }

        public string Value
        {
            get { return value; }
            set { throw new ApplicationException("Setter manufactured test exception"); }
        }
    }
}
