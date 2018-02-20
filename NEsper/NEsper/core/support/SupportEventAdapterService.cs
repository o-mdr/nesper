///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client.util;
using com.espertech.esper.compat;
using com.espertech.esper.compat.threading;
using com.espertech.esper.events.avro;
using com.espertech.esper.events;
using com.espertech.esper.util;

namespace com.espertech.esper.core.support
{
    public class SupportEventAdapterService
    {
        private static EventAdapterService _eventAdapterService;

        static SupportEventAdapterService()
        {
            _eventAdapterService = Allocate(
                new DefaultLockManager(timeout => new MonitorLock(timeout)),
                new ClassLoaderProviderDefault(
                    new ClassLoaderDefault(
                        new DefaultResourceManager(null, true)
                    )));
        }

        public static void Reset(
            ILockManager lockManager,
            ClassLoaderProvider classLoaderProvider)
        {
            _eventAdapterService = Allocate(lockManager, classLoaderProvider);
        }

        public static EventAdapterService Service
        {
            get { return _eventAdapterService; }
        }

        private static EventAdapterService Allocate(
            ILockManager lockManager,
            ClassLoaderProvider classLoaderProvider)
        {
            EventAdapterAvroHandler avroHandler = EventAdapterAvroHandlerUnsupported.INSTANCE;
            try
            {
                avroHandler = TypeHelper.Instantiate<EventAdapterAvroHandler>(
                    EventAdapterAvroHandlerConstants.HANDLER_IMPL, ClassForNameProviderDefault.INSTANCE);
            }
            catch
            {
            }

            return new EventAdapterServiceImpl(
                new EventTypeIdGeneratorImpl(), 5, avroHandler, 
                SupportEngineImportServiceFactory.Make(classLoaderProvider),
                lockManager);
        }
    }
} // end of namespace
