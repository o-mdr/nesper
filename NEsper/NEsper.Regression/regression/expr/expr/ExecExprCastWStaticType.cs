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
using com.espertech.esper.client.scopetest;
using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;
using com.espertech.esper.compat.logging;
using com.espertech.esper.supportregression.execution;


using NUnit.Framework;

namespace com.espertech.esper.regression.expr.expr
{
    public class ExecExprCastWStaticType : RegressionExecution {
    
        public override void Configure(Configuration configuration) {
            var map = new Dictionary<string, Object>();
            map.Put("anInt", typeof(string));
            map.Put("anDouble", typeof(string));
            map.Put("anLong", typeof(string));
            map.Put("anFloat", typeof(string));
            map.Put("anByte", typeof(string));
            map.Put("anShort", typeof(string));
            map.Put("intPrimitive", typeof(int));
            map.Put("intBoxed", typeof(int?));
            configuration.AddEventType("TestEvent", map);
        }
    
        public override void Run(EPServiceProvider epService) {
    
            string stmt = "select Cast(anInt, int) as intVal, " +
                    "Cast(anDouble, double) as doubleVal, " +
                    "Cast(anLong, long) as longVal, " +
                    "Cast(anFloat, float) as floatVal, " +
                    "Cast(anByte, byte) as byteVal, " +
                    "Cast(anShort, short) as shortVal, " +
                    "Cast(intPrimitive, int) as intOne, " +
                    "Cast(intBoxed, int) as intTwo, " +
                    "Cast(intPrimitive, java.lang.long) as longOne, " +
                    "Cast(intBoxed, long) as longTwo " +
                    "from TestEvent";
    
            EPStatement statement = epService.EPAdministrator.CreateEPL(stmt);
            var listener = new SupportUpdateListener();
            statement.Events += listener.Update;
    
            var map = new Dictionary<string, Object>();
            map.Put("anInt", "100");
            map.Put("anDouble", "1.4E-1");
            map.Put("anLong", "-10");
            map.Put("anFloat", "1.001");
            map.Put("anByte", "0x0A");
            map.Put("anShort", "223");
            map.Put("intPrimitive", 10);
            map.Put("intBoxed", 11);
    
            epService.EPRuntime.SendEvent(map, "TestEvent");
            EventBean row = listener.AssertOneGetNewAndReset();
            Assert.AreEqual(100, row.Get("intVal"));
            Assert.AreEqual(0.14d, row.Get("doubleVal"));
            Assert.AreEqual(-10L, row.Get("longVal"));
            Assert.AreEqual(1.001f, row.Get("floatVal"));
            Assert.AreEqual((byte) 10, row.Get("byteVal"));
            Assert.AreEqual((short) 223, row.Get("shortVal"));
            Assert.AreEqual(10, row.Get("intOne"));
            Assert.AreEqual(11, row.Get("intTwo"));
            Assert.AreEqual(10L, row.Get("longOne"));
            Assert.AreEqual(11L, row.Get("longTwo"));
        }
    }
} // end of namespace