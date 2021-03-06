///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client;
using com.espertech.esper.supportregression.execution;

using static com.espertech.esper.regression.events.xml.ExecEventXMLSchemaXPathBacked;

using NUnit.Framework;

namespace com.espertech.esper.regression.events.xml
{
    public class ExecEventXMLSchemaInvalid : RegressionExecution {
        public override void Configure(Configuration configuration) {
            configuration.AddEventType("TestXMLSchemaType", GetConfigTestType(null, false));
        }
    
        public override void Run(EPServiceProvider epService) {
            try {
                epService.EPAdministrator.CreateEPL("select element1 from TestXMLSchemaType#length(100)");
                Assert.Fail();
            } catch (EPStatementException ex) {
                Assert.AreEqual("Error starting statement: Failed to validate select-clause expression 'element1': Property named 'element1' is not valid in any stream [select element1 from TestXMLSchemaType#length(100)]", ex.Message);
            }
        }
    }
} // end of namespace
